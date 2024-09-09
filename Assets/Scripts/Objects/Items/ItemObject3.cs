using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject3 : MonoBehaviour
{
    [SerializeField] private SpeedItemTemplate speedItem;
    private Collider2D col;

    private void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            speedItem.SpeedBuff(other.gameObject);
            Destroy(gameObject);
        }
    }
}
