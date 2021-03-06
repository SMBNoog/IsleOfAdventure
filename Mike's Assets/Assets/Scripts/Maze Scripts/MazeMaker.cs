﻿using UnityEngine;
using System.Collections;
using Pathfinding;

public class MazeMaker : MonoBehaviour
{
    public GameObject Tile;
    public GameObject RLTile;
    public GameObject UDTile;
    //public GameObject Chest;

    void Start()
    {
        char[][] Dungeon = new char[100][];

        for(int k=0; k<100; k++)
        {
            Dungeon[k] = new char[100];
        }

        //choose a random i value and j value within range (30, 70) and place chest prefab there;

        for (int i = 0; i < 100; i+=2)
            for (int j = 0; j < 100; j+=2)
            {
                if (i >= 46 && i <= 52 && j >= 48 && j <= 54)
                    continue;
                else if (i <= 0)
                    continue;
                else if (j <= 0)
                    continue;
                else if (i >= 2 && i <= 6 && j >=2 && j <= 6)
                    continue;
                else if (i >= 96 && i <= 100 && j >= 2 && j <= 6)
                    continue;
                else if (i >= 96 && i <= 100 && j >= 96 && j <= 100)
                    continue;
                else if (i >= 2 && i <= 6 && j >= 96 && j <= 100)
                    continue;
                //else if (i = i value of chest && j = j value of chest)
                    //continue;
                else if (Random.Range(0f, 1f) < 0.08f)
                {
                    Dungeon[i][j] = 'B';
                    Instantiate(RLTile, new Vector3(i, j, 0f), Quaternion.identity);
                }
                else if (Random.Range(0f, 1f) < 0.08f)
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
}