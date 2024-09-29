using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private BoxCollider2D col;
    private GameObject player;
    [SerializeField] private int contactDamage;
    private bool isPlayerDead;
    private bool isThisEnemyDead;


    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        isPlayerDead = player.GetComponent<PlayerHealth>().IsDead();
        isThisEnemyDead = gameObject.GetComponent<EnemyHealth>().isDead;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (isPlayerDead == false && isThisEnemyDead == false)  other.gameObject.GetComponent<PlayerHealth>().TakeDamage(contactDamage);
            
            else Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), col, true);
        }
    }
}
