using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazzard_Laser : MonoBehaviour
{
    // Player Object to Ignore
    public BluePlayerController player_Blue;

    public bool canDamage;

    // Fire Point Origin
    public Transform firePoint;

    // Laser Trigger
    public bool isFiringLaser;

    // Player SFX Controller
    public PlayMultipleSFX sfx_AudioSource;

    // Laser Rendering and Particle Effects
    public LineRenderer lineRenderer; // Line Render

    public ParticleSystem impactEffect; // Particle System

    public Light impactLight;

    public float damageToGive = 5f;

    // Check if Player is Left or Right of Laser
    public checkObjectLeftOrRight leftOrRight;
    

    //======================================

    void Start()
    {
        // Audio Sources
        sfx_AudioSource = GetComponent<PlayMultipleSFX>();

        // Is Firing On
        isFiringLaser = true;

        // DMG to Give
        damageToGive = 5f;

        // Check if Player is LEft or Right
        leftOrRight = GetComponent<checkObjectLeftOrRight>();

        // Can Damage
        canDamage = player_Blue.canBeDamaged;

    }

    //======================================

    void Update() // Laser Effects and Alt Fire   // Update is called once per frame
    {
        if (isFiringLaser)
        {
            // Laser Graphics
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
                impactEffect.Play(); // Particles
                impactLight.enabled = true; // Light
                sfx_AudioSource.AudioSource_01.Play();
                sfx_AudioSource.AudioSource_02.Play();
            }

            // Rotate Direcion Particles Face
            Vector3 dir = firePoint.position - transform.forward;  // Vector with direction facing Player
            impactEffect.transform.rotation = Quaternion.LookRotation(dir); // Rotate Particle Collide                       
        }
        else // Turn Off Laser and Effects
        {
            lineRenderer.enabled = false;
            impactEffect.Stop(); // Particles
            impactLight.enabled = false; // Particle Lighting 
            sfx_AudioSource.AudioSource_01.Stop();
            sfx_AudioSource.AudioSource_02.Stop();
        }

        canDamage = player_Blue.canBeDamaged;
    }

    //======================================

    void LateUpdate() // Laser // THIS IS WHERE TO FIX THE LIGHT REMAINING WHEN NOT SHOOTING
    {

        // Laser Raycast Collision
        lineRenderer.SetPosition(0, firePoint.position);

        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, transform.forward, out hit, 5000)) /////////////////// 12 is max distance  ********************MAKE SEPERATE RAYCAST FOR EACH BARRIER AMOUNT CONDITION
        {
            lineRenderer.SetPosition(1, hit.point);
            impactEffect.transform.position = hit.point; // Particle Collision Location of End of Laser

            if (hit.collider.tag == "Blue_Player" & canDamage)  
            {
                // Calculate Direction to Knock Back Player
                if (leftOrRight.dirNum < 1)
                {
                    Vector3 _direction = Vector3.left;
                    _direction = _direction.normalized;

                    Debug.Log(_direction);

                    // Damage to Give Player
                    hit.collider.GetComponent<BluePlayerHealthManager>().PlayerGetHurt(damageToGive);

                    // Camera Shake  ///////////////////// CALLS ANOTHER SCRIPT FUNCTION
                    //mainCamera.GetComponent<CameraFollow>().ShakeIt();

                    // Knock Back Player
                    hit.collider.GetComponent<Blue_Damaged>().enabled = true;
                    hit.collider.GetComponent<Blue_Damaged>().knockBackForce = 10;
                    hit.collider.GetComponent<Blue_Damaged>().knockBackCounter = hit.collider.GetComponent<Blue_Damaged>().knockBackTime;
                    hit.collider.GetComponent<Blue_Damaged>().direction = _direction;
                }
                else
                {
                    Vector3 _direction = Vector3.right;
                    _direction = _direction.normalized;

                    Debug.Log(_direction);

                    // Damage to Give Player
                    hit.collider.GetComponent<BluePlayerHealthManager>().PlayerGetHurt(damageToGive);

                    // Camera Shake  ///////////////////// CALLS ANOTHER SCRIPT FUNCTION
                    //mainCamera.GetComponent<CameraFollow>().ShakeIt();

                    // Knock Back Player
                    hit.collider.GetComponent<Blue_Damaged>().enabled = true;
                    hit.collider.GetComponent<Blue_Damaged>().knockBackForce = 10;
                    hit.collider.GetComponent<Blue_Damaged>().knockBackCounter = hit.collider.GetComponent<Blue_Damaged>().knockBackTime;
                    hit.collider.GetComponent<Blue_Damaged>().direction = _direction;
                }
            }
        }
    }
}