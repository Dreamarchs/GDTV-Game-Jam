using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustDamageThings : MonoBehaviour
{
    public PlayerHealth ph;
    // Start is called before the first frame update
    void Start()
    {
        if (ph == null)
            if (GetComponentInParent<PlayerHealth>())
                ph = GetComponentInParent<PlayerHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<character>()) 
        {
            collision.SendMessage("Damage", ph.SelfAS(1));
        }
        
    }

}
