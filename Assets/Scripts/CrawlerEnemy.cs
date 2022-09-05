using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerEnemy : Enemy
{
    [HideInInspector]
    public collisionObjectData collisionData = new collisionObjectData(0);

    private int moveDir = 1;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        crawlerMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        crawlerIntractions(collision);
    }

    private void crawlerMovement()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + boundingBox.offset.x, transform.position.y + boundingBox.offset.y),
            Vector2.right * moveDir, boundingBox.bounds.size.x / 2 + hitRayLength, rayCollisionLayers);
        if (hit)
        {
            moveDir = -moveDir;
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        transform.position = new Vector2(transform.position.x + (moveDir * Speed * Time.deltaTime), transform.position.y);
    }

    private void crawlerIntractions(Collider2D collision)
    {
        checkCollisionSide(collision, "Player", boundingBox, out collisionData);

        if (collisionData.vertical == collisionSide.Up)
        {
            anim.SetBool("dead", true);
            GetComponent<Collider2D>().enabled = false;
            Speed = 0f;
        }
        else if(collisionData.vertical != collisionSide.Up)
        {
            if (collision.GetComponent<PlayerController>() != null)
                GameManager.instance.playerHealth -= 1;
        }
    }

    private void disbaleGO()
    {
        gameObject.SetActive(false);
    }
}
