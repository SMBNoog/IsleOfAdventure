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


public class SpawnSkeleton : MonoBehaviour {
    
    public List<SpawnArea> spawnAreas;

    public Skeleton CreateSkeleton(GameObject prefab, Vector2 pos, 
        float HP, float Atk, float Def, float Speed, float AmountOfStatToGive, TypeOfStatIncrease Type)
    {
        Skeleton skeleton_Clone = (Instantiate(prefab, pos, Quaternion.identity) as GameObject)
            .GetComponent<Skeleton>();
        skeleton_Clone.Initialize(HP, Atk, Def, Speed, AmountOfStatToGive, Type);
        return skeleton_Clone;
    }

    public Solider CreateSolider(GameObject prefab, Vector2 pos,
    float HP, float Atk, float Def, float Speed, float AmountOfStatToGive, TypeOfStatIncrease Type)
    {
        Solider skeleton_Clone = (Instantiate(prefab, pos, Quaternion.identity) as GameObject)
            .GetComponent<Solider>();
        skeleton_Clone.Initialize(HP, Atk, Def, Speed, AmountOfStatToGive, Type);
        return skeleton_Clone;
    }

    void Start()
    {
        foreach(SpawnArea area in spawnAreas)
        {
            for(int i=0; i<area.numberToSpawn; i++)
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
            
    }
}
