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
    public GameObject interfacesupplier;
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

    void Awake()
    {
        //load info about player

        //check player weapon

        //change enemies based on weapon
    }

    void Start()
    {
        playerscript = FindObjectOfType<Player>();
        player = playerscript.gameObject;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        IMessageDelegate messageDelegate = Interface.Find<IMessageDelegate>(interfacesupplier);
        if (messageDelegate != null)
        {
            messageDelegate.ShowMessageWithOkCancel(DialogMessage, "Take Weapon", "Leave Weapon", OnClickOK);
            openchest1.SetActive(true);
            openchest2.SetActive(true);
            GiveWeapon();          
            Time.timeScale = 0f;
        }
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

    //Button Control Scripts Below...

    public void OnClickOK()
    {
        playerscript.UpgradeWeapon(weaponreward);
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