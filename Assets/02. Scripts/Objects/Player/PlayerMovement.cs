using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private readonly float baseSpeed = 4.5f;
    [HideInInspector] public float currentSpeed;
    [SerializeField] private bool isInHud;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        currentSpeed = baseSpeed;
        if (!isInHud) GameObject.FindGameObjectWithTag("StatsUI").GetComponent<StatsUIContainer>().SetSpeedUI(currentSpeed);
    }


    // Update is called once per frame
    void Update()
    {
        //Player movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //Setting animation for movement
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }


    private void FixedUpdate()
    { 
        rb.velocity = currentSpeed * movement.normalized;
    }
}
