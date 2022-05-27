using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyBase
{

    public bool isAggro; // Whether the slime will target the player or just hop randomly
    public Vector2 minHop = new Vector2(100, 100); // Minimum hop, or the lowest values a slime will hop with
    public Vector2 maxHop = new Vector2(200, 200); // Maximum hop, the highest values it will hop with.
    //A hop will work by random.ranging the x and y of min and max hop
    public float timer = 0;
    public float minTimer = 1;
    public float maxTimer = 10;
    public bool isSticky = false; //If sticky, the slime will instantly kill all momentum when grounded 
    private bool overrideGroundCheck = false; // This is used when is sticky is enabled to not instantly kill momentum on jumping

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        timer = minTimer;

        if (isAggro && playerCollider == null)
        {
            Die(); //Kill self if it wouldnt be able to work
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        timer -= Time.deltaTime;

        if (timer <= 0) 
        {
            Hop();
            timer = Random.Range(minTimer, maxTimer);
        }

        if (!overrideGroundCheck && isGrounded) 
        {
            enemyState = EnemyState.Waiting;
        }

        if (isSticky) 
        {
            if (!overrideGroundCheck)
            {
                if (isGrounded)
                {
                    _rigidBody2D.velocity = new Vector2(0, 0);
                    enemyState = EnemyState.Waiting;
                }
            }
            else 
            {
                if (!isGrounded) 
                {
                    overrideGroundCheck = false;
                }
            }
        }

    }

    public void Hop() 
    {
        if (isAggro)
            if (attackTrigger.playerTouching)
                enemyState = EnemyState.Attacking;
             else
                enemyState = EnemyState.Wandering;


        bool left =false;
        if (enemyState == EnemyState.Attacking)
        {
            left = (target.transform.position.x - transform.position.x < 0);
        }
        else 
        {
            if (Random.Range(0, 2) == 1)
            {
                left = true;
            }
        }


        
        if (left)
        {
            _rigidBody2D.AddForce(new Vector2(-Random.Range(minHop.x, maxHop.x), Random.Range(minHop.y, maxHop.y)));
        }
        else 
        {
            _rigidBody2D.AddForce(new Vector2( Random.Range(minHop.x,maxHop.x),Random.Range(minHop.y,maxHop.y) ) );
        }
        if (isSticky) 
        {
            overrideGroundCheck = true;
        }
    }
}
