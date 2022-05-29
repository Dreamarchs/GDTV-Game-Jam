using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyer : EnemyBase
{ 
    bool wanderRight = false; // Quick bool to determine wander direction.  false = left, true = right
    public float wanderTime = 3; // When the knight is wandering, how long will he do so before checking for enemies
    public float wanderSpeed = .5f; // How fast the knight moves while wandering

    public float chaseTime = 1;

    public float attackTime = 5;

    public bool oscilates = false;
    public float oscilationVar = 10;
    public float oscilationSpeed = 1;
    bool oscilateUp = false;
    float startY;
    public float timer = 0; // Timer is used to manage whether the current action is in use

    bool hasAttacked = false;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        startY = transform.position.y;
        bodyDamage = true;
        //populate unused stuff with others
        unsafeTrigger = attackTrigger;
        

        //This cannot work with simulated phyiscs
        if (GetComponent<Rigidbody2D>()) 
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
            //_rigidBody2D.simulated = false;
        }

        if (playerCollider == null)
        {
            Die(); //Kill self if it wouldnt be able to work
        }

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
            }
        }
        else 
        {
            if (chaseTrigger.playerTouching) // We check for this first since it's the biggest
                if (attackTrigger.playerTouching) // second biggest
                        BeginAttack();
                else
                    BeginChase();
            else
                BeginWander();
        }

        void Attack()
        {
            if (hasAttacked)
                Wander();
            else
            {
                Vector3 pos = transform.position;
                pos.y = Mathf.Lerp(pos.y, target.transform.position.y + .16f, .05f);
                pos.x = Mathf.Lerp(pos.x, target.transform.position.x, .05f);
                transform.position = pos;
            }

        }
        void BeginAttack() 
        {
            timer = attackTime;
            enemyState = EnemyState.Attacking;
            FindPlayerDirection();
            SetFacing();
            hasAttacked = false;
        }
        void Wander() 
        {
            Vector3 pos = transform.position;
            if (oscilates)
            {
                if (oscilateUp)
                    pos.y += oscilationSpeed * Time.deltaTime;
                else
                    pos.y -= oscilationSpeed * Time.deltaTime;
                if ((pos.y - startY) > oscilationVar || (startY - pos.y) > oscilationVar)
                    oscilateUp = !oscilateUp;
            }
            if (wanderRight)
            {
                pos.x += wanderSpeed * Time.deltaTime;
            }
            else
            {
                pos.x -= wanderSpeed * Time.deltaTime;
            }
            transform.position = pos;
        }
        void BeginWander() 
        {
            enemyState = EnemyState.Wandering;
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
        void Chase() 
        {
            Vector3 pos = transform.position;
            if (oscilates)
            {
                if (oscilateUp)
                    pos.y += oscilationSpeed * Time.deltaTime;
                else
                    pos.y -= oscilationSpeed * Time.deltaTime;
                if ((pos.y - startY) > oscilationVar || (startY - pos.y) > oscilationVar)
                    oscilateUp = !oscilateUp;
            }
            if (!isPlayerLeft)
            {
                pos.x += wanderSpeed * Time.deltaTime;
            }
            else
            {
                pos.x -= wanderSpeed * Time.deltaTime;
            }
            transform.position = pos;
        }
        void BeginChase() 
        {
            timer = chaseTime;
            enemyState = EnemyState.Chasing;
            FindPlayerDirection();
            SetFacing();
        }

        void FindPlayerDirection()
        {
            isPlayerLeft = (target.transform.position.x - transform.position.x < 0);
        }

    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        hasAttacked = true;
    }
}
