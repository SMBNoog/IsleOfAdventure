using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum NPCTo { Tutorial, Forest, Castle, World, SceneLoader, NoWhere }

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

    private IAttributesManager attributes;
    private IMessageDelegate messageDelegate;

    public string DialogMessage
    {
        get
        {
            string message = "Empty Message";
            foreach (MessageByType m in messages)
            {
                if (m.weapon == WeaponType.Wooden)
                    message = m.message;
                else  // Add more conditions if needed
                    message = m.message;
            }
            return message;
        }
    }

    public void OnClickOK()
    {
        if (attributes != null && NPCTeleportTo != NPCTo.NoWhere)
        {
            attributes.SaveAttributes();
            Time.timeScale = 1.0f;

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
                case NPCTo.Tutorial: GameInfo.setArea(GameInfo.Area.TutorialArea); break;
                case NPCTo.World: GameInfo.setArea(GameInfo.Area.World); GameInfo.TutorialCompleted = true; break;
                case NPCTo.Forest: GameInfo.setArea(GameInfo.Area.Forest); break;
                case NPCTo.Castle: GameInfo.setArea(GameInfo.Area.Castle); break;
                case NPCTo.NoWhere: break;
                case NPCTo.SceneLoader: break;
            }            

            IMessageDelegate messageDelegate = Interface.Find<IMessageDelegate>(interfaceSupplierForMessageDelagate);

            if (messageDelegate != null)
            {
                messageDelegate.ShowMessage(DialogMessage, okButton, cancelButton, OnClickOK);
            }
        }
    }



}
