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
    public float HP_Median;
    public float Atk_Median;
    public float Def_Median;
    public float Speed;
    public float AmountOfStatToGive;
    public TypeOfStatIncrease typeOfStatDrop;
}


public class SpawnEnemies : MonoBehaviour {

    //public static List<GameObject> enemiesInstantiated;

    public List<SpawnArea> spawnAreas;
    

    public Skeleton CreateSkeleton(GameObject prefab, Vector2 pos, 
        float HP, float Atk, float Def, float Speed, float AmountOfStatToGive, TypeOfStatIncrease Type)
    {
        GameObject skeleton = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
        //enemiesInstantiated.Add(skeleton);
        Skeleton skeleton_Clone = skeleton.GetComponent<Skeleton>();
        skeleton_Clone.Initialize(HP, Atk, Def, Speed, AmountOfStatToGive, Type);
        return skeleton_Clone;
    }

    public Solider CreateSolider(GameObject prefab, Vector2 pos,
    float HP, float Atk, float Def, float Speed, float AmountOfStatToGive, TypeOfStatIncrease Type)
    {
        GameObject solider = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
        //enemiesInstantiated.Add(solider);
        Solider solider_Clone = solider.GetComponent<Solider>();
        solider_Clone.Initialize(HP, Atk, Def, Speed, AmountOfStatToGive, Type);
        return solider_Clone;
    }

    void Start()
    {
        //enemiesInstantiated = new List<GameObject>();
        foreach (SpawnArea area in spawnAreas)
        {
            for (int i = 0; i < area.numberToSpawn; i++)
            {
                if (area.typeOfEnemy == TypeOfEnemy.Skeleton)
                {
                    CreateSkeleton(area.prefab, area.spawnLocation.position,
                        area.HP_Median, area.Atk_Median, area.Def_Median,
                        area.Speed, area.AmountOfStatToGive, area.typeOfStatDrop);
                }
                else if (area.typeOfEnemy == TypeOfEnemy.Solider)
                {
                    CreateSolider(area.prefab, area.spawnLocation.position,
                        area.HP_Median, area.Atk_Median, area.Def_Median,
                        area.Speed, area.AmountOfStatToGive, area.typeOfStatDrop);
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
                
        StartCoroutine(RespawnEnemys());
    }
    
    public void RespawnWhat(GameObject prefab, Vector2 pos, float HP, float Atk, float Def,
                        float Speed, float AmountOfStatToGive, TypeOfEnemy TypeEnemy, TypeOfStatIncrease TypeStat)
    {
        if (TypeEnemy == TypeOfEnemy.Skeleton)
        {
            CreateSkeleton(prefab, pos, HP, Atk, Def, Speed, AmountOfStatToGive, TypeStat);
        }
        else if (TypeEnemy == TypeOfEnemy.Solider)
        {
            CreateSolider(prefab, pos, HP, Atk, Def, Speed, AmountOfStatToGive, TypeStat);
        }
    }
}
