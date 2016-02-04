using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour
{
    public Text text;
    public float timeLimit;

    void Update()
    {
        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
            Debug2Screen.Log("Time Remaining: " + (int)timeLimit);
        }
        else
        {
            Debug2Screen.Log("Game Over. Out Of Time.");
            Application.LoadLevel(0);
        }
    }
}