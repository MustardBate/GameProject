using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    [HideInInspector] public RoomType thisRoomType;
    [Space(15)]

    [Header("Sprites and Tilemap")]
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Tile topWallSprite;
    [SerializeField] private Tile bottomWallSprite;
    [SerializeField] private Tile lefWallSprite;
    [SerializeField] private Tile rightWallSprite;


    public enum RoomType
    {
        SpawnRoom,
        Treasure,
        Shop,
        Normal,
        Boss
    };


    private void Start()
    {
        DrawWalls();
        if (thisRoomType == RoomType.SpawnRoom || thisRoomType == RoomType.Treasure || thisRoomType == RoomType.Shop)
        {
            MaxEnemyToSpawnCount = 0;
        }

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

            var enemyIndex = UnityEngine.Random.Range(0, enemyTypes.Count);

            if (nextSpawnerIndex == spawnerIndex) spawnerIndex = UnityEngine.Random.Range(0, spawners.Count);

            var enemy = Instantiate(enemyTypes[enemyIndex], spawners[spawnerIndex].transform.position, Quaternion.identity, enemyHolder.transform);
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

    private void DrawWalls()
    {
        if (isTopRoomExists == false)
        {
            for(int i = -3; i <= 3; i++)
            {
                tileMap.SetTile(new Vector3Int(i, 4, 0), topWallSprite);
            }
        }

        if (isBottomRoomExists == false)
        {
            for (int i = -3; i <= 3; i++)
            {
                tileMap.SetTile(new Vector3Int(i, -5, 0), bottomWallSprite);
            }
        }

        if (isLeftRoomExists == false)
        {
            for (int i = -3; i <= 3; i++)
            {
                tileMap.SetTile(new Vector3Int(-9, i, 0) , lefWallSprite);
            }
        }

        if (isRightRoomExists == false)
        {
            for (int i = -3; i <= 3; i++)
            {
                tileMap.SetTile(new Vector3Int(8, i, 0), rightWallSprite);
            }
        }
    }

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
