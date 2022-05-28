using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D grounding;
    
    public float moveSpeed;
    public float maxSpeed;
    public float stopSpeed;
    public float jumpHeight; // Starting jump
    public float airFloat; // Post initial jump "jump" stuff

    public bool facingLeft = false;

    public bool grounded = true;
    public bool jumping = false;

    private Animator animator;

    ContactFilter2D cf = new ContactFilter2D();

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (rb.velocity.x != 0)
        {
            animator.SetBool("running", true);
        }
        else 
        {
            animator.SetBool("running", false);
        }

        //rb.AddForce(new Vector2( (moveSpeed * Input.GetAxis("Horizontal")) * Time.deltaTime, 0));

        if (Input.GetAxis("Horizontal") == 0) // If we're not moving we're stopping
        {
            if (rb.velocity.x > 0)
            {
                
                if (Mathf.Abs(rb.velocity.x) < stopSpeed)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                else
                {
                    rb.AddForce(new Vector2(-(stopSpeed * Time.deltaTime), 0));

                }
            }
            else
            {
                
                if (Mathf.Abs(rb.velocity.x) < stopSpeed)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
                else
                {
                    rb.AddForce(new Vector2((stopSpeed * Time.deltaTime), 0));

                }
            }
        }
        else // If we are moving let's regulate the speed.
        {
            rb.AddForce(new Vector2((moveSpeed * Input.GetAxis("Horizontal")) * Time.deltaTime, 0));
            
            if (rb.velocity.x > maxSpeed) 
            {
                rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            }
            if (rb.velocity.x < -maxSpeed) 
            {
                rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
            }

            if (rb.velocity.x > 0)
            {
                facingLeft = false;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (rb.velocity.x < 0)
            {
                facingLeft = true;
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }



        }

        if (Input.GetButtonDown("Jump")) 
        {
            if (grounding.IsTouching(cf.NoFilter()))
            {
                grounded = true;
                
            }
            else { grounded = false; }
        if (grounded) 
            {
                rb.AddForce(new Vector2(0, jumpHeight));
                grounded = false;
                jumping = true;
                animator.SetBool("jumping", true);
            }
        }

        if (jumping)
        {
            if (Input.GetButton("Jump"))
            {
                rb.AddForce(new Vector2(0, airFloat * Time.deltaTime));
            }
            else 
            {
                jumping = false;
            }
        }

        if (rb.velocity.y == 0)
        {
            animator.SetBool("jumping", false);
        }


        if (!grounded)
        {
            animator.SetBool("jumping", true);
            if (!jumping)
            {
                rb.AddForce(new Vector2(0, -airFloat * 2 * Time.deltaTime));
                //animator.SetTrigger("falling");
            }
            if (grounding.IsTouching(cf.NoFilter()))
            {
                grounded = true;
            }
        }
        else 
        {
        }

        if (Input.GetButtonDown("Fire1")) 
        {
            animator.SetTrigger("attacking");
        }

    }
}
