using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;


//This script is to be applied to any object with animations that use multiple sets of
//sprite sheets. This is necessary because the built in animation system won't set multiple
//textures.

struct TextureSet
{
    public Texture2D normalDepthMap;
    public Texture2D emissiveMap;
    public Texture2D specMap;
    public Texture2D aoMap;
}

public class SpriteLampTextureSwitcher : MonoBehaviour
{
    Material targetMaterial;
    SpriteRenderer targetRenderer;
    Dictionary<Texture2D, TextureSet> textureLookup;
    Texture2D previousTexture;

    Dictionary<Sprite, LightSet> lightSetLookup;

    public List<Texture2D> primaryTextures; //Contains all the spritesheets that the animations are going to use.
    public List<Texture2D> otherTextures;   //Contains all the other textures (normal maps, etc) corresponding to the above primary textures.



    void Start()
    {
        targetRenderer = GetComponent<SpriteRenderer>();
        targetMaterial = targetRenderer.material;
        if ((targetMaterial == null) || (targetRenderer == null))
        {
            Debug.Log("Error: Sprite Lamp animation script should be applied to objects with both a material component and an animator component.");
        }

        previousTexture = targetRenderer.sprite.texture;


        CreateDictionary();
    }

    void CreateDictionary()
    {
        textureLookup = new Dictionary<Texture2D, TextureSet>();
        foreach (Texture2D mainTexture in primaryTextures)
        {
            TextureSet newSet = new TextureSet();
            int foundTextures = 0;
            foreach (Texture2D thisTexture in otherTextures)
            {
                if ((mainTexture.name + "_NormalDepth") == thisTexture.name)
                {
                    newSet.normalDepthMap = thisTexture;
                    foundTextures++;
                }
                else if ((mainTexture.name + "_Normal") == thisTexture.name)
                {
                    newSet.normalDepthMap = thisTexture;
                    foundTextures++;
                }
                else if ((mainTexture.name + "_Emissive") == thisTexture.name)
                {
                    newSet.emissiveMap = thisTexture;
                    foundTextures++;
                }
                else if ((mainTexture.name + "_Specular") == thisTexture.name)
                {
                    newSet.specMap = thisTexture;
                    foundTextures++;
                }
                else if ((mainTexture.name + "_AO") == thisTexture.name)
                {
                    newSet.aoMap = thisTexture;
                    foundTextures++;
                }
            }
            if (foundTextures > 0)
            {
                textureLookup.Add(mainTexture, newSet);
            }

        }
    }

    void Update()
    {
        Texture2D newTexture = targetRenderer.sprite.texture;

        if (previousTexture != newTexture)
        {
            previousTexture = newTexture;

            if (textureLookup.ContainsKey(newTexture))
            {
                TextureSet thisSet = new TextureSet();
                textureLookup.TryGetValue(newTexture, out thisSet);
                if (thisSet.normalDepthMap != null)
                {
                    targetMaterial.SetTexture("_NormalDepth", thisSet.normalDepthMap);
                }
                if (thisSet.emissiveMap != null)
                {
                    targetMaterial.SetTexture("_EmissiveColour", thisSet.emissiveMap);
                }
                if (thisSet.specMap != null)
                {
                    targetMaterial.SetTexture("_SpecGloss", thisSet.emissiveMap);
                }
                if (thisSet.aoMap != null)
                {
                    targetMaterial.SetTexture("_AmbientOcclusion", thisSet.emissiveMap);
                }
            }
        }
    }
}

namespace Assets.Scripts
{
    class LightSet
    {
    }
}