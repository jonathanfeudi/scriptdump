using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    // SHOOTING

    public bool canShoot;
    private float canShootTimer;
    public BlueTurretController turret;
    public BlueTurretBulletController bullet;
    public Transform firePoint;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    void Awake() // INITIALIZE VARIABLES
    {
        turret = GetComponent<BlueTurretController>();
    }

    void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
    }


    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))  // THIS IS WHERE WE SHOOT
                {
                    visibleTargets.Add(target);

                    if (canShoot == true & turret.holding == false) 
                    {
                        BlueTurretBulletController newBullet = Instantiate(bullet, firePoint.position, Quaternion.LookRotation(dirToTarget)) as BlueTurretBulletController;
                        newBullet.direction = (target.position - transform.position).normalized;

                        canShoot = false;

                        canShootTimer = (float).15;
                    }

                    Debug.Log("I SEE YOUUUUUU");
                }
            }
        }
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void Update()
    {
        if (canShootTimer > 0)
        {
            canShootTimer -= Time.deltaTime;
        }

        if (canShootTimer <= 0)
        {
            canShoot = true;
            canShootTimer = 0;
        }
    }
}
