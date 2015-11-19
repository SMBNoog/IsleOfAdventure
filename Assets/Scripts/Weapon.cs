using UnityEngine;
using System.Collections;
using System;

public class Weapon : MonoBehaviour, IWeapon
{
    public delegate float GetBaseAtk();
    public GetBaseAtk GetBaseAtkDelegate;

    public float atk;
    public float def;

    public float Atk { get
        {
            float baseAtk = 0.0f;
            if (GetBaseAtkDelegate != null)
            {
                baseAtk = GetBaseAtkDelegate();
            }
            return atk + baseAtk;
        }
        set { atk = value; } }
    public float Def { get { return def; } set { def = value; } }
}
