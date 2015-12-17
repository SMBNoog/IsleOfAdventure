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
    public float amountOfStatToGive;
    public bool tutorialSkeleton;
    public bool townSkeleton;
}

public class SpawnResult
{
    public SpawnArea source;  // source base stats
    public Enemy enemy;  // enemy to compare with the dead enemy
}

public class SpawnEnemies : MonoBehaviour, ISpawner {
    public GameObject interfaceProvider; // The gameobject to find an interface on.

    public List<SpawnArea> spawnAreas;

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
    
    void Start() 
    {
        playerCurrentWeapon = Interface.Find<IPlayerCurrentWeapon>(interfaceProvider);
        if (playerCurrentWeapon == null)
            Debug.LogError("The script does not contain an interface called, \"IPlayerCurrentWeapon\"");

        spawnResults = new List<SpawnResult>();

        StartCoroutine(SpawnEnemiesNow());
    }

    IEnumerator SpawnEnemiesNow()
    {
        for (int u = 0; u < spawnAreas.Count; u++)
        {
            for (int i = 0; i < spawnAreas[u].numberToSpawn; i++)
            {
                int r1 = UnityEngine.Random.Range(1, 4);
                switch (r1)
                {
                    case 1: spawnAreas[u].typeOfStatDrop = TypeOfStatIncrease.HP; break;
                    case 2: spawnAreas[u].typeOfStatDrop = TypeOfStatIncrease.ATK; break;
                    case 3: spawnAreas[u].typeOfStatDrop = TypeOfStatIncrease.DEF; break;
                    default: spawnAreas[u].typeOfStatDrop = TypeOfStatIncrease.HP; break;
                }

                float y = spawnAreas[u].spawnLocation.position.y;
                //ScaleToYaxis(y, spawnAreas[u]);

                SpawnArea tempArea = new SpawnArea();
                if (y < 20f) // easy
                    tempArea = ScaleEnemyToWeaponType(1, spawnAreas[u]);
                else if (y > 20f && y < 80f) // moderate
                    tempArea = ScaleEnemyToWeaponType(2f, spawnAreas[u]);
                else if (y > 80f && y < 170f) // hard
                    tempArea = ScaleEnemyToWeaponType(3f, spawnAreas[u]);
                else if (y > 170f)
                    tempArea = ScaleEnemyToWeaponType(4f, spawnAreas[u]);

                if (spawnAreas[u].typeOfEnemy == TypeOfEnemy.Skeleton)
                {
                    SpawnResult result = new SpawnResult();
                    var skeleton = CreateSkeleton(spawnAreas[u].prefab, spawnAreas[u].spawnLocation.position + new Vector3(i, i, 0f),
                        HP_Median, Atk_Median, Def_Median,
                        tempArea.amountOfStatToGive, spawnAreas[u].typeOfStatDrop);
                    skeleton.Spawner = this;
                    result.enemy = skeleton;  
                    result.source = spawnAreas[u];  
                    spawnResults.Add(result); // add enemy to the list of spawned enemies
                }
            }
            yield return null;
        }
    }

    // The weapon type sets the base then the scale is based on the y cooridinate (south to north, easy to harder)
    public SpawnArea ScaleEnemyToWeaponType(float scale, SpawnArea area)
    {
        if (playerCurrentWeapon != null)
        {
            SpawnArea tempArea = new SpawnArea();
            tempArea = area;
            switch (playerCurrentWeapon.weaponType)
            {
                case WeaponType.Wooden:  // Wooden enemy stats
                    HP_Median = 120f * scale;
                    Atk_Median = 12f * scale;
                    Def_Median = 0.01f * scale;
                    switch (area.typeOfStatDrop)
                    {
                        case TypeOfStatIncrease.ATK: tempArea.amountOfStatToGive = 0.5f * scale; return tempArea;
                        case TypeOfStatIncrease.DEF: tempArea.amountOfStatToGive = 0.001f; return tempArea;
                        case TypeOfStatIncrease.HP: tempArea.amountOfStatToGive = 1 * scale; return tempArea;
                    }
                    break;
                case WeaponType.FlamingBlade:
                    HP_Median = 1200f * scale;
                    Atk_Median = 120f * scale;
                    Def_Median = 0.03f * scale;
                    switch (area.typeOfStatDrop)
                    {
                        case TypeOfStatIncrease.ATK: tempArea.amountOfStatToGive = 2f * scale; return tempArea;
                        case TypeOfStatIncrease.DEF: tempArea.amountOfStatToGive = 0.0015f; return tempArea;
                        case TypeOfStatIncrease.HP: tempArea.amountOfStatToGive = 20 * (scale - (scale / 3)); return tempArea;
                    }
                    break;
                case WeaponType.SilverDoomBringer:
                case WeaponType.GoldOathkeeper:
                case WeaponType.EpicCrusader:
                    HP_Median = 10000f * scale;
                    Atk_Median = 1000f * scale;
                    Def_Median = 0.1f * scale;
                    switch (area.typeOfStatDrop)
                    {
                        case TypeOfStatIncrease.ATK: tempArea.amountOfStatToGive = 50f * (scale - (scale / 3)); return tempArea;
                        case TypeOfStatIncrease.DEF: tempArea.amountOfStatToGive = 0.005f; return tempArea;
                        case TypeOfStatIncrease.HP: tempArea.amountOfStatToGive = 200f * (scale - (scale / 3)); return tempArea;
                    }
                    break;
            }
        }
        return null;
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

    public void ScaleToYaxis(float y, SpawnArea area)
    {
        if (y < 20f) // easy
            ScaleEnemyToWeaponType(1, area);
        else if (y > 20f && y < 80f) // moderate
            ScaleEnemyToWeaponType(2, area);
        else if (y > 80f && y < 170f) // hard
            ScaleEnemyToWeaponType(4, area);
        else if (y > 170f)
            ScaleEnemyToWeaponType(5, area);
    }

    IEnumerator RespawnEnemy(SpawnResult sr)
    {
        float r = UnityEngine.Random.Range(30f, 120f);
        yield return new WaitForSeconds(r);

        int r1 = UnityEngine.Random.Range(1, 6);
        switch (r1)
        {
            case 1: sr.source.typeOfStatDrop = TypeOfStatIncrease.HP; break;
            case 2:
            case 3: sr.source.typeOfStatDrop = TypeOfStatIncrease.ATK; break;
            case 4: sr.source.typeOfStatDrop = TypeOfStatIncrease.DEF; break;
            default: sr.source.typeOfStatDrop = TypeOfStatIncrease.HP; break;
        }

        float y = sr.source.spawnLocation.position.y;
        ScaleToYaxis(y, sr.source);

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
    }
}
