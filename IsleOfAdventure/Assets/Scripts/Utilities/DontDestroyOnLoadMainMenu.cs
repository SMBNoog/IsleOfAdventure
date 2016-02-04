using UnityEngine;
using System.Collections;

public class DontDestroyOnLoadMainMenu : MonoBehaviour {

    public static DontDestroyOnLoadMainMenu Instance;

    void Awake()
    {
        if (Instance)
            DestroyImmediate(gameObject);
        else
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
    }
}
