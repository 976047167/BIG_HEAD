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
    }

    public void PlayMusic()
    {
        SoundGroup musicGroup = dicSoundGroups[MUSIC_GROUP_KEY];
        if (musicGroup == null)
        {
            musicGroup = new SoundGroup(MUSIC_GROUP_KEY, null);
            dicSoundGroups[MUSIC_GROUP_KEY] = musicGroup;
        }
        //SoundAgent soundAgent = musicGroup.AddSoundAgentHelper();
    }

    public void PlaySound()
    {
        SoundGroup soundGroup = dicSoundGroups[SOUND_GROUP_KEY];
        if (soundGroup == null)
        {
            soundGroup = new SoundGroup(SOUND_GROUP_KEY, null);
            dicSoundGroups[SOUND_GROUP_KEY] = soundGroup;
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

