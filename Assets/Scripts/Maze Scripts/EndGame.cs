using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour
{
    public GameObject openchest;
    public GameObject UI;
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
            UI.SetActive(true);
            GiveWeapon();
            Time.timeScale = 0f;
        }
    }

    void GiveWeapon()
    {
        if (Random.Range(0f, 1f) < .70f)
        {
            weaponreward = WeaponType.Bronze;
            Debug2Screen.Log("You Win!\n You Obtained The Bronze Sword");
        }
        else if (Random.Range(0f, 1f) < .90f)
        {
            weaponreward = WeaponType.Silver;
            Debug2Screen.Log("You Win!\n You Obtained The Silver Sword");
        }
        else if (Random.Range(0f, 1f) < .98f)
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

    public void TakeWeapon()
    {
        player.ChangeWeapon(weaponreward);
        //save
        //exit to world
        Time.timeScale = 1;
        Application.LoadLevel(0);
    }

    public void LeaveWeapon()
    {
        //save
        //exit to world
        Time.timeScale = 1;
        Application.LoadLevel(0);
    }
}