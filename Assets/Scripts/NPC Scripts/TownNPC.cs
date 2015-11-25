using UnityEngine;
using System.Collections;
using System;

public class TownNPC : MonoBehaviour, INPCMessage
{
    public string message
    {
        get
        {
            return "Yo! I stand out in front of the town";
        }
    }

    public void OnClickOK()
    {
        //save player info
        //start minigame
        Debug.Log("Town guy started the game");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        IPlayerCurrentWeapon player = Interface.Find<IPlayerCurrentWeapon>(other.gameObject);
        if (player != null)
        {
            Dialogue.Instance.ShowMessage(message, OnClickOK);
        }
    }
}