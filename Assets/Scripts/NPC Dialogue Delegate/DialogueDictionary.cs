using UnityEngine;
using System.Collections.Generic;

public enum DictionaryKey {
                            InTutorialIntroNarration,
                            InTutorialInfo,
                            InTutorialToWorld_NotCompleted,
                            InTutorialToWorld_Completed,
                            SignAtSpawnIn,
                            InWorldIntroNarration,
                            InWorldToCastle,
                            InWorldToForest,
                            InForestIntroNarration,
                            InCastleToWorld,
                            InCastleFirstDoorWooden,
                            InCastleFirstDoorAboveWooden,
                            InCastleSecondDoorFlame,
                            InCastleSecondDoorAboveFlame
                          }


public class DialogueDictionary : MonoBehaviour
{
    public static Dictionary<DictionaryKey, string> NPCMessage_Dictionary = new Dictionary<DictionaryKey, string>();
    public static Dictionary<DictionaryKey, string> NPCButtonOKText_Dictionary = new Dictionary<DictionaryKey, string>();
    public static Dictionary<DictionaryKey, string> NPCButtonCancelText_Dictionary = new Dictionary<DictionaryKey, string>();

    void Awake()
    {

        // TEMPLATE
        // NPCMessage_Dictionary.Add(DictionaryKey. , "");
        // NPCButtonOKText_Dictionary.Add(DictionaryKey. , "");
        // NPCButtonCancelText_Dictionary.Add(DictionaryKey. , "");

        /********* IN TUTORIAL **********/
        /********************************/

        /********* Intro narration. **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InTutorialIntroNarration, "The Isle has been taken over by an evil Dragon! Use the power of your Sword to vanquish the evil Dragon Overlord!");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InTutorialIntroNarration, "OK!");
        
        /********* How to increase attributes **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InTutorialInfo, "Bush's can incrase your Health, Attack and Defense. Check out the Menu to see your current stats.");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InTutorialInfo, "Got it!");

        /********* NPC teleport to World **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InTutorialToWorld_NotCompleted, "Help defeat the skeletons!");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InTutorialToWorld_NotCompleted, "On it!");

        NPCMessage_Dictionary.Add(DictionaryKey.InTutorialToWorld_Completed, "Thanks for saving me! I can show you the way to the Isle where the Dragon has his Castle.");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InTutorialToWorld_Completed, "Ok!");
        NPCButtonCancelText_Dictionary.Add(DictionaryKey.InTutorialToWorld_Completed, "I like it here!");

        /********* IN WORLD **********/
        /*****************************/

        /********* Intro to World narration **********/


        /********* NPC teleport to Castle. **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InWorldToCastle, "Enter the Castle?");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InWorldToCastle, "Ok!");
        NPCButtonCancelText_Dictionary.Add(DictionaryKey.InWorldToCastle, "I like it here!");

        /********* NPC teleport to Forest **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InWorldToForest, "Enter the Forest?");


        /********* IN CASTLE **********/
        /******************************/

        /********* First Door Check for Weapon **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InCastleFirstDoorWooden, "You won't have a chance with that Wooden Sword. Go to the Forest to obtain a new one.");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InCastleFirstDoorWooden, "Ok!");
        
        NPCMessage_Dictionary.Add(DictionaryKey.InCastleFirstDoorAboveWooden, "You may enter! Good Luck!");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InCastleFirstDoorAboveWooden, "Alright!");

        /********* Second Door Check for Weapon **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InCastleSecondDoorFlame, "You won't have a chance with that Wooden Sword. Go back to the Forest!");

        NPCMessage_Dictionary.Add(DictionaryKey.InCastleSecondDoorAboveFlame, "You may pass.");

        /********* Door way teleport back to the World. **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InCastleToWorld, "Travel back to the world?");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InCastleToWorld, "Sure!");
        NPCButtonCancelText_Dictionary.Add(DictionaryKey.InCastleToWorld, "Nah");

        /********* IN FOREST **********/
        /******************************/

        /********* Intro narration **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InForestIntroNarration, "Find the chest before the time runs out.");

        /********* IN WORLD SIGNS **********/
        /***********************************/

        // Near Spawn in Area
        NPCMessage_Dictionary.Add(DictionaryKey.SignAtSpawnIn, "Forst West\nCastle North");
        
        
    }
}