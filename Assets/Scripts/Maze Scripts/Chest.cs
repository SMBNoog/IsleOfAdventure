using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour, INPCMessageAndAction
{
    public GameObject openchest1;
    public GameObject openchest2;
    public WeaponType weaponreward;
    private GameObject player;
    private Player playerscript;
    private string message;

    public string DialogMessage
    {
        get
        {
            return message;
        }
    }

    void Start()
    {
        playerscript = FindObjectOfType<Player>();
        player = playerscript.gameObject;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        
        IMessageDelegate messageDelegate = Interface.Find<IMessageDelegate>(FindObjectOfType<Dialogue>().gameObject);
        if (messageDelegate != null)
        {
            GiveWeapon();
            openchest1.SetActive(true);
            openchest2.SetActive(true);
            messageDelegate.ShowMessageWithOkCancel(DialogMessage, "Take Weapon", "Leave Weapon", OnClickOK);      
            
            Time.timeScale = 0f;
        }
        else
            Debug.Log("No IMessageDelegate found on other collider");
    }

    void GiveWeapon()
    {
        if (UnityEngine.Random.Range(0f, 1f) < .70f)
        {
            weaponreward = WeaponType.Bronze;
            message = "You Win!\n You Obtained The Bronze Sword";
        }
        else if (UnityEngine.Random.Range(0f, 1f) < .90f)
        {
            weaponreward = WeaponType.Silver;
            message = "You Win!\n You Obtained The Silver Sword";
        }
        else if (UnityEngine.Random.Range(0f, 1f) < .98f)
        {
            weaponreward = WeaponType.Gold;
            message = "You Win!\n You Obtained The Gold Sword";
        }
        else
        {
            weaponreward = WeaponType.Epic;
            message = "You Win!\n You Obtained The Epic Sword";
        }
    }

    public void OnClickOK()
    {
        if (weaponreward == WeaponType.Bronze)
        {
            if (playerscript.weaponType == WeaponType.Silver || playerscript.weaponType == WeaponType.Bronze || playerscript.weaponType == WeaponType.Gold || playerscript.weaponType == WeaponType.Epic)
                Debug.Log("Cannot Upgrade");
            else
                playerscript.UpgradeWeapon(weaponreward);
        }
        else if(weaponreward == WeaponType.Silver)
        {
            if(playerscript.weaponType == WeaponType.Bronze || playerscript.weaponType == WeaponType.Silver)
                Debug.Log("Cannot Upgrade");
            else
                playerscript.UpgradeWeapon(weaponreward);
        }
        else if (weaponreward == WeaponType.Gold)
        {
            if (playerscript.weaponType == WeaponType.Bronze || playerscript.weaponType == WeaponType.Silver || playerscript.weaponType == WeaponType.Gold)
                Debug.Log("Cannot Upgrade");
            else
                playerscript.UpgradeWeapon(weaponreward);
        }
        else if (weaponreward == WeaponType.Epic)
        {
            if (playerscript.weaponType == WeaponType.Epic)
                Debug.Log("Cannot Upgrade");
            else
                playerscript.UpgradeWeapon(weaponreward);
        }


        IAttributesManager attribute = Interface.Find<IAttributesManager>(player);
        if (attribute != null)
        {
            attribute.SaveAttributes(false);
            Time.timeScale = 1f;
            GameInfo.AreaToTeleportTo = GameInfo.Area.World;
            SceneManager.LoadScene("SceneLoader");
        }
    }

    public void OnClickCancel()
    {
        IAttributesManager attribute = Interface.Find<IAttributesManager>(player);
        if (attribute != null)
        {
            attribute.SaveAttributes(false);
            Time.timeScale = 1f;
            GameInfo.AreaToTeleportTo = GameInfo.Area.World;
            SceneManager.LoadScene("SceneLoader");
        }
    }
}