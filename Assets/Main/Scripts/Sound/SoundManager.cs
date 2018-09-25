using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigHead.Setting;

public class SoundManager
{
    const string MUSIC_MUTE_KEY = "MUSIC_MUTE";
    const string SOUND_MUTE_KEY = "SOUND_MUTE";
    const string MUSIC_VOLUME_KEY = "MUSIC_VOLUME";
    const string SOUND_VOLUME_KEY = "SOUND_VOLUME";
    public SoundManager()
    {

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
        set { }
    }
    public bool SoundMute
    {
        get { return Game.Setting.GetBool(SOUND_MUTE_KEY); }
        set { }
    }
    public float MusicVolume
    {
        get { return Game.Setting.GetFloat(MUSIC_VOLUME_KEY); }
        set { }
    }
    public float SoundVolume
    {
        get { return Game.Setting.GetFloat(SOUND_VOLUME_KEY); }
        set { }
    }


}
