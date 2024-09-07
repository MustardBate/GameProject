using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int health;
    [SerializeField] private int maxHealth;

    private Collider2D col;
    private SpriteRenderer sprite;
    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private AnimationClip deathAnimation;
    [SerializeField] private GameObject spriteRenderer;
    private EnemyHealthUI healthBar;

    [HideInInspector] public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
        sprite = spriteRenderer.GetComponent<SpriteRenderer>();
        healthBar = GetComponentInChildren<EnemyHealthUI>();
        animator = GetComponentInChildren<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        isDead = false;
    }

    
    public System.Func<int> DecreaseEnemyCount { get; set; }


    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(DamagedFlash());
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            isDead = true;
            healthBar.enabled = false;
            rb.isKinematic = true;
            col.enabled = false;
            // DecreaseEnemyCount();
            StopCoroutine(DamagedFlash());
            StartCoroutine(Death());
        }
    }


    IEnumerator Death()
    {
        var weapon = GetComponentInChildren<EnemyWeapon>();
        gameObject.GetComponent<EnemyMovement>().enabled = false;
        gameObject.GetComponent<EnemyCollision>().enabled = false;
        if (weapon != null) weapon.enabled = false;

        animator.SetTrigger("isDead");
        yield return new WaitForSeconds(deathAnimation.length);
        Destroy(gameObject);
    }


    IEnumerator DamagedFlash()
    {
        if (isDead == false)
        {
            int temp = 0;

            while (temp < 2)
            {
                sprite.color = new Color (1, 0, 0, .5f);
                yield return new WaitForSeconds(.03f);
                sprite.color = new Color (1, 1, 1, 1);
                yield return new WaitForSeconds(.03f);

                temp++;
            }

        }
    }
}
