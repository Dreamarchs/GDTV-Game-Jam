using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable : character
{
    public AudioClip breakingSound;
    public AudioSource AS;

    public bool spawnsItem; // If it spawns an item
    public GameObject DropItem; // The item to spawn
    public Transform itemSpawn; // Location to spawn it

    public override void Damage(AttackStruct attack)
    {
        base.Damage(attack);
        //Debug.Log($"damaged {dam}");
    }

    public override void Start()
    {
        base.Start();

        GameObject cam = GameObject.Find("Main Camera");

        if (AS == null)
        {
            if (cam.GetComponent<AudioSource>())
                AS = cam.GetComponent<AudioSource>();
        }
    }

    public override void Die() 
    {
        AS.PlayOneShot(breakingSound);

        if (spawnsItem) 
        {
            if (DropItem)
                GameObject.Instantiate(DropItem, itemSpawn.position, itemSpawn.rotation);
        }

        gameObject.SetActive(false);
        // TODO: Generic animation
    }
}
