using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    
    private AsyncOperation async = null; // When assigned, load is in progress.

    void Start()
    {
        StartCoroutine(LoadALevel(GameInfo.AreaToTeleportTo + ""));
        //StartCoroutine(LoadALevel("World"));
    }

    private IEnumerator LoadALevel(string sceneName)
    {
        yield return new WaitForSeconds(5f);
        //async = SceneManager.LoadSceneAsync(sceneName);
        SceneManager.LoadScene(sceneName);
        //yield return new WaitForSeconds(6f);
        yield return async;
    }
    

}


