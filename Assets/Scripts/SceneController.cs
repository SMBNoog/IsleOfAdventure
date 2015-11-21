using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

    public static SceneController Instance;

    public GameObject player;

    void Awake()
    {
        Instance = this;
    }
}
