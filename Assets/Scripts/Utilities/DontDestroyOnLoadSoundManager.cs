using UnityEngine;
using System.Collections;

public class DontDestroyOnLoadSoundManager : MonoBehaviour {
    
    public static DontDestroyOnLoadSoundManager Instance;

    void Awake()
    {
        if(Instance)
            DestroyImmediate(gameObject);
        else
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
    }
}
