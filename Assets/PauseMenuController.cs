﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour {

    public Image pauseMenu;

    public void ShowHideMainMenuButton()
    {
        if (pauseMenu.isActiveAndEnabled)
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
    
        
    }

    public void Continue()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
