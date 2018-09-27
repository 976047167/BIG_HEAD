using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Setting;
using AppSettings;

public class SoundManager
{
    const string MUSIC_MUTE_KEY = "MUSIC_MUTE";
    const string SOUND_MUTE_KEY = "SOUND_MUTE";
    const string MUSIC_VOLUME_KEY = "MUSIC_VOLUME";
    const string SOUND_VOLUME_KEY = "SOUND_VOLUME";
    GameObject goHelper = null;

    public SoundManager()
    {
        goHelper = new GameObject();
        goHelper.AddComponent<SoundManagerHelper>();
    }

    public void PlayMusic()
    {

    }

    public void PlaySound()
    {

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

