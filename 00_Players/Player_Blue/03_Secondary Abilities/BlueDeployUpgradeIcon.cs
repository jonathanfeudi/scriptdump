using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDeployUpgradeIcon : MonoBehaviour
{

    public BlueDeployableInteract deploy;

    public BlueAudioManager blueAudioManager;

    Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (deploy.blueUpgradeMeter > 0)
        {
            m_Animator.SetBool("_blueUpgrading", true);
            m_Animator.SetFloat("_blueUpgradeCountDownWindow", deploy.blueUpgradeMeter);
        }
        
        if (deploy.blueUpgradeMeter <= 0)
        {
            m_Animator.SetBool("_blueUpgrading", false);
            m_Animator.SetFloat("_blueUpgradeCountDownWindow", 0f);
        }
    }
}
