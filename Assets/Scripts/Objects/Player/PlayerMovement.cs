using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4.5f;
    private Vector2 movement;
    private Rigidbody2D rb;
    private bool isDead;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        isDead = gameObject.GetComponent<PlayerHealth>().IsDead();
    }


    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            //Player movement
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            //Setting animation for movement
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }


    private void FixedUpdate()
    {
        if (isDead == false)  rb.velocity = moveSpeed * movement.normalized;
    }
}
