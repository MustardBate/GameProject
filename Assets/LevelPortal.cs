using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPortal : MonoBehaviour
{
    private Collider2D col;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GameObject popUp;

    void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sprite.material.color = Color.white;
            popUp.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
