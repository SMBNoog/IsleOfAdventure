using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    void Start()
    {
        Debug.Log(GameInfo.TutorialNotCompleted);
    }

	public void StartGame()
    {
        if (GameInfo.TutorialNotCompleted)
            GameInfo.AreaToTeleportTo = GameInfo.Area.TutorialArea;
        else
            GameInfo.AreaToTeleportTo = GameInfo.Area.World;

        Application.LoadLevel("SceneLoader");
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        GameInfo.TutorialNotCompleted = true;
    }

    public void SetPlayerName(Text name)
    {
        GameInfo.PlayerName = name.text;
    }
}
