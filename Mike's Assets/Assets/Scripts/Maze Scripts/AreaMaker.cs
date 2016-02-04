using UnityEngine;
using System.Collections;

public class AreaMaker : MonoBehaviour
{
    public GameObject Area;
    public GameObject Top;
    public GameObject Bottom;
    public GameObject Left;
    public GameObject Right;

    void Start()
    {
        char[][] Forest = new char[104][];

        for (int k = 0; k < 104; k++)
        {
            Forest[k] = new char[104];
        }

        for (int i = 0; i < 104; i += 2)
            for (int j = 0; j < 104; j += 2)
            {
                if (i <= 0)
                {
                    Forest[i][j] = 'A';
                    Instantiate(Left, new Vector3(i, j, 1), Quaternion.identity);
                }
                else if (i >= 102)
                {
                    Forest[i][j] = 'B';
                    Instantiate(Right, new Vector3(i, j, 1), Quaternion.identity);
                }
                else if (j <= 0)
                {
                    Forest[i][j] = 'C';
                    Instantiate(Bottom, new Vector3(i, j, 1), Quaternion.identity);
                }
                else if (j >= 102)
                {
                    Forest[i][j] = 'D';
                    Instantiate(Top, new Vector3(i, j, 1), Quaternion.identity);
                }
                else
                {
                    Forest[i][j] = 'E';
                    Instantiate(Area, new Vector3(i, j, 1), Quaternion.identity);
                }
            }
    }
}