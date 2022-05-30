using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : character
{
    private basicMovement playerMove;
    public int score;

    private AudioSource AS;

    public AudioClip healFanfare;
    public AudioClip scoreItemFanfare;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (GetComponent<basicMovement>()) 
        {
            playerMove = GetComponent<basicMovement>();
        }
        if (GameObject.Find("Main Camera")) 
        {
            GameObject cam = GameObject.Find("Main Camera");
            if (cam.GetComponent<AudioSource>()) 
            {
                AS = cam.GetComponent<AudioSource>();
            }
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
        AS.PlayOneShot(healFanfare);
        playerMove.Healed();

    }

    public void AddScore(int _score) 
    {
        AS.PlayOneShot(scoreItemFanfare);
        score += Mathf.Abs(_score);
    }
}
