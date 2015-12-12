using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public Text userName;
    public Image inputField;

    public Button playButton;
    public Image playerImage;

    public Button exitButton;

    public GameObject inputCanvas;

    void Start()
    {
        inputCanvas.SetActive(false);
        PlayerPrefs.DeleteAll();   ////////////////////// REMOVE LATER
        //Debug.Log(GameInfo.TutorialCompleted);

        if (userName.text == "noname")
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

	public void PlayButton()
    {        
        
        if (!GameInfo.TutorialCompleted)
        {
            GameInfo.AreaToTeleportTo = GameInfo.Area.TutorialArea;
            playButton.gameObject.SetActive(false);
            playerImage.gameObject.SetActive(true);
            StartCoroutine(NameCheckThenStart());         
        }
        else
        {            
            GameInfo.AreaToTeleportTo = GameInfo.Area.World;
            inputCanvas.SetActive(true);
            SceneManager.LoadScene("SceneLoader");
        }
    }

    IEnumerator NameCheckThenStart()
    {
        if (userName.text != "")
        {
            inputCanvas.SetActive(true);
            SceneManager.LoadScene("SceneLoader");
        }
        else
        {
            inputField.gameObject.SetActive(true);
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

    public void ExitButton()
    {
        Application.Quit();
    }
}
