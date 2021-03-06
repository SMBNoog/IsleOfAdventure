﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum TypeOfNPC
{
    InTutorialToWorld,
    InTutorialIntro,
    InTutorialInfo,
    SignAtSpawnIn,
    SignAtForest,
    SignAtCastle,
    SignAtTown,
    InWorldToForest,
    InWorldToCastle,
    InWorldAtTown,
    InForestChestToWorld,
    InCastleFirstDoor,
    InCastleToWorld,
    Boss
}

public class NPCInteraction : MonoBehaviour {

    public TypeOfNPC typeOfNPC;
    public NPCTo NPCTeleportTo;

    private IAttributesManager attributes;
    //private IMessageDelegate messageDelegate;
    private IWeapon weapon;

    string message = "No Message";
    string okButton = "Ok";
    string cancelButton = "Cancel";
    
    void Start()
    {
        StartCoroutine(DelayThenTurnOnCollider());
    }

    IEnumerator DelayThenTurnOnCollider()
    {
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        GetComponent<Collider2D>().enabled = true;
    }

    public void OnClickOK()
    {
        if (attributes != null && NPCTeleportTo != NPCTo.NoWhere)
        {
            // Set Area to teleport     
            switch (NPCTeleportTo)
            {
                case NPCTo.Tutorial: GameInfo.AreaToTeleportTo = GameInfo.Area.TutorialArea; break;
                case NPCTo.World: GameInfo.AreaToTeleportTo = GameInfo.Area.World; break;
                case NPCTo.Forest: GameInfo.AreaToTeleportTo = GameInfo.Area.Forest; break;
                case NPCTo.Castle: GameInfo.AreaToTeleportTo = GameInfo.Area.Castle; break;
                case NPCTo.MainMenu: GameInfo.AreaToTeleportTo = GameInfo.Area.MainMenu; break;
                case NPCTo.NoWhere: break;
            }

            if (typeOfNPC == TypeOfNPC.InTutorialToWorld)
            {
                GameInfo.TutorialCompleted = true;
                GameInfo.LastPos = new Vector2(-2.7f, -17.7f); // set spawn in world for the 1st time
                attributes.SaveAttributes(false);
            }
            else if (typeOfNPC == TypeOfNPC.InCastleToWorld || typeOfNPC == TypeOfNPC.InForestChestToWorld)
            {
                attributes.SaveAttributes(false);
            }
            else //otherwise going from the world to Castle or Forest
                attributes.SaveAttributes(true);

            Time.timeScale = 1.0f;

            SceneManager.LoadScene(GameInfo.sceneLoader);
        } 
        else
        {
            if(GameInfo.AreaToTeleportTo == GameInfo.Area.Castle)
            {
                gameObject.SetActive(false);
            }
        }
        
    }

    IEnumerator DelayThenEnableCollider()
    {
        yield return new WaitForSeconds(1.5f);
        GetComponent<Collider2D>().enabled = true;
    }

    void AssignMessage()
    {
        switch (typeOfNPC)
        {
            case TypeOfNPC.InTutorialToWorld:  // Tutorial >>> World
                if (Skeleton.numberOfTutorialSkeletons > 0) // Do checks here for weapon or enemies killed
                {
                    message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.InTutorialToWorld_NotCompleted];
                    okButton = DialogueDictionary.NPCButtonOKText_Dictionary[DictionaryKey.InTutorialToWorld_NotCompleted];
                }
                else
                {
                    message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.InTutorialToWorld_Completed];
                    okButton = DialogueDictionary.NPCButtonOKText_Dictionary[DictionaryKey.InTutorialToWorld_Completed];
                    cancelButton = DialogueDictionary.NPCButtonCancelText_Dictionary[DictionaryKey.InTutorialToWorld_Completed];
                } break;
            case TypeOfNPC.InTutorialIntro: // Tutorial Intro 
                message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.InTutorialIntro];
                okButton = DialogueDictionary.NPCButtonOKText_Dictionary[DictionaryKey.InTutorialIntro];
                Destroy(gameObject, .01f); break;
            case TypeOfNPC.InTutorialInfo: // Bush Info
                message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.InTutorialInfo];
                okButton = DialogueDictionary.NPCButtonOKText_Dictionary[DictionaryKey.InTutorialInfo];
                Destroy(gameObject, .01f); break;
            case TypeOfNPC.InCastleFirstDoor:   // Castle First Door
                if(weapon.WeaponType == WeaponType.Wooden)
                {
                    message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.InCastleFirstDoorWooden];
                    okButton = DialogueDictionary.NPCButtonOKText_Dictionary[DictionaryKey.InCastleFirstDoorWooden];
                }
                else
                {
                    message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.InCastleFirstDoorNonWooden];
                    okButton = DialogueDictionary.NPCButtonOKText_Dictionary[DictionaryKey.InCastleFirstDoorNonWooden];
                }
                break;
            case TypeOfNPC.InCastleToWorld: // Castle >>> World
                message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.InCastleToWorld];
                okButton = DialogueDictionary.NPCButtonOKText_Dictionary[DictionaryKey.InCastleToWorld]; 
                cancelButton = DialogueDictionary.NPCButtonCancelText_Dictionary[DictionaryKey.InCastleToWorld]; break;
            case TypeOfNPC.InWorldToCastle: // World >>> Castle
                message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.InWorldToCastle]; break;
            case TypeOfNPC.InWorldToForest: // World >>> Forest
                message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.InWorldToForest]; break;

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        weapon = Interface.Find<IWeapon>(other.gameObject);
        attributes = Interface.Find<IAttributesManager>(other.gameObject);

        if (attributes != null)
        {
            AssignMessage();
            Time.timeScale = 0.0f;

            IMessageDelegate messageDelegate = Interface.Find<IMessageDelegate>(FindObjectOfType<Dialogue>().gameObject);

            if (messageDelegate != null)
            {
                if (NPCTeleportTo == NPCTo.NoWhere)
                    messageDelegate.ShowMessageWithOk(message, okButton, OnClickOK);
                else if (Skeleton.numberOfTutorialSkeletons > 0)
                    messageDelegate.ShowMessageWithOk(message, okButton);
                else
                    messageDelegate.ShowMessageWithOkCancel(message, okButton, cancelButton, OnClickOK);
                GetComponent<Collider2D>().enabled = false;
                StartCoroutine(DelayThenEnableCollider());
            }
            else { Debug.LogError("Dialogue could not be found."); }
        }
        
    }
}
