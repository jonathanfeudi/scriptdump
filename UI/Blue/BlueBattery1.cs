using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueBattery1 : MonoBehaviour
{
    public BlueGunController blueBattery1;

    public Image image;

    void Start()
    {
        blueBattery1 = FindObjectOfType<BlueGunController>();

        image = GetComponent<Image>();

    }

    void Update()
    {
        var tempColor = image.color;

        // Change Transparency
        if (blueBattery1.barrierCounter < 2)
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