using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbFlyer : EnemyBase
{
    bool oneDirectional = true;
    bool onlyLeft = true;

    bool wanderRight = false; // Quick bool to determine wander direction.  false = left, true = right
    public float wanderTime = 3; // When the knight is wandering, how long will he do so before checking for enemies
    public float wanderSpeed = .5f; // How fast the knight moves while wandering

    public bool oscilates = false;
    public float oscilationVar = 10;
    public float oscilationSpeed = 1;
    bool oscilateUp = false;
    float startY;
    public float timer = 0; // Timer is used to manage whether the current action is in use



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
            Wander();
        }
        else
        {
                BeginWander();
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
            if (!oneDirectional)
            {
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
            else 
            {
                wanderRight = !onlyLeft;
                SetFacing(onlyLeft);
            }


            
        }

    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
