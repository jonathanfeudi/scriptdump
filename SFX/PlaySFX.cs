using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    public AudioSource SFX;

    // Start is called before the first frame update
    void Start()
    {
        SFX = GetComponent<AudioSource>();

        SFX.Play();
    }
}



