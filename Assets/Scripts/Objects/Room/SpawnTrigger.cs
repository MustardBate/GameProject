using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private List<GameObject> enemiesToSpawn;
    private Rigidbody2D playerRb;
    private bool hasEntered;
    // private Collider2D playerCol;


    // Start is called before the first frame update
    void Start()
    {
        enemiesToSpawn = GetComponentInParent<RoomObject>().enemiesSpawned;
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();  
        // playerCol = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();

        hasEntered = GetComponentInParent<RoomObject>().hasEnteredRoom;
    }

    // private void Update()
    // {
    //     if (hasEntered == true)
    //     {

    //     }
    // }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(StartFight());
            hasEntered = true;
        }

        if (hasEntered == true)
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
    }


    IEnumerator StartFight()
    {
        Debug.Log("Spawning...");
        playerRb.velocity = Vector2.zero;

        yield return new WaitForSeconds(1.5f);

        foreach (GameObject enemy in enemiesToSpawn)
        {
            if (enemy != null)  enemy.SetActive(true);
            else break;
        }
    }
}
