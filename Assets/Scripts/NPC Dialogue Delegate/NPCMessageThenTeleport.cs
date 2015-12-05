using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum NPCTo { Tutorial, Forest, Castle, World, NoWhere, MainMenu }

[Serializable]
public class MessageByType
{
    public string message;
    public WeaponType weapon;
}

public class NPCMessageThenTeleport : MonoBehaviour, INPCMessageAndAction {

    // Add check for weapon

    public List<MessageByType> messages;
    public string okButton;
    public string cancelButton;
    public GameObject interfaceSupplierForMessageDelagate;
    public NPCTo NPCTeleportTo;

    public bool TutorialNPC = false;

    public bool townNPC = false;
    public bool InsideCastle = false;
    public bool InsideForest = false;

    public string townMessage = "Help defend the Town?";

    private IAttributesManager attributes;
    private IMessageDelegate messageDelegate;

    private GameObject tempPlayer;

    public string DialogMessage
    {
        get
        {
            string message = "Empty Message";

            if(townNPC)
            {
                message = townMessage;
            }
            else
            {
                if (messages.Count == 1)
                    message = messages[0].message;
                else
                {
                    foreach (MessageByType m in messages)
                    {
                        if (m.weapon == WeaponType.Wooden)
                        {
                            message = m.message;
                            break;
                        }
                        else if (m.weapon == WeaponType.Bronze)
                        {
                            message = m.message;
                            break;
                        }
                        else if (m.weapon == WeaponType.Silver)
                        {
                            message = m.message;
                            break;
                        }
                        else if (m.weapon == WeaponType.Gold)
                        {
                            message = m.message;
                            break;
                        }
                        else if (m.weapon == WeaponType.Epic)
                        {
                            message = m.message;
                            break;
                        }
                    }
                }             
            }
            return message;
        }
    }

    public void OnClickOK()
    {
        if(townNPC)
        {
            // Start town minigame
            Time.timeScale = 1.0f;
        }
        else if (attributes != null && NPCTeleportTo != NPCTo.NoWhere)
        {
            if (TutorialNPC)
            {                
                GameInfo.StartTutorial = false;
                GameInfo.LastPos = new Vector2(-2.7f, -17.7f);
                attributes.SaveAttributes(false);
            }
            else if(InsideCastle || InsideForest)
            {
                attributes.SaveAttributes(false);
            }
            else
                attributes.SaveAttributes(true);

            //if (GameInfo.AreaToTeleportTo == GameInfo.Area.Forest)
            //{
            //    ICurrentPos currentPos = Interface.Find<ICurrentPos>(tempPlayer);
            //    if (currentPos != null)
            //        GameInfo.LastPos = currentPos.postion - new Vector2(-2f, -2f);
            //    else
            //        Debug.Log("Couldn't find ICurrentPos");           
            
            Time.timeScale = 1.0f;
            Application.LoadLevel(GameInfo.sceneLoader);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        attributes = Interface.Find<IAttributesManager>(other.gameObject);
        
        if (attributes != null)
        {
            tempPlayer = other.gameObject;
            Time.timeScale = 0.0f;

            switch (NPCTeleportTo)
            {
                case NPCTo.Tutorial: GameInfo.AreaToTeleportTo = GameInfo.Area.TutorialArea; break;
                case NPCTo.World: GameInfo.AreaToTeleportTo = GameInfo.Area.World; break;
                case NPCTo.Forest: GameInfo.AreaToTeleportTo = GameInfo.Area.Forest; break;
                case NPCTo.Castle: GameInfo.AreaToTeleportTo = GameInfo.Area.Castle; break;
                case NPCTo.MainMenu: GameInfo.AreaToTeleportTo = GameInfo.Area.MainMenu; break;
                case NPCTo.NoWhere: break;
            }            

            IMessageDelegate messageDelegate = Interface.Find<IMessageDelegate>(interfaceSupplierForMessageDelagate);

            if (messageDelegate != null)
            {
                messageDelegate.ShowMessage(DialogMessage, okButton, cancelButton, OnClickOK);
            }
        }
    }



}
