using UnityEngine;
using System.Collections;

public class MazeMaker : MonoBehaviour
{
    public GameObject Tile;
    public GameObject RLTile;
    public GameObject UDTile;
    public GameObject Enemy;

    void Start()
    {
        char[][] Dungeon = new char[100][];

        for(int k=0; k<100; k++)
        {
            Dungeon[k] = new char[100];
        }

        for (int i = 0; i < 100; i+=2)
            for (int j = 0; j < 100; j+=2)
            {
                if (i >= 46 && i <= 52 && j >= 48 && j <= 54)
                    continue;
                if (i <= 0)
                    continue;
                if (j <= 0)
                    continue;
                if (i == 2 && j == 2)
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
                else if (Random.Range(0f, 1f) <  0.25f)
                {
                    Dungeon[i][j] = 'C';
                    Instantiate(Tile, new Vector3(i, j, 0f), Quaternion.identity);
                }        
                else if (Random.Range(0f, 1f) < 0.3f)
                {
                    Dungeon[i][j] = 'D';
                    Instantiate(Enemy, new Vector3(i, j, 0f), Quaternion.identity);
                }          
            }
    }
}