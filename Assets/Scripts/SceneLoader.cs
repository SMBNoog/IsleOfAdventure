using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

    public Slider progressBar;

    private AsyncOperation async = null; // When assigned, load is in progress.

    void Start()
    {
        //StartCoroutine(LoadALevel(GameInfo.sceneToLoad));
        StartCoroutine(LoadALevel("TheWorld"));
    }

    private IEnumerator LoadALevel(string levelName)
    {
        async = Application.LoadLevelAsync(levelName);
        yield return async;

        yield return new WaitForSeconds(2f);
    }

    void Update()
    {
        Debug.Log(async.progress);
        progressBar.value = async.progress * 100f;
    }

}


