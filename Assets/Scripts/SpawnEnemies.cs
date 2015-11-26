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
    public TypeOfStatIncrease typeOfStatDrop;
    public bool tutorialSkeleton;
}

public class SpawnResult
{
    public SpawnArea source;  // source base stats
    public Enemy enemy;  // enemy to compare with the dead enemy
}

public class SpawnEnemies : MonoBehaviour, ISpawner {
    
    public List<SpawnArea> spawnAreas;
    public GameObject interfaceProvider; // The gameobject to find an interface on.

    private List<SpawnResult> spawnResults;
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
        if (playerCurrentWeapon == null)
            Debug.LogError("The script does not contain an interface called, \"IPlayerCurrentWeapon\"");

        spawnResults = new List<SpawnResult>();     
     
        foreach (SpawnArea area in spawnAreas)
        {
            for (int i = 0; i < area.numberToSpawn; i++)
            {
                float y = area.spawnLocation.position.y;
                if (y < 20f) // easy
                    ScaleEnemyToWeaponType(1);
                else if (y > 20f && y < 80f) // moderate
                    ScaleEnemyToWeaponType(2);
                else if (y > 80f && y < 170f) // hard
                    ScaleEnemyToWeaponType(4);

                if (area.typeOfEnemy == TypeOfEnemy.Skeleton)
                {
                    SpawnResult result = new SpawnResult();
                    var skeleton = CreateSkeleton(area.prefab, area.spawnLocation.position + new Vector3(i, i, 0f),
                        HP_Median, Atk_Median, Def_Median,
                        AmountOfStatToGive, area.typeOfStatDrop);
                    skeleton.Spawner = this;    
                    result.enemy = skeleton;  //polymorphism, reference this skeleton to compare later
                    result.source = area;   // reference this area values for respawning
                    spawnResults.Add(result); // add enemy to the list of spawned enemies
                }
                else if (area.typeOfEnemy == TypeOfEnemy.Solider)
                {
                    // TODO
                }
            }
        }
    }

    // The weapon type sets the base then the scale is based on the y cooridinate (south to north, easy to harder)
    public void ScaleEnemyToWeaponType(float scale)
    {
        if (playerCurrentWeapon != null)
        { 
            TypeOfStatIncrease typeOfStat = spawnAreas[0].typeOfStatDrop;
            switch (playerCurrentWeapon.weaponType)
            {
                case WeaponType.Wooden:  // Wooden enemy stats
                    HP_Median = 100f * scale;
                    Atk_Median = 10f * scale;
                    Def_Median = 0.01f * scale;
                    switch (typeOfStat)
                    {
                        case TypeOfStatIncrease.ATK: AmountOfStatToGive = 0.5f; break;
                        case TypeOfStatIncrease.DEF: AmountOfStatToGive = 0.0025f; break;
                        case TypeOfStatIncrease.HP: AmountOfStatToGive = 1; break;
                    }
                    break;
                case WeaponType.Bronze:
                    HP_Median = 1000 * scale;
                    Atk_Median = 100 * scale;
                    Def_Median = 0.03f * scale;
                    switch (typeOfStat)
                    {
                        case TypeOfStatIncrease.ATK: AmountOfStatToGive = 1f; break;
                        case TypeOfStatIncrease.DEF: AmountOfStatToGive = 0.005f; break;
                        case TypeOfStatIncrease.HP: AmountOfStatToGive = 10; break;
                    }
                    break;
                case WeaponType.Silver:
                case WeaponType.Gold:
                case WeaponType.Epic:
                    HP_Median = 10000 * scale;
                    Atk_Median = 1000 * scale;
                    Def_Median = 0.1f * scale;
                    switch (typeOfStat)
                    {
                        case TypeOfStatIncrease.ATK: AmountOfStatToGive = 10f; break;
                        case TypeOfStatIncrease.DEF: AmountOfStatToGive = 0.005f; break;
                        case TypeOfStatIncrease.HP: AmountOfStatToGive = 100f; break;
                    }
                    break;
            }
        }
    }

    public void Died(Enemy enemy) // called from enemy that died
    {
        foreach(SpawnResult sr in spawnResults)
        {
            if (enemy == sr.enemy) // find the dead enemy 
            {
                if(!sr.source.tutorialSkeleton)
                    StartCoroutine(RespawnEnemy(sr));
            }
        }
    }

    IEnumerator RespawnEnemy(SpawnResult sr)
    {
        float r = UnityEngine.Random.Range(10f, 20f);
        yield return new WaitForSeconds(r);

        float y = sr.source.spawnLocation.position.y;
        if (y > -40f && y < 20f) // easy
            ScaleEnemyToWeaponType(1);
        else if (y > 20f && y < 80f)
            ScaleEnemyToWeaponType(2);
        else if (y > 80f && y < 170f)
            ScaleEnemyToWeaponType(4);

        if (sr.source.typeOfEnemy == TypeOfEnemy.Skeleton)
        {
            SpawnResult result = new SpawnResult();
            var skeleton = CreateSkeleton(sr.source.prefab, sr.source.spawnLocation.position,
                HP_Median, Atk_Median, Def_Median,
                AmountOfStatToGive, sr.source.typeOfStatDrop);
            skeleton.Spawner = this;
            result.enemy = skeleton;  //polymorphism, reference this skeleton to compare later
            result.source = sr.source;   // reference this area values for respawning
            spawnResults[spawnResults.IndexOf(sr)] = result; // assign new enemy into dead enemy's index in the list  
        }
        else if(sr.source.typeOfEnemy == TypeOfEnemy.Solider)
        {
            //TODO
        } 
    }
}
