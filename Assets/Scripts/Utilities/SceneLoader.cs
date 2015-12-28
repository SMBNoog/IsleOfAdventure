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
        if(GameInfo.AreaToTeleportTo != GameInfo.Area.World)
            yield return new WaitForSeconds(4f);
        async = SceneManager.LoadSceneAsync(sceneName);
        //SceneManager.LoadScene(sceneName);
        //yield return new WaitForSeconds(6f);
        yield return async;
    }
    

}


