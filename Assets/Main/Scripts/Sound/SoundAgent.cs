using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Sound;
using System;
using DG.Tweening;
/// <summary>
/// 声音代理。
/// </summary>
public class SoundAgent : MonoBehaviour, ISoundAgent
{
    private SoundGroup m_SoundGroup;
    private int m_SerialId;
    private object m_SoundAsset;
    private DateTime m_SetSoundAssetTime;
    private bool m_MuteInSoundGroup;
    private float m_VolumeInSoundGroup;

    private Transform m_CachedTransform = null;
    private AudioSource m_AudioSource = null;
    private float m_Volume = 1f;
    OnAssetDestory onAssetDestory = null;


    /// <summary>
    /// 初始化声音代理的新实例。
    /// </summary>
    /// <param name="soundGroup">所在的声音组。</param>
    public void Init(SoundGroup soundGroup)
    {
        if (soundGroup == null)
        {
            throw new Exception("Sound group is invalid.");
        }

        m_AudioSource = gameObject.GetOrAddComponent<AudioSource>();
        m_AudioSource.playOnAwake = false;
        m_AudioSource.rolloffMode = AudioRolloffMode.Custom;
        m_CachedTransform = transform;

        m_SoundGroup = soundGroup;
        m_SerialId = 0;
        m_SoundAsset = null;
        Reset();
    }

    /// <summary>
    /// 获取所在的声音组。
    /// </summary>
    public ISoundGroup SoundGroup
    {
        get
        {
            return m_SoundGroup;
        }
    }

    /// <summary>
    /// 获取或设置声音的序列编号。
    /// </summary>
    public int SerialId
    {
        get
        {
            return m_SerialId;
        }
        set
        {
            m_SerialId = value;
        }
    }

    /// <summary>
    /// 获取当前是否正在播放。
    /// </summary>
    public bool IsPlaying
    {
        get
        {
            return m_AudioSource.isPlaying;
        }
    }

    /// <summary>
    /// 获取或设置播放位置。
    /// </summary>
    public float Time
    {
        get
        {
            return m_AudioSource.time;
        }
        set
        {
            m_AudioSource.time = value;
        }
    }

    /// <summary>
    /// 获取是否静音。
    /// </summary>
    public bool Mute
    {
        get
        {
            return m_AudioSource.mute;
        }
    }

    /// <summary>
    /// 获取或设置在声音组内是否静音。
    /// </summary>
    public bool MuteInSoundGroup
    {
        get
        {
            return m_MuteInSoundGroup;
        }
        set
        {
            m_MuteInSoundGroup = value;
            RefreshMute();
        }
    }

    /// <summary>
    /// 获取或设置是否循环播放。
    /// </summary>
    public bool Loop
    {
        get
        {
            return m_AudioSource.loop;
        }
        set
        {
            m_AudioSource.loop = value;
        }
    }

    /// <summary>
    /// 获取或设置声音优先级。
    /// </summary>
    public int Priority
    {
        get
        {
            return m_AudioSource.priority;
        }
        set
        {
            m_AudioSource.priority = value;
        }
    }

    /// <summary>
    /// 获取或设置音量大小。
    /// </summary>
    public float Volume
    {
        get
        {
            return m_Volume;
        }
        set
        {
            m_Volume = value;
            m_AudioSource.volume = m_Volume;
        }
    }

    /// <summary>
    /// 获取或设置在声音组内音量大小。
    /// </summary>
    public float VolumeInSoundGroup
    {
        get
        {
            return m_VolumeInSoundGroup;
        }
        set
        {
            m_VolumeInSoundGroup = value;
            RefreshVolume();
        }
    }

    /// <summary>
    /// 获取或设置声音音调。
    /// </summary>
    public float Pitch
    {
        get
        {
            return m_AudioSource.pitch;
        }
        set
        {
            m_AudioSource.pitch = value;
        }
    }

    /// <summary>
    /// 获取或设置声音立体声声相。
    /// </summary>
    public float PanStereo
    {
        get
        {
            return m_AudioSource.panStereo;
        }
        set
        {
            m_AudioSource.panStereo = value;
        }
    }

    /// <summary>
    /// 获取或设置声音空间混合量。0.0是2D，1是3D
    /// </summary>
    public float SpatialBlend
    {
        get
        {
            return m_AudioSource.spatialBlend;
        }
        set
        {
            m_AudioSource.spatialBlend = value;
        }
    }

    /// <summary>
    /// 获取或设置声音最大距离。
    /// </summary>
    public float MaxDistance
    {
        get
        {
            return m_AudioSource.maxDistance;
        }
        set
        {
            m_AudioSource.maxDistance = value;
        }
    }


    /// <summary>
    /// 获取声音创建时间。
    /// </summary>
    internal DateTime SetSoundAssetTime
    {
        get
        {
            return m_SetSoundAssetTime;
        }
    }

    /// <summary>
    /// 播放声音。
    /// </summary>
    public void Play()
    {
        m_AudioSource.Play();
    }

    Tweener fadeTweener = null;
    /// <summary>
    /// 播放声音。
    /// </summary>
    /// <param name="fadeInSeconds">声音淡入时间，以秒为单位。</param>
    public void Play(float fadeInSeconds)
    {
        m_AudioSource.Play();
        if (fadeInSeconds > 0f)
        {
            if (fadeTweener != null)
            {
                fadeTweener.Complete();
            }
            fadeTweener = DOTween.To(() => { return 0f; }, (f) => { m_AudioSource.volume = f; }, m_Volume, fadeInSeconds)
                .Play();

        }

    }

    /// <summary>
    /// 停止播放声音。
    /// </summary>
    public void Stop()
    {
        m_AudioSource.Stop();
    }

    /// <summary>
    /// 停止播放声音。
    /// </summary>
    /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
    public void Stop(float fadeOutSeconds)
    {
        if (fadeOutSeconds > 0f)
        {
            if (fadeTweener != null)
            {
                fadeTweener.Complete();
            }
            fadeTweener = DOTween.To(() => { return m_AudioSource.volume; }, (f) => { m_AudioSource.volume = f; }, 0f, fadeOutSeconds)
                .OnComplete(() => { m_AudioSource.Stop(); })
                .Play();
        }
        else
        {
            m_AudioSource.Stop();
        }
    }

    /// <summary>
    /// 暂停播放声音。
    /// </summary>
    public void Pause()
    {
        m_AudioSource.Pause();
    }

    /// <summary>
    /// 暂停播放声音。
    /// </summary>
    /// <param name="fadeOutSeconds">声音淡出时间，以秒为单位。</param>
    public void Pause(float fadeOutSeconds)
    {
        if (fadeOutSeconds > 0f)
        {
            if (fadeTweener != null)
            {
                fadeTweener.Complete();
            }
            fadeTweener = DOTween.To(() => { return m_AudioSource.volume; }, (f) => { m_AudioSource.volume = f; }, 0f, fadeOutSeconds)
                .OnComplete(() => { m_AudioSource.Pause(); })
                .Play();
        }
        else
        {
            m_AudioSource.Stop();
        }
    }

    /// <summary>
    /// 恢复播放声音。
    /// </summary>
    public void Resume()
    {
        m_AudioSource.UnPause();
    }

    /// <summary>
    /// 恢复播放声音。
    /// </summary>
    /// <param name="fadeInSeconds">声音淡入时间，以秒为单位。</param>
    public void Resume(float fadeInSeconds)
    {
        if (fadeInSeconds > 0f)
        {
            if (fadeTweener != null)
            {
                fadeTweener.Complete();
            }
            fadeTweener = DOTween.To(() => { return 0f; }, (f) => { m_AudioSource.volume = f; }, m_Volume, fadeInSeconds)
                .OnComplete(() => { m_AudioSource.UnPause(); })
                .Play();
        }
        else
        {
            m_AudioSource.Stop();
        }
    }

    /// <summary>
    /// 重置声音代理。
    /// </summary>
    public void Reset()
    {
        ReleaseSoundAsset();

        m_SetSoundAssetTime = DateTime.MinValue;
        Time = Constant.DefaultTime;
        MuteInSoundGroup = Constant.DefaultMute;
        Loop = Constant.DefaultLoop;
        Priority = Constant.DefaultPriority;
        VolumeInSoundGroup = Constant.DefaultVolume;
        Pitch = Constant.DefaultPitch;
        PanStereo = Constant.DefaultPanStereo;
        SpatialBlend = Constant.DefaultSpatialBlend;
        MaxDistance = Constant.DefaultMaxDistance;
        m_CachedTransform.localPosition = Vector3.zero;
        m_AudioSource.clip = null;
        m_Volume = Constant.DefaultVolume;
    }

    public bool SetSoundAsset(object soundAsset, OnAssetDestory onAssetDestory)
    {
        Reset();
        m_SoundAsset = soundAsset;
        m_SetSoundAssetTime = DateTime.Now;
        AudioClip audioClip = soundAsset as AudioClip;
        if (audioClip == null)
        {
            return false;
        }
        this.onAssetDestory = onAssetDestory;
        m_AudioSource.clip = audioClip;
        return true;
    }

    internal void RefreshMute()
    {
        m_AudioSource.mute = m_SoundGroup.Mute || m_MuteInSoundGroup;
    }

    internal void RefreshVolume()
    {
        Volume = m_SoundGroup.Volume * m_VolumeInSoundGroup;
    }


    /// <summary>
    /// 释放声音资源。
    /// </summary>
    /// <param name="soundAsset">要释放的声音资源。</param>
    public void ReleaseSoundAsset()
    {
        if (onAssetDestory != null)
        {
            onAssetDestory();
        }
    }

}
