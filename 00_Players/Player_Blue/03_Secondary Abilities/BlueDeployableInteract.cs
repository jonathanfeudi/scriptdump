using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDeployableInteract : MonoBehaviour
{
    public BluePlayerController bluePlayer;

    public BlueTurretController deployTurret;

    public BlueBarrierController deployBarrier;

    public float counterCanInteract = 2f;

    public float returnDeployDoubleTapCounter = 0f;

    public AudioClip sfx;

    public AudioSource sfxReturnDeploy;

    public SpriteRenderer _sprite;

    public bool isDetroyed = true;

    public bool sfxReturnDeployTrigger = false;

    public bool touchingBlue = false;

    public bool blueUpgrading = false;

    public int blueUpgradeMeterMAX = 2;

    public float blueUpgradeMeter = 0;

    public int blueUpgradeMeterCounter = 0;

    public int blueUpgradeLevel = 0;

    private int blueUpgradeLevelMAX = 1;

    public int buttonPresses = 0;

    // Particle System
    public ParticleSystem particleEffect; // Particle System
    ParticleSystem.EmissionModule emissionModule;

    public Light particleLight;

    public float particleCountDown;

    private void Awake()
    {
        bluePlayer = GameObject.FindGameObjectWithTag("Blue_Player").GetComponent<BluePlayerController>();

        emissionModule = particleEffect.emission;
    }

    public void Update()
    {
        // Count Down for Sprite Interaction
        if (counterCanInteract > 0)
        {
            //counterCanInteract -= Time.deltaTime;
        }

        // Count Down for Double Tap Return Deploy
        if (returnDeployDoubleTapCounter > 0)
        {
            returnDeployDoubleTapCounter -= Time.deltaTime;
        }

        if (returnDeployDoubleTapCounter < 0)
        {
            returnDeployDoubleTapCounter = 0;
            buttonPresses = 0;
        }

        // In Upgrade State
        if (bluePlayer.GetComponent<Blue_Upgrade_Deployable>().enabled == true & blueUpgrading == true & blueUpgradeLevel < blueUpgradeLevelMAX)
        {
            Debug.Log("UPGRADE_DEPLOYABLE_State");
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                if (blueUpgradeMeter > 1f)  // Fail
                {
                    Debug.Log("UPGRADE FAILED");
                    bluePlayer.GetComponent<BlueAudioManager>().AudioSource_04.Play();
                    bluePlayer.GetComponent<Blue_Upgrade_Deployable>().enabled = false;
                    bluePlayer.GetComponent<Blue_Idle>().enabled = true;
                    blueUpgradeMeterCounter = 0;
                    blueUpgradeMeter = 0;
                    blueUpgrading = false;
                }

                if (blueUpgradeMeter <= 1f & blueUpgrading == true ) // Success
                {
                    Debug.Log("UPGRADE +");
                    blueUpgradeMeterCounter += 1;
                    particleCountDown = .5f;
                    particleEffect.Play(); // Particles
                    particleLight.enabled = true; // Light
                    if (blueUpgradeMeterCounter < 3)
                    {
                        bluePlayer.GetComponent<BlueAudioManager>().AudioSource_02.Play();
                    }
                    blueUpgradeMeter = blueUpgradeMeterMAX;
                }
            }
        }

        // Upgrade Mini Game Meter

        // Double Tap "A" Counter
        if (touchingBlue == true)
        {
            // A Button - Take Back Deploy // Upgrade Deploy
            // NOT IN UPGRADE STATE
            if (blueUpgrading == false)
            {
          
                // Take Back Initiated
                if (Input.GetKeyDown(KeyCode.Joystick1Button0) || (Input.GetKeyDown("space")) & ((buttonPresses <= 1) & (bluePlayer.GetComponent<Blue_Upgrade_Deployable>().enabled = false)))
                {
                    if (deployTurret != null)
                    {
                        if (deployTurret.holding == false)
                        {
                            returnDeployDoubleTapCounter = .35f;
                        }
                    }

                    if (deployBarrier != null)
                    {
                        if (deployBarrier.holding == false)
                        {
                            returnDeployDoubleTapCounter = .35f;
                        }
                    }

                    buttonPresses += 1;

                    // Upgrade Meter Timing 
                    if (blueUpgradeMeter == 0 & blueUpgradeLevel < blueUpgradeLevelMAX)
                    {
                        blueUpgradeMeter = blueUpgradeMeterMAX;
                    }
                }

                // Destroy Object
                if ((buttonPresses == 2) & (returnDeployDoubleTapCounter > 0))
                {
                    //bluePlayer.deploy_1 = Vector3.zero;
                    //bluePlayer.deploy_2 = Vector3.zero;
                    bluePlayer.theGun.barrierCounter -= 1;
                    bluePlayer.theGun.canDeploy = true;
                    buttonPresses = 0;
                    Destroy(gameObject);
                    
                }

                // Upgrade Deployable
                if (bluePlayer.useController == true)
                {
                    if (Input.GetKeyDown(KeyCode.Joystick1Button0) & (blueUpgradeMeter <= 1f) & blueUpgradeLevel < blueUpgradeLevelMAX)
                    {
                        blueUpgrading = true;
                        blueUpgradeMeter = blueUpgradeMeterMAX;
                        blueUpgradeMeterCounter += 1;

                        particleCountDown = .5f;
                        particleEffect.Play(); // Particles
                        particleLight.enabled = true; // Light

                        bluePlayer.GetComponent<BlueAudioManager>().AudioSource_02.Play();
                        bluePlayer.GetComponent<Blue_Idle>().enabled = false;
                        bluePlayer.GetComponent<Blue_Upgrade_Deployable>().enabled = true;
                    }


                }
                
                if (bluePlayer.useController == false)
                {
                    if (Input.GetKeyDown("space") & (blueUpgradeMeter <= 1f))
                    {
                        Debug.Log("SPACE PRESSED");
                        blueUpgrading = true;
                        blueUpgradeMeter = blueUpgradeMeterMAX;
                        blueUpgradeMeterCounter += 1;
                        bluePlayer.GetComponent<BlueAudioManager>().AudioSource_02.Play();
                        bluePlayer.GetComponent<Blue_Idle>().enabled = false;
                        bluePlayer.GetComponent<Blue_Upgrade_Deployable>().enabled = true;
                    }
                }
            }

            // Reset Upgrading Variable
            // Cancel Upgrade
            if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                bluePlayer.GetComponent<BlueAudioManager>().AudioSource_04.Play();
                bluePlayer.GetComponent<Blue_Upgrade_Deployable>().enabled = false;
                bluePlayer.GetComponent<Blue_Idle>().enabled = true;
                blueUpgradeMeterCounter = 0;
                blueUpgradeMeter = 0;
                blueUpgrading = false;
            }

            // Upgrade Meter Countdown
            if (blueUpgradeMeter > 0)
            {
                blueUpgradeMeter -= Time.deltaTime;
            }

            if (blueUpgradeMeter < 0)
            {
                blueUpgradeMeterCounter = 0;
                blueUpgradeMeter = 0f;
                bluePlayer.GetComponent<Blue_Upgrade_Deployable>().enabled = false;
                bluePlayer.GetComponent<Blue_Idle>().enabled = true;
            }
        }

        // 3 PRESSES to UPGRADE
        if (blueUpgradeMeterCounter == 3)
        {
            emissionModule.rateOverTime = 100f;
            blueUpgrading = false;
            bluePlayer.GetComponent<BlueAudioManager>().AudioSource_03.Play();
            blueUpgradeMeterCounter = 0;
            blueUpgradeMeter = 0f;
            blueUpgradeLevel += 1;
            bluePlayer.GetComponent<Blue_Upgrade_Deployable>().enabled = false;
            bluePlayer.GetComponent<Blue_Idle>().enabled = true;
        }

        // UPGRADED
        if (blueUpgradeLevel > 0)
        {
            if (deployTurret != null)
            {
                deployTurret.upgradeLevel = blueUpgradeLevel;
            }

            if (deployBarrier != null)
            {
                deployBarrier.upgradeLevel = blueUpgradeLevel;
            }
        }

        // Particle Effects
        if (particleCountDown > 0)
        {
            particleCountDown -= Time.deltaTime;
        }

        if (particleCountDown <= 0)
        {
            particleEffect.Stop(); // Particles
            particleLight.enabled = false; // Light
        }
    }

    //Detect collisions between the GameObjects with Colliders attached
    private void OnTriggerStay(Collider other)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (other.name == "Player_Blue" & counterCanInteract <= 0 )
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            //Debug.Log("Do something here");

            if (deployTurret != null)
            {
                if (deployTurret.holding == false)
                {
                    touchingBlue = true;

                    _sprite.GetComponent<SpriteRenderer>().enabled = true;
                }
            }

            if (deployBarrier != null)
            {
                if (deployBarrier.holding == false)
                {
                    touchingBlue = true;

                    _sprite.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (other.name == "Player_Blue")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            //Debug.Log("Do something here");

            blueUpgradeMeter = 0f;

            blueUpgradeMeterCounter = 0;

            touchingBlue = false;

            _sprite.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Disarm On Destroy Event
    void OnApplicationQuit()
    {
        isDetroyed = false;
    }

    void OnDestroy()
    {
        if (isDetroyed == true)
        {
            Destroy(transform.parent.gameObject);

            // Fix Double Tap at Perfect Bug Making Moment
            bluePlayer.GetComponent<Blue_Upgrade_Deployable>().enabled = false;
            bluePlayer.GetComponent<Blue_Idle>().enabled = true;
        }
    }



}