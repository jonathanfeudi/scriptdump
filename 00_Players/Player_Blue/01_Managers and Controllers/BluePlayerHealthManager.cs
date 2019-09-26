using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlayerHealthManager : MonoBehaviour
{

    public int startingHealth;
    public float currentHealth;

    public float flashLength;
    private float flashCounter;

    private Renderer rend;
    private Color storedColor;

    public BluePlayerController playerController;

    // Start is called before the first frame update
    void Start() // Create Step
    {
        //Health
        currentHealth = startingHealth;

        // Store Color to return to from Flash White
        rend = GetComponent<Renderer>();
        storedColor = rend.material.GetColor("_Color");

        //  Store Self ID - used with KnockBack
        playerController = FindObjectOfType<BluePlayerController>();
    }

    // Update is called once per frame
    void Update() // Step Event
    {
        // Death
        if(currentHealth <= 0)
        {
            //gameObject.SetActive(false);
        }

        // Flash
        if(flashCounter > 0)
        {
            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                rend.material.SetColor("_Color", storedColor);
            }
        }
    }

    // Take Damage 
    public void PlayerGetHurt(float damageAmount)  // Collision Event?
    {
        // Damage
        currentHealth -= damageAmount;

        // Flash
        flashCounter = flashLength;
        rend.material.SetColor("_Color", Color.white);
    }
}
