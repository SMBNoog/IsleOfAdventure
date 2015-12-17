using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Area
{
    FirstRoom, HardLeft, EasyRight, BossRoom
}

[System.Serializable]
public class CastleSpawnArea
{
    public Area area;
    public Vector2 position;
    public int amountOfEnemy;
    public CastleEnemy castleEnemy;
}

[System.Serializable]
public class CastleEnemy
{
    public GameObject prefab_Enemy;
    public float HP;
    public float Atk;
    public float Def;
    public bool Respawn;
    public float amountOfStatToGive;
    public TypeOfStatIncrease type;
}


public class CastleEnemySpawner : MonoBehaviour
{

    public List<CastleSpawnArea> castleSpawnAreas;
    private IPlayerCurrentWeapon playerCurrentWeapon;

    void Start()
    {
        GameObject playerObj = FindObjectOfType<Player>().gameObject;

        playerCurrentWeapon = Interface.Find<IPlayerCurrentWeapon>(playerObj);
        if (playerCurrentWeapon == null)
            Debug.LogError("IPlayerCurrentWeapon could't be found");

        StartCoroutine(SpawnEnemiesNow());
    }

    public Skeleton CreateEnemy(GameObject prefab, Vector2 pos,
        float HP, float Atk, float Def, float AmountOfStatToGive, TypeOfStatIncrease Type)
    {
        GameObject skeleton = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
        Skeleton skeleton_Clone = skeleton.GetComponent<Skeleton>();
        skeleton_Clone.Initialize(HP, Atk, Def, AmountOfStatToGive, Type);
        return skeleton_Clone;
    }

    IEnumerator SpawnEnemiesNow()
    {
        foreach (CastleSpawnArea area in castleSpawnAreas)
        {
            for (int i = 0; i < area.amountOfEnemy; i++)
            {
                CastleSpawnArea result = new CastleSpawnArea();
                var skeleton = CreateEnemy(area.castleEnemy.prefab_Enemy, area.position,
                    area.castleEnemy.HP, area.castleEnemy.Atk, area.castleEnemy.Def,
                    area.castleEnemy.amountOfStatToGive, area.castleEnemy.type);
                ////skeleton.Spawner = this;
                //result.enemy = skeleton;
                //result.source = area;
                //spawnResults.Add(result); // add enemy to the list of spawned enemies
                            
            }
            yield return null;
        }
    }

}
