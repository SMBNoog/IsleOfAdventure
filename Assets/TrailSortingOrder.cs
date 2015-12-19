using UnityEngine;
using System.Collections;

public class TrailSortingOrder : MonoBehaviour {

    TrailRenderer tr;

    void Start()
    {
        tr = GetComponent<TrailRenderer>();

        tr.sortingLayerName = "Default";
        tr.sortingOrder = 50;
    }
}
