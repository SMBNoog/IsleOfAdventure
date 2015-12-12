using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TypeOfClip
{
    SwordMiss, SwordHit, ForestMusic, BackgroundMusic
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
        DontDestroyOnLoad(this);
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

