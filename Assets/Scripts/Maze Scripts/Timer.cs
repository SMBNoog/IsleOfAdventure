using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour
{
    public float timeLimit;
    public Slider slider;

    void Update()
    {
        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
            slider.value = timeLimit;
        }
        else
        {
            //save new stats then load the world
            Application.LoadLevel(0);
        }
    }
}