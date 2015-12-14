using UnityEngine;
using System.Collections;

public class DontDestroyOnLoadEscapeCanvas : MonoBehaviour {

    public static DontDestroyOnLoadEscapeCanvas Instance;

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
