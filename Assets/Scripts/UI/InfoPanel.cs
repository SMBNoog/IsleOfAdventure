using UnityEngine;
using System.Collections;

public class InfoPanel : MonoBehaviour {

	public void OkButton()
    {
        Time.timeScale = 1.0f;

        Destroy(gameObject);
    }
}
