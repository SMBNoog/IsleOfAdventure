using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Chest : MonoBehaviour, INPCMessageAndAction
{
    public GameObject openchest1;
    public GameObject openchest2;
    public GameObject canvas;
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
            SoundManager.Instance.StopAll();
            SoundManager.Instance.Play(TypeOfClip.ChestOpen);
            GiveWeapon();
            openchest1.SetActive(true);
            openchest2.SetActive(true);
            messageDelegate.ShowMessageWithOkCancel(DialogMessage, "Take Weapon", "Leave Weapon", OnClickOK, OnClickCancel);      
            
            Time.timeScale = 0f;
        }
        else
            Debug.Log("No IMessageDelegate found on other collider");
    }

    void GiveWeapon()
    {
        if (UnityEngine.Random.Range(0f, 1f) < 0.02f)
        {
            weaponreward = WeaponType.EpicCrusader;
            message = "You Made it!\n You Found the Epic Sword!";
        }
        else if (UnityEngine.Random.Range(0f, 1f) < 0.10f)
        {
            weaponreward = WeaponType.GoldOathkeeper;
            message = "You Made it!\n You Found a Gold Sword";
        }
        else if (UnityEngine.Random.Range(0f, 1f) < 0.30f)
        {
            weaponreward = WeaponType.SilverDoomBringer;
            message = "You Made it!\n You Found a Silver Sword";
        }
        else
        {
            weaponreward = WeaponType.FlamingBlade;
            message = "You Made it!\n You Found a Flame Sword";
        }
    }

    public void OnClickOK()
    {
        if (weaponreward == WeaponType.FlamingBlade)
        {
            if (playerscript.weaponType == WeaponType.SilverDoomBringer || playerscript.weaponType == WeaponType.FlamingBlade || playerscript.weaponType == WeaponType.GoldOathkeeper || playerscript.weaponType == WeaponType.EpicCrusader)
                Debug.Log("Cannot Upgrade");
            else
                playerscript.UpgradeWeapon(weaponreward);
        }
        else if(weaponreward == WeaponType.SilverDoomBringer)
        {
            if(playerscript.weaponType == WeaponType.GoldOathkeeper || playerscript.weaponType == WeaponType.SilverDoomBringer || playerscript.weaponType == WeaponType.EpicCrusader)
                Debug.Log("Cannot Upgrade");
            else
                playerscript.UpgradeWeapon(weaponreward);
        }
        else if (weaponreward == WeaponType.GoldOathkeeper)
        {
            if (playerscript.weaponType == WeaponType.GoldOathkeeper || playerscript.weaponType == WeaponType.EpicCrusader)
                Debug.Log("Cannot Upgrade");
            else
                playerscript.UpgradeWeapon(weaponreward);
        }
        else if (weaponreward == WeaponType.EpicCrusader)
        {
            if (playerscript.weaponType == WeaponType.EpicCrusader)
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