using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    /// <summary>
    /// This code is basically just something that checks all the grounding on it's own and will tell the enemy, to prevent issues with midair stuff.
    /// </summary>
    public bool ground;
    public Collider2D col;

    public EnemyBase enemy;

    // Start is called before the first frame update
    void Start()
    {
        if (col == null)
        {
            col = gameObject.GetComponent<Collider2D>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ground = true;
        enemy.SendMessage("SetGrounded", ground);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ContactFilter2D cf = new ContactFilter2D();
        List<Collider2D> tempList = new List<Collider2D>();
        col.OverlapCollider(cf.NoFilter(), tempList);
        if (tempList.Count == 0) 
        {
            ground = false;
            
        }
        enemy.SendMessage("SetGrounded", ground);
    }
}
