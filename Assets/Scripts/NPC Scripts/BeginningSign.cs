using UnityEngine;
using System.Collections;
using System;

public class BeginningSign : MonoBehaviour, INPCMessage
{
    public string message
    {
        get
        {
            return "Yo! I'm Bill the Board! \nThe forest is to your left, \nThe town is to your right, \nand the castle is straight ahead.";
        }
    }

    public void OnClickOK()
    {
        //Do Nothing Cause Your A Sign
        Debug.Log("Sign did nothing cause he's a sign");
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