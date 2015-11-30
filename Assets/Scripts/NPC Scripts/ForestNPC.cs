using UnityEngine;
using System.Collections;
using System;

public class ForestNPC : MonoBehaviour, INPCMessage
{
    public string message
    {
        get
        {
            return "I'm glad you're here! \nI was chased out of the forest by a group of enemies and accidently dropped my sword. \nCan you go in and retrieve it for me?";
        }
    }

    public void OnClickOK()
    {
        //save player info
        //load the forest level
        Debug.Log("Forest guy loaded the new scene");
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
