using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject[] healthBars = new GameObject[10];
    public PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < healthBars.Length; i++) 
        {
            if (playerHealth.visibleHealth < i) 
            {
                healthBars[i].SetActive(false);
            }
            else 
            {
                healthBars[i].SetActive(true);
            }
        }
        
    }
}
