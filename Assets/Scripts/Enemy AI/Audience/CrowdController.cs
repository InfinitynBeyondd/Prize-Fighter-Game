using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdController : MonoBehaviour
{
    [SerializeField] Animator clawAnimator; // Reference to the Claw Controller.
    [SerializeField] Animator crowdAnimator; // Reference to the specific Crowd Animator.

    // Start is called before the first frame update
    void Start()
    {
        crowdAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clawAnimator.GetBool("isDamaged"))
        {
            crowdAnimator.SetBool("bossIsDamaged", true);
        }
        if (clawAnimator.GetBool("isRaised")) 
        {
            crowdAnimator.SetBool("bossIsDamaged", false);
        }
    }
}
