using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform shotPoint;
    private float rangeTilShoot;

    private GameObject player;
    private float distance;

    [SerializeField] private float timeBetweenShot;
    private float timer;

    private bool isPlayerDead;
    private bool isThisEnemyDead;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rangeTilShoot = gameObject.GetComponentInParent<EnemyMovement>().rangeTilPursuit;

        isPlayerDead = player.GetComponent<PlayerHealth>().IsDead();
        isThisEnemyDead = GetComponentInParent<EnemyHealth>().isDead;

        timer = timeBetweenShot;
    }


    // Update is called once per frame
    void Update()
    {
        if (isPlayerDead == false && isThisEnemyDead == false)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
        }
    }


    private void FixedUpdate()
    {
        if (isPlayerDead == false && isThisEnemyDead == false)
        {
            // Rotating gun's sprite
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.right = direction;

            UnityEngine.Vector2 scale = transform.localScale;
            if (direction.x < 0) 
            {
                scale.y = -1;
            }

            else if (direction.x > 0) 
            {
                scale.y = 1;
            }

            transform.localScale = scale;

            Shoot(timeBetweenShot);
        }
    }


    private void Shoot(float timeBetweenShot)
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Instantiate(projectile, shotPoint.position, shotPoint.rotation);
            timer = timeBetweenShot;
        }
    }
}
