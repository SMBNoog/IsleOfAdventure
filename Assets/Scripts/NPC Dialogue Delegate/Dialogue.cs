using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Dialogue : MonoBehaviour, IMessageDelegate
{
    public delegate void DialogueDelegate();

    public DialogueDelegate dialogueDelegate;
    public DialogueDelegate dialogueDelegateCancel;

    public Image dialogue_PanelOkCancel;
    public Text textDuo;
    public Text okButtonDuo;
    public Text cancelButtonDuo;

    public Image dialogue_PanelOk;
    public Text textSingle;
    public Text okButtonSingle;
            
    public void OnClickOkButtonDuo()
    {
        Time.timeScale = 1.0f;
        dialogueDelegate();        
        dialogue_PanelOkCancel.gameObject.SetActive(false);
    }

    public void OnClickCancelDuo()
    {
        dialogue_PanelOkCancel.gameObject.SetActive(false);
        dialogueDelegate = null;
        if(dialogueDelegateCancel != null)
        dialogueDelegateCancel();
        Time.timeScale = 1.0f;
    }

    public void OnClickOkButtonSingle()
    {
        Time.timeScale = 1.0f;
        dialogueDelegate();
        dialogue_PanelOk.gameObject.SetActive(false);
    }
    
    public void DefualtOkButton()
    {
        dialogue_PanelOkCancel.gameObject.SetActive(false);
    }

    public void ShowMessageWithOkCancel(string dialogMessage, string okButton, string cancelButton, DialogueDelegate onClickOK)
    {
        textDuo.text = dialogMessage;
        okButtonDuo.text = okButton;
        cancelButtonDuo.text = cancelButton;
        dialogueDelegate = onClickOK;
        dialogue_PanelOkCancel.gameObject.SetActive(true);
    }

    public void ShowMessageWithOkCancel(string dialogMessage, string okButton, string cancelButton, DialogueDelegate onClickOK, DialogueDelegate onClickCancel)
    {
        textDuo.text = dialogMessage;
        okButtonDuo.text = okButton;
        cancelButtonDuo.text = cancelButton;
        dialogueDelegate = onClickOK;
        dialogueDelegateCancel = onClickCancel;
        dialogue_PanelOkCancel.gameObject.SetActive(true);
    }

    public void ShowMessageWithOk(string dialogMessage, string okButton, DialogueDelegate onClickOK)
    {
        textSingle.text = dialogMessage;
        okButtonSingle.text = okButton;
        dialogueDelegate = onClickOK;
        dialogue_PanelOk.gameObject.SetActive(true);
    }
    
    public void ShowMessageWithOk(string dialogMessage, string okButton)
    {
        textSingle.text = dialogMessage;
        okButtonSingle.text = okButton;
        dialogueDelegate = DefualtOkButton;
        dialogue_PanelOk.gameObject.SetActive(true);
    }
}