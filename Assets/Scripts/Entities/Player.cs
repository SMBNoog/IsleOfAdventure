using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using CnControls;

public enum AttackDirectionState { up, down, left, right }
public enum WeaponType { wooden, bronze, brass, silver, gold, epic}

[System.Serializable]
public class AttackDirection
{
    public GameObject obj;
    public AttackDirectionState state;
}

[System.Serializable]
public class Weapons
{
    public string name;
    public GameObject weapon;
    public WeaponType weaponType;
    public List<AttackDirection> attackSpriteArray;
}

public class Player : Entity, IAttacker {

    [SerializeField]
    private AttackDirectionState attackDirection;

    public List<Weapons> weaponList;

    public float HP_Cap = 1000;
    public float DEF_Cap = 0.25f;
    public Slider HP_Slider;
    [SerializeField]
    private float maxHP_Slider = 100f;

    public WellBeingState wellBeing { get; set; }
    public ActionState actionState { get; set; }
    public Team Team { get { return Team.Player; } }
    public Vector2 Pos { get { return transform.position; } }
    public float Atk { get; set; }
    public TypeOfStatIncrease typeOfStatIncrease { get; set; } // Doesn't use
    
    public float delayBetweenAutoAttacks = 0.4f;
    public float delayBetweenSpamAttacks = 0.1f;

    private bool canTakeDamage = true;
    private bool canAttackMonsters = true;
    private Rigidbody2D rb2D;
    private Vector3 lastPosition;

    private WeaponType currentWeapon;

    

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

        //ChangeWeapon(1, 1);
        currentWeapon = WeaponType.wooden;

        // Player starting stats
        HP = maxHP_Slider; // set in inspecter
        Atk = 8f;
        Def = 0.1f;
        Speed = 3.5f;

        attackDirection = AttackDirectionState.right;
        //Debug.Log("Player ATK: " + Atk + " | DEF: " + Def);
    }

    #region Attacking Logic - Update(), IEnum Attack(), IEnum DelayAttack()
    void Update()
    {
        if (wellBeing == WellBeingState.Alive)
        {
            if(HP_Slider.maxValue != maxHP_Slider)
                HP_Slider.maxValue = maxHP_Slider;

            // Regen when Idle
            if (actionState == ActionState.Idle && HP < maxHP_Slider)
                HP += 0.1f;

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk_Right") || anim.GetCurrentAnimatorStateInfo(0).IsName("Idle_Right"))
                attackDirection = AttackDirectionState.right;
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk_Left") || anim.GetCurrentAnimatorStateInfo(0).IsName("Idle_Left"))
                attackDirection = AttackDirectionState.left;
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk_Up") || anim.GetCurrentAnimatorStateInfo(0).IsName("Idle_Up"))
                attackDirection = AttackDirectionState.up;
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk_Down") || anim.GetCurrentAnimatorStateInfo(0).IsName("Idle_Down"))
                attackDirection = AttackDirectionState.down;

            // Spam attack
            if (CnInputManager.GetButtonDown("Swing"))
            {
                foreach(Weapons w in weaponList)
                {
                    if(w.weaponType == currentWeapon)
                    {
                        foreach (AttackDirection att in w.attackSpriteArray)
                        {
                            if (att.state == attackDirection)
                                StartCoroutine(Attack(att.obj));
                        }
                    }
                }

            }

            // Auto attack
            if (CnInputManager.GetButton("Swing"))
            {
                foreach(Weapons w in weaponList)
                {
                    if (w.weaponType == currentWeapon)
                    {
                        foreach (AttackDirection atkDir in w.attackSpriteArray)
                        {
                            if (atkDir.state == attackDirection && canAttackMonsters)
                            {
                                canAttackMonsters = false;
                                StartCoroutine(Attack(atkDir.obj));
                                StartCoroutine(DelayAttack());
                            }
                        }
                    }
                }
            }

            lastPosition = myT.position;
        } 

        if (HP_Slider.value != HP)
            HP_Slider.value = HP;
    }// end Update

    IEnumerator Attack(GameObject obj)
    {
        if (wellBeing == WellBeingState.Alive)
        {
            obj.SetActive(true); // Turn on sword in selected direction
            actionState = ActionState.EngagedInBattle;
            SoundManager.Instance.Play(TypeOfClip.SwordMiss);
            yield return new WaitForSeconds(delayBetweenSpamAttacks);
            obj.SetActive(false);
        }
    }

    IEnumerator DelayAttack()
    {
        if(wellBeing == WellBeingState.Alive)
        {
            yield return new WaitForSeconds(delayBetweenAutoAttacks);
            canAttackMonsters = true;
        }
    }
    #endregion

    void FixedUpdate()
    {
        // Movement of Player
        if (wellBeing == WellBeingState.Alive)
        {
            float horizontal = CnInputManager.GetAxis("Horizontal");
            float vertical = CnInputManager.GetAxis("Vertical");
            anim.SetFloat("Horizontal Input", horizontal);
            anim.SetFloat("Vertical Input", vertical);

            rb2D.velocity = new Vector2(horizontal * Speed, vertical * Speed);

            if (rb2D.velocity == Vector2.zero && actionState != ActionState.EngagedInBattle)
            {
                //Debug.Log("Changing state to Idle");
                actionState = ActionState.Idle;
            }            
            else
            {
                actionState = ActionState.Walking;
                anim.SetBool("Idle", false);
            }

            StartCoroutine(CheckIfIdle(horizontal, vertical));
        }
    }

    //// Dynamically turn on and off depending on the scene.
    //void OnTriggerStay(Collider2D other)
    //{
    //    if(other.collider == null)
    //    {

    //    }
    //}

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
    void OnCollisionEnter2D(Collision2D other)
    {
        if(wellBeing == WellBeingState.Alive)
        {
            IWeapon weapon = Interface.Find<IWeapon>(other.collider.gameObject);
            IAttacker attacker = Interface.Find<IAttacker>(other.gameObject);
            if (attacker != null && weapon != null)  // If it's an attacker
            {
                float dmg = attacker.Atk - (attacker.Atk * Def);
                actionState = ActionState.EngagedInBattle;
                if (canTakeDamage)
                {
                    StartCoroutine(DelayDamageReceived(dmg));
                    canTakeDamage = false;
                }
            }
        }
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
