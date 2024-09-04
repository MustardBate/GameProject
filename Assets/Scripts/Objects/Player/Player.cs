using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4.5f;
    private Rigidbody2D rb;
    private Collider2D col;

    private UnityEngine.Vector2 movement;

    private readonly float flashTimer = .13f;
    private readonly int flashCount = 3;
    private SpriteRenderer sprite;


    private int health;
    [SerializeField] private int maxHealth;

    // public HealthBar healthBar;
    private Animator animator;

    // [SerializeField] private AudioSource audioSource;
    // [SerializeField] private AudioClip audioClip;


    // Start is called before the first frame update
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<Collider2D>();    
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();

        health = maxHealth;
    }


    // Update is called once per frame
    private void Update()
    {
        if (IsDead() == false)
        {
            //Player movement
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            //Setting animation for movement
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }


    private void FixedUpdate()
    {
        if (IsDead() == false)  rb.velocity = moveSpeed * movement.normalized;
    }


    //Function to handle player's health after taking damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(DamagedFlash());

        // healthBar.setHealth(health);

        if (IsDead() == true)
        {
            OnDeath();
        }
    }


    //Function to flash the player's health bar when damaged
    private IEnumerator DamagedFlash()
    {
        if (IsDead() == false)
        {
            int temp = 0;
            col.enabled = false;

            while (temp < flashCount)
            {
                sprite.color = new Color (1, 0, 0, .5f);
                yield return new WaitForSeconds(flashTimer);
                sprite.color = new Color (1, 1, 1, 1);
                yield return new WaitForSeconds(flashTimer);

                temp++;
            }

            col.enabled = true;
        }
    }


    //Function to be called when player dies
    private void OnDeath()
    {
        //Disable all children of this game object (remove the gun)
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        //Disable all physics of this game object
        rb.isKinematic = true;
        rb.velocity = UnityEngine.Vector2.zero;

        animator.SetTrigger("isDead");
    }


    //Boolean to check if player is dead
    public bool IsDead()
    {
        if (health <= 0)
        {   
            return true;
        }
        return false;
    }
}


