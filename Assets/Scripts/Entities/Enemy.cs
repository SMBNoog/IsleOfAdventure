using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum EnemyFacing { left, right, up, down }

public class Enemy : Entity {
    
    protected float amountOfStatToGiveAponDeath { get; set; }
    protected Vector3 targetPlayer { get; set; }
    protected Rigidbody2D rb2D;
    protected Vector2 spawnPosition;
    protected float HP_Start;
    protected float Atk_Start;
    protected float Def_Start;
    protected TypeOfStatIncrease Stat_Start;
    protected float Amount_Start;
    protected TypeOfEnemy Type_Start;

    protected bool switching;

    //public static List<DeadEnemy> deadEnemyList;


    // Apon death give stats to player
    protected void GiveStatsToPlayerAponDeath(float amount, TypeOfStatIncrease stat)
    {
        Debug.Log("Type: " + stat + "  Amount:  " + amount);
        Player player = FindObjectOfType<Player>(); 
        player.IncreaseStat(amount, stat);
    }

    // When the Players Weapon hits this enemy, take damage and engage
    protected void OnCollisionEnter2D(Collision2D other)
    {   

        IWeapon weapon = Interface.Find<IWeapon>(other.collider.gameObject);
        if (weapon != null)
        {
            float wAtk = weapon.Atk;  //weapon + base          
            float dmg = wAtk - (wAtk * Def);
            //Debug.Log("Weapon ATK = " + wAtk);
            if(HP > 0)
                DamagedBy(dmg);
            //actionState = ActionState.EngagedInBattle;
            SoundManager.Instance.Play(TypeOfClip.SwordHit);            
        }
    }   

    protected void OnTriggerEnter2D(Collider2D other)
    {

    }

    public override void Die()
    {
        Debug.Log("The Child hasn't been setup to die!");
    }
}
