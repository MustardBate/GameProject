using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [HideInInspector] public int maxHealth = 5;
    public int currentHealth;
    public PlayerHealthUI healthBar;

    private Rigidbody2D rb;
    private Collider2D col;
    private Animator animator;
    private SpriteRenderer sprite;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        col = gameObject.GetComponent<Collider2D>();
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        healthBar = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<PlayerHealthUI>();
    

        currentHealth = maxHealth;
        healthBar.SetNewMaxHealth(maxHealth, currentHealth);
        healthBar.SetHealthNumber(maxHealth, currentHealth);
    }


    private void Update()
    {
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(DamagedFlash());

        healthBar.SetHealth(currentHealth);
        healthBar.SetHealthNumber(maxHealth, currentHealth);

        if (IsDead() == true)
        {
            currentHealth = 0;
            healthBar.SetHealthNumber(maxHealth, currentHealth);
            OnDeath();
        }
    }


    //Function to flash the player's currentHealth bar when damaged
    private IEnumerator DamagedFlash()
    {
        if (IsDead() == false)
        {
            int temp = 0;
            col.enabled = false;

            while (temp < 3)
            {
                sprite.color = new Color (1, 0, 0, .5f);
                yield return new WaitForSeconds(.13f);
                sprite.color = new Color (1, 1, 1, 1);
                yield return new WaitForSeconds(.13f);

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
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        rb.velocity = UnityEngine.Vector2.zero;

        animator.SetTrigger("isDead");
    }


    //Boolean to check if player is dead
    public bool IsDead()
    {
        if (currentHealth <= 0)
        {   
            return true;
        }
        return false;
    }
}
