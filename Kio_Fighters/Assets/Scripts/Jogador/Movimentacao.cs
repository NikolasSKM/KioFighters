using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentacao : MonoBehaviour
{

    public Vector2 minBound;
    public Vector2 maxBound;

    private float moveHorizontal;

    public float speed;
    //public float jumpForce;
    private Rigidbody2D rb;
    private bool facingRight = true;
    
    private Animator anim;
    //private bool inFloor = false;
    //private Transform groundCheck;

    //jump stuff
    float distance = 1.08f;
    //layermask ground
    int layerMask = 1 << 8;

    public float jumpingForce;
    bool jumping = false;
    int jumpTimes = 0;
    int jumpJumps = 3;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //groundCheck = gameObject.transform.Find("GroundCheck");
    }

    // Update is called once per frame
    void Update()
    {

        moveHorizontal = Input.GetAxisRaw("Horizontal");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, distance, layerMask);
        anim.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
        rb.position = new Vector2(
            Mathf.Clamp(rb.position.x, minBound.x, maxBound.x), rb.position.y);

        if (rb.position.y < -3.93)
        {
            Destroy(gameObject);
        }

        //inFloor = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        
        if(hit)
        {
            if(jumpTimes > 0)
            {
                jumpTimes = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (hit)
            {
                jumping = true;
                jumpTimes++;
                anim.SetTrigger("Jumping");
            }
        }

        if (jumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingForce);
            jumping = false;
        }

        if(!hit && !jumping && Input.GetKeyDown(KeyCode.Space) && jumpTimes < jumpJumps)
        {
            jumping = true;
            jumpTimes++;
            anim.SetTrigger("Jumping");
        }

        //if (Input.GetButtonDown("jump") && inFloor)
        //{
        //    jumping = true;
        //    anim.SetTrigger("jumping");
        //}

        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if(moveHorizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnDrawGizmos/*Selected*/()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = -Vector2.up * 1.15f;
        Gizmos.DrawRay(transform.position, direction);
    }
}
