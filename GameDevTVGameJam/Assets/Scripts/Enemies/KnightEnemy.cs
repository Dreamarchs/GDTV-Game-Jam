using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightEnemy : EnemyBase
{


    //This Knight is designed to be a slow and heavy enemy, so there'll be a lot of delays
    //This requires the use of many states for it to hold positions.
    //There are also many configurable options to make it quick to change and more enjoyable.

    //Todo: Proper attacking, currently just shows an object
    //Todo: Shielding

    public float actionDelay = 1; // Delay between actions
    public float attackLag = .25f; // The delay before the knight attacks
    public float attackDuration = 1; // The time an attack is out
    public float postAttackLag = 2; // The delay after the attack is executed
    private float attackTime; // The combined lag and attack duration
    public float wanderTime = 3; // When the knight is wandering, how long will he do so before checking for enemies
    public float wanderSpeed = .5f; // How fast the knight moves while wandering
    bool wanderRight = false; // Quick bool to determine wander direction.  false = left, true = right
    public float retreatTime = .6f; // Time knight waits to check distances after beginning to retreat
    public float retreatMultiplier = .5f; // A multiplier that applies to acceleration when retreating to make it so backing up is slower
    public float chaseTime = .8f; // Time knight takes before checking distances after beginning to chase
    

    public GameObject attackObject;

    float timer = 0; // Timer is used to manage whether the current action is in use



    //States:
    //Wandering: Will move back and forth, shield raised
    //Chasing: Player is in range, and the knight will move to it
    //Attacking: The knight is in range to attack the player and will do so
    //Retreating: The player is too close and the night will back up.

    

    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (playerCollider == null) 
        {
            Die(); //Kill self if it wouldnt be able to work
        }
        attackTime = attackLag + postAttackLag + attackDuration;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            switch (enemyState)
            {
                case EnemyState.Wandering: Wander(); break;
                case EnemyState.Attacking: Attack(); break;
                case EnemyState.Chasing: Chase(); break;
                case EnemyState.Retreating: Retreat(); break;
            }
        }
        else
        {

            if (chaseTrigger.playerTouching) // We check for this first since it's the biggest
                if (attackTrigger.playerTouching) // second biggest
                    if (unsafeTrigger.playerTouching) // smallest
                        BeginRetreat();
                    else // Else means that its in the smaller bigger collider, so we do the action it'd want to do.
                        BeginAttack();
                else 
                    BeginChase();
            else 
                BeginWander();

            // _rigidBody2D.AddForce(new Vector2(-_rigidBody2D.velocity.x, 0)); // Remove horizontal speeds.
            _rigidBody2D.velocity = new Vector2(0, 0);
        }
        
    }

    

    

    void FindPlayerDirection() 
    {
        isPlayerLeft = (target.transform.position.x - transform.position.x < 0);
    }
    void BeginChase() 
    {
        timer = chaseTime;
        enemyState = EnemyState.Chasing;
        FindPlayerDirection();
        SetFacing();
    }
    void Chase() 
    {
        if (isPlayerLeft)
        {
            if (_rigidBody2D.velocity.x > -maxSpeed)
                _rigidBody2D.AddForce(new Vector2(-acceleration, 0));
        }
        else 
        {
            if (_rigidBody2D.velocity.x < maxSpeed)
                _rigidBody2D.AddForce(new Vector2(acceleration, 0));
        }

        if (attackTrigger.playerTouching)
        {
            timer = 0;
        }

    }

    void BeginAttack() 
    {
        timer = attackTime;
        enemyState = EnemyState.Attacking;
        FindPlayerDirection();
        SetFacing();
    }

    void Attack() 
    {
        if (timer > attackTime - attackLag) // Winding up
        { 
            //Basically doing nothing rn

        }
        else // past winding up
        {
            if (timer > attackTime - (attackLag + attackDuration)) // Attacking
            {
                //Currently this means just enabling the attack object
                attackObject.SetActive(true);
            }
            else 
            {
                // IN post lag its just standing there
                attackObject.SetActive(false);
            }
        }
    
    }

    void BeginRetreat() 
    {
        timer = retreatTime;
        enemyState = EnemyState.Retreating;
        FindPlayerDirection();
        SetFacing();
    }
    void Retreat() 
    {
        if (isPlayerLeft)
        {
            if (_rigidBody2D.velocity.x < maxSpeed)
                _rigidBody2D.AddForce(new Vector2(acceleration*retreatMultiplier, 0));
        }
        else
        {
            if (_rigidBody2D.velocity.x > -maxSpeed)
                _rigidBody2D.AddForce(new Vector2(-acceleration * retreatMultiplier, 0));
            
        }
    }

    void BeginWander() //Sets up a direction to wander in + timer
    {
        timer = wanderTime;
        if (Random.Range(1, 3) == 1) 
        {
            wanderRight = false;
            SetFacing(true);
            //Debug.Log("Left");
        }
        else 
        {
            wanderRight = true;
            SetFacing(false);
            //Debug.Log("Right");
        }
    }

    void Wander() //This just moves in a direction slowly
    {
        if (wanderRight) 
        {
            _rigidBody2D.AddForce(new Vector2(wanderSpeed, 0));
        }
        else 
        {
            _rigidBody2D.AddForce(new Vector2(-wanderSpeed, 0));
        }
    }
}
