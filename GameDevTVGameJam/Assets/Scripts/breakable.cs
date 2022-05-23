using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakable : character
{
    public override void Die() 
    {
        gameObject.SetActive(false);
        // TODO: add sound
        // TODO: Generic animation
    }
}
