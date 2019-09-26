using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueHeal : MonoBehaviour
{
    public BluePlayerController blueHealControl;

    public Image image;

    public int heal_ID;

    void Start()
    {
        blueHealControl = FindObjectOfType<BluePlayerController>();

        image = GetComponent<Image>();

    }

    void Update()
    {
        var tempColor = image.color;

        // Change Transparency
        if (blueHealControl.blueHeals >= heal_ID)
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