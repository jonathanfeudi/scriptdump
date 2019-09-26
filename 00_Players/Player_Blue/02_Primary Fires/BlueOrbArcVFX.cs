using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueOrbArcVFX : MonoBehaviour
{
    // Orb Parent
    public GameObject blueOrb;

    // Orb Controller
    public BlueOrbController blueOrbController;

    // Fired
    public bool _fired = false;

    // Arc Parameters
    public GameObject arcPrefab;  // What to Make

    public LightningBoltScript _arc; // Arc to Update

    public Vector3 startPosition;
    public Vector3 endPosition;

    // List Enemies
    public List<GameObject> listEnemies;

    // List Arcs
    public List<GameObject> arcObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        startPosition = blueOrb.transform.position;

        //foreach loop
        foreach (GameObject newArc in arcObjects)
        {
            newArc.GetComponent<LightningBoltScript>().RecieveArcStartPosition(blueOrb.transform.position);
        }

    }

    // Add Enemy to List
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" & blueOrbController.fired == true)
        {
            if (listEnemies.Contains(other.gameObject))
            {
                // Do nothing
            }
            else
            {
                listEnemies.Add(other.gameObject);
                endPosition = other.transform.position;
                CreateAcr();
            }
        }
    }

    // Collides with Enemy Create Arc
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" /*& blueOrbController.fired == true & _fired == false*/)
        {
            _fired = true;
            endPosition = other.transform.position;
        }
    }

    // Stops Colliding with Enemy Destroy Arc
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" & blueOrbController.fired == true)
        {
            if (listEnemies.Contains(other.gameObject))
            {
                listEnemies.Remove(other.gameObject);
            }
        }
    }

    void CreateAcr()
    {
        //Use this code inside your script that instantiate the bullets:
        GameObject newArc = (GameObject)Instantiate(arcPrefab, startPosition, transform.rotation);
        //Do not forget to set correct position and rotation wich you want.

        //Sending to instantiated bulletPrefab the parameters bulletSpeed and bulletrange:
        newArc.GetComponent<LightningBoltScript>().RecieveArcParameter(blueOrb.transform.position, endPosition);

        //_arc = obj.GetComponent<LightningBoltScript>();
        arcObjects.Add(newArc);
    }

    void OnDestroy()
    {
        foreach (GameObject newArc in arcObjects)
        {
            Destroy(newArc.gameObject);
        }
    }
}
