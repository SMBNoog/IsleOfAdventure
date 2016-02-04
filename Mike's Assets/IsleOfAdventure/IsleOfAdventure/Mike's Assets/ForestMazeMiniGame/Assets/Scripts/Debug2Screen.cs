using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Debug2Screen : MonoBehaviour
{
    private static Debug2Screen instance;

    void Start()
    {
        instance = this;
    }

    public static void Log (string message)
    {
        instance.GetComponent<Text>().text = message;
    }
}