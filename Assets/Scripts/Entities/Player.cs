using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using CnControls;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Weapons
{
    public string name;
    public GameObject weapon;
    public WeaponType weaponType;
}

//public static class Foo
//{
//    public static void DoSomething<T>(T thing) where T : IAttacker, IPlayerCurrentWeapon
//    {
        
//    }
//}

public class Player : Entity, IAttacker, IPlayerCurrentWeapon, IAttributesManager, ICurrentHP, ICurrentPos
{
    public List<Weapons> weaponList;  // Set list of weapons in the inspector

    // IPlayerCurrentWeapon interface
    public WeaponType weaponType { get { return currentWeapon; } }    
    private WeaponType currentWeapon;

    // ICurrentHP interface
    public float currentHP { get { return HP_Slider.value; } }
    public float currentMaxHP { get { return maxHP; } }

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

    public GameObject hpStatUp;
    public GameObject atkStatUp;
    public GameObject defStatUp;
    
    // IAttacker interface
    public WellBeingState wellBeing { get; set; }
    public ActionState actionState { get; set; }
    public Team Team { get { return Team.Player; } }
    public Vector2 Pos { get { return transform.position; } }
    public float Atk { get; set; }
    public TypeOfStatIncrease typeOfStatIncrease { get; set; } // Doesn't use

    public Vector2 postion { get { return Pos; } }

    public Button respawnButton; ///////////// REMOVE THIS WHEN MAIN MENU IS CREATED /////////////
    
    private float maxHP = 100f;     // Maximum HP updated when increased

    // Taking damage 
    private bool canTakeDamage = true;
    public float delayToTakeDamageAgain = 1.0f;

    private Rigidbody2D rb2D;

    private bool savingAttributes = false;

    private bool playingSwingSound = false;

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
            SaveAttributes(true);       
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
            rb2D.transform.position = GameInfo.LastPos;
        else if (GameInfo.AreaToTeleportTo == GameInfo.Area.TutorialArea)
            rb2D.transform.position = new Vector2(-84.2f, -107.5f);
        else if (GameInfo.AreaToTeleportTo == GameInfo.Area.Forest)
        {
            int r = (int)UnityEngine.Random.Range(0f, 4f);
            switch (r)
            {
                case 1: rb2D.transform.position = new Vector2(2f, 2f); break;
                case 2: rb2D.transform.position = new Vector2(2f, 100f); break;
                case 3: rb2D.transform.position = new Vector2(100f, 100f); break;
                case 4: rb2D.transform.position = new Vector2(100f, 2f); break;
            }
        }
        else if (GameInfo.AreaToTeleportTo == GameInfo.Area.Castle)
            rb2D.transform.position = new Vector2(1f, -7f); 

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
            
            
            // Right Stick (Weapon Movement)
            float horizontalR = CnInputManager.GetAxisRaw("HorizontalRight");
            float verticalR = CnInputManager.GetAxisRaw("VerticalRight");
                        
            foreach (Weapons w in weaponList)   
            {
                if (w.weaponType == currentWeapon)
                {
                    // If Weapon is not being used, disable the collider
                    if (verticalR == 0f && horizontalR == 0f)
                    {
                        //w.weapon.GetComponentInChildren<Collider2D>().enabled = false;
                        w.weapon.gameObject.SetActive(false);
                        break;
                    }
                    else
                        w.weapon.gameObject.SetActive(true);
                    //w.weapon.GetComponentInChildren<Collider2D>().enabled = true;

                    // If Weapon is below the waist of the player change the sorting order to display above
                    if (verticalR < -0.2f)
                        w.weapon.GetComponentInChildren<SpriteRenderer>().sortingOrder = 111;
                    else
                        w.weapon.GetComponentInChildren<SpriteRenderer>().sortingOrder = 80;

                    // Move the weapon to the direction the Right stick is pointing
                    w.weapon.transform.localPosition = new Vector2(horizontalR - weaponXoffSet , verticalR - weaponYoffset) * weaponOffset;
                    // Calculate the angle the Right stick is pointing
                    //Debug.Log("H: " + horizontalR + "     V:  " + verticalR);
                
                    float myAngle = Mathf.Atan2(verticalR, horizontalR) * Mathf.Rad2Deg;
                    // Change the angle of the weapon to point the direction of the Right stick
                    w.weapon.transform.eulerAngles = new Vector3(0f, 0f, myAngle);

                    if (!playingSwingSound)
                    {
                        playingSwingSound = true;
                        StartCoroutine(PlaySwordSwingMiss());
                    }

                    break; // found weapon break loop
                }
            }
            if(!savingAttributes)
                StartCoroutine(AutoSave());
        } 
    }// end Update

    IEnumerator PlaySwordSwingMiss()
    {
        SoundManager.Instance.Play(TypeOfClip.SwordMiss);
        yield return new WaitForSeconds(0.4f);
        playingSwingSound = false;
    }

    IEnumerator AutoSave()
    {
        savingAttributes = true;
        yield return new WaitForSeconds(30f);
        SaveAttributes(false);
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
    public void SaveAttributes(bool savePos)
    {
        //Debug.Log("Saving");
        GameInfo.PlayerMaxHP = maxHP;
        GameInfo.PlayerAtk = Atk;
        GameInfo.PlayerDef = Def;
        GameInfo.PlayerSpeed = Speed;
        GameInfo.CurrentWeapon = currentWeapon;       
        if(savePos)
            GameInfo.LastPos = Pos;
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
                case TypeOfStatIncrease.HP: maxHP += amount;
                    GameObject obj1 = Instantiate(hpStatUp) as GameObject;                    
                    obj1.transform.SetParent(transform);
                    obj1.transform.localPosition = new Vector3(0, 0);
                    obj1.transform.localScale = new Vector3(1f, 1f, 1f);                    
                    obj1.gameObject.SetActive(true); break;
                case TypeOfStatIncrease.ATK: Atk += amount;
                    GameObject obj2 = Instantiate(atkStatUp) as GameObject;
                    obj2.transform.SetParent(transform);
                    obj2.transform.localPosition = new Vector3(0, 0);
                    obj2.transform.localScale = new Vector3(1f, 1f, 1f);
                    obj2.gameObject.SetActive(true); break; 
                case TypeOfStatIncrease.DEF: Def += amount;
                    GameObject obj3 = Instantiate(defStatUp) as GameObject;
                    obj3.transform.SetParent(transform);
                    obj3.transform.localPosition = new Vector3(0, 0);
                    obj3.transform.localScale = new Vector3(1f, 1f, 1f);
                    obj3.gameObject.SetActive(true); break;
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
            SaveAttributes(false); 
            StartCoroutine(DelayForAnimationThenRespawn()); //delay for tombstone            
        }        
    }

    // Wait for Death animation
    IEnumerator DelayForAnimationThenRespawn()
    {
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(2f);
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
                else if(GameInfo.AreaToTeleportTo == GameInfo.Area.TutorialArea)
                {
                    anim.SetTrigger("Respawn");
                    rb2D.transform.position = new Vector2(-85f, -108f);
                    wellBeing = WellBeingState.Alive;
                    rb2D.isKinematic = false;
                    anim.gameObject.SetActive(true);
                }    
                else // Forest or Castle
                {
                    GameInfo.AreaToTeleportTo = GameInfo.Area.World;
                    SceneManager.LoadScene("SceneLoader");
                }
            }
        }
        respawnButton.gameObject.SetActive(false);        
    }
}
