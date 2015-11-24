using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum TypeOfEnemy { Skeleton, Solider}

[System.Serializable]
public class SpawnArea {
    public string name;
    public Transform spawnLocation;
    public GameObject prefab;
    public TypeOfEnemy typeOfEnemy;
    public int numberToSpawn;
    //public float HP_Median;
    //public float Atk_Median;
    //public float Def_Median;
    //public float Speed;
    //public float AmountOfStatToGive;
    public TypeOfStatIncrease typeOfStatDrop;
}

public class SpawnResult
{
    public SpawnArea source;  // source base stats
    public Enemy enemy;  // enemy to compare with the dead enemy
}

public class SpawnEnemies : MonoBehaviour, ISpawner {

    //public static List<GameObject> enemiesInstantiated;

    public List<SpawnArea> spawnAreas;

    private List<SpawnResult> spawnResults;

    public GameObject interfaceProvider; //player game object or whatever we need to find an interface on

    private IPlayerCurrentWeapon playerCurrentWeapon;

    private float HP_Median;
    private float Atk_Median;
    private float Def_Median;
    private float AmountOfStatToGive;

    public Skeleton CreateSkeleton(GameObject prefab, Vector2 pos, 
        float HP, float Atk, float Def, float AmountOfStatToGive, TypeOfStatIncrease Type)
    {
        GameObject skeleton = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
        Skeleton skeleton_Clone = skeleton.GetComponent<Skeleton>();
        skeleton_Clone.Initialize(HP, Atk, Def, AmountOfStatToGive, Type);
        return skeleton_Clone;
    }

    public Solider CreateSolider(GameObject prefab, Vector2 pos,
    float HP, float Atk, float Def, float AmountOfStatToGive, TypeOfStatIncrease Type)
    {
        GameObject solider = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
        Solider solider_Clone = solider.GetComponent<Solider>();
        solider_Clone.Initialize(HP, Atk, Def, AmountOfStatToGive, Type);
        return solider_Clone;
    }

    void Start()
    {
        playerCurrentWeapon = Interface.Find<IPlayerCurrentWeapon>(interfaceProvider);

        spawnResults = new List<SpawnResult>();

        ScaleEnemyToWeaponType();       
     
        //enemiesInstantiated = new List<GameObject>();
        foreach (SpawnArea area in spawnAreas)
        {
            for (int i = 0; i < area.numberToSpawn; i++)
            {
                if (area.typeOfEnemy == TypeOfEnemy.Skeleton)
                {
                    SpawnResult result = new SpawnResult();
                    var skeleton = CreateSkeleton(area.prefab, area.spawnLocation.position + new Vector3(i, i, 0f),
                        HP_Median, Atk_Median, Def_Median,
                        AmountOfStatToGive, area.typeOfStatDrop);
                    Debug.Log("Skeleton spawned");
                    skeleton.Spawner = this;    // 
                    result.enemy = skeleton;  //polymorphism, reference this skeleton to compare later
                    result.source = area;   // reference this area values for respawning
                    spawnResults.Add(result); // add enemy to the list
                }
                else if (area.typeOfEnemy == TypeOfEnemy.Solider)
                {
                    CreateSolider(area.prefab, area.spawnLocation.position + new Vector3(i, i, 0f),
                        HP_Median, Atk_Median, Def_Median,
                        AmountOfStatToGive, area.typeOfStatDrop);
                }
            }
        }
    }

    public void ScaleEnemyToWeaponType()
    {
        TypeOfStatIncrease typeOfStat = spawnAreas[0].typeOfStatDrop;
        switch (playerCurrentWeapon.weaponType)
        {
            case WeaponType.Wooden:
                HP_Median = 100;
                Atk_Median = 10;
                Def_Median = 0.01f;
                switch (typeOfStat)
                {
                    case TypeOfStatIncrease.ATK: AmountOfStatToGive = 0.5f; break;
                    case TypeOfStatIncrease.DEF: AmountOfStatToGive = 0.0025f; break;
                    case TypeOfStatIncrease.HP: AmountOfStatToGive = 1; break;
                }
                break;
            case WeaponType.Bronze:
                HP_Median = 1000;
                Atk_Median = 100;
                Def_Median = 0.03f;
                switch (typeOfStat)
                {
                    case TypeOfStatIncrease.ATK: AmountOfStatToGive = 10f; break;
                    case TypeOfStatIncrease.DEF: AmountOfStatToGive = 0.005f; break;
                    case TypeOfStatIncrease.HP: AmountOfStatToGive = 10; break;
                }
                break;
            case WeaponType.Silver:
            case WeaponType.Gold:
            case WeaponType.Epic:
                HP_Median = 10000;
                Atk_Median = 1000;
                Def_Median = 0.1f;
                switch (typeOfStat)
                {
                    case TypeOfStatIncrease.ATK: AmountOfStatToGive = 10f; break;
                    case TypeOfStatIncrease.DEF: AmountOfStatToGive = 0.005f; break;
                    case TypeOfStatIncrease.HP: AmountOfStatToGive = 100f; break;
                }
                break;
        }
    }

    public void Died(Enemy enemy) // called from enemy that died
    {
        foreach(SpawnResult sr in spawnResults)
        {
            if (enemy == sr.enemy) // find the dead enemy 
            {
                ScaleEnemyToWeaponType();
                StartCoroutine(RespawnEnemy(sr));
            }
        }
    }

    IEnumerator RespawnEnemy(SpawnResult sr)
    {
        float r = UnityEngine.Random.Range(60f, 480f);
        yield return new WaitForSeconds(r);

        SpawnResult result = new SpawnResult();
        var skeleton = CreateSkeleton(sr.source.prefab, sr.source.spawnLocation.position,
            HP_Median, Atk_Median, Def_Median,
            AmountOfStatToGive, sr.source.typeOfStatDrop);
        skeleton.Spawner = this;
        result.enemy = skeleton;  //polymorphism, reference this skeleton to compare later
        result.source = sr.source;   // reference this area values for respawning
        spawnResults[spawnResults.IndexOf(sr)] = result; // assign new enemy into dead enemy's index in the list        
    }

    //public void RespawnWhat(GameObject prefab, Vector2 pos, float HP, float Atk, float Def,
    //                    float Speed, float AmountOfStatToGive, TypeOfEnemy TypeEnemy, TypeOfStatIncrease TypeStat)
    //{
    //    if (TypeEnemy == TypeOfEnemy.Skeleton)
    //    {
    //        CreateSkeleton(prefab, pos, HP, Atk, Def, AmountOfStatToGive, TypeStat);
    //    }
    //    else if (TypeEnemy == TypeOfEnemy.Solider)
    //    {
    //        CreateSolider(prefab, pos, HP, Atk, Def, AmountOfStatToGive, TypeStat);
    //    }
    //}
}
