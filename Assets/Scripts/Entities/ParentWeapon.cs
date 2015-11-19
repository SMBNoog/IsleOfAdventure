using UnityEngine;
using System.Collections;

public class ParentWeapon : MonoBehaviour, IWeapon {

    public float Atk { get { return gameObject.transform.GetComponentInParent<Weapon>().Atk; } set { } }
    
    public float Def { get { return gameObject.transform.GetComponentInParent<Weapon>().Def; } set { } }

}
