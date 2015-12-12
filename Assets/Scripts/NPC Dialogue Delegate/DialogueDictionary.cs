using UnityEngine;
using System.Collections.Generic;

public enum DictionaryKey {
                            InTutorialIntro,
                            InTutorialInfo,
                            InTutorialToWorld_NotCompleted,
                            InTutorialToWorld_Completed,
                            DirectionsSign,
                            InWorldToCastle,
                            InWorldToForest,
                            ToForestNonWooden,
                            InForestWooden,
                            InForestNonWooden,
                            InCastleToWorld,
                            InCastleFirstDoorWooden,
                            InCastleFirstDoorNonWooden
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

        /********* Intro tutorial area. **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InTutorialIntro, "The Isle has been taken over by an evil Dragon! Use the power of your Sword to vanquish the evil Dragon Overlord!");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InTutorialIntro, "OK!");

        NPCMessage_Dictionary.Add(DictionaryKey.InTutorialInfo, "Bush's can incrase your Health, Attack and Defense. Check out the Menu to see your current stats.");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InTutorialInfo, "Got it!");
        
        /********* NPC in tutorial area. **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InTutorialToWorld_NotCompleted, "Help defeat the skeletons!");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InTutorialToWorld_NotCompleted, "On it!");

        NPCMessage_Dictionary.Add(DictionaryKey.InTutorialToWorld_Completed, "Thanks for saving me! I can show you the way to the Isle where the Dragon has his Castle.");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InTutorialToWorld_Completed, "Ok!");
        NPCButtonCancelText_Dictionary.Add(DictionaryKey.InTutorialToWorld_Completed, "I like it here!");


        /********* NPC in World area for Castle. **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InWorldToCastle, "Enter the Castle?");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InWorldToCastle, "Ok!");
        NPCButtonCancelText_Dictionary.Add(DictionaryKey.InWorldToCastle, "I like it here!");

        /********* NPC in World area for Forest. **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InWorldToForest, "Enter the Forest?");


        
        /********* First Door in castle area. **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InCastleFirstDoorWooden, "You won't have a chance with that Wooden Sword. Go to the Forest to obtain a new one.");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InCastleFirstDoorWooden, "Ok!");
        
        NPCMessage_Dictionary.Add(DictionaryKey.InCastleFirstDoorNonWooden, "You may enter! Good Luck!");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InCastleFirstDoorNonWooden, "Alright!");

        /********* NPC in Castle area to teleport back to the World. **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InCastleToWorld, "Travel back to the world?");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InCastleToWorld, "Sure!");
        NPCButtonCancelText_Dictionary.Add(DictionaryKey.InCastleToWorld, "Nah");
        
        NPCMessage_Dictionary.Add(DictionaryKey.DirectionsSign, "Forst West   Castle North");

        //npcDialogue.Add("Forest Wood", "Message");
        //npcDialogue.Add("Forest Non Wood", "Message");

        //npcDialogue.Add("Inside Forest", "Message");

        //npcDialogue.Add("Castle Wood", "Message");
        //npcDialogue.Add("Castle Bronze", "Message");
        //npcDialogue.Add("Castle Silver", "Message");
        //npcDialogue.Add("Castle Gold", "Message");
        //npcDialogue.Add("Castle Epic", "Message");

        //npcDialogue.Add("Inside Castle", "Message");

        //npcDialogue.Add("Boss", "Message");
    }
}