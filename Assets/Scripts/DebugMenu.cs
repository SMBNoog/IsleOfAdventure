using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebugMenu : MonoBehaviour {

    public Image debugPanel;

    public void ShowHidePanel()
    {
        if(debugPanel.isActiveAndEnabled)
            debugPanel.gameObject.SetActive(false);
        else
            debugPanel.gameObject.SetActive(true);
    }

    public void ChangeToFlame()
    {
        GameObject player = FindObjectOfType<Player>().gameObject;
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
        GameObject player = FindObjectOfType<Player>().gameObject;
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
        GameObject player = FindObjectOfType<Player>().gameObject;
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
        GameObject player = FindObjectOfType<Player>().gameObject;
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

    public void TeleportToForest()
    {
        GameObject player = FindObjectOfType<Player>().gameObject;
        if (player != null)
        {
            IAttributesManager att = Interface.Find<IAttributesManager>(player);
            if(att != null)
            {
                att.SaveAttributes(false);
                Time.timeScale = 1.0f;
                GameInfo.LastPos = new Vector2(-2.7f, -17.7f);
                GameInfo.AreaToTeleportTo = GameInfo.Area.Forest;
                SceneManager.LoadScene(GameInfo.sceneLoader);
            }
        }
    }

    public void TeleportToCastle()
    {
        GameObject player = FindObjectOfType<Player>().gameObject;
        if (player != null)
        {
            IAttributesManager att = Interface.Find<IAttributesManager>(player);
            if (att != null)
            {
                att.SaveAttributes(false);
                Time.timeScale = 1.0f;
                GameInfo.LastPos = new Vector2(-2.7f, -17.7f);
                GameInfo.AreaToTeleportTo = GameInfo.Area.Castle;
                SceneManager.LoadScene(GameInfo.sceneLoader);
            }
        }
    }
}
