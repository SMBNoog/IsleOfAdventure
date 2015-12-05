using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueDictionary : MonoBehaviour
{
    Dictionary<string, string> npcDialogue = new Dictionary<string, string>();

    void Start()
    {
        npcDialogue.Add("Tutorial", "Message");

        npcDialogue.Add("Sign", "Message");

        npcDialogue.Add("Forest Wood", "Message");
        npcDialogue.Add("Forest Bronze", "Message");
        npcDialogue.Add("Forest Silver", "Message");
        npcDialogue.Add("Forest Gold", "Message");
        npcDialogue.Add("Forest Epic", "Message");

        npcDialogue.Add("Inside Forest", "Message");

        npcDialogue.Add("Castle Wood", "Message");
        npcDialogue.Add("Castle Bronze", "Message");
        npcDialogue.Add("Castle Silver", "Message");
        npcDialogue.Add("Castle Gold", "Message");
        npcDialogue.Add("Castle Epic", "Message");

        npcDialogue.Add("Inside Castle", "Message");

        npcDialogue.Add("Boss", "Message");
    }
}