using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject[] healthBars = new GameObject[10];
    public PlayerHealth playerHealth;
    public Text scoreNumber;
    public Text powerNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreNumber.text = $"{playerHealth.score}";
        powerNumber.text = $"{playerHealth.GetComponent<basicMovement>().power}";
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
