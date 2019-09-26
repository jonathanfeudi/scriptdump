using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueBattery2 : MonoBehaviour
{
    public BlueGunController blueBattery2;

    public Image image;

    void Start()
    {
        blueBattery2 = FindObjectOfType<BlueGunController>();

        image = GetComponent<Image>();

    }

    void Update()
    {
        var tempColor = image.color;

        
        // Change Transparency
        if (blueBattery2.barrierCounter == 0)
        {
            tempColor.a = 1f;
        }
        else
        {
            tempColor.a = .25f;
        }
        image.color = tempColor;
    }
}