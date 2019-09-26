using UnityEngine;

using System.Collections;

public class BlueTurretBulletController : MonoBehaviour { 

    public float speed;

    public float lifeTime;

    public int damageToGive;

    public int knockBackCounter;

    public GameObject target;

    private Rigidbody myRigidbody;
    public Vector3 direction;
    public Vector3 moveVelocity;

    // Scaling Limit
    public Vector3 maxLocalScale;
    float maxlocalScaleMagnitude;
    
    // Start is called before the first frame update
    void Start()
    {

        myRigidbody = GetComponent<Rigidbody>();

        knockBackCounter = 1;

        maxLocalScale = new Vector3(2, 1, 2);
        maxlocalScaleMagnitude = maxLocalScale.magnitude;

        //Physics.IgnoreLayerCollision(9,11);
    }

    // Update is called once per frame
    void Update()
    {
        moveVelocity = direction * speed;  // Shoot Orb //moveVelocity = transform.forward * speed;  // Shoot Orb

        lifeTime -= Time.deltaTime;  // Destroy over time


        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }


    void OnCollisionEnter(Collision other)  // Collides with Enemy and Wall
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageToGive, knockBackCounter);
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }

    // Move the Rigid Body
    void FixedUpdate()
    {
        myRigidbody.velocity = moveVelocity;
    }
}
