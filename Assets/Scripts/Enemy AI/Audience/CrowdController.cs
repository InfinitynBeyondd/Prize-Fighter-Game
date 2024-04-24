using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdController : MonoBehaviour
{
    [SerializeField] ClawController clawController; // Reference to the Claw Controller.
    [SerializeField] Animator crowdAnimator; // Reference to the specific Crowd Animator.

    // Start is called before the first frame update
    void Start()
    {
        crowdAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clawController.currentState == ClawController.StateOfClaw.Damaged)
        {
            crowdAnimator.SetBool("bossIsDamaged", true);
        }
        if (clawController.currentState == ClawController.StateOfClaw.Hunting) 
        {
            crowdAnimator.SetBool("bossIsDamaged", false);
        }
    }
}
