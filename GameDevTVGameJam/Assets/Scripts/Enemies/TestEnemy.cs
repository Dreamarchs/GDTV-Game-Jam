using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : EnemyBase
{

    public bool runRight;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //This is a basic runner, that'll ignore everything
        base.enemyState = EnemyState.Running;
        base.bodyDamage = true;
        base.doesAttack = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move() 
    {
        if (runRight) 
        {
            if (_rigidBody2D.velocity.x < maxSpeed)
            {
                _rigidBody2D.AddForce(new Vector2(acceleration, 0));
            }
        }
        else 
        {
            if (_rigidBody2D.velocity.x > -maxSpeed)
            {
                _rigidBody2D.AddForce(new Vector2(-acceleration, 0));
            }
        }
        
    }
}
