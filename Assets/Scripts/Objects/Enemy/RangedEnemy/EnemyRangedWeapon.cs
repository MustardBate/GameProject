using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EnemyRangedWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    private float timer;
    [SerializeField] private float timeBetweenShot;
    [SerializeField] private Transform shotPoint;
    private float rangeTilShoot;
    private GameObject player;
    private float distance;

    private bool playerIsDead;

    private void Start()
    {
        timer = timeBetweenShot;
        player = GameObject.FindGameObjectWithTag("Player");
        rangeTilShoot = gameObject.GetComponentInParent<RangedEnemy>().rangeTilPursuit;

        playerIsDead = player.GetComponent<Player>().IsDead();
    }

    private void Update()
    {
        if (playerIsDead == false)
        {
            distance = UnityEngine.Vector2.Distance(transform.position, player.transform.position);
        }
    }


    private void FixedUpdate()
    {
        if (playerIsDead == false)
        {   
            UnityEngine.Vector2 direction = new (player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
            transform.right = direction;

            if (distance <= rangeTilShoot)
            {
                // UnityEngine.Vector2 dir = (UnityEngine.Vector2)(player.transform.position - transform.position).normalized;
                // transform.right = dir;

                // //Flip the gun corrosponding to the direction of the mouse
                // UnityEngine.Vector2 scale = transform.localScale;
                // if (dir.x < 0) 
                // {
                //     scale.y = 1;
                // }
                // else if (dir.x > 0) 
                // {
                //     scale.y = -1;
                // }

                // transform.localScale = scale;

                // Shooting
                timer -= Time.deltaTime;    
                if (timer <= 0)
                {
                    // Debug.Log("Firing");
                    Instantiate(projectile, transform.position, transform.rotation);
                    timer = timeBetweenShot;
                }
            }
        }

        else distance = 0;
    }
}
