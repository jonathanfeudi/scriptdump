using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueAltFire : MonoBehaviour
{
    public BlueGunController blueAltFire;

    public Image image;

    void Start()
    {
        blueAltFire = FindObjectOfType<BlueGunController>();

        image = GetComponent<Image>();

    }

    void Update()
    {
        var tempColor = image.color;

        // Change Transparency
        if (blueAltFire.altFire == true)
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