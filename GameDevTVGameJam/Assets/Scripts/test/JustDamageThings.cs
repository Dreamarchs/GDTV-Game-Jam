using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustDamageThings : MonoBehaviour
{
    public PlayerHealth ph;
    public basicMovement bm;
    public EnemyBase eb;
    // Start is called before the first frame update
    void Start()
    {
        if (ph == null)
            if (GetComponentInParent<PlayerHealth>())
                ph = GetComponentInParent<PlayerHealth>();
        if (bm == null)
            if (GetComponentInParent<basicMovement>())
                bm = GetComponentInParent<basicMovement>();
        if (eb == null)
            if (GetComponentInParent<EnemyBase>())
                eb = GetComponentInParent<EnemyBase>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (ph && bm)
            if (collision.GetComponent<character>()) 
            {
                collision.SendMessage("Damage", ph.SelfAS(bm.power));
            }
        else if (eb)
                if (collision.GetComponent<character>())
                    collision.SendMessage("Damage", eb.SelfAS());
    }

}
