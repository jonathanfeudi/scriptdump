using UnityEngine;

public class SwapNormals : MonoBehaviour
{
    public Texture2D NormalMapTexture;
    public Texture2D NormalMapTexture_01;
    public Texture2D NormalMapTexture_02;
    public Texture2D NormalMapTexture_03;
    public Texture2D NormalMapTexture_04;



    // Use this for initialization
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.material.EnableKeyword("_NORMALMAP");

        //Texture2D _sprite = spriteRenderer.material.mainTexture;

        //meshRenderer.material.GetTexture("_MainTex")


    }


    void Update()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        //spriteRenderer.material.GetTexture("_MainTex");

        spriteRenderer.material.EnableKeyword("_NORMALMAP");


        if (spriteRenderer.sprite.name == "Blue_Idle_0" 
            || spriteRenderer.sprite.name == "Blue_Idle_1" 
            || spriteRenderer.sprite.name == "Blue_Idle_2"
            || spriteRenderer.sprite.name == "Blue_Idle_3"
            || spriteRenderer.sprite.name == "Blue_Idle_4"
            || spriteRenderer.sprite.name == "Blue_Idle_5")      
        {
            spriteRenderer.material.SetTexture("_BumpMap", NormalMapTexture);
        }

        if (spriteRenderer.sprite.name == "Blue_Run_0_South_0"
            || spriteRenderer.sprite.name == "Blue_Run_0_South_1"
            || spriteRenderer.sprite.name == "Blue_Run_0_South_2"
            || spriteRenderer.sprite.name == "Blue_Run_0_South_3"
            || spriteRenderer.sprite.name == "Blue_Run_0_South_4"
            || spriteRenderer.sprite.name == "Blue_Run_0_South_5")
        {
            spriteRenderer.material.SetTexture("_BumpMap", NormalMapTexture_01);
        }

        if (spriteRenderer.sprite.name == "Blue_Run_2_East_0"
            || spriteRenderer.sprite.name == "Blue_Run_2_East_1"
            || spriteRenderer.sprite.name == "Blue_Run_2_East_2"
            || spriteRenderer.sprite.name == "Blue_Run_2_East_3"
            || spriteRenderer.sprite.name == "Blue_Run_2_East_4"
            || spriteRenderer.sprite.name == "Blue_Run_2_East_5")
        {
            spriteRenderer.material.SetTexture("_BumpMap", NormalMapTexture_02);
        }

        if (spriteRenderer.sprite.name == "Blue_Run_4_North_0"
            || spriteRenderer.sprite.name == "Blue_Run_4_North_1"
            || spriteRenderer.sprite.name == "Blue_Run_4_North_2"
            || spriteRenderer.sprite.name == "Blue_Run_4_North_3"
            || spriteRenderer.sprite.name == "Blue_Run_4_North_4"
            || spriteRenderer.sprite.name == "Blue_Run_4_North_5")
        {
            spriteRenderer.material.SetTexture("_BumpMap", NormalMapTexture_03);
        }

        if (spriteRenderer.sprite.name == "Blue_Run_6_West_0"
            || spriteRenderer.sprite.name == "Blue_Run_6_West_1"
            || spriteRenderer.sprite.name == "Blue_Run_6_West_2"
            || spriteRenderer.sprite.name == "Blue_Run_6_West_3"
            || spriteRenderer.sprite.name == "Blue_Run_6_West_4"
            || spriteRenderer.sprite.name == "Blue_Run_6_West_5")
        {
            spriteRenderer.material.SetTexture("_BumpMap", NormalMapTexture_04);
        }
    }
}
