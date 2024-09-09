using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private HealthItemTemplate healthItem;
    private Collider2D col;

    private void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            healthItem.HealthBuff(other.gameObject);
            Destroy(gameObject);
        }
    }
}
