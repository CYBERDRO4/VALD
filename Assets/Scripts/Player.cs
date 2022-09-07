using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //...........
public class Player : Unit
{
   // [SerializeField]
//    private int lives = 5;
    [SerializeField]
    private float speed = 5F;
    [SerializeField]
    private float jumpForce = 15.0F;

    float movement = 0f;//.........
    Rigidbody2D rb;//.........

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();//........
    }


    private bool isGrounded;


    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();





    }


    private void FixedUpdate()
    {
        CheckGround();
        Vector2 velocity = rb.velocity;//........
        velocity.x = movement;//........
        rb.velocity = velocity;//........
    }

    private void Update()
    {
        movement = Input.GetAxis("Horizontal");//........
        if (isGrounded)  State = CharState.Idle;


        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Horizontal") != 0) Sprint();
        else if (speed > 5) StopSprint();


    }


    private void Sprint()
    {
        speed = Mathf.Clamp(speed + Time.deltaTime * 30, 5, 10);
    }
    private void StopSprint()
    {
        speed = Mathf.Clamp(speed - Time.deltaTime * 30, 5, 10);
    }
    private void Run()
    {

        if (canMove)
        {
            Vector3 direction = transform.right * Input.GetAxis("Horizontal");

            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);


            sprite.flipX = direction.x < 0.0F;
            if (isGrounded) State = CharState.Run;
        }
    }

    private void Jump()
    {
        State = CharState.Jump;
        rigidbody.AddForce(new Vector2(Input.GetAxis("Horizontal") * 0.25f, jumpForce), ForceMode2D.Impulse);
    }



    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);

        isGrounded = colliders.Length > 1;

    }





}


public enum CharState
{
    Idle,
    Run,
    Jump

}

