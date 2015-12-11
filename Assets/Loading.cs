using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Loading : MonoBehaviour {

    public Text text;

	
	void Start()
    {
        StartCoroutine(LoadingL());
    }

    IEnumerator LoadingL()
    {
        text.text = "Loading.  ";
        yield return new WaitForSeconds(1f);
        text.text = "Loading.. ";
        yield return new WaitForSeconds(1f);
        text.text = "Loading...";
        yield return new WaitForSeconds(1f);
        StartCoroutine(LoadingL());
        yield return null;
    }
}
