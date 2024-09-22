using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor.Callbacks;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private List<GameObject> enemiesToSpawn;
    private Rigidbody2D playerRb;

    [Header("Other spawnTriggers in this room")]
    [SerializeField] private List<GameObject> otherSpawnTriggers;
    [Space(15)]

    [Header("Doors")]
    [SerializeField] GameObject topDoor;
    [SerializeField] GameObject bottomDoor;
    [SerializeField] GameObject leftDoor;
    [SerializeField] GameObject rightDoor;
    private List<GameObject> doors;
    
    // private Collider2D playerCol;


    // Start is called before the first frame update
    void Start()
    {
        // enemiesToSpawn = GetComponentInParent<RoomObject>().enemiesSpawned;
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();  
        doors = new List<GameObject>() { topDoor, bottomDoor, leftDoor, rightDoor };
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(StartFight());
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject trigger in otherSpawnTriggers)
            {
                trigger.SetActive(false);
            }

            gameObject.SetActive(false);
        }

    }


    private IEnumerator StartFight()
    {
        // CloseValidDoors();
        Debug.Log("Spawning...");

        Debug.Log("Freeze!");
        playerRb.constraints = RigidbodyConstraints2D.FreezePosition;

        yield return new WaitForSeconds(1);

        Debug.Log("Unfreeze!");
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;

        Debug.Log("Spawned");

        GetComponentInParent<RoomObject>().ActivateEnemy();
    }


    private void CloseValidDoors()
    {
        bool isTop = GetComponentInParent<RoomObject>().isTopRoomExists;
        bool isBottom = GetComponentInParent<RoomObject>().isBottomRoomExists;
        bool isLeft = GetComponentInParent<RoomObject>().isLeftRoomExists;
        bool isRight = GetComponentInParent<RoomObject>().isRightRoomExists;

        if (isTop) topDoor.SetActive(true);
        if (isBottom) bottomDoor.SetActive(true);
        if (isLeft) leftDoor.SetActive(true);
        if (isRight) rightDoor.SetActive(true);
    }
}
