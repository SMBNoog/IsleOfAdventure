using UnityEngine;
using System.Collections;
using Pathfinding;

public class MazeMaker : MonoBehaviour
{
    public GameObject Tile;
    public GameObject RLTile;
    public GameObject UDTile;
    public GameObject Enemy;
    //// This creates a Grid Graph
    //GridGraph gg;

    void Start()
    {
        //// This holds all graph data
        //AstarData data = AstarPath.active.astarData;

        //// This creates a Grid Graph
        //gg = data.AddGraph(typeof(GridGraph)) as GridGraph;

        //// Setup a grid graph with some values
        //gg.width = 15;
        //gg.depth = 15;
        //gg.nodeSize = 0.7f;

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
                if (i == 100 && j == 2)
                    continue;
                if (i == 100 && j == 100)
                    continue;
                if (i == 2 && j == 100)
                    continue;
                if (Random.Range(0f, 1f) < 0.01f)
                {
                    Dungeon[i][j] = 'A';
                    Instantiate(Enemy, new Vector3(i, j, 0f), Quaternion.identity);
                }
                else if (Random.Range(0f, 1f) < 0.05f)
                {
                    Dungeon[i][j] = 'B';
                    Instantiate(RLTile, new Vector3(i, j, 0f), Quaternion.identity);
                }
                else if (Random.Range(0f, 1f) < 0.1f)
                {
                    Dungeon[i][j] = 'C';
                    Instantiate(UDTile, new Vector3(i, j, 0f), Quaternion.identity);
                }
                else if (Random.Range(0f, 1f) <  0.25f)
                {
                    Dungeon[i][j] = 'D';
                    Instantiate(Tile, new Vector3(i, j, 0f), Quaternion.identity);
                }               
            }
    }

    //void Update()
    //{
    //    gg.center = player.transform.position;

    //    // Updates internal size from the above values
    //    gg.UpdateSizeFromWidthDepth();
    //    // Scans all graphs, do not call gg.Scan(), that is an internal method
    //    AstarPath.active.Scan();
    //}
        //// Setup a grid graph with some values
        //gg.width = 120;
        //gg.depth = 120;
        //gg.nodeSize = 1;
        //gg.center = new Vector3(50, 50, 0);

        //// Updates internal size from the above values
        //gg.UpdateSizeFromWidthDepth();

        //// Scans all graphs, do not call gg.Scan(), that is an internal method

        //AstarPath.active.Scan();
    }