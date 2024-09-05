using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class EnemyLogic : MonoBehaviour
{
    //Health
    protected int health;
    [SerializeField] private int fullHealth;

    //Range and walking speed
    public float rangeTilPursuit;
    protected float distance;
    [SerializeField] protected float walkingSpeed;

    //Damage dealt when colliding with player
    [SerializeField] private int contactDamage;

    private Collider2D col;
    private Rigidbody2D rb;
    private Animator animator;
    protected UnityEngine.Vector2 direction;
    
    protected GameObject player;
    //Check if player is dead
    protected bool playerIsDead;

    // Check if this enemy is dead 
    private bool isDead;


    protected void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = gameObject.GetComponent<Animator>();
        col = gameObject.GetComponent<Collider2D>();  

        playerIsDead = player.GetComponent<Player>().IsDead();
        
        health = fullHealth;
        isDead = false;
    }


    protected void Update()
    {
        if (playerIsDead == false && isDead == false)
        {
            distance = UnityEngine.Vector2.Distance(transform.position, player.transform.position);

            direction = (player.transform.position - transform.position).normalized;
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
    }


    //Function to allow enemy to approach player once in range
    protected void ChasePlayer()
    {
        if (distance <= rangeTilPursuit && playerIsDead == false && isDead == false)
        {
            transform.position = UnityEngine.Vector2.MoveTowards(transform.position, player.transform.position, walkingSpeed * Time.deltaTime);
            animator.SetBool("isPlayerClose", true);
        }

        else animator.SetBool("isPlayerClose", false);
    }


    protected void FixedUpdate()
    {
        if(playerIsDead == false)
        {
            ChasePlayer();
        }
    }


    //Function to decrease enemy's health when damaged
    public void TakeDamage(int damage)
    {
        health -= damage;
        // healthBar.setHealth(health);

        if (health <= 0)
        {
            Callback();
            StartCoroutine(DeathAnimation());
        }
    }


    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerIsDead == false)
            {
                other.gameObject.GetComponent<Player>().TakeDamage(contactDamage);
            }
            
            else 
            {
                Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
            }
        }
    }


    public System.Func<int> Callback { get; set; }


    IEnumerator DeathAnimation()
    {
        isDead = true;
        rb.isKinematic = true;
        distance = 0;
        col.enabled = false;
        animator.SetTrigger("isDead");

        yield return new WaitForSeconds(1.1f);

        Destroy(gameObject);
    }


    //Draw the range until pursuit of enemy
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, rangeTilPursuit);
    }
}


