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
        //StartCoroutine(LoadALevel("World"));
    }

    private IEnumerator LoadALevel(string sceneName)
    {
        async = SceneManager.LoadSceneAsync(sceneName);
        yield return async;

        yield return new WaitForSeconds(1f);
    }

    void Update()
    {
        if(async != null)
        {
            //Debug.Log(async.progress);
            progressBar.value = async.progress * 100f;
        }

    }

}


