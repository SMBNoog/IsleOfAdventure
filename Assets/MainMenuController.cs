﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public Text userName;
    public Image inputField;

    public GameObject inputCanvas;

    void Start()
    {
        inputCanvas.SetActive(false);

        if (GameInfo.PlayerName == null)
        {
            userName.text = GameInfo.PlayerName;
            userName.gameObject.SetActive(true);
            inputField.gameObject.SetActive(false);
        }
    }

	public void StartGame()
    {
        if (GameInfo.StartTutorial)
            GameInfo.AreaToTeleportTo = GameInfo.Area.TutorialArea;
        else
            GameInfo.AreaToTeleportTo = GameInfo.Area.World;

        inputCanvas.SetActive(true);

        Application.LoadLevel("SceneLoader");
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        GameInfo.StartTutorial = true;
    }

    public void SetPlayerName(Text name)
    {
        GameInfo.PlayerName = name.text;
        userName.text = name.text;
        userName.gameObject.SetActive(true);
        inputField.gameObject.SetActive(false);
    }
}
