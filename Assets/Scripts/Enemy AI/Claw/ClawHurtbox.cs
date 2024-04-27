using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawHurtbox : MonoBehaviour
{
    ClawController clawController;
    [SerializeField] GameObject clawPlatformParent;
    [SerializeField] GameObject[] clawPlatforms;
    [SerializeField] private Animator phaseAC;    

    // Start is called before the first frame update
    void Start()
    {
        clawController = GetComponentInParent<ClawController>();
        //clawPlatformParent = GameObject.FindGameObjectWithTag("BossPlats").GetComponent<GameObject>();
        //phaseAC.SetBool("phaseEnd", false);
    }

    // When the boss is hit, the proper claw targets will spawn and the next platform phase will appear.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            clawController.clawTargets.gameObject.SetActive(false);
            clawPlatforms[clawController.bossHitsTaken].SetActive(false);
            //phaseAC.SetBool("phaseStart", false);
            //phaseAC.SetBool("phaseEnd", true);

            clawController.bossHitsTaken++;
            clawController.ClawHeadGetsHit();

            clawController.currentState = ClawController.StateOfClaw.Damaged;
            //clawController.clawMatIndex = 1;
            //Invoke("ClawPhaseSet", 2.0f);

            //clawController.SetNewClawTargets();
            clawController.clawTargets.gameObject.SetActive(true);
            clawPlatforms[clawController.bossHitsTaken].SetActive(true);
            Debug.Log("HIT CONFIRMED!");
        }
    }

    void ClawPhaseSet()
    {
        clawController.clawTargets.gameObject.SetActive(true);
        clawPlatforms[clawController.bossHitsTaken].SetActive(true);
        //phaseAC.SetBool("phaseEnd", false);
        //phaseAC.SetBool("phaseStart", true);
    }

}
