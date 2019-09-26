using UnityEngine;

using System.Collections;

public class BlueBulletController : MonoBehaviour { 

    public float speed;

    public float lifeTime;

    public int damageToGive;

    public int knockBackCounter;

    //public BluePlayerController bluePlayer;

    //public BlueGunController blueGun;

    //private bool fired;

    private Rigidbody myRigidbody;
    public Vector3 moveInput;
    public Vector3 moveVelocity;

    // Scaling Limit
    //public Vector3 maxLocalScale;
    //float maxlocalScaleMagnitude;

    void Awake() // INITIALIZE VARIABLES
    {
        //blueGun = GameObject.FindGameObjectWithTag("Blue_Gun").GetComponent<BlueGunController>();

        //bluePlayer = GameObject.FindGameObjectWithTag("Blue_Player").GetComponent<BluePlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {

        myRigidbody = GetComponent<Rigidbody>();

        knockBackCounter = 1;
        //fired = false;

        //maxLocalScale = new Vector3(2, 1, 2);
        //maxlocalScaleMagnitude = maxLocalScale.magnitude;

        Physics.IgnoreLayerCollision(9,12); //////////
    }

    // Update is called once per frame
    void Update()
    {/*
        if (blueGun.isFiringLaser == true && bluePlayer.canShoot == false && fired == false)
        {
            transform.position = blueGun.firePoint.position; // Lock to Blue while Chargine

            float actualLocalScaleMagnitude = transform.localScale.magnitude;
            if (actualLocalScaleMagnitude < maxlocalScaleMagnitude)
            {
                transform.localScale += new Vector3(0.005F, 0, 0.005F);
            }          
        }
        
        if (blueGun.isFiringLaser == false)
        {
            fired = true;
            
        }

        if (fired == true)
        {

             //moveInput = new Vector3(Input.GetAxisRaw("RHorizontal"), 0f, -Input.GetAxisRaw("RVertical")); // CONTROL Orb
             //moveVelocity = moveInput * speed; // CONTROL BULLET

            moveVelocity = transform.forward * speed;  // Shoot Orb

            lifeTime -= Time.deltaTime;  // Destroy over time

        }

        if (lifeTime <= 0)
        {
            bluePlayer.canShoot = true;
            Destroy(gameObject);
        }

        // DESTROY IF BLUE ENTERS PARTICULAR STATES
        if (bluePlayer.GetComponent<Blue_Warp>().enabled)
        {
            //bluePlayer.canShoot = true;
            //Destroy(gameObject);
        }*/
    }

    void OnCollisionEnter(Collision other)  // Collides with Enemy and Wall
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageToGive, knockBackCounter);
            //bluePlayer.canShoot = true;
            //fired = false;
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "OUTOFBOUNDS")
        {
            //bluePlayer.canShoot = true;
            //fired = false;
            Destroy(gameObject);
        }

        //if (other.gameObject.layer == "Blue_Player") // NEED TO CHANGE - Set to Layer of Players
        {
            // Physics.IgnoreCollision(bluePlayer.GetComponent<Collider>(),  GetComponent<Collider>());  // By Tag

            
        }
    }

    

    // Move the Rigid Body
    void FixedUpdate()
    {
        myRigidbody.velocity = moveVelocity;
    }
}
