using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBarrierController : MonoBehaviour
{
    [Header("Initialized Variables")]

    public BluePlayerController bluePlayer;

    public BlueGunController blueGun;

    public Animator anim;

    public bool holding;

    public float lifeTime;

    public Vector3 _position;

    public bool warpSelectionOn = false;

    public GameObject warpPoint;

    public Light warpLight;

    public Color warpColor;

    public bool _selected = false;

    public static GameObject instance;

    public bool isDetroyed = true;

    public int upgradeLevel = 0;

    ///

    void Awake() // INITIALIZE VARIABLES
    {
        bluePlayer = GameObject.FindGameObjectWithTag("Blue_Player").GetComponent<BluePlayerController>();

        blueGun = GameObject.FindGameObjectWithTag("Blue_Gun").GetComponent<BlueGunController>();

        instance = this.gameObject;

        warpColor = warpLight.color;
    }

    void Start()
    {
        // Turn OFF Blue Can Shoot
        bluePlayer.canShoot = false;

        holding = true;
    }

    void Update()
    {
        // Blue Has Deployable Armed
        if (holding == true)
        {
            transform.position = blueGun.barrierDeployPoint.position; // Lock to Blue while Chargine
                        
            var lookPos = bluePlayer.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
        }

        // Deploy
        if ((!Input.GetMouseButton(1) & !Input.GetKey(KeyCode.Joystick1Button5)) & holding == true)
        {
            holding = false;

            // Turn On Blue Can Shoot
            bluePlayer.canShoot = true;

            // Turn On Can Orb
            if (GameObject.Find("Blue_Orb(Clone)") == null)
            {
                blueGun.canOrb = true;
            }

            // Set Warp Points

            _position = warpPoint.transform.position;

            if (bluePlayer.deploy_1 == Vector3.zero)
            {
                bluePlayer.deploy_1 = _position;
                bluePlayer.deploy_1_ID = instance;
            }
            else
            {
                bluePlayer.deploy_2 = _position;
                bluePlayer.deploy_2_ID = instance;
            }

            // Reset Can Deploy Variable
            if (blueGun.barrierCounter < 2)
            {
                blueGun.canDeploy = true;  // Reset Can Deply for Blue Gun
            }

            anim.SetTrigger("Active");
        }

        // Warp Indicator On
        if ((bluePlayer.GetComponent<Blue_Warp>().enabled == true) & _selected == true)
        {
            warpSelectionOn = true;
            warpLight.enabled = true;
        }
        else
        {
            warpSelectionOn = false;
            warpLight.enabled = false;
        }

        // Change Color
        if (bluePlayer.GetComponent<Blue_Warp>().altWarp == true)
        {
            warpLight.color = Color.yellow;
        }
        else
        {
            warpLight.color = warpColor;
        }

    }

    // Play SFX On Destroy
    private void OnDestroy()
    {
        if (isDetroyed == true)
        {
            bluePlayer.GetComponent<BlueAudioManager>().AudioSource_01.Play();
        }

        if (bluePlayer.deploy_1 == _position)
        {
            bluePlayer.deploy_1 = bluePlayer.deploy_2;
            bluePlayer.deploy_2 = Vector3.zero;
        }

        if (bluePlayer.deploy_2 == _position)
        {
            bluePlayer.deploy_2 = Vector3.zero;
        }
    }

    // Disarm On Destroy Event
    void OnApplicationQuit()
    {
        isDetroyed = false;
    }
}
