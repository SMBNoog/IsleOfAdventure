using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//public enum TypeOfControl { VirtualJoyStick, DPad }

public class InputType : MonoBehaviour {

    //public Image virtualJoyStick;
    //public Image dPad;
    public Button retry;

    public static InputType Instance;

    void Awake()
    {
        Instance = this;
    }

    //TypeOfControl type;

    //void Start()
    //{
    //    type = TypeOfControl.DPad;
    //}

    //public void SwitchControl()
    //{
    //    if(type == TypeOfControl.VirtualJoyStick)
    //    {
    //        dPad.gameObject.SetActive(true);            
    //        virtualJoyStick.gameObject.SetActive(false);
    //        type = TypeOfControl.DPad; 
    //    }
    //    else if (type == TypeOfControl.DPad)
    //    {
    //        dPad.gameObject.SetActive(false);
    //        type = TypeOfControl.VirtualJoyStick;
    //        virtualJoyStick.gameObject.SetActive(true);
    //    }
    //}

    public void Retry()
    {
        Time.timeScale = 1.0f;
        Application.LoadLevel(Application.loadedLevel);
    }
}
