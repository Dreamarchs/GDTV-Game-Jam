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

    public float attackTime;
    public float lagTime;
    public GameObject attackHitbox;
    private float attackTimer;

    public int power = 1;

    public int specialPower = 5;
    public GameObject specialAttack;
    public Transform specialSpawn;

    public bool facingLeft = false;

    public bool grounded = true;
    public bool jumping = false;

    public int damage = 1;

    public Vector2 DamageKick = new Vector2(200,100);

    private Animator animator;
    private AudioSource audioSource;

    public AudioClip damagedClip;
    public AudioClip attackClip;
    public AudioClip deathClip;
    public AudioClip healedClip;
    public AudioClip pickupClip;
    public AudioClip jumpClip;

    public AudioClip powerFanfare;
    public AudioClip UpgradeFanfare;


    ContactFilter2D cf = new ContactFilter2D();

    // Start is called before the first frame update
    void Start()
    {
        attackHitbox.SetActive(false);

        if (GetComponent<Animator>())
            animator = GetComponent<Animator>();
        if (GameObject.Find("Main Camera").GetComponent<AudioSource>())
            audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        //audioSource.PlayOneShot(damaged);
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer - lagTime < 0)
            {
                attackHitbox.SetActive(false);
            }
        }
        else 
        {
            if (attackHitbox.activeSelf == true)
                attackHitbox.SetActive(false);
        }

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

            if (Input.GetAxis("Horizontal") > 0)
            {
                facingLeft = false;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                facingLeft = true;
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }

        if (Input.GetButtonDown("Jump")) 
        {
            animator.SetBool("jumping", true);
            if (grounding.IsTouching(cf.NoFilter()))
            {
                grounded = true;
            }
            else { grounded = false; }
            if (grounded) 
            {
                audioSource.PlayOneShot(jumpClip);
                rb.AddForce(new Vector2(0, jumpHeight));
                grounded = false;
                jumping = true;
            }
        }

        if (jumping)
        {
            if (Input.GetButton("Jump"))
            {
                animator.SetBool("jumping", true);
                rb.AddForce(new Vector2(0, airFloat * Time.deltaTime));

                if (rb.velocity.y < -1) 
                {
                    jumping = false;
                }

            }
            else 
            {
                jumping = false;
            }
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
                animator.SetBool("jumping", false);
            }
        }
        else 
        {
            if (!grounding.IsTouching(cf.NoFilter()))
            {
                grounded = false;
            }
            animator.SetBool("jumping", false);
        }

        if (Input.GetButtonDown("Fire1")) 
        {
            if ((attackTimer <= 0)) 
            {
                audioSource.PlayOneShot(attackClip);
                animator.SetTrigger("attacking");
                attackHitbox.SetActive(true);
                attackTimer = attackTime + lagTime;
            }
        }

        if (Input.GetButtonDown("Fire2")) 
        {
            if (power >= specialPower) 
            {
                power -= specialPower;
                GameObject.Instantiate(specialAttack, specialSpawn.position, specialSpawn.rotation);
            }
        }

        animator.SetBool("Grounded", grounded);

    }

    public void Damaged(bool left) 
    {
        audioSource.PlayOneShot(damagedClip);
        if (left)
        {
            rb.AddForce(new Vector2(-DamageKick.x, DamageKick.y));
        }
        else 
        {
            rb.AddForce(DamageKick);
        }
    }

    public void AddPower(int _power) 
    {
        power += Mathf.Abs(_power);
        audioSource.PlayOneShot(powerFanfare);
    }

    public void Died() 
    {
        audioSource.PlayOneShot(deathClip);
    }

    public void Healed() 
    {
        audioSource.PlayOneShot(healedClip);
    }

    public void Upgrade() 
    {
        damage++;
        audioSource.PlayOneShot(UpgradeFanfare);
    }

}
