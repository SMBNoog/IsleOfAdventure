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
    InForestIntro,
    InWorldToForest,
    InWorldToCastle,
    InWorldAtTown,
    InForestChestToWorld,
    InCastleFirstDoor,
    InCastleSecondDoor,
    InCastleToWorld,
    Boss
}

public class NPCInteraction : MonoBehaviour {

    public TypeOfNPC typeOfNPC;
    public NPCTo NPCTeleportTo;

    private IAttributesManager attributes;
    //private IMessageDelegate messageDelegate;
    private Player player;

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
            }

            if (typeOfNPC == TypeOfNPC.InTutorialToWorld && Skeleton.numberOfTutorialSkeletons <= 0)
            {
                GameInfo.TutorialCompleted = true;
                GameInfo.LastPos = new Vector2(-2.7f, -17.7f); // set spawn in world for the 1st time
                attributes.SaveAttributes(false);
                Time.timeScale = 1.0f;
                SceneManager.LoadScene(GameInfo.sceneLoader);
            }
            else if (typeOfNPC == TypeOfNPC.InCastleToWorld || typeOfNPC == TypeOfNPC.InForestChestToWorld)
            {
                attributes.SaveAttributes(false);
                Time.timeScale = 1.0f;
                SceneManager.LoadScene(GameInfo.sceneLoader);
            }
            else
            {
                //otherwise going from the world to Castle or Forest
                attributes.SaveAttributes(true);
                Time.timeScale = 1.0f;
                SceneManager.LoadScene(GameInfo.sceneLoader);
            }
        }
        else if (typeOfNPC == TypeOfNPC.InCastleFirstDoor)
        {
            if (player.weaponType != WeaponType.Wooden)
            {
                gameObject.SetActive(false);
                return;
            }
        }
        //else
        //{
        //    if (GameInfo.AreaToTeleportTo == GameInfo.Area.Castle)
        //    {
        //        gameObject.SetActive(false);
        //    }
        //}

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
            // ************************* Tutorial
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
                message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.InTutorialIntroNarration];
                okButton = DialogueDictionary.NPCButtonOKText_Dictionary[DictionaryKey.InTutorialIntroNarration];
                Destroy(gameObject, .01f); break;
            case TypeOfNPC.InTutorialInfo: // Bush Info
                message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.InTutorialInfo];
                okButton = DialogueDictionary.NPCButtonOKText_Dictionary[DictionaryKey.InTutorialInfo];
                Destroy(gameObject, .01f); break;

            // ************************* Castle
            case TypeOfNPC.InCastleFirstDoor:   // Castle First Door
                if(player.weaponType == WeaponType.Wooden)
                {
                    message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.InCastleFirstDoorWooden];
                    okButton = DialogueDictionary.NPCButtonOKText_Dictionary[DictionaryKey.InCastleFirstDoorWooden];
                }
                else
                {
                    message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.InCastleFirstDoorAboveWooden];
                    okButton = DialogueDictionary.NPCButtonOKText_Dictionary[DictionaryKey.InCastleFirstDoorAboveWooden];
                }
                break;
            case TypeOfNPC.InCastleToWorld: // Castle >>> World
                message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.InCastleToWorld];
                okButton = DialogueDictionary.NPCButtonOKText_Dictionary[DictionaryKey.InCastleToWorld]; 
                cancelButton = DialogueDictionary.NPCButtonCancelText_Dictionary[DictionaryKey.InCastleToWorld]; break;

            // ************************* World
            case TypeOfNPC.InWorldToCastle: // World >>> Castle
                message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.InWorldToCastle]; break;
            case TypeOfNPC.InWorldToForest: // World >>> Forest
                message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.InWorldToForest]; break;
            case TypeOfNPC.SignAtSpawnIn: // Sing at spawn in
                message = DialogueDictionary.NPCMessage_Dictionary[DictionaryKey.SignAtSpawnIn]; break;

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if(other.gameObject.tag == "Player")
        {
            player = other.GetComponent<Player>();
        }

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
                else if (NPCTeleportTo == NPCTo.World && Skeleton.numberOfTutorialSkeletons > 0)
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
