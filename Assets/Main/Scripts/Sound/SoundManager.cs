using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Setting;
using AppSettings;
using BigHead.Sound;
using UnityEngine.Audio;
/// <summary>
/// 背景音乐一个频道，覆盖式播放；
/// 特效声音三个频道，优先级同时播放；
/// 语音一个频道，队列播放；
/// </summary>
public class SoundManager
{
    const string MUSIC_MUTE_KEY = "MUSIC_MUTE";
    const string ALL_MUTE_KEY = "ALL_MUTE";
    const string MUSIC_VOLUME_KEY = "MUSIC_VOLUME";
    const string ALL_VOLUME_KEY = "ALL_VOLUME";
    const string MUSIC_GROUP_KEY = "MUSIC";
    const string SOUND_GROUP_KEY = "SOUND";
    const string VOICE_GROUP_KEY = "VOICE";
    GameObject goHelper = null;

    AudioMixer audioMixer = null;
    AudioMixerGroup allMixGroup = null;

    Dictionary<string, SoundGroup> dicSoundGroups = new Dictionary<string, SoundGroup>();

    Queue<int> queueVoice = new Queue<int>();
    int currentVoice = 0;
    public static SoundManager Instance { get; private set; }
    public SoundManager()
    {
        Instance = this;
        goHelper = new GameObject();
        goHelper.AddComponent<SoundManagerHelper>();

        if (!dicSoundGroups.ContainsKey(MUSIC_GROUP_KEY))
        {
            SoundGroup musicGroup = new SoundGroup(MUSIC_GROUP_KEY, audioMixer == null ? null : audioMixer.FindMatchingGroups("BGM")[0]);
            dicSoundGroups[MUSIC_GROUP_KEY] = musicGroup;
            musicGroup.Volume = MusicVolume;
            musicGroup.Mute = MusicMute;
            musicGroup.AddSoundAgentHelper(goHelper.transform);
        }
        if (!dicSoundGroups.ContainsKey(SOUND_GROUP_KEY))
        {
            SoundGroup soundGroup = new SoundGroup(SOUND_GROUP_KEY, audioMixer == null ? null : audioMixer.FindMatchingGroups("Sound")[0]);
            dicSoundGroups[SOUND_GROUP_KEY] = soundGroup;
            soundGroup.AddSoundAgentHelper(goHelper.transform);
            soundGroup.AddSoundAgentHelper(goHelper.transform);
            soundGroup.AddSoundAgentHelper(goHelper.transform);

        }
        if (!dicSoundGroups.ContainsKey(VOICE_GROUP_KEY))
        {
            SoundGroup voiceGroup = new SoundGroup(VOICE_GROUP_KEY, audioMixer == null ? null : audioMixer.FindMatchingGroups("Voice")[0]);
            dicSoundGroups[VOICE_GROUP_KEY] = voiceGroup;
            voiceGroup.AddSoundAgentHelper(goHelper.transform);

        }
        Messenger.AddListener<int>(MessageId.SOUND_STOPED, OnSoundStoped);
    }
    //public IEnumerator Init()
    //{
    //    ResourceManager.LoadAudioMixer("Sound/Mixer/Main");
    //        allMixGroup
    //}
    public void Play(int soundId)
    {
        if (soundId == 0)
        {
            return;
        }
        SoundTableSetting soundTable = SoundTableSettings.Get(soundId);
        if (soundTable == null)
        {
            Debug.LogError("soundtable doesn't exist id = " + soundId);
            return;
        }
        SoundGroup soundGroup = dicSoundGroups[soundTable.Group];
        if (soundGroup == null)
        {
            Debug.LogError("soundGroup [" + soundTable.Group + "] doesn't exist!id = " + soundId);
            return;
        }
        if (soundGroup.Name == VOICE_GROUP_KEY)
        {

            if (currentVoice == 0)
            {
                currentVoice = soundId;
                LoadSoundAssetAndPlay(soundId);
            }
            else
            {
                queueVoice.Clear();
                queueVoice.Enqueue(soundId);
            }
            return;
        }

        LoadSoundAssetAndPlay(soundId);
    }
    void LoadSoundAssetAndPlay(int soundId)
    {
        if (soundId == 0)
        {
            return;
        }
        SoundTableSetting soundTable = SoundTableSettings.Get(soundId);
        string path = string.Format("Sound/{0}{1}", soundTable.Path, (ResourceManager.EditorMode ? "." + soundTable.Extension : ResourceManager.BUNDLE_SUFFIX));
        ResourceManager.LoadSound(path, LoadSoundAssetSuccess, LoadSoundAssetFaild, soundId, soundTable);
    }
    void LoadSoundAssetSuccess(string path, object[] userdata, AudioClip audioAsset, OnAssetDestory onDestory)
    {
        int soundId = (int)userdata[0];
        SoundTableSetting soundTable = (SoundTableSetting)userdata[1];
        SoundGroup soundGroup = dicSoundGroups[soundTable.Group];
        PlaySoundParams soundParams = new PlaySoundParams();
        soundParams.Time = soundTable.Time;
        soundParams.MuteInSoundGroup = Constant.DefaultMute;
        soundParams.Loop = soundTable.Loop;
        soundParams.Priority = soundTable.Priority;
        soundParams.VolumeInSoundGroup = Constant.DefaultVolume;
        soundParams.FadeInSeconds = soundTable.FadeInSeconds;
        soundParams.FadeOutSeconds = soundTable.FadeOutSeconds;
        soundParams.Pitch = soundTable.Pitch;
        soundParams.PanStereo = soundTable.PanStereo;
        soundParams.SpatialBlend = soundTable.SpatialBlend;
        soundParams.MaxDistance = soundTable.MaxDistance;
        PlaySoundErrorCode? errCode;
        soundGroup.PlaySound(soundId, audioAsset, soundParams, onDestory, out errCode);
        if (errCode != null)
        {
            Debug.LogError(soundId + "  PlaySoundErrorCode=>" + errCode.ToString());
        }
    }

    void LoadSoundAssetFaild(string path, object[] userdata)
    {
        Debug.LogError("path doesn't exist!\n" + path + "\nID:" + userdata[0]);
    }


    void OnSoundStoped(int soundId)
    {
        if (soundId == currentVoice)
        {
            if (queueVoice.Count > 0)
            {
                currentVoice = queueVoice.Dequeue();
                LoadSoundAssetAndPlay(currentVoice);
            }
            else
                currentVoice = 0;
        }
    }
    public void StopAll()
    {
        foreach (var group in dicSoundGroups)
        {
            group.Value.StopAllLoadedSounds();
            group.Value.ReleaseAllSoundAssets();
        }
        queueVoice.Clear();
    }
    public void StopAll(int exceptId)
    {
        foreach (var group in dicSoundGroups)
        {
            group.Value.StopAllLoadedSounds(exceptId);
            group.Value.ReleaseAllSoundAssets(exceptId);
        }
        queueVoice.Clear();
    }
    public void StopAllVoice()
    {
        queueVoice.Clear();
    }
    public bool MusicMute
    {
        get { return Game.Setting.GetBool(MUSIC_MUTE_KEY); }
        set
        {
            Game.Setting.SetBool(MUSIC_MUTE_KEY, value);
            Game.Setting.Save();
            if (dicSoundGroups.ContainsKey(MUSIC_GROUP_KEY))
            {
                dicSoundGroups[MUSIC_GROUP_KEY].Mute = value;
            }
        }
    }

    public bool ALLMute
    {
        get { return Game.Setting.GetBool(ALL_MUTE_KEY); }
        set
        {
            Game.Setting.SetBool(ALL_MUTE_KEY, value);
            Game.Setting.Save();
            foreach (var group in dicSoundGroups)
            {
                group.Value.RefreshMute();
            }
        }
    }
    /// <summary>
    /// 背景音量
    /// </summary>
    public float MusicVolume
    {
        get { return Game.Setting.GetFloat(MUSIC_VOLUME_KEY); }
        set
        {
            Game.Setting.SetFloat(MUSIC_VOLUME_KEY, value);
            if (dicSoundGroups.ContainsKey(MUSIC_GROUP_KEY))
            {
                dicSoundGroups[MUSIC_GROUP_KEY].Volume = value;
            }
        }
    }
    public void SaveMusicVolume(float value)
    {
        MusicVolume = value;
        Game.Setting.Save();
    }
    /// <summary>
    /// 总音量
    /// </summary>
    public float AllVolume
    {
        get { return Game.Setting.GetFloat(ALL_VOLUME_KEY); }
        set
        {
            Game.Setting.SetFloat(ALL_VOLUME_KEY, value);
            foreach (var group in dicSoundGroups)
            {
                group.Value.RefreshVolume();
            }
        }
    }
    public void SaveAllVolume(float value)
    {
        AllVolume = value;
        Game.Setting.Save();
    }

}

