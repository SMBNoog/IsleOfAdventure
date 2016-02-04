using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour {

    public Image debugPanel;

    public void ShowHidePanel()
    {
        if(debugPanel.isActiveAndEnabled)
            debugPanel.gameObject.SetActive(false);
        else
            debugPanel.gameObject.SetActive(true);

    }
}
