using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueHealthDisplay : MonoBehaviour
{
    public Text blueHealthText;

    public BluePlayerHealthManager blueCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        //  Store Self ID - used with KnockBack
        blueCurrentHealth = FindObjectOfType<BluePlayerHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        blueHealthText.text = "HP. "; // + blueCurrentHealth.currentHealth;
    }
}
