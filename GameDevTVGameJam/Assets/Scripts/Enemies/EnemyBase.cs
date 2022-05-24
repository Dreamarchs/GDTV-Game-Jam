using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : character
{

    public float acceleration = 1; // How fast the enemy can accelerate/gain speed
    public float maxSpeed = 10; // How fast the enemy can go

    public float hitStunDur = 1; // How long getting hit should stun the enemy
    public int attackDamage = 1; // Damage that the enemy should do

    public GameObject target; // The target / player
    PlayerHealth playerHealth;

    public Collider2D attackCollider; // The collider that tells the enemy its okay to attack
    public Collider2D unsafeCollider; // The collider that tells the enemy to move away
    public Collider2D groundCollider; // Tells the enemy if it's on the ground

    public Rigidbody2D _rigidBody2D;

    public bool bodyDamage; // Does touching this enemy deal damage?
    public bool doesAttack; // Does this enemy attack?

    public string EnemyName = "Monster"; // In case we ever want to pull a name

    public EnemyState enemyState = EnemyState.Inactive;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Debug.Log("Test");
        if (_rigidBody2D == null) 
        {
            _rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        }

        playerHealth = _rigidBody2D.GetComponent<PlayerHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die() 
    {
        Destroy(gameObject);
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