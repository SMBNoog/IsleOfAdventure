using UnityEngine;
using System.Collections.Generic;

public enum DictionaryKey {
                            InTutorialIntroNarration,
                            InTutorialInfo,
                            InTutorialToWorld_NotCompleted,
                            InTutorialToWorld_Completed,
                            SignAtSpawnIn,
                            InWorldNPCIntro,
                            InWorldToCastle,
                            InWorldToForest,
                            InForestIntroNarration,
                            InCastleToWorld,
                            InCastleFirstDoorWooden,
                            InCastleFirstDoorAboveWooden,
                            InCastleSecondDoorFlame,
                            InCastleSecondDoorAboveFlame,
                            InWorldAtForest
                          }


public class DialogueDictionary : MonoBehaviour
{
    public static Dictionary<DictionaryKey, string> NPCMessage_Dictionary = new Dictionary<DictionaryKey, string>();
    public static Dictionary<DictionaryKey, string> NPCButtonOKText_Dictionary = new Dictionary<DictionaryKey, string>();
    public static Dictionary<DictionaryKey, string> NPCButtonCancelText_Dictionary = new Dictionary<DictionaryKey, string>();

    public static void FillDictionary()
    {
        string playerName = GameInfo.PlayerName.ToString();
        // TEMPLATE
        // NPCMessage_Dictionary.Add(DictionaryKey. , "");
        // NPCButtonOKText_Dictionary.Add(DictionaryKey. , "");
        // NPCButtonCancelText_Dictionary.Add(DictionaryKey. , "");

        /********* IN TUTORIAL **********/
        //------------------------------//

        /********* Intro narration. **********/
         
        NPCMessage_Dictionary.Add(DictionaryKey.InTutorialIntroNarration, playerName+"!"+"\nThe Isle has been taken over by an evil Dragon! Use the power of your Sword to vanquish the evil Dragon Overlord!");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InTutorialIntroNarration, "OK!");
                
        /********* How to increase attributes **********/
        //NPCMessage_Dictionary.Add(DictionaryKey.InTutorialInfo, "Bush's can incrase your Health, Attack and Defense. Check out the Menu to see your current stats.");
        //NPCButtonOKText_Dictionary.Add(DictionaryKey.InTutorialInfo, "Got it!");

        /********* NPC teleport to World **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InTutorialToWorld_NotCompleted, "Help! Save me from these skeletons!");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InTutorialToWorld_NotCompleted, "On it!");

        NPCMessage_Dictionary.Add(DictionaryKey.InTutorialToWorld_Completed, "Thanks for saving me "+playerName+"! Wow you might have what it takes to defeat the Dragon. I can show you the way to the Isle, if you'd like."); // add in line about sword
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InTutorialToWorld_Completed, "Ok!");
        NPCButtonCancelText_Dictionary.Add(DictionaryKey.InTutorialToWorld_Completed, "Not yet");

        /********* IN WORLD **********/
        //---------------------------//

        /********* Intro to World NPC from Tutorial **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InWorldNPCIntro, "I've heard legends of an Epic Sword deep in the Forest. Once you obtain more power, use it to save us and vanquish the Dragon in the Castle. Good luck!");


        /********* NPC teleport to Castle. **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InWorldToCastle, "Are you here to save us from the Dragon? I don't think you can with that Wooden Sword.");

        //NPCMessage_Dictionary.Add(DictionaryKey.  "You may have a chance with that sword. Would you like to enter now to defeat the Dragon?"
        // ok and not yet

        //Enter the Castle?

        NPCButtonOKText_Dictionary.Add(DictionaryKey.InWorldToCastle, "Ok!");
        NPCButtonCancelText_Dictionary.Add(DictionaryKey.InWorldToCastle, "Not yet");

        /********* NPC near the Forest **********/    
        NPCMessage_Dictionary.Add(DictionaryKey.InWorldAtForest, "Woah! I barely made it out in time! I found some treasure in the forest maze, but I know the Epic Sword from the legends is still in there. Maybe you will have better luck.");

        /********* Enter the Forest **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InWorldToForest, "Enter the Forest?");

        /********* IN CASTLE **********/
        //----------------------------//

        /********* First Door Check for Weapon **********/   // NPC in castle
        NPCMessage_Dictionary.Add(DictionaryKey.InCastleFirstDoorWooden, "You won't have a chance with that Wooden Sword. Go to the Forest to obtain a new one.");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InCastleFirstDoorWooden, "Ok!");
        
        NPCMessage_Dictionary.Add(DictionaryKey.InCastleFirstDoorAboveWooden, "You may enter! Good Luck!");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InCastleFirstDoorAboveWooden, "Alright!");

        ///********* Second Door Check for Weapon **********/
        //NPCMessage_Dictionary.Add(DictionaryKey.InCastleSecondDoorFlame, "You won't have a chance with that Wooden Sword. Go back to the Forest!");

        //NPCMessage_Dictionary.Add(DictionaryKey.InCastleSecondDoorAboveFlame, "You may pass.");

        /********* Door way teleport back to the World. **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InCastleToWorld, "Leave the Castle?");
        //NPCButtonOKText_Dictionary.Add(DictionaryKey.InCastleToWorld, "Sure!");
        //NPCButtonCancelText_Dictionary.Add(DictionaryKey.InCastleToWorld, "Nah");

        /********* IN FOREST **********/
        //----------------------------//

        /********* Intro narration **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InForestIntroNarration, "Find the treasure chest before time runs out.");

        /********* IN WORLD SIGNS **********/
        //---------------------------------//

        // Near Spawn in Area
        NPCMessage_Dictionary.Add(DictionaryKey.SignAtSpawnIn, "Forest West\nCastle North");
        
    }
}