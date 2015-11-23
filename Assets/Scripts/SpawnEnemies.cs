using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

public class SpawnEnemies : MonoBehaviour {

    //public static List<GameObject> enemiesInstantiated;

    public List<SpawnArea> spawnAreas;

    private Player player;

    public float HP_Median;
    public float Atk_Median;
    public float Def_Median;
    public float AmountOfStatToGive;
    public TypeOfStatIncrease typeOfStat;

    public Skeleton CreateSkeleton(GameObject prefab, Vector2 pos, 
        float HP, float Atk, float Def, float AmountOfStatToGive, TypeOfStatIncrease Type)
    {
        GameObject skeleton = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
        //enemiesInstantiated.Add(skeleton);
        Skeleton skeleton_Clone = skeleton.GetComponent<Skeleton>();
        skeleton_Clone.Initialize(HP, Atk, Def, AmountOfStatToGive, Type);
        return skeleton_Clone;
    }

    public Solider CreateSolider(GameObject prefab, Vector2 pos,
    float HP, float Atk, float Def, float AmountOfStatToGive, TypeOfStatIncrease Type)
    {
        GameObject solider = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
        //enemiesInstantiated.Add(solider);
        Solider solider_Clone = solider.GetComponent<Solider>();
        solider_Clone.Initialize(HP, Atk, Def, AmountOfStatToGive, Type);
        return solider_Clone;
    }

    void Start()
    {
        player = FindObjectOfType<Player>();

        typeOfStat = spawnAreas[0].typeOfStatDrop;

        switch (player.currentWeapon)
        {
            case WeaponType.Wooden:
                HP_Median = 100;
                Atk_Median = 10;
                Def_Median = 0.01f;
                switch(typeOfStat)
                {
                    case TypeOfStatIncrease.ATK: AmountOfStatToGive = 0.5f; break;
                    case TypeOfStatIncrease.DEF: AmountOfStatToGive = 0.0025f; break;
                    case TypeOfStatIncrease.HP: AmountOfStatToGive = 1; break;
                } break;
            case WeaponType.Bronze:
                HP_Median = 1000;
                Atk_Median = 100;
                Def_Median = 0.03f;
                switch (typeOfStat)
                {
                    case TypeOfStatIncrease.ATK: AmountOfStatToGive = 10f; break;
                    case TypeOfStatIncrease.DEF: AmountOfStatToGive = 0.005f; break;
                    case TypeOfStatIncrease.HP: AmountOfStatToGive = 10; break;
                } break;
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
                } break;
        }
     
        //enemiesInstantiated = new List<GameObject>();
        foreach (SpawnArea area in spawnAreas)
        {
            for (int i = 0; i < area.numberToSpawn; i++)
            {
                if (area.typeOfEnemy == TypeOfEnemy.Skeleton)
                {
                    CreateSkeleton(area.prefab, area.spawnLocation.position,
                        HP_Median, Atk_Median, Def_Median,
                        AmountOfStatToGive, area.typeOfStatDrop);
                }
                else if (area.typeOfEnemy == TypeOfEnemy.Solider)
                {
                    CreateSolider(area.prefab, area.spawnLocation.position,
                        HP_Median, Atk_Median, Def_Median,
                        AmountOfStatToGive, area.typeOfStatDrop);
                }
            }
        }
        StartCoroutine(RespawnEnemys());

        //Debug.Log(enemiesInstantiated.Count);
    }

    IEnumerator RespawnEnemys()
    {
        float r = Random.Range(60, 180);
        yield return new WaitForSeconds(r);
        // spawn enemies in the dead enemies array
        StartCoroutine(RespawnEnemys());
    }
    
    public void RespawnWhat(GameObject prefab, Vector2 pos, float HP, float Atk, float Def,
                        float Speed, float AmountOfStatToGive, TypeOfEnemy TypeEnemy, TypeOfStatIncrease TypeStat)
    {
        if (TypeEnemy == TypeOfEnemy.Skeleton)
        {
            CreateSkeleton(prefab, pos, HP, Atk, Def, AmountOfStatToGive, TypeStat);
        }
        else if (TypeEnemy == TypeOfEnemy.Solider)
        {
            CreateSolider(prefab, pos, HP, Atk, Def, AmountOfStatToGive, TypeStat);
        }
    }
}
