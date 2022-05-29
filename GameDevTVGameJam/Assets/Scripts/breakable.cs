using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable : character
{
    public AudioClip breakingSound;
    public AudioSource AS;

    public override void Damage(AttackStruct attack)
    {
        base.Damage(attack);
        //Debug.Log($"damaged {dam}");
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Die() 
    {
        AS.PlayOneShot(breakingSound);
        gameObject.SetActive(false);
        // TODO: add sound
        
        // TODO: Generic animation
    }
}
