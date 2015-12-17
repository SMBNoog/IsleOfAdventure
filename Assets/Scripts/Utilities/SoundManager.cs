using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    public static SoundManager Instance;

    void Awake()
    {
        Instance = this;
        //DontDestroyOnLoad(this);
    }

    void Start()
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
        //Debug.Log(SceneManager.GetActiveScene().name);
        //Debug.Log(GameInfo.Area.MainMenu.ToString());
        StopAll();
        if (SceneManager.GetActiveScene().name == GameInfo.Area.MainMenu.ToString())
            Play(TypeOfClip.TitleMusic);
        else if(SceneManager.GetActiveScene().name == GameInfo.Area.TutorialArea.ToString() ||
            SceneManager.GetActiveScene().name == GameInfo.Area.World.ToString())
        {
            Play(TypeOfClip.WorldMusic);
        }
        else if (SceneManager.GetActiveScene().name == GameInfo.Area.Forest.ToString())
        {
            Play(TypeOfClip.ForestMusic);
        }
        else if (SceneManager.GetActiveScene().name == GameInfo.Area.Castle.ToString())
        {
            Play(TypeOfClip.CastleMusic);
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

