using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTransparency : MonoBehaviour
{

    public SpriteRenderer color;
    
    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //SpriteRenderer.color = new Color(1f, 1f, 1f, .5f);
    }
}
