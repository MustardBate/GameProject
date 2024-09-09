using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject2 : MonoBehaviour
{
    [SerializeField] private DamageItemTemplate damageItem;
    private Collider2D col;

    private void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            damageItem.DamageBuff(other.gameObject);
            Destroy(gameObject);
        }
    }
}
