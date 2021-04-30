using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private bool facingLeft;

    public Rigidbody2D rigitbody;

    public Animator anim;

    public GameObject weapon;

    public void Start()
    {
        anim = GetComponent<Animator>();
        rigitbody = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        var moveInputX = Input.GetAxis("Horizontal");
        var moveInputY = Input.GetAxis("Vertical");
        if (moveInputX == 0 && moveInputY == 0)
        {
            anim.SetBool("IsRunning", false);
        }
        else
        {
            anim.SetBool("IsRunning", true);
        }
        rigitbody.velocity = new Vector2(moveInputX, moveInputY) * speed;
        if (moveInputX > 0 && !facingLeft || moveInputX < 0 && facingLeft)
            Flip();
    }

    private void Flip()
    {
        facingLeft = !facingLeft;
        var scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
        weapon.transform.localScale = scaler;
    }
}
