using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D grounding;
    
    public float moveSpeed;
    public float stopSpeed;
    public float jumpHeight; // Starting jump
    public float airFloat; // Post initial jump "jump" stuff

    public bool grounded = true;
    public bool jumping = false;
    ContactFilter2D cf = new ContactFilter2D();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        rb.AddForce(new Vector2(moveSpeed * Input.GetAxis("Horizontal"), 0));

        if (Input.GetButtonDown("Jump")) 
        {
            if (grounded) 
            {
                
                if (grounding.IsTouching( cf.NoFilter()  )) 
                {
                    rb.AddForce(new Vector2(0, jumpHeight));
                }
                
                grounded = false;
                jumping = true;
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


        if (!grounded) 
        {
            if (!jumping)
            {
                rb.AddForce(new Vector2(0, -airFloat * 2 * Time.deltaTime));
            }
            if (rb.velocity.y == 0 && grounding.IsTouching(cf.NoFilter() ) )
            { 
                grounded = true; 
            }
        }

    }
}
