using UnityEngine;
using System.Collections;

public class SecondDoorInCastle : MonoBehaviour {

    public GameObject door;

    bool startCheckingForSkeletons = false;

    void Start()
    {
        StartCoroutine(DelayThenCheck());
    }

    void Update()
    {
        if (Skeleton.numberOfCastleSkeletons == 30 && startCheckingForSkeletons) // first room cleared
            Destroy(gameObject);
    }

    IEnumerator DelayThenCheck()
    {
        yield return new WaitForSeconds(10f);
        startCheckingForSkeletons = true;
    }
}
