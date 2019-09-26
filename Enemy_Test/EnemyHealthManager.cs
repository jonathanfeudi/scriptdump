using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    public float health;
    public float currentHealth;

    public float flashLength;
    private float flashCounter;

    private Renderer rend;
    private Color storedColor;

    public EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;

        // Flash White
        rend = GetComponent<Renderer>();
        storedColor = rend.material.GetColor("_Color");

        // KnockBack
        //enemyController = FindObjectOfType<EnemyController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        // Flash
        if (flashCounter > 0)
        {
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                rend.material.SetColor("_Color", storedColor);
            }
        }
    }

    // Take Damage and Flash
    public void HurtEnemy(float damage, float knockBackCounter)
    {
        // Damage
        currentHealth -= damage;

        // Flash
        flashCounter = flashLength;
        rend.material.SetColor("_Color", Color.white);

        // KnockBack
        //enemyController.knockBackCounter = (knockBackCounter);
    }



    /* // Alternative Flash Enemy when Damaged - NOT TESTED //
    public float flashTime;
    Color origionalColor
    public MeshRenderer renderer;
    void Start()
    {
        origionalColor = renderer.color;
    }
    void FlashRed()
    {
        renderer.color = Color.red;
        Invoke("ResetColor", flashTime);
    }
    void ResetColor()
    {
        renderer.color = origionalColor;
    }
    */


}
