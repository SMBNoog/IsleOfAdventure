using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CastleController : MonoBehaviour
{
    public Sprite openDoor;
    public Sprite closedDoor;

    public static int RoomOneEnemies
    {
        get
        {
            return RoomOneEnemies;
        }
        set
        {
            if (value <= 0) ;
            // killed all of these enemies, trigger door opening
            else
                RoomOneEnemies = value;
        }
    }

    // TODO: Sean's spawning Method, increase number of enemies in room

    void Start()
    {
        // Call spawning method
    }

    void OpenRoomOne()
    {

    }

    void CloseRoomOne()
    {

    }

    void SpawnBoss()
    {

    }

    void End()
    {

    }




}
