using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
     * First we are going to create this base form its going to hold everything that all 3 forms have
     * nothing else
     * 3 abilities mapped out to the same buttons
     * these abilities have cooldowns
     * 
     * 
     */

public class PlayerFormBase : MonoBehaviour
{
    // have these gameobjects here for use later to do what we want with the abilities like activate them
    public GameObject firstAbility;
    public GameObject secondAbility;
    public GameObject thirdAbility;

    public float firstCooldown;
    public float secondCooldown;
    public float thirdCooldown;

    private bool firstCanUse;
    private bool secondCanUse;
    private bool thirdCanUse;

    public KeyCode firstKey;
    public KeyCode secondKey;
    public KeyCode thirdKey;
    // Start is called before the first frame update
    void Start()
    {
        firstCanUse = true;
        secondCanUse = true;
        thirdCanUse = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(firstKey) && firstCanUse)
        {
            //use first ability
            //start the coroutine for the cooldown
            firstCanUse = false;
            StartCoroutine(startFirstCooldown(firstCooldown));
            print("First Ability Used");
        }
        
        if (Input.GetKeyDown(secondKey) && secondCanUse)
        {
            //use first ability
            //start the coroutine for the cooldown
            secondCanUse = false;
            StartCoroutine(startSecondCooldown(secondCooldown));
            print("Second Ability Used");
        }
        
        if (Input.GetKeyDown(thirdKey) && thirdCanUse)
        {
            //use first ability
            //start the coroutine for the cooldown
            thirdCanUse = false;
            StartCoroutine(startThirdCooldown(thirdCooldown));
            print("Third Ability Used");
        }

        IEnumerator startFirstCooldown(float cooldown)
        {
            yield return new WaitForSeconds(cooldown);
            firstCanUse = true;
        }
        
        IEnumerator startSecondCooldown(float cooldown)
        {
            yield return new WaitForSeconds(cooldown);
            secondCanUse = true;
        }
        
        IEnumerator startThirdCooldown(float cooldown)
        {
            yield return new WaitForSeconds(cooldown);
            thirdCanUse = true;
        }
        
    }
}
