using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomObject : MonoBehaviour
{
    public Vector2Int roomPosition;
    [Header("Walls")]
    [SerializeField] GameObject topWall;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject rightWall;
    [Space(15)]

    [Header("Doors")]
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;
    [Space(15)]

    // Variables for detecting neighbouring rooms
    [HideInInspector] public bool isTopRoomExists = false;
    [HideInInspector] public bool isBottomRoomExists = false;
    [HideInInspector] public bool isLeftRoomExists = false;
    [HideInInspector] public bool isRightRoomExists = false;
    private int adjacentRoomsCount = 0;

    // Variables for enemy spawning
    private int MaxEnemyToSpawnCount;
    private int currentEnemyAliveCount;

    [Header("Spawners")]
    [SerializeField] private List<GameObject> spawners = new();
    [SerializeField] private List<GameObject> enemyTypes;
    [SerializeField] private GameObject enemyHolder;

    [HideInInspector] public List<GameObject> enemiesSpawned = new();

    // Set room type for logic
    public RoomType thisRoomType;


    public enum RoomType
    {
        SpawnRoom,
        Normal,
        Boss
    };


    private void Start()
    {
        if (thisRoomType == RoomType.SpawnRoom) MaxEnemyToSpawnCount = 0;

        else if (thisRoomType == RoomType.Boss)
        {
            MaxEnemyToSpawnCount = 1;
            // SpawnBoss();
        }

        else
        {
            MaxEnemyToSpawnCount = 3;
            RandomlySpawnEnemy();
        }
    }


    // To open the door for the player when all enemies are dead (Closing the door is handled by the SpawnTrigger script)


    private void RandomlySpawnEnemy()
    {
        var randomNumberOfEnemies = UnityEngine.Random.Range(2, MaxEnemyToSpawnCount + 1);
        currentEnemyAliveCount = randomNumberOfEnemies;

        for (int i = 0; i < randomNumberOfEnemies; i++)
        {
            var spawnerIndex = UnityEngine.Random.Range(0, spawners.Count);
            var nextSpawnerIndex = UnityEngine.Random.Range(1, spawners.Count - 1);

            if (nextSpawnerIndex == spawnerIndex) spawnerIndex = UnityEngine.Random.Range(0, spawners.Count);

            var enemy = Instantiate(enemyTypes[0], spawners[spawnerIndex].transform.position, Quaternion.identity, enemyHolder.transform);
            enemy.GetComponent<EnemyHealth>().DecreaseEnemyCount = () =>
            {
                currentEnemyAliveCount--;
                if (currentEnemyAliveCount == 0) RoomCleared();
                return 0;
            };
            enemiesSpawned.Add(enemy);
        }

        foreach (GameObject enemy in enemiesSpawned)
        {
            enemy.SetActive(false);
        }
    }


    public void ActivateEnemy()
    {
        foreach (GameObject enemy in enemiesSpawned) enemy.SetActive(true);
    }


    private void RoomCleared()
    {
        topDoor.SetActive(false);
        bottomDoor.SetActive(false);
        leftDoor.SetActive(false);
        rightDoor.SetActive(false);
    }


    private void SpawnBoss()
    {
        return;
    }


    // public void DecreaseEnemyAliveCount(int enemyAssignedRoomId)
    // {
    //     if (enemyAssignedRoomId == roomId)
    //     {
    //         currentEnemyAliveCount--;
    //     }
    // }


    // To connect this room to other valid rooms
    public void SetUpDoors()
    {
        if (isTopRoomExists == true)
        {
            topWall.SetActive(false);
            adjacentRoomsCount++;
        }

        if (isBottomRoomExists == true)
        {
            bottomWall.SetActive(false);
            adjacentRoomsCount++;
        }

        if (isLeftRoomExists == true)
        {
            leftWall.SetActive(false);
            adjacentRoomsCount++;
        }

        if (isRightRoomExists == true)
        {
            rightWall.SetActive(false);
            adjacentRoomsCount++;
        }
    }
}
