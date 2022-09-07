using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGhost : Unit
{
    [SerializeField]
    private int lives = 5;
    [SerializeField]
    private float speed = 15.0F;
    [SerializeField]
    private float jumpForce = 15.0F;


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
    }

    private void Update()
    {
        State = CharState.Idle;


        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();


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


