using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Bush : Enemy, IBushDie
{
    public ISpawner Spawner
    {
        get; set;
    }

    // Interface properties
    public WellBeingState wellBeing { get; set; }
    public ActionState actionState { get; set; }
    public TypeOfStatIncrease typeOfStatIncrease { get; set; }
    public Team Team { get { return Team.Enemy; } }
    public Vector2 Pos { get { return transform.position; } }
    public float Atk { get; set; }

    // Skeleton Instance
    public void Initialize(float HP, Vector3 pos, float Atk, float Def, float amountToStatToGive, TypeOfStatIncrease stat)
    {
        float multiplier = UnityEngine.Random.Range(-.1f, .3f);
        this.HP = HP;
        this.Atk = Atk;
        this.Def = Def;
        transform.position = pos;
        amountOfStatToGiveAponDeath = amountToStatToGive;
        typeOfStatIncrease = stat;

    }

    public override void Die()
    {
        int r = (int)UnityEngine.Random.Range(1f, 5f);
        switch (r)
        {
            case 1: GiveStatsToPlayerAponDeath(amountOfStatToGiveAponDeath, typeOfStatIncrease); break;
            default: break;
        }
        Spawner.Died(this);
    }

}
