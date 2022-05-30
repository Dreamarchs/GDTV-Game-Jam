using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerRelay : MonoBehaviour
{

    public Collider2D playerCollider;
    public bool playerTouching;

    // Start is called before the first frame update
    void Start()
    {
        playerTouching = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == playerCollider || collision.gameObject.tag == "Player")
        {
            playerTouching = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == playerCollider || collision.gameObject.tag == "Player")
        {
            playerTouching = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
