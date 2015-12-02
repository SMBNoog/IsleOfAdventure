using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using CnControls;

[System.Serializable]
public class Weapons
{
    public string name;
    public GameObject weapon;
    public WeaponType weaponType;
}

public class Player : Entity, IAttacker, IPlayerCurrentWeapon, IAttributesManager
{
    public List<Weapons> weaponList;  // Set list of weapons in the inspector

    // IPlayerCurrentWeapon interface
    public WeaponType weaponType { get { return currentWeapon; } }    
    private WeaponType currentWeapon;

    public float weaponYoffset = 0.6f;
    public float weaponXoffSet = 0.6f;
    public float weaponOffset = 0.07f;

    // Player Starting stats
    public float HP_Cap = 1000000;
    public float DEF_Cap = 0.50f;
    public float startingHP = 100f;
    public float regenHP_Multiplier = 0.005f;
    public float startingAtk = 10f;
    public float startingDef = 0.1f;
    public float startingSpeed = 4f;
    
    public Slider HP_Slider;
    
    // IAttacker interface
    public WellBeingState wellBeing { get; set; }
    public ActionState actionState { get; set; }
    public Team Team { get { return Team.Player; } }
    public Vector2 Pos { get { return transform.position; } }
    public float Atk { get; set; }
    public TypeOfStatIncrease typeOfStatIncrease { get; set; } // Doesn't use
        
    public Button respawnButton; ///////////// REMOVE THIS WHEN MAIN MENU IS CREATED /////////////

    // Debug Text to see current status, ATK set in Weapon class
    public Text debugHP_Text;
    public Text debugMaxHP_Text;
    public Text debugDef_Text;
    public Text debugWeapon_Text;

    [SerializeField]
    private float maxHP = 100f;     // Maximum HP updated when increased

    // Taking damage 
    private bool canTakeDamage = true;
    public float delayToTakeDamageAgain = 1.0f;

    private Rigidbody2D rb2D;

    private bool savingAttributes = false;
    
    public void OnEnable()
    {
        // Search the children to find Weapons.
        foreach (var weapon in gameObject.GetComponentsInChildren<Weapon>())
        {
            // Add a blank method as shortcut. Instead of typing the method >>> private float getAtk() { return Atk;  }
            weapon.GetBaseAtkDelegate += () => { return Atk; };  
        }
    }

    void Awake()
    {
        wellBeing = WellBeingState.Alive;           
        
        //if(!GameInfo.TutorialCompleted)     
        
        if (GameInfo.AreaToTeleportTo == GameInfo.Area.TutorialArea)
        {
            // Player starting stats
            HP = startingHP;
            Atk = startingAtk;
            Def = startingDef;
            Speed = startingSpeed;
            UpgradeWeapon(WeaponType.Wooden); // will add 100 more hp     
            SaveAttributes();       
            Debug.Log("Player Starting HP: " + HP + " | ATK: " + Atk + " | DEF: " + Def);
        }
        else
        {
          
        }
    }

    void Start () {
        anim = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();

        LoadAttributes();
        LoadWeapon(currentWeapon);

        // Hard code positions to spawn in each zone
        if (GameInfo.AreaToTeleportTo == GameInfo.Area.World)
            rb2D.transform.position = new Vector2(-2.7f, -17.7f);
        else if (GameInfo.AreaToTeleportTo == GameInfo.Area.TutorialArea)
            rb2D.transform.position = new Vector2(-84.2f, -107.5f);
        else if (GameInfo.AreaToTeleportTo == GameInfo.Area.Forest)
            Debug.Log("Randomly pick between the four corners to spawn here");
        else if (GameInfo.AreaToTeleportTo == GameInfo.Area.Forest)
            Debug.Log("Spawn at the entrance");

        HP = maxHP;
    }

    void Update()
    {
        if (wellBeing == WellBeingState.Alive)
        {
            // Update the Slider UI for HP
            if (HP_Slider.maxValue != maxHP) 
                HP_Slider.maxValue = maxHP;
            if (HP_Slider.value != HP)
                HP_Slider.value = HP;

            // Regen when Idle and not max HP
            if (actionState == ActionState.Idle && HP < maxHP)
                HP += maxHP * regenHP_Multiplier;

            // Debug
            debugHP_Text.text = "HP: " + HP;
            debugMaxHP_Text.text = "MaxHP: " + maxHP;
            debugDef_Text.text = "Def: " + Def;
            debugWeapon_Text.text = currentWeapon+"";

            // Right Stick (Weapon Movement)
            float horizontalR = CnInputManager.GetAxisRaw("HorizontalRight");
            float verticalR = CnInputManager.GetAxisRaw("VerticalRight");
                        
            foreach (Weapons w in weaponList)   
            {
                if (w.weaponType == currentWeapon)
                {
                    // If Weapon is not being used, disable the collider
                    if (verticalR == 0f && horizontalR == 0f)
                        w.weapon.GetComponentInChildren<Collider2D>().enabled = false;
                    else
                        w.weapon.GetComponentInChildren<Collider2D>().enabled = true;

                    // If Weapon is below the waist of the player change the sorting order to display above
                    if (verticalR < -0.2f)
                        w.weapon.GetComponentInChildren<SpriteRenderer>().sortingOrder = 101;
                    else
                        w.weapon.GetComponentInChildren<SpriteRenderer>().sortingOrder = 80;

                    // Move the weapon to the direction the Right stick is pointing
                    w.weapon.transform.localPosition = new Vector2(horizontalR - weaponXoffSet , verticalR - weaponYoffset) * weaponOffset;
                    // Calculate the angle the Right stick is pointing
                    //Debug.Log("H: " + horizontalR + "     V:  " + verticalR);
                
                    float myAngle = Mathf.Atan2(verticalR, horizontalR) * Mathf.Rad2Deg;
                    // Change the angle of the weapon to point the direction of the Right stick
                    w.weapon.transform.eulerAngles = new Vector3(0f, 0f, myAngle);

                    break; // found weapon break loop
                }
            }
            if(!savingAttributes)
                StartCoroutine(AutoSave());
        } 
    }// end Update

    IEnumerator AutoSave()
    {
        savingAttributes = true;
        yield return new WaitForSeconds(30f);
        SaveAttributes();
        savingAttributes = false;
    }
    
    void FixedUpdate()
    {
        if (wellBeing == WellBeingState.Alive)
        {
            // Left Stick (Player Movement)
            float horizontal = CnInputManager.GetAxis("Horizontal");
            float vertical = CnInputManager.GetAxis("Vertical");
            anim.SetFloat("Horizontal Input", horizontal);
            anim.SetFloat("Vertical Input", vertical);

            // Move Player
            rb2D.velocity = new Vector2(horizontal * Speed, vertical * Speed);
            
            // If the conditions for idle exist...
            if (rb2D.velocity == Vector2.zero && actionState != ActionState.EngagedInBattle)
            {
                //Debug.Log("Changing state to Idle");
                actionState = ActionState.Idle;
            }            
            else
            {
                anim.SetBool("Idle", false);
                actionState = ActionState.Running;
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

    // Enemy is hitting player (Change to RayCast maybe)
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
                    canTakeDamage = false;
                    DamagedBy(dmg);
                    StartCoroutine(DelayWhenDamageCanBeRecieved());
                }
            }
        }
    }

    IEnumerator DelayWhenDamageCanBeRecieved()
    {
        yield return new WaitForSeconds(delayToTakeDamageAgain);
        canTakeDamage = true;
    }
    
    // Save attributes before zoning
    public void SaveAttributes()
    {
        //Debug.Log("Saving");
        GameInfo.PlayerMaxHP = maxHP;
        GameInfo.PlayerAtk = Atk;
        GameInfo.PlayerDef = Def;
        GameInfo.PlayerSpeed = Speed;
        GameInfo.CurrentWeapon = currentWeapon;
        PlayerPrefs.Save();
    }

    // Load attributes after zoning
    public void LoadAttributes()
    {
        if (PlayerPrefs.HasKey("MaxHP"))
        {
            maxHP = GameInfo.PlayerMaxHP;
            Atk = GameInfo.PlayerAtk;
            Def = GameInfo.PlayerDef;
            Speed = GameInfo.PlayerSpeed;
            currentWeapon = GameInfo.CurrentWeapon;        
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
                case TypeOfStatIncrease.HP: maxHP += amount; break;
                case TypeOfStatIncrease.ATK: Atk += amount; break;
                case TypeOfStatIncrease.DEF: Def += amount; break;
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
            else  // Turn on weapon of current type
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
        //save
    }

    public void DebugChangeToBronzeButton()
    {
        UpgradeWeapon(WeaponType.Bronze);
    }

    public void DebugChangeToSilverButton()
    {
        UpgradeWeapon(WeaponType.Silver);
    }

    public void DebugChangeToGoldButton()
    {
        UpgradeWeapon(WeaponType.Gold);
    }

    public void DebugChangeToEpicButton()
    {
        UpgradeWeapon(WeaponType.Epic);
    }

    public override void Die()
    {
        if(wellBeing != WellBeingState.Dead)
        {
            wellBeing = WellBeingState.Dead;
            rb2D.isKinematic = true; 
            SaveAttributes(); 
            StartCoroutine(DelayForAnimationThenRespawn()); //delay for tombstone            
        }        
    }

    // Wait for Death animation
    IEnumerator DelayForAnimationThenRespawn()
    {
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(3f);
        respawnButton.gameObject.SetActive(true);
        Time.timeScale = 0.0f;  //pause until button pressed
    }

    public void RespawnPlayerButton()
    {
        LoadAttributes();
        HP = maxHP;
        Time.timeScale = 1.0f;
        foreach (Weapons w in weaponList)
        {
            if(currentWeapon == w.weaponType)
            {
                LoadWeapon(w.weaponType);         
                if(GameInfo.AreaToTeleportTo == GameInfo.Area.World)
                {
                    rb2D.transform.position = new Vector2(-2.7f, -17.7f);
                    wellBeing = WellBeingState.Alive;
                    rb2D.isKinematic = false;
                    anim.gameObject.SetActive(true);
                    anim.SetTrigger("Respawn");
                }                    
                else
                {
                    GameInfo.AreaToTeleportTo = GameInfo.Area.World;
                    Application.LoadLevel("SceneLoader");
                }
            }
        }
        respawnButton.gameObject.SetActive(false);        
    }
}
