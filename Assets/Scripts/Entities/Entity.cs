using UnityEngine;
using System.Collections;
using System;


public abstract class Entity : MonoBehaviour {

    protected float HP;
    protected float Def;
    protected float Speed;

    protected Animator anim;
        
    protected virtual void DamagedBy(float dmg)
    {
        HP = (HP - dmg) <= 0 ? 0 : HP - dmg;
        if (HP == 0)
        {
            Die();
        }
                              
    }

    public abstract void Die();
}
