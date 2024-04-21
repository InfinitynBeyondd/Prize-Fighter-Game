using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawHurtbox : MonoBehaviour
{
    ClawController clawController;
    [SerializeField] GameObject clawPlatformParent;
    [SerializeField] GameObject[] clawPlatforms;

    // Start is called before the first frame update
    void Start()
    {
        clawController = GetComponentInParent<ClawController>();
        //clawPlatformParent = GameObject.FindGameObjectWithTag("BossPlats").GetComponent<GameObject>();
        
    }

    // When the boss is hit, the proper claw targets will spawn and the next platform phase will appear.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            clawController.clawTargets.gameObject.SetActive(false);
            clawPlatforms[clawController.bossHitsTaken].SetActive(false);

            clawController.bossHitsTaken++;
            clawController.ClawHeadGetsHit();

            clawController.currentState = ClawController.StateOfClaw.Damaged;

            clawController.clawTargets.gameObject.SetActive(true);
            clawPlatforms[clawController.bossHitsTaken].SetActive(true);

            //clawController.SetNewClawTargets();

            Debug.Log("HIT CONFIRMED!");
        }
    }

}
