using UnityEngine;

using System.Collections;

public class BlueGunController : MonoBehaviour
{
    [Header("Initialized Variables")]

    // Player Controller
    public BluePlayerController bluePlayer;

    // Laser Trigger
    public bool isFiringLaser;

    public bool altFire;

    // Blues Fire Point Origin
    public Transform firePoint;

    // Blues Orb Point Origin
    public Transform orbPoint;

    // Blues Deply Barrier Point
    public Transform barrierDeployPoint;

    // Blues Deply Turret Point
    public Transform turretDeployPoint;

    [Header("Laser Variables")]

    public int damageOverTime = 10;

    // Player SFX Controller
    private BlueGunSoundSFX sfx_AudioSource;

    private bool laserCollider = false;

    // Orbs 
    public BlueOrbController orb;  
    public float orbSpeed;

    public bool canOrb = true;

    public float timeBetweenShots;
    private float shotCounter;

    public Transform target; // Enemy Point Origin 

     // Laser Rendering and Particle Effects
    public LineRenderer lineRenderer; // Line Render

    // Laser Reset Point
    public Transform laserResetPoint2bats;
    public Transform laserResetPoint1bats;
    public Transform laserResetPoint0bats;

    private float maxDistance;

    public ParticleSystem impactEffect; // Particle System

    public Light impactLight;
    //public Light handLight;

    // Blue Deployables
    public int barrierCounter;  // Counter for both Turret and Barrier
    public bool canDeploy;

    // Blue Barrier
    public BlueBarrierController barrier;
    public bool canHoldBarrier;
    public bool deployBarrier;

    // Blue Turret
    public BlueTurretController turret;
    public bool canHoldTurret;
    public bool deployTurret;

    //======================================

    // Start is called before the first frame update (Create Event?)
    void Start()
    {
        altFire = false;

        canDeploy = true;

        canHoldBarrier = false;

        // Audio Sources
        sfx_AudioSource = GetComponent<BlueGunSoundSFX>();
    }

    //======================================

    // USE Primary Fire //

    // Laser Effects

    void Update() // Laser Effects and Alt Fire   // Update is called once per frame
    {
        // Max Laser Distance Raycast Var

        // 2Bats = Full
        if (barrierCounter == 0)
        {
            maxDistance = 12;
        }

        // 1Bat = 1/2
        if (barrierCounter == 1)
        {
            maxDistance = 9;
        }

        // 0Bats = 0...duh
        if (barrierCounter == 2)
        {
            maxDistance = 7;
        }

        //======================================

        if (isFiringLaser && !altFire)
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
            Vector3 dir = firePoint.position - transform.forward * 10 + transform.position;  // Vector with direction facing Player
            impactEffect.transform.rotation = Quaternion.LookRotation(dir); // Rotate Particle Collide                       
        }
        else // Turn Off Laser and Effects
        {
            TurnOffLaser();
        }

        //======================================

        // ALTERNATE FIRE
        if (isFiringLaser && altFire) // Charge Shot Alt 
        {
            if (bluePlayer.useController == true)
            {
                if (canOrb == true & Input.GetKeyDown(KeyCode.Joystick1Button7))
                {
                    BlueOrbController newOrb = Instantiate(orb, orbPoint.position, orbPoint.rotation) as BlueOrbController;

                    canOrb = false;
                }
            }

            if (bluePlayer.useController == false)
            {
                if (canOrb == true & Input.GetMouseButtonDown(0))
                {
                    BlueOrbController newOrb = Instantiate(orb, orbPoint.position, orbPoint.rotation) as BlueOrbController;

                    canOrb = false;
                }
            }
        }

        //======================================

        // USE ABILITIES //

        // Barrier
        if (canHoldBarrier == true && barrierCounter < 2 && canDeploy) 
        {
            BlueBarrierController newBarrier = Instantiate(barrier, barrierDeployPoint.position, barrierDeployPoint.rotation) as BlueBarrierController;

            canHoldBarrier = false;

            canDeploy = false;

            barrierCounter += 1;

            // Turn Off Laser
            TurnOffLaser();

            isFiringLaser = false;

            // Turn Off Blue Can Shoot
            bluePlayer.canShoot = false;

            // Turn Off Can Orb
            canOrb = false;
        }

        // Turret
        if (canHoldTurret == true && barrierCounter < 2 && canDeploy) 
        {
            BlueTurretController newTurret = Instantiate(turret, turretDeployPoint.position, turretDeployPoint.rotation) as BlueTurretController;

            canHoldTurret = false;

            canDeploy = false;

            barrierCounter += 1;

            // Turn Off Laser
            TurnOffLaser();

            isFiringLaser = false;

            // Turn Off Blue Can Shoot
            bluePlayer.canShoot = false;

            // Turn Off Can Orb
            canOrb = false;
        }
    }

    //======================================

    void LateUpdate() // Laser // THIS IS WHERE TO FIX THE LIGHT REMAINING WHEN NOT SHOOTING // Damages Enemy
    {
        if (!altFire)
        {
            // Laser Raycast Collision
            lineRenderer.SetPosition(0, firePoint.position);
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, transform.forward, out hit, maxDistance)) /////////////////// 12 is max distance  ********************MAKE SEPERATE RAYCAST FOR EACH BARRIER AMOUNT CONDITION
            {
                //laserCollider = false;

                if (hit.collider)
                {
                    lineRenderer.SetPosition(1, hit.point);
                    impactEffect.transform.position = hit.point; // Particle Collision Location of End of Laser
                }

                // Laser Damage Enemy
                if (hit.collider.tag == "Enemy")
                {
                    //Debug.Log(hit.collider.tag);

                    if (isFiringLaser)
                    {
                        hit.collider.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageOverTime * Time.deltaTime, 0);
                    }
                }
            }
            else
            {
                // 2Bats = Full
                if (barrierCounter == 0)
                {
                    impactEffect.transform.position = laserResetPoint2bats.transform.position;
                }

                // 1Bat = 1/2
                if (barrierCounter == 1)
                {
                    impactEffect.transform.position = laserResetPoint1bats.transform.position;
                }

                // 0Bats = 0...duh
                if (barrierCounter == 2)
                {
                    impactEffect.transform.position = laserResetPoint0bats.transform.position;
                }

                lineRenderer.SetPosition(1, impactEffect.transform.position);
            }
        }
    }

    void TurnOffLaser()
    {
        // Turn Off Laser
        lineRenderer.enabled = false;
        impactEffect.Stop(); // Particles
        impactLight.enabled = false; // Particle Lighting 
        sfx_AudioSource.AudioSource_01.Stop();
        sfx_AudioSource.AudioSource_02.Stop();

        laserCollider = false;
    }
}

