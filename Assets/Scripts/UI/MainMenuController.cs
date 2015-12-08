using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public Text userName;
    public Image inputField;

    public GameObject inputCanvas;

    void Start()
    {
        inputCanvas.SetActive(false);
        //PlayerPrefs.DeleteAll();
        Debug.Log(GameInfo.TutorialCompleted);
        if (GameInfo.TutorialCompleted)
        {
            userName.text = GameInfo.PlayerName;
            userName.gameObject.SetActive(true);
        }
        //if (GameInfo.PlayerName != null)
        //{
        //    userName.text = GameInfo.PlayerName;
        //    userName.gameObject.SetActive(true);
        //    inputField.gameObject.SetActive(false);
        //}
    }

	public void StartGame()
    {        
        
        if (!GameInfo.TutorialCompleted)
        {
            GameInfo.AreaToTeleportTo = GameInfo.Area.TutorialArea;
            inputField.gameObject.SetActive(true);

            Debug.Log(userName.text);
            StartCoroutine(NameCheckThenStart());         
        }
        else
        {            
            GameInfo.AreaToTeleportTo = GameInfo.Area.World;
            inputCanvas.SetActive(true);
            Application.LoadLevel("SceneLoader");
        }
    }

    IEnumerator NameCheckThenStart()
    {
        if (userName.text != "noname")
        {
            inputCanvas.SetActive(true);
            Application.LoadLevel("SceneLoader");
        }
        else
        {
            yield return new WaitForSeconds(1f);
            StartCoroutine(NameCheckThenStart());

        }

        yield return null;
            
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        GameInfo.TutorialCompleted = true;
    }

    public void SetPlayerName(Text name)
    {
        GameInfo.PlayerName = name.text;
        userName.text = name.text;
        userName.gameObject.SetActive(true);
        inputField.gameObject.SetActive(false);
    }
}
