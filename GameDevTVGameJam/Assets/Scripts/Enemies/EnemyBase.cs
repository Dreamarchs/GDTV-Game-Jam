using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : character
{

    public float acceleration = 1; // How fast the enemy can accelerate/gain speed
    public float maxSpeed = 10; // How fast the enemy can go

    public float hitStunDur = 1; // How long getting hit should stun the enemy
    public float hitStun = 0; // The hit stun timer
    public bool isStunned; // If the enemy is stunned
    public bool usesShield;
    public int attackDamage = 1; // Damage that the enemy should do

    public GameObject target; // The target / player
    protected PlayerHealth playerHealth;
    protected Collider2D playerCollider;

    public Collider2D groundCollider;  // Tells the Enemy if its on the ground

    public EnemyTriggerRelay attackTrigger;// The collider that tells the enemy its okay to attack
    public EnemyTriggerRelay unsafeTrigger;// The collider that tells the enemy to move away
    public EnemyTriggerRelay chaseTrigger;// Tells the enemy to approach the player
    public ShieldRelay shieldRelay;//Tells the enemy if a attack should be blocked

    public bool isGrounded;

    public Rigidbody2D _rigidBody2D;

    public bool bodyDamage; // Does touching this enemy deal damage?
    public bool doesAttack; // Does this enemy attack?

    public string EnemyName = "Monster"; // In case we ever want to pull a name

    public EnemyState enemyState = EnemyState.Inactive;

    public bool isPlayerLeft = true; // Bool to determine chasing/retreating direction
    protected bool isFacingLeft = true;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Debug.Log("Test");
        if (_rigidBody2D == null) 
        {
            _rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        }
        if (target != null) 
        {
            playerHealth = target.GetComponent<PlayerHealth>();
            playerCollider = target.GetComponent<Collider2D>();
        }

        attackTrigger.playerCollider = playerCollider;
        unsafeTrigger.playerCollider = playerCollider;
        chaseTrigger.playerCollider = playerCollider;


    }



    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        CheckStun();
        if (isStunned)
            hitStun -= Time.deltaTime;
        
    }

    bool FindPlayerDirection()
    {
        return (target.transform.position.x - transform.position.x < 0); // True = left, False = Right
    }

    public void SetFacing()
    {
        SetFacing(FindPlayerDirection());
    }

    public void SetFacing(bool left)
    {
        isFacingLeft = left;
        if (left)
        {
            
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public void SetGrounded(bool b) { isGrounded = b; }

    public override void Damage(int dam)
    {
        CheckStun();
        if (!isStunned) // If not hitStunned
        {
            if (!usesShield)
            {
                base.Damage(dam);
                isStunned = true;
                hitStun = hitStunDur;
            }
            else // If this enemy uses a shield
            {
                if (shieldRelay.shieldHit) // Check if the attack is blocked
                {
                    // Nothing / sound 
                    //Debug.Log("Blocked Attack");
                }
                else
                {
                    base.Damage(dam);
                    isStunned = true;
                    hitStun = hitStunDur;
                }
            }
        }
        else 
        {
            //Debug.Log("Cant damage while stunned.");
        }
        
    }

    public void CheckStun() 
    {
        isStunned = (hitStun > 0);
    }

    public bool CheckShield() // If the shield should block the attack, returns false if it shouldnt
    {
        // IF they're both facing the same direction, the player is hitting around the shield
        if (FindPlayerDirection() == isFacingLeft)
            return false;

        if (shieldRelay.shieldHit)
        {
            return true;
        }

        return false;
    }


    public override void Die() 
    {
        //Destroy(gameObject);
        enemyState = EnemyState.Inactive;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bodyDamage) {
            if (collision.gameObject == target.gameObject)
            {
                playerHealth.SendMessage("Damage", attackDamage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bodyDamage)
        {
            if (collision.gameObject == target.gameObject)
            {
                target.SendMessage("Damage", attackDamage);
            }
        }
    }

}

public enum EnemyState 
{ 
    Wandering,
    Chasing,
    Retreating,
    Attacking,
    Blocking,
    Running,
    Inactive,
    Other

}