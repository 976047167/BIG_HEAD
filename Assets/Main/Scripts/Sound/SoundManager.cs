using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Setting;
using AppSettings;
using BigHead.Sound;

public class SoundManager
{
    const string MUSIC_MUTE_KEY = "MUSIC_MUTE";
    const string SOUND_MUTE_KEY = "SOUND_MUTE";
    const string MUSIC_VOLUME_KEY = "MUSIC_VOLUME";
    const string SOUND_VOLUME_KEY = "SOUND_VOLUME";
    const string MUSIC_GROUP_KEY = "MUSIC";
    const string SOUND_GROUP_KEY = "SOUND";
    GameObject goHelper = null;


    Dictionary<string, SoundGroup> dicSoundGroups = new Dictionary<string, SoundGroup>();

    public SoundManager()
    {
        goHelper = new GameObject();
        goHelper.AddComponent<SoundManagerHelper>();

        if (!dicSoundGroups.ContainsKey(MUSIC_GROUP_KEY))
        {
            SoundGroup musicGroup = new SoundGroup(MUSIC_GROUP_KEY, null);
            dicSoundGroups[MUSIC_GROUP_KEY] = musicGroup;

            musicGroup.AddSoundAgentHelper(goHelper.transform);
        }
        if (!dicSoundGroups.ContainsKey(SOUND_GROUP_KEY))
        {
            SoundGroup soundGroup = new SoundGroup(SOUND_GROUP_KEY, null);
            dicSoundGroups[SOUND_GROUP_KEY] = soundGroup;
            soundGroup.AddSoundAgentHelper(goHelper.transform);
            soundGroup.AddSoundAgentHelper(goHelper.transform);
            soundGroup.AddSoundAgentHelper(goHelper.transform);
        }
    }

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
    public void StopAll()
    {
        foreach (var group in dicSoundGroups)
        {
            group.Value.StopAllLoadedSounds();
            group.Value.ReleaseAllSoundAssets();
        }
    }
    public void StopAll(int exceptId)
    {
        foreach (var group in dicSoundGroups)
        {
            group.Value.StopAllLoadedSounds(exceptId);
            group.Value.ReleaseAllSoundAssets(exceptId);
        }
    }

    public bool MusicMute
    {
        get { return Game.Setting.GetBool(MUSIC_MUTE_KEY); }
        set
        {
            Game.Setting.SetBool(MUSIC_MUTE_KEY, value);
            Game.Setting.Save();
        }
    }
    public bool SoundMute
    {
        get { return Game.Setting.GetBool(SOUND_MUTE_KEY); }
        set
        {
            Game.Setting.SetBool(SOUND_MUTE_KEY, value);
            Game.Setting.Save();
        }
    }
    public float MusicVolume
    {
        get { return Game.Setting.GetFloat(MUSIC_VOLUME_KEY); }
        set
        {
            Game.Setting.SetFloat(MUSIC_VOLUME_KEY, value);

        }
    }
    public void SaveMusicVolume(float value)
    {
        MusicVolume = value;
        Game.Setting.Save();
    }
    public float SoundVolume
    {
        get { return Game.Setting.GetFloat(SOUND_VOLUME_KEY); }
        set
        {
            Game.Setting.SetFloat(SOUND_VOLUME_KEY, value);

        }
    }
    public void SaveSoundVolume(float value)
    {
        SoundVolume = value;
        Game.Setting.Save();
    }


}

