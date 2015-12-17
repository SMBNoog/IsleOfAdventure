using UnityEngine;
using System.Collections;

public class MazeMaker : MonoBehaviour
{
    public GameObject Tile;
    public GameObject RLTile;
    public GameObject UDTile;

    void Start()
    {
        char[][] Dungeon = new char[20][];
        Dungeon[0] = new char[20];
        Dungeon[1] = new char[20];
        Dungeon[2] = new char[20];
        Dungeon[3] = new char[20];
        Dungeon[4] = new char[20];
        Dungeon[5] = new char[20];
        Dungeon[6] = new char[20];
        Dungeon[7] = new char[20];
        Dungeon[8] = new char[20];
        Dungeon[9] = new char[20];
        Dungeon[10] = new char[20];
        Dungeon[11] = new char[20];
        Dungeon[12] = new char[20];
        Dungeon[13] = new char[20];
        Dungeon[14] = new char[20];
        Dungeon[15] = new char[20];
        Dungeon[16] = new char[20];
        Dungeon[17] = new char[20];
        Dungeon[18] = new char[20];
        Dungeon[19] = new char[20];

        for (int i = 0; i < 20; i++)
            for (int j = 0; j < 20; j++)
            {
                if (i >= 9 && i <= 11 && j >= 9 && j <= 12)
                    continue;
                if (Random.Range(0f, 1f) < 0.05f)
                {
                    Dungeon[i][j] = 'A';
                    Instantiate(RLTile, new Vector3(i, j, 0f), Quaternion.identity);
                }
                else if (Random.Range(0f, 1f) < 0.1f)
                {
                    Dungeon[i][j] = 'B';
                    Instantiate(UDTile, new Vector3(i, j, 0f), Quaternion.identity);
                }
                else if (Random.Range(0f, 1f) <  .3f)
                {
                    Dungeon[i][j] = 'C';
                    Instantiate(Tile, new Vector3(i, j, 0f), Quaternion.identity);
                }                  
            }
    }
}