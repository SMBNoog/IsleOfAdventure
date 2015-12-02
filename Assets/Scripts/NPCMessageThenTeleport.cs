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
    public string townMessage = "Help defend the Town?";

    private IAttributesManager attributes;
    private IMessageDelegate messageDelegate;

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
        if (attributes != null && NPCTeleportTo != NPCTo.NoWhere)
        {
            if (TutorialNPC)
            {
                GameInfo.TutorialNotCompleted = false;
            }
            attributes.SaveAttributes();
            Time.timeScale = 1.0f;
            GameInfo.TutorialNotCompleted = true;
            Application.LoadLevel(GameInfo.sceneToLoad);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        attributes = Interface.Find<IAttributesManager>(other.gameObject);
        if(attributes != null)
        {
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
