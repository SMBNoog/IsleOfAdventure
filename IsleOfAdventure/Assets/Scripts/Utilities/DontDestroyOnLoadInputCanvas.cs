using UnityEngine;
using System.Collections;

public class DontDestroyOnLoadInputCanvas : MonoBehaviour {
    
    public static DontDestroyOnLoadInputCanvas Instance;

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
