using UnityEngine;
using System.Collections;
using System;

public class CastleNPC : MonoBehaviour, INPCMessage
{
    public string message
    {
        get
        {
            return "Yo, I'm the castle guard";
        }
    }

    public void OnClickOK()
    {
        //save player infor
        //load the castle level
        Debug.Log("Castle guy loaded a new scene");
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