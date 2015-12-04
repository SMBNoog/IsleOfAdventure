using UnityEngine;
using System.Collections;
using System;

public class Chest : MonoBehaviour, IMessageDelegate
{
    public GameObject openchest;
    public WeaponType weaponreward;
    Player player;

    void Awake()
    {
        //load info about player

        //check player weapon

        //change enemies based on weapon
    }

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            openchest.SetActive(true);
            GiveWeapon();
            Time.timeScale = 0f;
        }
    }

    void GiveWeapon()
    {
        if (UnityEngine.Random.Range(0f, 1f) < .70f)
        {
            weaponreward = WeaponType.Bronze;
            Debug2Screen.Log("You Win!\n You Obtained The Bronze Sword");
        }
        else if (UnityEngine.Random.Range(0f, 1f) < .90f)
        {
            weaponreward = WeaponType.Silver;
            Debug2Screen.Log("You Win!\n You Obtained The Silver Sword");
        }
        else if (UnityEngine.Random.Range(0f, 1f) < .98f)
        {
            weaponreward = WeaponType.Gold;
            Debug2Screen.Log("You Win!\n You Obtained The Gold Sword");
        }
        else
        {
            weaponreward = WeaponType.Epic;
            Debug2Screen.Log("You Win!\n You Obtained The Epic Sword");
        }
    }

    //Button Control Scripts Below...

    public void OnClickOK()
    {
        player.UpgradeWeapon(weaponreward);
        //save

        //unpause game and exit to world
        Time.timeScale = 1;
        // tell gameinfo 
        Application.LoadLevel("ScneneLoader");
    }

    public void OnClickCancel()
    {
        //save

        //unpause game and exit to world
        Time.timeScale = 1;
        Application.LoadLevel("SceneLoader");
    }

    public void ShowMessage(string dialogMessage, string okButton, string cancelButton, Dialogue.DialogueDelegate onClickOK)
    {
        throw new NotImplementedException();
    }
}