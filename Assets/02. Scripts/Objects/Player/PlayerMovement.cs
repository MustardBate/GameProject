using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private readonly float baseSpeed = 4.5f;
    [HideInInspector] public float currentSpeed;
    [SerializeField] private bool isInHud;
    private PlayerMovement freeze;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        freeze = gameObject.GetComponent<PlayerMovement>();

        currentSpeed = baseSpeed;
        if (!isInHud) GameObject.FindGameObjectWithTag("StatsUI").GetComponent<StatsUIContainer>().SetSpeedUI(currentSpeed);
        StartCoroutine(LockMovement());
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


    // Ignore player's inputs during scene transition
    private IEnumerator LockMovement()
    {
        freeze.enabled = false;
        yield return new WaitForSeconds(1f);
        freeze.enabled = true;
    }
}
