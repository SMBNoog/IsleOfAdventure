using UnityEngine;
using System.Collections;
using CnControls;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MobileBackButton : MonoBehaviour {

    public Image backButton;
    

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {            
            backButton.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
	}

    public void NoButton()
    {
        backButton.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void YesButton()
    {
        if(SceneManager.GetActiveScene().name != "MainMenu")
        {
            IAttributesManager att = Interface.Find<IAttributesManager>(FindObjectOfType<Player>().gameObject);

            if (att != null)
            {
                att.SaveAttributes(false);
            }
        }

        Application.Quit();
    }
}
