using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Range(3f, 50f)]
    [SerializeField] private float bulletSpeed;

    [Range(.1f, 3f)]
    [SerializeField] private float lifeTime;

    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private int damageScale = 1;
    [SerializeField] private int upgradedDamage = 0;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }


    private void Update()
    {
        SizeUpdate();
    }


    private void FixedUpdate()
    {
        rb.velocity = transform.right * bulletSpeed;
    }


    private void SizeUpdate()
    {
        //The bigger the damage, the bigger the bullet\
        var initialPlayerDamage = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<PlayerGun>().damage;
        var updatedPlayerDamage = (GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<PlayerGun>().damage + upgradedDamage) * damageScale * .5f;

        var scale = updatedPlayerDamage/initialPlayerDamage;

        UnityEngine.Vector2 changeSize = new (scale, scale);

        transform.localScale = changeSize;
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Enemy"))
    //     {
    //         other.gameObject.GetComponent<EnemyManager>().takeDamage(damage);
    //         Destroy(gameObject);
    //     }
    // }


    private void OnTriggerEnter2D(Collider2D other)
    {
        var playerDamage = (GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<PlayerGun>().damage + upgradedDamage) * damageScale;
    
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyManager>().TakeDamage(playerDamage);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
