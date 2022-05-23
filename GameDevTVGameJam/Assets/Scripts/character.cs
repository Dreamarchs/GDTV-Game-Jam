using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    public int startHP = 10; // The hp it starts with.  Hiding the other HP stuff to just make sure we dont mess up in editor
    int HP; // Health the character has
    int maxHP; // Maximum Health the character has
    public bool isPlayer; // Whether the character is friendly to the player or not

    public void Start()
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

    public bool getPlayer() { return isPlayer; }

    public void Damage(int dam) 
    {
        HP -= Mathf.Abs(dam);
        DeathCheck();
        Debug.Log($"damaged {dam}");
    }

    public void Heal(int heal) 
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

}