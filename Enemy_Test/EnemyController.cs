using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Initialized Variables")]
    private Rigidbody myRB;
    public float moveSpeed;

    public BluePlayerController thePlayer; // Player to Look At

    // KnockBack
    public float knockBackForce;
    public float knockBackTime;
    public float knockBackCounter;


    // Start is called before the first frame update (Create Event?)
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        thePlayer = FindObjectOfType<BluePlayerController>();
    }

    // KnockBack

    void OnCollisionEnter(Collision other)  // Collides with Enemy and Wall
    {
        if (other.gameObject.tag == "Projectile")
        {
            knockBackCounter = 1;
        }
    }

    // Fixed Update Loads at same time interval every time

    void FixedUpdate()
    {
        if (knockBackCounter <= 0)
        {
            myRB.velocity = (transform.forward * moveSpeed);
        }
        else
        {
            myRB.velocity = ((transform.forward * -1) * moveSpeed);
            knockBackCounter -= (Time.deltaTime) * 3;
        }
    }

    // Update is called once per frame
    // Look at Player
    void Update()
    {
        transform.LookAt(thePlayer.transform.position);
    }
}
