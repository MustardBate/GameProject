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
            Debug.Log("Spawning...");

            foreach (GameObject trigger in otherSpawnTriggers)
            {
                trigger.SetActive(false);
            }

            gameObject.SetActive(false);
        }
    }
}
