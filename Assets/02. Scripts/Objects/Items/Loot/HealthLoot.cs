using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLoot : MonoBehaviour
{
    [SerializeField] private int healthHeal;
    public int lootDropChance;
    private Collider2D col;


    private void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().healthBar.SetHealth(other.gameObject.GetComponent<PlayerHealth>().currentHealth += healthHeal);
            other.gameObject.GetComponent<PlayerHealth>().healthBar
            .SetHealthNumber(other.gameObject.GetComponent<PlayerHealth>().maxHealth, other.gameObject.GetComponent<PlayerHealth>().currentHealth);
            Destroy(gameObject);
        }
    }
}
