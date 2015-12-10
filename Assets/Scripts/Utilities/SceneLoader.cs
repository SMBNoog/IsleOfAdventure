using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public Slider progressBar;

    private AsyncOperation async = null; // When assigned, load is in progress.

    void Start()
    {
        StartCoroutine(LoadALevel(GameInfo.AreaToTeleportTo + ""));
        progressBar.value = 0f;
        //StartCoroutine(LoadALevel("World"));
    }

    private IEnumerator LoadALevel(string sceneName)
    {
        yield return new WaitForSeconds(6f);
        async = SceneManager.LoadSceneAsync(sceneName);
        //yield return new WaitForSeconds(6f);
        yield return async;
    }

    void Update()
    {
        if(async != null)
        {
            //Debug.Log(async.progress);
            progressBar.value = async.progress * 100f;
        }
        else
        {
            progressBar.value += .015f;
            return;
        }

    }

}


