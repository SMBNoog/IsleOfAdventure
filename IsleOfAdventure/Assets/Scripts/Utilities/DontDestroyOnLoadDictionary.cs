using UnityEngine;
using System.Collections;

public class DontDestroyOnLoadDictionary : MonoBehaviour {
    
    public static DontDestroyOnLoadDictionary Instance;

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
