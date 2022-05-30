using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public ItemBehaviour itemBehaviour;
    public int value;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Break() 
    {
        Destroy(gameObject);
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject GO = collision.gameObject;
        Debug.Log("touch player");
        if (GO.CompareTag("Player"))
        {
            switch (itemBehaviour)
            {
                case ItemBehaviour.Nothing: Break(); break;
                case ItemBehaviour.Score: GO.SendMessage("AddScore", value); Break(); break;
                case ItemBehaviour.Heal: GO.SendMessage("Heal", value); Break(); break;
                case ItemBehaviour.Damage: GO.SendMessage("Damage", value); Break(); break;
                case ItemBehaviour.Upgrade: GO.SendMessage("Upgrade"); Break(); break;
                case ItemBehaviour.Power: GO.SendMessage("AddPower", value); Break(); break;
            }
        }
        else 
        {
            if (GO.CompareTag("Enemy"))
                Break();
        }
    }

}

public enum ItemBehaviour 
{
    Nothing,
    Score,
    Heal,
    Damage,
    Upgrade,
    Power
}
