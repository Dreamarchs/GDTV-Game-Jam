using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRelay : MonoBehaviour
{
    public bool shieldHit;

    // Start is called before the first frame update
    void Start()
    {
        shieldHit = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            shieldHit = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            shieldHit = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
