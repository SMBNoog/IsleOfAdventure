using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public static Dialogue Instance;

    public delegate void DialogueDelegate();

    public DialogueDelegate dialogueDelegate;

    public Text text;

    public Image dialogue_Panel;

    void Start()
    {
        Instance = this;
    }

    public void ShowMessage(string message, DialogueDelegate OnClickOK)
    {
        //display message
        text.text = message;
        dialogueDelegate = OnClickOK;
        dialogue_Panel.gameObject.SetActive(true);
    }

    public void OnClickOK()
    {
        dialogueDelegate();
        dialogue_Panel.gameObject.SetActive(false);
    }
}