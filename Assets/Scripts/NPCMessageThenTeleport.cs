using UnityEngine;
using System.Collections;
using System;

public enum NPCTo { Tutorial, Forest, Castle, World, SceneLoader, NoWhere }

public class NPCMessageThenTeleport : MonoBehaviour, INPCMessageAndAction {

    public string dialogMessage;
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
            return dialogMessage;
        }
    }

    public void OnClickOK()
    {
        Debug.Log("Running Ok click from delegate");
        if (attributes != null && NPCTeleportTo != NPCTo.NoWhere)
        {
            attributes.SaveAttributes();
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
