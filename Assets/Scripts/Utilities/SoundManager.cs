using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TypeOfClip
{
    SwordMiss, SwordHit, ForestMusic, WorldMusic, CastleMusic, Fall, ChestOpen, PowerUp, BushHit, TitleMusic
}

[System.Serializable]
public class AudioSource_Files
{
    public TypeOfClip type;
    public AudioSource source;
}

public class SoundManager : MonoBehaviour
{

    public List<AudioSource_Files> audioSources;
    public AudioSource mainMusic;

    public static SoundManager Instance;

    void Awake()
    {
        Instance = this;
        //DontDestroyOnLoad(this);
    }

    void OnEnable()
    {
        UpdateBackgroundMusic();
    }

    public void Play(TypeOfClip type)
    {
        foreach (AudioSource_Files file in audioSources)
        {
            if (file.type == type)
            {
                file.source.Play();
            }
        }
    }

    public void StopAll()
    {
        foreach (AudioSource_Files file in audioSources)
        {
            file.source.Stop();
        }
    }

    public void UpdateBackgroundMusic()
    {
        StopAll();
        if (GameInfo.AreaToTeleportTo == GameInfo.Area.World || GameInfo.AreaToTeleportTo == GameInfo.Area.TutorialArea)
        {
            Play(TypeOfClip.WorldMusic);
        }
        else if (GameInfo.AreaToTeleportTo == GameInfo.Area.Forest)
        {
            Play(TypeOfClip.ForestMusic);
        }
        else if (GameInfo.AreaToTeleportTo == GameInfo.Area.Castle)
        {
            Play(TypeOfClip.CastleMusic);
        }
        else
            Play(TypeOfClip.TitleMusic);
    }

    //public void ChangeSoundFxVolume(Slider slider)
    //{
    //    foreach (AudioSource_Files file in audioSources)
    //    {
    //        file.source.volume = slider.value;
    //    }
    //    FindObjectOfType<Character>().GetComponent<AudioSource>().volume = slider.value;
    //}

    //public void ChangeMusicVolume(Slider slider)
    //{
    //    mainMusic.volume = slider.value;
    //}
}

