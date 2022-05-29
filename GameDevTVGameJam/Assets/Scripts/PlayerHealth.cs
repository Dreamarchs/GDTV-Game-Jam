using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : character
{
    private basicMovement playerMove;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (GetComponent<basicMovement>()) 
        {
            playerMove = GetComponent<basicMovement>();
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

    }
    public override void Damage(int dam)
    {
        base.Damage(dam);
        
        //Debug.Log("Player Damaged");
    }

    public override void Damage(AttackStruct attack)
    {
        base.Damage(attack);
        playerMove.Damaged( (transform.position.x < attack.attackTransform.position.x ) );

    }

    public override void Die()
    {
        base.Die();
        playerMove.Died();
    }
    public override void Heal(int heal) 
    {
        base.Heal(heal);
        playerMove.Healed();
    }
}
