using UnityEngine;
using System.Collections;
using System;

//[Serializable]
//public class DeadEnemy
//{
//    public GameObject prefab;
//    public Vector2 pos;
//    public float HP;
//    public float Atk;
//    public float Def;
//    public float AmountOfStatToGive;
//    public TypeOfEnemy TypeEnemy;
//    public TypeOfStatIncrease TypeStat;
//}

public abstract class Entity : MonoBehaviour {

    //public abstract WellBeingState wellBeing { get; set; }
    //public abstract ActionState actionState { get; set; }
    //public abstract Team Team { get; }
    //public abstract TypeOfStatIncrease typeOfStatIncrease { get; set; }
    //public abstract Vector2 Pos { get; }
    //public abstract float Atk { get; set; }

    protected float HP;
    protected float Def;
    protected float Speed;

    protected Animator anim;

    protected virtual void DamagedBy(float dmg)
    {
        //    Debug.Log(this + ":  Current HP: " + HP);
        //    Debug.Log(this + ":  Damage taken: " + dmg);
        HP = (HP - dmg) <= 0 ? 0 : HP - dmg;
        if (HP == 0)
        {
            Die();
            Debug.Log("calling die()");
        }
                              
    }

    public abstract void Die();
}
