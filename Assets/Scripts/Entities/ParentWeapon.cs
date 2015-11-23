using UnityEngine;
using System.Collections;
using System;

public class ParentWeapon : MonoBehaviour, IWeapon {

    public float Atk { get { return gameObject.transform.GetComponentInParent<Weapon>().Atk; } set { } }
    
    public float Def { get { return gameObject.transform.GetComponentInParent<Weapon>().Def; } set { } }

    public WeaponType WeaponType { get; set; }
}
