using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


[System.Serializable]
public class SpawnAreaBush
{
    public string name;
    public Transform spawnLocation;
    public GameObject prefab;
    public int row;
    public int col;
    public TypeOfStatIncrease typeOfStatDrop;
}

public class SpawnResultBush
{
    public SpawnAreaBush source;  // source base stats
    public Enemy enemy;  // enemy to compare with the dead enemy
}

public class SpawnBushes : MonoBehaviour, ISpawner
{
    public List<SpawnAreaBush> spawnAreas;

    private List<SpawnResultBush> spawnResults;
    private IPlayerCurrentWeapon playerCurrentWeapon;

    private float HP_Median;
    private float Atk_Median;
    private float Def_Median;
    private float AmountOfStatToGive;

    public Bush CreateBush(GameObject prefab, Vector2 pos,
        float HP, float Atk, float Def, float AmountOfStatToGive, TypeOfStatIncrease type)
    {

        GameObject bush = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
        Bush bush_Clone = bush.GetComponent<Bush>();
        bush_Clone.Initialize(HP, Atk, Def, AmountOfStatToGive, type);
        return bush_Clone;
    }

    void Start()
    {
        playerCurrentWeapon = Interface.Find<IPlayerCurrentWeapon>(FindObjectOfType<Player>().gameObject);
        if (playerCurrentWeapon == null)
            Debug.LogError("The script does not contain an interface called, \"IPlayerCurrentWeapon\"");

        spawnResults = new List<SpawnResultBush>();

        StartCoroutine(SpawnBushesNow());
    }

    private bool scaled = false;
    IEnumerator SpawnBushesNow()
    {
        foreach (SpawnAreaBush area in spawnAreas)
        {
            for (int i = 0; i < area.row; i ++)
            {
                for (int j = 0; j<area.col; j++)
                {
                    int r = UnityEngine.Random.Range(1, 4);
                    switch (r)
                    {
                        case 1: area.typeOfStatDrop = TypeOfStatIncrease.HP; break;
                        case 2: area.typeOfStatDrop = TypeOfStatIncrease.ATK; break;
                        case 3: area.typeOfStatDrop = TypeOfStatIncrease.DEF; break;
                        default: area.typeOfStatDrop = TypeOfStatIncrease.HP; break;
                    }

                    float y = area.spawnLocation.position.y;
                    ScaleToYaxis(y, area.typeOfStatDrop);

                    if (y < 20f)
                        ScaleEnemyToWeaponType(1, area.typeOfStatDrop);
                    else if (y > 20f && y < 80f) // moderate
                        ScaleEnemyToWeaponType(2, area.typeOfStatDrop);
                    else if (y > 80f && y < 170f) // hard
                        ScaleEnemyToWeaponType(3, area.typeOfStatDrop);
                    else if (y > 170f)
                        ScaleEnemyToWeaponType(4, area.typeOfStatDrop);                                        

                    SpawnResultBush result = new SpawnResultBush();
                    Vector3 adjPos = area.spawnLocation.position + new Vector3(i*2, j*2, 0f);
                    var bush = CreateBush(area.prefab, adjPos,
                        HP_Median, Atk_Median, Def_Median,
                        AmountOfStatToGive, area.typeOfStatDrop);
                    bush.Spawner = this;
                    result.enemy = bush;  //polymorphism, reference this skeleton to compare later
                    result.source = area;   // reference this area values for respawning
                    spawnResults.Add(result); // add enemy to the list of spawned enemies
                }
            }
            yield return null;
        }
    }

    // The weapon type sets the base then the scale is based on the y cooridinate (south to north, easy to harder)
    public void ScaleEnemyToWeaponType(float scale, TypeOfStatIncrease type)
    {
        if (playerCurrentWeapon != null)
        {
            switch (playerCurrentWeapon.weaponType)
            {
                case WeaponType.Wooden:
                    HP_Median = 1f;
                    Atk_Median = 0f;
                    Def_Median = 0f;
                    switch (type)
                    {
                        case TypeOfStatIncrease.ATK: AmountOfStatToGive = 0.5f; break;
                        case TypeOfStatIncrease.DEF: AmountOfStatToGive = 0.001f; break;
                        case TypeOfStatIncrease.HP: AmountOfStatToGive = 1; break;
                    }
                    break;
                case WeaponType.Bronze:
                    HP_Median = 1f;
                    Atk_Median = 0f;
                    Def_Median = 0f;
                    switch (type)
                    {
                        case TypeOfStatIncrease.ATK: AmountOfStatToGive = 1f * (scale - (scale / 3)); break;
                        case TypeOfStatIncrease.DEF: AmountOfStatToGive = 0.0015f; break;
                        case TypeOfStatIncrease.HP: AmountOfStatToGive = 10 * (scale - (scale / 3)); break;
                    }
                    break;
                case WeaponType.Silver:
                case WeaponType.Gold:
                case WeaponType.Epic:
                    HP_Median = 1f;
                    Atk_Median = 0f;
                    Def_Median = 0f;
                    switch (type)
                    {
                        case TypeOfStatIncrease.ATK: AmountOfStatToGive = 50f * (scale - (scale / 3)); break;
                        case TypeOfStatIncrease.DEF: AmountOfStatToGive = 0.002f; break;
                        case TypeOfStatIncrease.HP: AmountOfStatToGive = 500f * (scale - (scale / 3)); break;
                    }
                    break;
            }
        }
        scaled = true;
    }

    public void Died(Enemy enemy) // called from enemy that died
    {
        foreach (SpawnResultBush sr in spawnResults)
        {
            if (enemy == sr.enemy) // find the dead enemy 
            {
                StartCoroutine(RespawnEnemy(sr));
            }
        }
    }

    public void ScaleToYaxis(float y, TypeOfStatIncrease type)
    {
        if (y < 20f) // easy
            ScaleEnemyToWeaponType(1, type);
        else if (y > 20f && y < 80f) // moderate
            ScaleEnemyToWeaponType(2, type);
        else if (y > 80f && y < 170f) // hard
            ScaleEnemyToWeaponType(4, type);
        else if (y > 170f)
            ScaleEnemyToWeaponType(5, type);
    }

    IEnumerator RespawnEnemy(SpawnResultBush sr)
    {
        float r = UnityEngine.Random.Range(60f, 240f);
        yield return new WaitForSeconds(r);

        float y = sr.source.spawnLocation.position.y;
        ScaleToYaxis(y, sr.source.typeOfStatDrop);
        
        SpawnResultBush result = new SpawnResultBush();
        var bush = CreateBush(sr.source.prefab, sr.source.spawnLocation.position,
            HP_Median, Atk_Median, Def_Median,
            AmountOfStatToGive, sr.source.typeOfStatDrop);
        bush.Spawner = this;
        result.enemy = bush;
        result.source = sr.source;   
        spawnResults[spawnResults.IndexOf(sr)] = result; 
    }
}

