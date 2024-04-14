using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawHurtbox : MonoBehaviour
{
    ClawController clawController;

    // Start is called before the first frame update
    void Start()
    {
        clawController = GetComponentInParent<ClawController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            clawController.bossHitsTaken++;
            clawController.ClawHeadGetsHit();

            clawController.currentState = ClawController.StateOfClaw.Damaged;

            Debug.Log("HIT CONFIRMED!");
        }
    }

}
