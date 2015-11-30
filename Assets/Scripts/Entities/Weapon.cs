using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Weapon : MonoBehaviour, IWeapon
{
    // Create a delegate to retreive atk from whatever wants to tell us
    public delegate float GetBaseAtk();
    public GetBaseAtk GetBaseAtkDelegate; // Delegate to get the base attack from anyone that wants to tell it

    public Text debugAtk_Text;

    public float atk;
    public float def;
    public WeaponType type;

    public float Atk { get
        {
            float baseAtk = 0.0f;
            if (GetBaseAtkDelegate != null)
            {
                // ask the delegate to retreive the base atk from whoever has told us thier atk
                baseAtk = GetBaseAtkDelegate();
            }
            return atk + baseAtk;
        }
        set { atk = value; } }

    public float Def { get { return def; } set { def = value; } }

    public WeaponType WeaponType { get; set; }

    void Update()
    {
        debugAtk_Text.text = "Atk: " + Atk;
    }
}
