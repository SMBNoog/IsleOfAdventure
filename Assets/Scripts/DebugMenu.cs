using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebugMenu : MonoBehaviour {

    public Image debugPanel;
    private GameObject player;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != GameInfo.Area.MainMenu.ToString()) // || SceneManager.GetActiveScene().name != "SceneLoader")
            player = FindObjectOfType<Player>().gameObject;
    }

    public void ShowHidePanel()
    {
        if(debugPanel.isActiveAndEnabled)
            debugPanel.gameObject.SetActive(false);
        else
            debugPanel.gameObject.SetActive(true);
    }

    public void ChangeToFlame()
    {
        if (player != null)
        {
            IDebugChangeWeapon ichangeWeapon = Interface.Find<IDebugChangeWeapon>(player);
            if(ichangeWeapon != null)
            {
                ichangeWeapon.DebugChangeToFlameButton();
            }
        }
        else
            Debug.LogError("Player not found.");       
    }

    public void ChangeToSilver()
    {
        if (player != null)
        {
            IDebugChangeWeapon ichangeWeapon = Interface.Find<IDebugChangeWeapon>(player);
            if (ichangeWeapon != null)
            {
                ichangeWeapon.DebugChangeToSilverButton();
            }
        }
        else
            Debug.LogError("Player not found.");
    }

    public void ChangeToGold()
    {
        if (player != null)
        {
            IDebugChangeWeapon ichangeWeapon = Interface.Find<IDebugChangeWeapon>(player);
            if (ichangeWeapon != null)
            {
                ichangeWeapon.DebugChangeToGoldButton();
            }
        }
        else
            Debug.LogError("Player not found.");
    }

    public void ChangeToEpic()
    {
        if (player != null)
        {
            IDebugChangeWeapon ichangeWeapon = Interface.Find<IDebugChangeWeapon>(player);
            if (ichangeWeapon != null)
            {
                ichangeWeapon.DebugChangeToEpicButton();
            }
        }
        else
            Debug.LogError("Player not found.");
    }
}
