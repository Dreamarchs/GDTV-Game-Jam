using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    public int startHP = 10; // The hp it starts with.  Hiding the other HP stuff to just make sure we dont mess up in editor
    int HP; // Health the character has
    int maxHP; // Maximum Health the character has
    public bool isPlayer; // Whether the character is friendly to the player or not
    public int visibleHealth;

    public virtual void Start()
    {
        //Making sure things start alive, at minimum
        if (startHP < 1)
        {
            startHP = 1;
        }
        //Initializing HP with start HP
        HP = startHP;
        maxHP = startHP;
    }

    public virtual void Update() 
    {
        visibleHealth = HP;
    }

    public bool getPlayer() { return isPlayer; }

    public virtual void Damage(int dam) 
    {
        HP -= Mathf.Abs(dam);
        DeathCheck();
        //Debug.Log($"damaged {dam}");
    }

    public virtual void Damage(AttackStruct attack)
    {
        Damage(attack.damage);
        //Debug.Log($"damaged {dam}");
    }

    public virtual  void Heal(int heal) 
    {
        HP += Mathf.Abs(heal);

        if (HP > maxHP)
        {
            HP = maxHP;
        }

    }

    //
    //ABOUT: Check if character has died
    //
    public void DeathCheck() 
    {
        if (HP <= 0)
        {
            Die();
        }
    }

    public virtual void Die() // Should be overwritten depending on enemy death animation stuff
    { 
    
    }

    public virtual AttackStruct SelfAS() 
    {
        return new AttackStruct(gameObject);
    }
    public virtual AttackStruct SelfAS(int damage)
    {
        return new AttackStruct(damage, gameObject);
    }

    public virtual AttackStruct SelfAS(int damage, Vector2 velocity)
    {
        return new AttackStruct(damage, gameObject, velocity);
    }

}


public struct AttackStruct
{
    public int damage;
    public GameObject attacker;
    public GameObject source;
    public Transform attackTransform;
    public Vector2 velocity;

    public AttackStruct(GameObject a)
    {
        damage = 1;
        attacker = a;
        source = a;
        attackTransform = a.transform;
        velocity = new Vector2(0, 0);
    }

    public AttackStruct(int d, GameObject a) 
    {
        damage = d;
        attacker = a;
        source = a;
        attackTransform = a.transform;
        velocity = new Vector2(0, 0);
    }
    public AttackStruct(GameObject a, Vector2 vel)
    {
        damage = 1;
        attacker = a;
        source = a;
        attackTransform = a.transform;
        velocity = vel;
    }
    public AttackStruct(int d, GameObject a, Vector2 vel)
    {
        damage = d;
        attacker = a;
        source = a;
        attackTransform = a.transform;
        velocity = vel;
    }
}