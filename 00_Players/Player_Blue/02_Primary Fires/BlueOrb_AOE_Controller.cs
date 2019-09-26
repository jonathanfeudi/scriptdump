using UnityEngine;

using System.Collections;

public class BlueOrb_AOE_Controller : MonoBehaviour { 

    public float speed;

    public float lifeTime;

    public float damageToGive;

    public int knockBackCounter;

    public BluePlayerController bluePlayer;

    public BlueGunController blueGun;

    private bool fired;

    private Rigidbody myRigidbody;

    //public Vector3 moveInput;
    public Vector3 _direction;
    public Vector3 moveVelocity;

    // Scaling Limit
    public Vector3 maxLocalScale;
    float maxlocalScaleMagnitude;

    // Arc FX
    //public GameObject arcPrefab;

    //public Vector3 startPosition;
    //public Vector3 endPosition;

    void Awake() // INITIALIZE VARIABLES
    {
        bluePlayer = GameObject.FindGameObjectWithTag("Blue_Player").GetComponent<BluePlayerController>();

        blueGun = GameObject.FindGameObjectWithTag("Blue_Gun").GetComponent<BlueGunController>();
    }

    // Start is called before the first frame update
    void Start()
    {

        myRigidbody = GetComponent<Rigidbody>();

        knockBackCounter = 1;
        fired = false;

        maxLocalScale = new Vector3(2, 1, 2);
        maxlocalScaleMagnitude = maxLocalScale.magnitude;

        Physics.IgnoreLayerCollision(9,12); //////////
    }

    // Update is called once per frame
    void Update()
    {
        if (blueGun.isFiringLaser == true /*&& bluePlayer.canShoot == false*/ && fired == false)
        {
            transform.position = blueGun.orbPoint.position; // Lock to Blue while Chargine

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

            if (bluePlayer.useController == true)
            {
                if (_direction == Vector3.zero)
                {
                    _direction = (gameObject.transform.position - bluePlayer.transform.position).normalized;
                }
            }
            else
            {
                if (_direction == Vector3.zero)
                {
                    _direction = (gameObject.transform.position - bluePlayer.transform.position).normalized;
                }
            }

            moveVelocity = _direction * speed;  // Shoot Orb

            lifeTime -= Time.deltaTime;  // Destroy over time

        }

        if (lifeTime <= 0)
        {
            fired = false; // Added later
            Destroy(gameObject);
        }

        // DESTROY IF BLUE ENTERS PARTICULAR STATES
        if (bluePlayer.GetComponent<Blue_Warp>().enabled & fired == false)
        {
            bluePlayer.canShoot = true;
            Destroy(gameObject);
        }

        // Arc FX
        //startPosition = transform.position;
    }

    //void OnCollisionEnter(Collision other)  // Collides with Enemy and Wall
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" & fired == true)
        {
            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageToGive, knockBackCounter);
            Debug.Log("ENEMY ELECTROCUTION!");
            //endPosition = other.transform.position;
        }
    }

    // Move the Rigid Body
    void FixedUpdate()
    {
        {
            myRigidbody.velocity = moveVelocity;
        }
    }

}

    

