using UnityEngine;
using System.Collections.Generic;

public enum DictionaryKey {
                            InTutorialToWorld_NotCompleted,
                            InTutorialToWorld_Completed,
                            DirectionsSign,
                            ToForestWooden,
                            ToForestNonWooden,
                            InForestWooden,
                            InForestNonWooden,
                            ToCastleWooden,
                            ToCastleNonWooden,
                            InCastleBarrier,
                          }

public class DialogueDictionary : MonoBehaviour
{
    public static Dictionary<DictionaryKey, string> NPCMessage_Dictionary = new Dictionary<DictionaryKey, string>();
    public static Dictionary<DictionaryKey, string> NPCButtonOKText_Dictionary = new Dictionary<DictionaryKey, string>();
    public static Dictionary<DictionaryKey, string> NPCButtonCancelText_Dictionary = new Dictionary<DictionaryKey, string>();

    void Start()
    {
        /********* NPC in tutorial area. **********/
        NPCMessage_Dictionary.Add(DictionaryKey.InTutorialToWorld_NotCompleted, "Help defeat the skeletons!");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InTutorialToWorld_NotCompleted, "On it!");

        NPCMessage_Dictionary.Add(DictionaryKey.InTutorialToWorld_Completed, "Thanks for saving me! Let me help you get to the Isle. kadjfkjdalk fjdklasjfldasjkdlsjfkda sjdfjlkdasjl dkajda k j kldf j dfaj dkaj dkajdf klfdalj dj af klj akja dfkl jka dfaljdf lajdfl ja ");
        NPCButtonOKText_Dictionary.Add(DictionaryKey.InTutorialToWorld_Completed, "Ok!");
        NPCButtonCancelText_Dictionary.Add(DictionaryKey.InTutorialToWorld_Completed, "Hold On. I like it here!");



        NPCMessage_Dictionary.Add(DictionaryKey.DirectionsSign, "Message");

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