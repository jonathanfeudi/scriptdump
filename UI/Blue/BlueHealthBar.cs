using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueHealthBar : MonoBehaviour
{
    private RectTransform rt;

    public BluePlayerHealthManager blueCurrentHealth;

    public Image image;

    public float hpScale;


    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        
        blueCurrentHealth = FindObjectOfType<BluePlayerHealthManager>();

        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        hpScale = (float)blueCurrentHealth.currentHealth / 100;

        rt.localScale = new Vector3(hpScale, 1, 1);


        var tempColor = image.color;

        tempColor.r = ((float)blueCurrentHealth.currentHealth / 100);
        //tempColor.g = ((float)blueCurrentHealth.currentHealth / 100)/2;
        //tempColor.b = -((float)blueCurrentHealth.currentHealth / 100)/2;

        image.color = tempColor;

    }
}
