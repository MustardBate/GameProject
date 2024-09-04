using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomObject : MonoBehaviour
{
    private Vector2Int roomPosition;
    [SerializeField] GameObject topWall;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject rightWall;

    [HideInInspector] public bool isTopRoomExists = false;
    [HideInInspector] public bool isBottomRoomExists = false;
    [HideInInspector] public bool isLeftRoomExists = false;
    [HideInInspector] public bool isRightRoomExists = false;

    private List<GameObject> spawnTriggers;
    [SerializeField] private int enemyInRoomCount;
    private int currentEnemyAliveCount;
    // [SerializeField] private List<GameObject> spawners;
    // [SerializeField] private List<GameObject> enemiesToSpawn;
    // [SerializeField] private GameObject enemyHolder;
    // public List<GameObject> enemiesSpawned = new ();
    private RoomType thisRoomType;


    public enum RoomType 
    {
        SpawnRoom,
        Normal,
        Boss
    };


    private void Start()
    {
        currentEnemyAliveCount = enemyInRoomCount;
        // Debug.Log($"{thisRoomType} is at {roomPosition}, has {spawnTriggers.Count} triggers");
        // roomPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        // foreach (Transform child in transform)
        // {
        //     if (child.CompareTag("EnemySpawner")) spawners.Add(child.gameObject);
        // }

        // RandomlySpawnEnemy();
    }

    private void Update()
    {
        if (currentEnemyAliveCount < 0)
        {
            OpenDoors();
        }
    }

    public Vector2Int GetRoomPosition()
    {
        return roomPosition;
    }


    public RoomType SetRoomType(RoomType assignedRoomType)
    {
        thisRoomType = assignedRoomType;
        return thisRoomType;
    }


    public Vector2Int SetRoomPosition(Vector2Int newRoomPosition)
    {
        roomPosition = newRoomPosition;
        return roomPosition;
    }


    // private void RandomlySpawnEnemy()
    // {
    //     for (int i = 0; i < enemyInRoomCount; i++)
    //     {
    //         var spawnerIndex = Random.Range(0, spawners.Count);
    //         var nextSpawnerIndex = Random.Range(0, spawners.Count - 1);

    //         if (nextSpawnerIndex == spawnerIndex) spawnerIndex = Random.Range(0, spawners.Count);

    //         var enemyIndex = Random.Range(0, enemiesToSpawn.Count);

    //         var enemy = Instantiate(enemiesToSpawn[enemyIndex], spawners[spawnerIndex].transform.position, Quaternion.identity, enemyHolder.transform);
    //         enemiesSpawned.Add(enemy);
    //     }

    //     foreach (GameObject enemy in enemiesSpawned)
    //     {
    //         enemy.SetActive(false);
    //     }
    // }


    public void SetUpDoors()
    {
        // topWall.SetActive(true);
        // bottomWall.SetActive(true);
        // leftWall.SetActive(true);
        // rightWall.SetActive(true);

        if (isTopRoomExists == true)    topWall.SetActive(false);  

        if (isBottomRoomExists == true) bottomWall.SetActive(false);

        if (isLeftRoomExists == true)   leftWall.SetActive(false);

        if (isRightRoomExists == true)  rightWall.SetActive(false);
    }

    private void OpenDoors()
    {
        
    }
}
