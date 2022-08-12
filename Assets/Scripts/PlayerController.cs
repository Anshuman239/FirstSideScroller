using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Declaring public variables at same place for clean and easy to read code.
    public float speed;
    public float jumpHeight;
    public Vector2 rayOrigion;
    public float rayLength;
    public LayerMask collisionLayers;

    //Declaring private variables at same place for clean and easy to read code.
    private Rigidbody2D rb;
    private SpriteRenderer spr;
    private Animator anim;

    //Start is called only once when scene loads up.
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    //Update is called every frame by unity game engine.
    private void Update()
    {
        //Calling function inside of update, so it is executed every frame.
        Movement();
        characterAnimation();
    }

    //Custom function to add movement to our character.
    private void Movement()
    {
        //Check to see if we are hitting the ground.
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + rayOrigion.x, transform.position.y + rayOrigion.y), Vector2.down, rayLength, collisionLayers);
        
        //Draw a ray for our visual refrence. Only seen in editor.
        Debug.DrawRay(new Vector2(transform.position.x + rayOrigion.x, transform.position.y + rayOrigion.y), rayLength * Vector2.down, Color.red);

        //Add jump velocity when player hits jump key and is on ground.
        if (Input.GetKeyDown(KeyCode.Space) & hit)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpHeight));
        }

        //Add linear velocity to character when player hits direction keys.
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(speed * Time.deltaTime * 100, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-speed * Time.deltaTime * 100, rb.velocity.y);
        }
    }

    //Custom function to add animation to our character. You will notice few code lines are exact copy from 'Movement()' function. We could have called put this code in 'Movement()' function
    //but i keep it seperate for clear and easy to understand code. 'Movement()' only does movement and 'characterAnimation()' only does animation.
    private void characterAnimation()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + rayOrigion.x, transform.position.y + rayOrigion.y), Vector2.down, rayLength, collisionLayers);

        //Flip the character based on direction of movement.
        if (Input.GetKey(KeyCode.RightArrow))
        {
            spr.flipX = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            spr.flipX = true;
        }

        //Setting variables we set in animator (ctrl + 6) window. We have to build a animation transition tree in animator window.
        anim.SetFloat("speedX", Math.Abs(rb.velocity.x));
        anim.SetBool("grounded", hit);
    }
}
