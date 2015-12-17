using UnityEngine;
using System.Collections;

public class DontDestroyOnLoadDebug : MonoBehaviour
{
    public static DontDestroyOnLoadDebug Instance;

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
