using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float bulletSpeed;
    [SerializeField] private List<Sprite> bulletSprites;

    private int bulletDamage;
    private readonly float lifeTime = 1.2f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private CircleCollider2D col;

    private enum Levels { Base, Lv1, Lv2, Lv3, Max }
    private Levels level;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        col = gameObject.GetComponent<CircleCollider2D>();

        Destroy(gameObject, lifeTime);

        bulletDamage = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<PlayerGun>().currentDamage;
        bulletSpeed = GameObject.FindGameObjectWithTag("PlayerGun").GetComponent<PlayerGun>().bulletSpeed;

        level = Levels.Base;
    }


    private void Update()
    {
        LevelUpProjectile();
        ChangeSprite();
    }


    private void FixedUpdate()
    {
        rb.velocity = transform.right * bulletSpeed;
    }


    private void LevelUpProjectile()
    {
        if (bulletDamage < 15) return;
        else if (bulletDamage >= 15 && bulletDamage < 20) level = Levels.Lv1;
        else if (bulletDamage >= 20 && bulletDamage < 30) level = Levels.Lv2;
        else if (bulletDamage >= 30 && bulletDamage < 40) level = Levels.Lv3;
        else if (bulletDamage >= 40) level = Levels.Max;
    }


    private void ChangeSprite()
    {
        if (level == Levels.Base) return;
        else if (level == Levels.Lv1) {sprite.sprite = bulletSprites[0]; col.radius = .15f;}
        else if (level == Levels.Lv2) {sprite.sprite = bulletSprites[1]; col.radius = .236f;}
        else if (level == Levels.Lv3) {sprite.sprite = bulletSprites[2]; col.radius = .314f;}
        else if (level == Levels.Max) {sprite.sprite = bulletSprites[3]; col.radius = .4f;}
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.GetComponent<EnemyHealth>().isDead == false)
            {
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
                Destroy(gameObject);
            }

            else Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
