using UnityEngine;
using System.Collections;
using System;

public enum EnemyFacing { left, right, up, down }

public class Enemy : Entity {
    
    protected float amountOfStatToGiveAponDeath { get; set; }
    protected Vector3 targetPlayer { get; set; }
    protected Rigidbody2D rb2D;

    // Apon death give stats to player
    protected void GiveStatsToPlayerAponDeath(float amount, TypeOfStatIncrease stat)
    {
        Player player = FindObjectOfType<Player>();  // fix this
        player.IncreaseStat(amount, stat);
    }

    // When the Players Weapon hits this enemy, take damage and engage
    protected void OnCollisionEnter2D(Collision2D other)
    {
        IWeapon weapon = Interface.Find<IWeapon>(other.collider.gameObject);
        if (weapon != null)
        {
            float wAtk = weapon.Atk;            
            float pAtk = other.gameObject.GetComponentInParent<Player>().Atk; //hack
            float dmg = (pAtk + wAtk) - ((pAtk + wAtk) * Def);
            Debug.Log("Weapon ATK = " + wAtk + "\nPlayer ATK = " + pAtk);
            DamagedBy(dmg);
            //actionState = ActionState.EngagedInBattle;
            SoundManager.Instance.Play(TypeOfClip.SwordHit);            
        }
    }

    public override void Die()
    {
        Debug.Log("The Child hasn't been setup to die!");
    }
}
