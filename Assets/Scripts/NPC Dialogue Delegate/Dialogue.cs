using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Dialogue : MonoBehaviour, IMessageDelegate
{
    public delegate void DialogueDelegate();

    public DialogueDelegate dialogueDelegate;

    public Text text;
    public Text okButton;
    public Text cancelButton;

    public Image dialogue_Panel;
    
    public void OnClickOK()
    {
        Time.timeScale = 1.0f;
        dialogueDelegate();        
        dialogue_Panel.gameObject.SetActive(false);
    }

    public void OnClickCancel()
    {
        dialogue_Panel.gameObject.SetActive(false);
        dialogueDelegate = null;
        Time.timeScale = 1.0f;
    }

    public void ShowMessage(string dialogMessage, string okButton, string cancelButton, DialogueDelegate onClickOK)
    {
        //display message
        text.text = dialogMessage;
        this.okButton.text = okButton;
        this.cancelButton.text = cancelButton;
        dialogueDelegate = onClickOK;
        dialogue_Panel.gameObject.SetActive(true);
    }
}