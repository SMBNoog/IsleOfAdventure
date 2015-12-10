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
    public float amountOfStatToGive;
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
    //private float AmountOfStatToGive;

    public Bush CreateBush(GameObject prefab, Vector2 pos,
        float HP, float Atk, float Def, float AmountOfStatToGive, TypeOfStatIncrease type)
    {
        GameObject bush = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
        Bush bush_Clone = bush.GetComponent<Bush>();
        bush_Clone.Initialize(HP, pos, Atk, Def, AmountOfStatToGive, type);
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
            for (int i = 0; i < area.row*2; i+=2)
            {
                for (int j = 0; j < area.col*2; j+=2)
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
                    ScaleToYaxis(y, area);

                    SpawnAreaBush tempBush = new SpawnAreaBush();
                    if (y < 20f)
                        tempBush = ScaleEnemyToWeaponType(1, area);
                    else if (y > 20f && y < 80f) // moderate
                        tempBush = ScaleEnemyToWeaponType(2, area);
                    else if (y > 80f && y < 170f) // hard
                        tempBush = ScaleEnemyToWeaponType(3, area);
                    else if (y > 170f)
                        tempBush = ScaleEnemyToWeaponType(4, area);                                        

                    SpawnResultBush result = new SpawnResultBush();
                    //Vector3 tempPos = area.spawnLocation.position + new Vector3(i+2, j+2, 0f);
                    var bush = CreateBush(area.prefab, area.spawnLocation.position,
                        HP_Median, Atk_Median, Def_Median,
                        tempBush.amountOfStatToGive, area.typeOfStatDrop);
                    bush.Spawner = this;
                    result.enemy = bush;  //polymorphism, reference this skeleton to compare later
                    //area.spawnLocation.position = tempPos;
                    result.source = area;   // reference this area values for respawning                    
                    spawnResults.Add(result); // add enemy to the list of spawned enemies

                }
            }
            yield return null;
        }
    }

    // The weapon type sets the base then the scale is based on the y cooridinate (south to north, easy to harder)
    public SpawnAreaBush ScaleEnemyToWeaponType(float scale, SpawnAreaBush area)
    {        
        if (playerCurrentWeapon != null)
        {
            SpawnAreaBush tempArea = area;
            switch (playerCurrentWeapon.weaponType)
            {
                case WeaponType.Wooden:
                    HP_Median = 1f;
                    Atk_Median = 0f;
                    Def_Median = 0f;
                    switch (area.typeOfStatDrop)
                    {
                        case TypeOfStatIncrease.ATK: tempArea.amountOfStatToGive = 0.5f; return tempArea; 
                        case TypeOfStatIncrease.DEF: tempArea.amountOfStatToGive = 0.001f; return tempArea;
                        case TypeOfStatIncrease.HP: tempArea.amountOfStatToGive = 1; return tempArea;
                    }
                    break;
                case WeaponType.Bronze:
                    HP_Median = 1f;
                    Atk_Median = 0f;
                    Def_Median = 0f;
                    switch (area.typeOfStatDrop)
                    {
                        case TypeOfStatIncrease.ATK: tempArea.amountOfStatToGive = 1f * (scale - (scale / 3)); return tempArea;
                        case TypeOfStatIncrease.DEF: tempArea.amountOfStatToGive = 0.0015f; return tempArea;
                        case TypeOfStatIncrease.HP: tempArea.amountOfStatToGive = 10 * (scale - (scale / 3)); return tempArea;
                    }
                    break;
                case WeaponType.Silver:
                case WeaponType.Gold:
                case WeaponType.Epic:
                    HP_Median = 1f;
                    Atk_Median = 0f;
                    Def_Median = 0f;
                    switch (area.typeOfStatDrop)
                    {
                        case TypeOfStatIncrease.ATK: tempArea.amountOfStatToGive = 50f * (scale - (scale / 3)); return tempArea;
                        case TypeOfStatIncrease.DEF: tempArea.amountOfStatToGive = 0.002f; return tempArea;
                        case TypeOfStatIncrease.HP: tempArea.amountOfStatToGive = 500f * (scale - (scale / 3)); return tempArea;
                    }
                    break;

            }
        }
        return null;
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

    public void ScaleToYaxis(float y, SpawnAreaBush area)
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

    IEnumerator RespawnEnemy(SpawnResultBush sr)
    {
        float r = UnityEngine.Random.Range(4f, 8f);
        yield return new WaitForSeconds(r);

        Debug.Log("index : BEFORE  " + spawnResults.IndexOf(sr));

        int r1 = UnityEngine.Random.Range(1, 4);
        switch (r1)
        {
            case 1: sr.source.typeOfStatDrop = TypeOfStatIncrease.HP; break;
            case 2: sr.source.typeOfStatDrop = TypeOfStatIncrease.ATK; break;
            case 3: sr.source.typeOfStatDrop = TypeOfStatIncrease.DEF; break;
            default: sr.source.typeOfStatDrop = TypeOfStatIncrease.HP; break;
        }
        Debug.Log("index : AFTER  " + spawnResults.IndexOf(sr));
        

        float y = sr.source.spawnLocation.position.y;
        ScaleToYaxis(y, sr.source);
        
        SpawnResultBush result = new SpawnResultBush();
        var bush = CreateBush(sr.source.prefab, sr.source.spawnLocation.position,
            HP_Median, Atk_Median, Def_Median,
            sr.source.amountOfStatToGive, sr.source.typeOfStatDrop);
        bush.Spawner = this;
        result.enemy = bush;
        result.source = sr.source;
        Debug.Log("index : AFTERAFTER " + spawnResults.IndexOf(sr));
        Debug.Log("count : " + spawnResults.Count);

        spawnResults[spawnResults.IndexOf(sr) >= 0 ? spawnResults.IndexOf(sr) : 0] = result; 
    }
}

