using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomObject : MonoBehaviour
{
    [SerializeField] private int roomId;
    private Vector2Int roomPosition;
    [SerializeField] GameObject topWall;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject rightWall;

    [HideInInspector] public bool isTopRoomExists = false;
    [HideInInspector] public bool isBottomRoomExists = false;
    [HideInInspector] public bool isLeftRoomExists = false;
    [HideInInspector] public bool isRightRoomExists = false;

    [SerializeField] private int enemyToSpawnCount;
    [HideInInspector] public int currentEnemyAliveCount; 

    [SerializeField] private List<GameObject> spawners = new ();
    [SerializeField] private List<GameObject> enemyTypes;
    [SerializeField] private GameObject enemyHolder;
    private List<GameObject> enemiesSpawned = new ();

    private RoomType thisRoomType;


    public enum RoomType 
    {
        SpawnRoom,
        Normal,
        Boss
    };


    private void Start()
    {
        currentEnemyAliveCount = enemyToSpawnCount;

       if (thisRoomType == RoomType.SpawnRoom) enemyToSpawnCount = 0;

       else if (thisRoomType == RoomType.Boss) 
        {
            enemyToSpawnCount = 1;
            SpawnBoss();
        }

       else 
       {
            enemyToSpawnCount = 3;
            RandomlySpawnEnemy();
       }
    }


    private void Update()
    {
        if (currentEnemyAliveCount < 0)
        {
            Debug.Log($"Room {roomId} open doors!");
        }
    }


    // Get room position
    public Vector2Int GetRoomPosition()
    {
        return roomPosition;
    }


    // Set room type (for logic)
    public RoomType SetRoomType(RoomType assignedRoomType)
    {
        thisRoomType = assignedRoomType;
        return thisRoomType;
    }


    // Set room position when generating the map
    public Vector2Int SetRoomPosition(Vector2Int newRoomPosition)
    {
        roomPosition = newRoomPosition;
        return roomPosition;
    }


    // Set room id in order to assign this room's id to enemies created in this room
    public int SetRoomId(int newId)
    {
        roomId = newId;
        return roomId;
    }


    private void RandomlySpawnEnemy()
    {
        for (int i = 0; i < enemyToSpawnCount; i++)
        {
            var spawnerIndex = UnityEngine.Random.Range(0, spawners.Count);
            var nextSpawnerIndex = UnityEngine.Random.Range(1, spawners.Count - 1);

            if (nextSpawnerIndex == spawnerIndex) spawnerIndex = UnityEngine.Random.Range(0, spawners.Count);


            var enemy = Instantiate(enemyTypes[0], spawners[spawnerIndex].transform.position, Quaternion.identity, enemyHolder.transform);
            enemy.GetComponent<EnemyManager>().SetAssignedRoomId(roomId);    
            enemiesSpawned.Add(enemy);
        }

        // foreach (GameObject enemy in enemiesSpawned)
        // {
        //     enemy.SetActive(false);
        // }
    }


    private void SpawnBoss()
    {
        return;
    }


    public void DecreaseEnemyAliveCount(int enemyAssignedRoomId)
    {
        if (enemyAssignedRoomId == roomId)
        {
            currentEnemyAliveCount--;
        }
    }


    // To connect this room to other valid rooms
    public void SetUpDoors()
    {
        if (isTopRoomExists == true)    topWall.SetActive(false);  

        if (isBottomRoomExists == true) bottomWall.SetActive(false);

        if (isLeftRoomExists == true)   leftWall.SetActive(false);

        if (isRightRoomExists == true)  rightWall.SetActive(false);
    }


    // To open the door for the player when all enemies are dead (Closing the door is handled by the SpawnTrigger script)
    private void OpenDoors()
    {
        if (currentEnemyAliveCount == 0)
        {
            // Open the doors
            return;
        }
    }
}
