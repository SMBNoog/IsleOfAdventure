using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using CnControls;

//public enum AttackDirectionState { up, down, left, right }

//[System.Serializable]
//public class AttackDirection
//{
//    public GameObject obj;
//    public AttackDirectionState state;
//}

[System.Serializable]
public class Weapons
{
    public string name;
    public GameObject weapon;
    public WeaponType weaponType;
    //public List<AttackDirection> attackSpriteArray;
}

public class Player : Entity, IAttacker, IPlayerCurrentWeapon {

    //[SerializeField]
    //private AttackDirectionState attackDirection;

    public List<Weapons> weaponList;

    public float HP_Cap = 1000;
    public float DEF_Cap = 0.50f;
    public Slider HP_Slider;
    [SerializeField]
    private float maxHP_Slider = 100f;

    public WellBeingState wellBeing { get; set; }
    public ActionState actionState { get; set; }
    public Team Team { get { return Team.Player; } }
    public Vector2 Pos { get { return transform.position; } }
    public float Atk { get; set; }
    public TypeOfStatIncrease typeOfStatIncrease { get; set; } // Doesn't use

    public WeaponType weaponType
    {
        get { return currentWeapon; }
    }

    public float delayBetweenAutoAttacks = 0.4f;
    public float delayBetweenSpamAttacks = 0.1f;

    private bool canTakeDamage = true;
    private bool canAttackMonsters = true;
    private Rigidbody2D rb2D;
    private Vector3 lastPosition;

    public WeaponType currentWeapon;

    

    // When the Player GameObject is enabled.
    public void OnEnable()
    {
        // Search the children to find Weapons.
        foreach (var weapon in gameObject.GetComponentsInChildren<Weapon>())
        {
            // Add a blank method as shortcut. Instead of typing >>> private float getAtk() { return Atk;  }
            weapon.GetBaseAtkDelegate += () => { return Atk; };
        }
    }

    void Start () {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        anim.SetFloat("Horizontal Input", 0f);
        
        myT = transform;

        wellBeing = WellBeingState.Alive;
        
        // Player starting stats
        Atk = 8f;
        Def = 0.1f;
        Speed = 4f;

        ChangeWeapon(WeaponType.Flame);

        HP = maxHP_Slider; // set in inspecter

        //attackDirection = AttackDirectionState.right;
        //Debug.Log("Player ATK: " + Atk + " | DEF: " + Def);
    }

    #region Animator state
    void Update()
    {
        //Debug.Log("Action State : " + actionState);
        if (wellBeing == WellBeingState.Alive)
        {
            if(HP_Slider.maxValue != maxHP_Slider)
                HP_Slider.maxValue = maxHP_Slider;

            // Regen when Idle
            if (actionState == ActionState.Idle && HP < maxHP_Slider)
                HP += maxHP_Slider * .001f;

            AnimatorStateInfo stateName = anim.GetCurrentAnimatorStateInfo(0);
            
            //if (stateName.IsName("Walk_Right") || stateName.IsName("Idle_Right"))
            //    attackDirection = AttackDirectionState.right;
            //else if (stateName.IsName("Walk_Left") || stateName.IsName("Idle_Left"))
            //    attackDirection = AttackDirectionState.left;
            //else if (stateName.IsName("Walk_Up") || stateName.IsName("Idle_Up"))
            //    attackDirection = AttackDirectionState.up;
            //else if (stateName.IsName("Walk_Down") || stateName.IsName("Idle_Down"))
            //    attackDirection = AttackDirectionState.down;

            //// Spam attack
            //if (CnInputManager.GetButtonDown("Swing"))
            //{
            //    StartCoroutine(AttackWith(weaponNEW));
            //    //foreach (Weapons w in weaponList)
            //    //{
            //    //    if(w.weaponType == currentWeapon)
            //    //    {
            //    //        foreach (AttackDirection att in w.attackSpriteArray)
            //    //        {
            //    //            if (att.state == attackDirection)
            //    //                StartCoroutine(Attack(att.obj));
            //    //        }
            //    //    }
            //    //}
                
            //}

            // Auto attack
            if (CnInputManager.GetButton("Swing"))
            {

                //StartCoroutine(DelayAttack(weaponNEW)); 

                foreach (Weapons w in weaponList)
                {
                    if (w.weaponType == currentWeapon)
                    {
                        // power up weapon move here
                    }
                }
            }

            //if (CnInputManager.GetButtonUp("Swing"))
            //{

            //    weaponNEW.gameObject.SetActive(false);
            //}

            lastPosition = myT.position;
        } 

        if (HP_Slider.value != HP)
            HP_Slider.value = HP;
    }// end Update

    //IEnumerator AttackWith(GameObject weapon)
    //{
    //    if (wellBeing == WellBeingState.Alive)
    //    {
    //        weapon.SetActive(true); // Turn on sword in selected direction
    //        actionState = ActionState.EngagedInBattle;
    //        SoundManager.Instance.Play(TypeOfClip.SwordMiss);
    //        yield return new WaitForSeconds(delayBetweenSpamAttacks);
    //        weapon.SetActive(false);
    //    }
    //}

    //IEnumerator DelayAttack(/*AttackDirection atkDir*/GameObject weapon)
    //{
    //    if(wellBeing == WellBeingState.Alive)
    //    {
    //        yield return new WaitForSeconds(delayBetweenAutoAttacks);
    //        //canAttackMonsters = true;

    //        // power up then fire special weapon
    //        StartCoroutine(AttackWith(weapon));
    //    }
    //}
    #endregion

    void FixedUpdate()
    {
        if (wellBeing == WellBeingState.Alive)
        {
            float horizontal = CnInputManager.GetAxis("Horizontal");
            float vertical = CnInputManager.GetAxis("Vertical");
            anim.SetFloat("Horizontal Input", horizontal);
            anim.SetFloat("Vertical Input", vertical);

            rb2D.velocity = new Vector2(horizontal * Speed, vertical * Speed);

            foreach (Weapons w in weaponList)
            {
                if (w.weaponType == currentWeapon)
                {
                    if (vertical < 0.5f && vertical != 0)
                        w.weapon.GetComponentInChildren<SpriteRenderer>().sortingOrder = 101;
                    else
                        w.weapon.GetComponentInChildren<SpriteRenderer>().sortingOrder = 80;

                    w.weapon.transform.localPosition = new Vector2(horizontal, vertical - .4f) * .07f;
                    float myAngle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;
                    w.weapon.transform.eulerAngles = new Vector3(0f, 0f, myAngle);
                }
            }

            if (rb2D.velocity == Vector2.zero && actionState != ActionState.EngagedInBattle)
            {
                //Debug.Log("Changing state to Idle");
                actionState = ActionState.Idle;
            }            
            else
            {
                anim.SetBool("Idle", false);
                actionState = ActionState.RunningAway;
            }

            StartCoroutine(CheckIfIdle(horizontal, vertical));
        }
    }
    
    IEnumerator CheckIfIdle(float horizontal, float vertical)
    {
        if (horizontal == 0f && vertical == 0f)
        {
            yield return new WaitForSeconds(0.3f);
            if (horizontal == 0f && vertical == 0f && actionState == ActionState.Idle)
            {
                //Debug.Log("Changing animation state to Idle");
                anim.SetBool("Idle", true);
            }
        }
    }

    // Enemy is hitting player
    void OnCollisionStay2D(Collision2D other)
    {
        if(wellBeing == WellBeingState.Alive)
        {
            IAttacker attacker = Interface.Find<IAttacker>(other.gameObject);
            if (attacker != null)  // If it's an attacker
            {
                float dmg = attacker.Atk - (attacker.Atk * Def);
                actionState = ActionState.EngagedInBattle;
                if (canTakeDamage)
                {
                    actionState = ActionState.EngagedInBattle;
                    StartCoroutine(DelayToChangeToIdle());
                    StartCoroutine(DelayDamageReceived(dmg));
                    canTakeDamage = false;
                }
            }
        }
    }

    IEnumerator DelayToChangeToIdle()
    {
        yield return new WaitForSeconds(3f);
        actionState = ActionState.RunningAway;
    }

    IEnumerator DelayDamageReceived(float dmg)
    {
        DamagedBy(dmg);
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;
    }
    
    public void SaveAttributes()
    {
        PlayerPrefs.SetFloat("HP", HP);
        PlayerPrefs.SetFloat("Atk", Atk);
        PlayerPrefs.SetFloat("Def", Def);
        PlayerPrefs.SetFloat("Speed", Speed);
        // TODO: include weapon
    }

    public void LoadAttributes()
    {
        if (PlayerPrefs.HasKey("HP") && PlayerPrefs.HasKey("Atk") && PlayerPrefs.HasKey("Def") && PlayerPrefs.HasKey("Speed"))
        {
            PlayerPrefs.GetFloat("HP");
            PlayerPrefs.GetFloat("Atk");
            PlayerPrefs.GetFloat("Def");
            PlayerPrefs.GetFloat("Speed");
            // Include weapon
        }
        else
            Debug.Log("Key's have not been Set.");
    }

    // Killed enemy, increase stat
    public void IncreaseStat(float amount, TypeOfStatIncrease stat)
    {
        if(wellBeing == WellBeingState.Alive)
        {
            switch (stat)
            {
                case TypeOfStatIncrease.HP: maxHP_Slider += amount;
                    Debug.Log("Max HP is now = " + maxHP_Slider); break;
                case TypeOfStatIncrease.ATK: Atk += amount;
                    Debug.Log("Max Atk is now = " + Atk); break;
                case TypeOfStatIncrease.DEF: Def += amount;
                    Debug.Log("Max Def is now = " + Def); break;
            }
        }
    }

    public void ChangeWeapon(WeaponType type)
    {
        currentWeapon = type;
        if (type == WeaponType.Flame)
        {
            maxHP_Slider += 1000f;
            foreach(Weapons w in weaponList)
            {
                if (type != w.weaponType)
                    w.weapon.gameObject.SetActive(false);
                else
                    w.weapon.SetActive(true);
            }
            //Atk += 100f;
        }
        else if (type == WeaponType.Silver)
        {
            maxHP_Slider += 10000f;
            foreach (Weapons w in weaponList)
            {
                if (type != w.weaponType)
                    w.weapon.gameObject.SetActive(false);
                else
                    w.weapon.SetActive(true);
            }
            //Atk += 1000f;
        }
        else if (type == WeaponType.Gold)
        {
            maxHP_Slider += 2500f;
            foreach (Weapons w in weaponList)
            {
                if (type != w.weaponType)
                    w.weapon.gameObject.SetActive(false);
                else
                    w.weapon.SetActive(true);
            }
            //Atk += 250f;
        }
        else if (type == WeaponType.Epic)
        {
            maxHP_Slider += 7500f;
            foreach (Weapons w in weaponList)
            {
                if (type != w.weaponType)
                    w.weapon.gameObject.SetActive(false);
                else
                    w.weapon.SetActive(true);
            }
            //Atk += 2000f;
        }
    }

    public override void Die()
    {
        anim.SetTrigger("Death");
        wellBeing = WellBeingState.Dead;
        rb2D.isKinematic = true;
        StartCoroutine(DelayThenEndGame());
    }

    // Wait for Death animation
    IEnumerator DelayThenEndGame()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0.0f;
        InputType.Instance.retry.gameObject.SetActive(true);   
    }
}
