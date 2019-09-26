using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtBox : MonoBehaviour
{
    // Camera 
    public Camera mainCamera;

    public int damageToGive;

    public AudioClip SFX;

    // Can Damage

    public bool canDamagePlayer;

    // Collider

    Collider other;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();

        canDamagePlayer = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("DAMAGED_State");

        if (other.gameObject.tag == "Blue_Barrier")
        {
            canDamagePlayer = false;
            this.other = other;
        }

        if (other.gameObject.tag == "Blue_Player")
        {
            if (canDamagePlayer == true)
            {
                // Calculate Direction to Knock Back Player
                Vector3 _direction = other.transform.position - transform.position;
                _direction = _direction.normalized;

                // Damage to Give Player
                other.gameObject.GetComponent<BluePlayerHealthManager>().PlayerGetHurt(damageToGive);

                // Camera Shake  ///////////////////// CALLS ANOTHER SCRIPT FUNCTION
                mainCamera.GetComponent<CameraFollow>().ShakeIt();

                // Play DMG SFX
                //AudioSource.PlayClipAtPoint(SFX, transform.position);

                // Knock Back Player
                other.GetComponent<Blue_Damaged>().enabled = true;
                other.GetComponent<Blue_Damaged>().knockBackCounter = other.GetComponent<Blue_Damaged>().knockBackTime;
                other.GetComponent<Blue_Damaged>().direction = _direction;

                // Turn Off Cand Damage Player as to not damaged repeatedly
                canDamagePlayer = false;
            }
            
        }
    }

    void Update()
    {
        if (canDamagePlayer == false && !other)
        {
            canDamagePlayer = true;
        }
    }
}
