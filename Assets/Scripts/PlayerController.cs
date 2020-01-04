using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    //public float verticalMult;

    private Rigidbody2D rb;
    private Animator anim;

    public Vector2 moveVelocity;

    public bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove) {
            MovementInput();
        } else
        {
            moveVelocity = new Vector2(0, 0);
            anim.SetBool("Walking", false);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    void MovementInput()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * speed;
        //moveVelocity = new Vector2(moveVelocity.x, moveVelocity.y * verticalMult);

        if (moveVelocity != Vector2.zero)
        {
            anim.SetBool("Walking", true);

            if (moveVelocity.x > 0)
            {
                transform.localScale = new Vector2(1, transform.localScale.y);
            }
            else if (moveVelocity.x < 0)
            {
                transform.localScale = new Vector2(-1, transform.localScale.y);
            }
        }
        else
        {
            anim.SetBool("Walking", false);
        }
    }

}
