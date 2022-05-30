using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartForce : MonoBehaviour
{
    public Vector2 force = new Vector2(200, 200);
    private basicMovement ply;
    Rigidbody2D rb;
    int dam;
    // Start is called before the first frame update
    void Start()
    {
        

        if (GameObject.Find("PlayerModel")) 
        {
            if (GameObject.Find("PlayerModel").GetComponent<basicMovement>())
                ply = GameObject.Find("PlayerModel").GetComponent<basicMovement>();
                dam = ply.power;
        }

        if (GetComponent<Rigidbody2D>())
        {
            rb = GetComponent<Rigidbody2D>();
            if (ply)
            {
                if (ply.facingLeft)
                {
                    rb.AddForce(new Vector2(-force.x, force.y));
                }
                else 
                {
                    rb.AddForce(force);
                }
            }
            else 
            {
                rb.AddForce(force);
            }
            
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.SendMessage("Damage", dam);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.SendMessage("Damage", dam);
        Destroy(gameObject);
    }
}
