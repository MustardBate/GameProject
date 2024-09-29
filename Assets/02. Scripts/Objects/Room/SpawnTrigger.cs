using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private Rigidbody2D playerRb;

    [Header("Other spawnTriggers in this room")]
    [SerializeField] private List<GameObject> otherSpawnTriggers;
    
    // private Collider2D playerCol;


    // Start is called before the first frame update
    void Start()
    {
        // enemiesToSpawn = GetComponentInParent<RoomObject>().enemiesSpawned;
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();  
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
        GetComponentInParent<RoomObject>().CloseValidDoors();
        // Debug.Log("Spawning...");

        // Debug.Log("Freeze!");
        playerRb.constraints = RigidbodyConstraints2D.FreezePosition;

        yield return new WaitForSeconds(1);

        // Debug.Log("Unfreeze!");
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Debug.Log("Spawned");

        GetComponentInParent<RoomObject>().ActivateEnemy();
    }
}
