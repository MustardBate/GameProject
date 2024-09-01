using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomObject : MonoBehaviour
{
    [SerializeField] private Vector2Int roomPosition;
    [SerializeField] GameObject topWall;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject leftWall;
    [SerializeField] GameObject rightWall;

    public bool isTopRoomExists = false;
    public bool isBottomRoomExists = false;
    public bool isLeftRoomExists = false;
    public bool isRightRoomExists = false;

    [SerializeField] private int enemyCount;
    [SerializeField] private List<GameObject> spawners;
    [SerializeField] private List<GameObject> enemiesToSpawn;
    [SerializeField] private GameObject enemyHolder;
    public List<GameObject> enemiesSpawned = new ();

    public bool hasEnteredRoom = false;

    // private void Update() 
    // {

    // // }
    // private void Awake()
    // {
    //     this.roomPosition = new Vector2Int();
    // }

    private void Start()
    {
        roomPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        foreach (Transform child in transform)
        {
            if (child.CompareTag("EnemySpawner")) spawners.Add(child.gameObject);
        }

        RandomlySpawnEnemy();
    }


    public Vector2Int GetRoomPosition()
    {
        return roomPosition;
    }


    // public Vector2Int SetRoomPosition(Vector2Int newRoomPosition)
    // {
    //     roomPosition = newRoomPosition;
    //     return roomPosition;
    // }


    private void RandomlySpawnEnemy()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            var spawnerIndex = Random.Range(0, spawners.Count);
            var nextSpawnerIndex = Random.Range(0, spawners.Count - 1);

            if (nextSpawnerIndex == spawnerIndex) spawnerIndex = Random.Range(0, spawners.Count);

            var enemyIndex = Random.Range(0, enemiesToSpawn.Count);

            var enemy = Instantiate(enemiesToSpawn[enemyIndex], spawners[spawnerIndex].transform.position, Quaternion.identity, enemyHolder.transform);
            enemiesSpawned.Add(enemy);
        }

        foreach (GameObject enemy in enemiesSpawned)
        {
            enemy.SetActive(false);
        }
    }


    public void SetUpDoors(RoomObject room)
    {
        if (room.isTopRoomExists == true)    room.topWall.SetActive(false);  

        if (room.isBottomRoomExists == true) room.bottomWall.SetActive(false);

        if (room.isLeftRoomExists == true)   room.leftWall.SetActive(false);

        if (room.isRightRoomExists == true)  room.rightWall.SetActive(false);
    }
}
