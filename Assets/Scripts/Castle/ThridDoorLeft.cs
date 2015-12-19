using UnityEngine;
using System.Collections;

public class ThridDoorLeft : MonoBehaviour {

    bool startCheckingForSkeletons = false;

    void Start()
    {
        StartCoroutine(DelayThenCheck());
    }

    void Update()
    {
        if (Skeleton.numberOfCastleSkeletons == 15 && startCheckingForSkeletons) // first room cleared
            Destroy(gameObject);
    }

    IEnumerator DelayThenCheck()
    {
        yield return new WaitForSeconds(5f);
        startCheckingForSkeletons = true;
    }
}
