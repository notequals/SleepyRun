﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public Queue<Floor> floors = new Queue<Floor>();

    [System.NonSerialized]
    public Floor currentFloor;

    [System.NonSerialized]
    public Floor lastFloor;

    public Floor floorPrefab;

    Player player;

    Transform floorHolder;

    int numFloorsToPregenerate = 10;
    int numFloorsTillDestruction = 5;

    void Start()
    {
        player = GameManager.instance.player;

        floorHolder = (new GameObject("Floor Holder")).transform;
    }

    void Update()
    {
        CheckFloors();
    }

    void MakeFloor()
    {
        Vector3 spawnPos;

        if (lastFloor)
        {
            spawnPos = lastFloor.transform.position + new Vector3(0, 0, lastFloor.length);
        }
        else
        {
            spawnPos = player.transform.position;
            spawnPos.y = 0;
        }

        lastFloor = Instantiate(floorPrefab, spawnPos, Quaternion.identity);
        
        lastFloor.transform.SetParent(floorHolder);

        floors.Enqueue(lastFloor);
    }

    void CheckFloors()
    {
        if (floors.Count <= numFloorsToPregenerate)
        {
            MakeFloor();
        }

        var oldestFloor = floors.Peek();
        if (player.transform.position.z - oldestFloor.transform.position.z > oldestFloor.length * numFloorsTillDestruction)
        {
            floors.Dequeue();
            Destroy(oldestFloor.gameObject);
        }
    }
}