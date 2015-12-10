using UnityEngine;
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
    InCastleBlock,
    InCastleToWorld,
    Boss
}

public class NPCInteraction : MonoBehaviour {

    public TypeOfNPC typeOfNPC;
    public NPCTo NPCTeleportTo;

    private IAttributesManager attributes;
    private IMessageDelegate messageDelegate;

    string message;
    string okButton;
    string cancelButton;
    
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
            case TypeOfNPC.InTutorialToWorld:
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
            case TypeOfNPC.InTutorialIntro:
                message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.InTutorialIntro];
                okButton = DialogueDictionary.NPCButtonOKText_Dictionary[DictionaryKey.InTutorialIntro];
                Destroy(gameObject, .01f); break;
            case TypeOfNPC.InTutorialInfo:
                message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.InTutorialInfo];
                okButton = DialogueDictionary.NPCButtonOKText_Dictionary[DictionaryKey.InTutorialInfo];
                Destroy(gameObject, .01f); break;

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        attributes = Interface.Find<IAttributesManager>(other.gameObject);

        if (attributes != null)
        {
            AssignMessage();
            Time.timeScale = 0.0f;

            IMessageDelegate messageDelegate = Interface.Find<IMessageDelegate>(FindObjectOfType<Dialogue>().gameObject);

            if (messageDelegate != null)
            {
                if (NPCTeleportTo == NPCTo.NoWhere || Skeleton.numberOfTutorialSkeletons > 0)
                    messageDelegate.ShowMessageWithOk(message, okButton);                
                else
                    messageDelegate.ShowMessageWithOkCancel(message, okButton, cancelButton, OnClickOK);
                GetComponent<Collider2D>().enabled = false;
                StartCoroutine(DelayThenEnableCollider());
            }
        }
        
    }
}
