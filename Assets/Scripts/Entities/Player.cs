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

public class Player : Entity, IAttacker, IPlayerCurrentWeapon, IAttributesManager/*, IPlayerAccumulatedHP*/
{

    //[SerializeField]
    //private AttackDirectionState attackDirection;

    public List<Weapons> weaponList;

    public float HP_Cap = 1000;
    public float DEF_Cap = 0.50f;
    public Slider HP_Slider;
    [SerializeField]
    private float maxHP = 100f;

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
    public WeaponType currentWeapon;

    //public float a_HP { get; set; } // Aquired HP in this zone

    public float delayBetweenAutoAttacks = 0.4f;
    public float delayBetweenSpamAttacks = 0.1f;

    // only set in the world
    public Transform respawnInTheWorld;    
    public Transform tutorialSpawn;

    private bool canTakeDamage = true;
    //private bool canAttackMonsters = true;
    private Rigidbody2D rb2D;
    //private Vector3 lastPosition;

    public Button respawnButton;

    // Debug Text to see current stats
    public Text debugHP_Text;
    public Text debugMaxHP_Text;
    //public Text debugAtk_Text;
    public Text debugDef_Text;
    public Text debugWeapon_Text;

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

    void Awake()
    {
        wellBeing = WellBeingState.Alive;                
        
        if (GameInfo.currentArea == GameInfo.Area.TutorialArea)
        {
            // Player starting stats
            Atk = 10f;
            Def = 0.1f;
            Speed = 4f;
            HP = 100f;
            UpgradeWeapon(WeaponType.Wooden); // will add 100 more hp
            HP = maxHP;
            Debug.Log("Player Starting HP: " + HP + " | ATK: " + Atk + " | DEF: " + Def);
        }
        else
        {
            Debug.Log("Loading attributes");
            LoadAttributes();
            LoadWeapon(currentWeapon);            
        }
    }

    void Start () {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();

        if (GameInfo.currentArea == GameInfo.Area.TutorialArea)
            TeleportToTutorialArea(true);
        else if (GameInfo.currentArea == GameInfo.Area.World)
            TeleportToTutorialArea(false);

        
        //anim.SetFloat("Horizontal Input", 0f);
        //attackDirection = AttackDirectionState.right;
    }

    public void TeleportToTutorialArea(bool tutorial)
    {
        rb2D.transform.position = tutorial? tutorialSpawn.position:respawnInTheWorld.position; 
    }

    #region Animator state
    void Update()
    {
        //Debug.Log("Action State : " + actionState);
        if (wellBeing == WellBeingState.Alive)
        {
            if(HP_Slider.maxValue != maxHP)
                HP_Slider.maxValue = maxHP;

            // Regen when Idle
            if (actionState == ActionState.Idle && HP < maxHP)
                HP += maxHP * .005f;

            debugHP_Text.text = "HP: " + HP;
            debugMaxHP_Text.text = "MaxHP: " + maxHP;
            //debugAtk_Text.text = "Atk: " + Atk;
            debugDef_Text.text = "Def: " + Def;
            debugWeapon_Text.text = currentWeapon+"";

            //AnimatorStateInfo stateName = anim.GetCurrentAnimatorStateInfo(0);
            
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

            //// Auto attack
            //if (CnInputManager.GetButton("Swing"))
            //{

            //    //StartCoroutine(DelayAttack(weaponNEW)); 

            //    foreach (Weapons w in weaponList)
            //    {
            //        if (w.weaponType == currentWeapon)
            //        {
            //            // power up weapon move here
            //        }
            //    }
            //}

            //if (CnInputManager.GetButtonUp("Swing"))
            //{

            //    weaponNEW.gameObject.SetActive(false);
            //}

            //lastPosition = transform.position;
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

            float horizontalR = CnInputManager.GetAxis("HorizontalRight");
            float verticalR = CnInputManager.GetAxis("VerticalRight");

            rb2D.velocity = new Vector2(horizontal * Speed, vertical * Speed);

            foreach (Weapons w in weaponList)
            {
                if (w.weaponType == currentWeapon)
                {
                    if (verticalR == 0f && horizontalR == 0f)
                        w.weapon.GetComponentInChildren<Collider2D>().enabled = false;
                    else
                        w.weapon.GetComponentInChildren<Collider2D>().enabled = true;
                    
                    if (verticalR < -0.2f)
                        w.weapon.GetComponentInChildren<SpriteRenderer>().sortingOrder = 101;
                    else
                        w.weapon.GetComponentInChildren<SpriteRenderer>().sortingOrder = 80;
                    


                    w.weapon.transform.localPosition = new Vector2(horizontalR, verticalR - .4f) * .07f;
                    float myAngle = Mathf.Atan2(verticalR, horizontalR) * Mathf.Rad2Deg;
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
    
    // Save attributes before zoning
    public void SaveAttributes()
    {
        GameInfo.PlayerMaxHP = maxHP;
        GameInfo.PlayerAtk = Atk;
        GameInfo.PlayerDef = Def;
        GameInfo.PlayerSpeed = Speed;
        GameInfo.CurrentWeapon = currentWeapon;

        //PlayerPrefs.SetFloat("HP", HP);
        //PlayerPrefs.SetFloat("MaxHP", maxHP);
        //PlayerPrefs.SetFloat("Atk", Atk);
        //PlayerPrefs.SetFloat("Def", Def);
        //PlayerPrefs.SetFloat("Speed", Speed);
        //PlayerPrefs.SetInt("WeaponType", (int)currentWeapon);
        
    }

    // Load attributes after zoning
    public void LoadAttributes()
    {
        //if (PlayerPrefs.HasKey("HP"))
        //{
            maxHP = GameInfo.PlayerMaxHP;
            Atk = GameInfo.PlayerAtk;
            Def = GameInfo.PlayerDef;
            Speed = GameInfo.PlayerSpeed;
            currentWeapon = GameInfo.CurrentWeapon;

            //HP = PlayerPrefs.GetFloat("HP") + a_HP;
            //maxHP = PlayerPrefs.GetFloat("MaxHP");
            //Atk = PlayerPrefs.GetFloat("Atk");
            //Def = PlayerPrefs.GetFloat("Def");
            //Speed = PlayerPrefs.GetFloat("Speed");
            //currentWeapon = (WeaponType)PlayerPrefs.GetInt("WeaponType");            
        //}
        //else
        //    Debug.Log("Key's have not been Set.");
    }

    // Killed enemy, increase stat
    public void IncreaseStat(float amount, TypeOfStatIncrease stat)
    {
        if(wellBeing == WellBeingState.Alive)
        {
            switch (stat)
            {
                case TypeOfStatIncrease.HP:
                    maxHP += amount;
                    //a_HP += amount;
                    //Debug.Log("Max HP is now = " + maxCurrentHP); 
                    break;
                case TypeOfStatIncrease.ATK: Atk += amount;
                    //Debug.Log("Max Atk is now = " + Atk);
                    break;
                case TypeOfStatIncrease.DEF: Def += amount;
                    //Debug.Log("Max Def is now = " + Def);
                    break;
            }
        }
    }

    public void LoadWeapon(WeaponType type)
    {
        currentWeapon = type;
        foreach (Weapons w in weaponList)
        {
            if (type != w.weaponType)
                w.weapon.gameObject.SetActive(false);
            else
                w.weapon.gameObject.SetActive(true);
        }
    }

    public void UpgradeWeapon(WeaponType type)
    {
        currentWeapon = type;
        switch(type)
        {   // When upgrading weapon boost max HP
            case WeaponType.Wooden: maxHP += 100f; break;
            case WeaponType.Bronze: maxHP += 1000f; break;
            case WeaponType.Silver: maxHP += 10000f; break;
            case WeaponType.Gold: maxHP += 2500f; break;
            case WeaponType.Epic: maxHP += 7500f; break;
        }
        LoadWeapon(type);
    }

    public void DebugChangeToBronze()
    {
        UpgradeWeapon(WeaponType.Bronze);
    }

    public void DebugChangeToSilver()
    {
        UpgradeWeapon(WeaponType.Silver);
    }

    public override void Die()
    {
        if(wellBeing != WellBeingState.Dead)
        {
            wellBeing = WellBeingState.Dead;
            rb2D.isKinematic = true; // freeze player position
            SaveAttributes(); // save current stats
            StartCoroutine(DelayForAnimationThenRespawn()); //delay for tombstone

            
        }        
    }

    // Wait for Death animation
    IEnumerator DelayForAnimationThenRespawn()
    {
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(3f);
        respawnButton.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        //RespawnPlayer();  
    }

    public void RespawnPlayer()
    {
        Debug.Log("Button hit");
        LoadAttributes();
        HP = maxHP;
        foreach (Weapons w in weaponList)
        {
            if(currentWeapon == w.weaponType)
            {
                Debug.Log("found weapon");

                LoadWeapon(w.weaponType);         
                if(GameInfo.currentArea == GameInfo.Area.World)       
                    rb2D.transform.position = respawnInTheWorld.position;
                else
                {
                    Debug.Log("Goingt o the world!");
                    Application.LoadLevel("SceneLoader");
                }
                Time.timeScale = 1.0f;
                wellBeing = WellBeingState.Alive;
                rb2D.isKinematic = false;
                anim.gameObject.SetActive(true);
                anim.SetTrigger("Respawn");
            }
        }
        respawnButton.gameObject.SetActive(false);
    }
}
