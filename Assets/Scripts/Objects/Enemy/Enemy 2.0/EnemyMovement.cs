using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float rangeTilPursuit;
    public float distance;
    private Vector2 direction;
    [SerializeField] private float walkingSpeed;

    private Animator animator;
    private Rigidbody2D rb;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponentInChildren<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        direction = (player.transform.position - transform.position).normalized;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
    }


    private void FixedUpdate()
    {
        if (distance <= rangeTilPursuit)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, walkingSpeed * Time.deltaTime);
            GetComponentInChildren<EnemyWeapon>().enabled = true;
            animator.SetBool("isPlayerClose", true);
        }

        else 
        {
            animator.SetBool("isPlayerClose", false);
            GetComponentInChildren<EnemyWeapon>().enabled = false;
        }
    }


    //Range
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, rangeTilPursuit);
    }
}
