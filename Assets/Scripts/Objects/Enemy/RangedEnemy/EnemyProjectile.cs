using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private Rigidbody2D rb;

    [Range (.1f, 3f)]
    [SerializeField] private float lifeTime;

    [Range(3f, 50f)]
    [SerializeField] private float bulletSpeed;

    [SerializeField] private int damage;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(gameObject, lifeTime);
    }


    // Update is called once per frame
    private void Update()
    {
        rb.velocity = bulletSpeed * transform.right;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
