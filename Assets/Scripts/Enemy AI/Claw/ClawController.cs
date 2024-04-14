using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawController : MonoBehaviour
{
    public Animator clawAnimator;
    public Transform fullClaw; // Controls the transform for the claw's entire body.
    public Transform clawHead; // Controls the transform for just the claw's head. May need to be strictly controlled through the animator.

    public float speedBetweenTargets;
    public float descentSpeed;
    public int findToDescendDelay;
    public int bossHP;

    [SerializeField] Transform clawTargets; // Target spots the claw will aim between. 
    [SerializeField] int clawTargetIndex; // Index of the target array that the claw is headed towards.    
    [SerializeField] Transform[] clawTargetsArray;
    Queue<Transform> clawTargetsQueue = new Queue<Transform>();

    [SerializeField] enum StateOfClaw { Hunting, Distracted, Stuck, Damaged } // Enum that trafcks the state of the claw and controls movement patterns.
    StateOfClaw currentState;

    [Header("HOLOGRAM POSITIONS")]
    [SerializeField] Transform[] hologramArray = new Transform[2];
    [SerializeField] int hologramIndex; // Index of the hologram array that the claw is headed towards.
    [SerializeField] Collider clawHurtbox;
    [SerializeField] Collider clawHitbox;

    // Start is called before the first frame update
    void Start()
    {
        currentState = StateOfClaw.Hunting;
        hologramIndex = 0;
        SetClawTargetsForFirstPhase();
    }

    // Update is called once per frame
    void Update()
    {
        if (hologramArray[hologramIndex].gameObject.activeSelf)
        {
            currentState = StateOfClaw.Distracted;
        }
    }

    // FixedUpdate is called once per physics change
    void FixedUpdate()
    {
        MoveFullClawToTarget();
    }

    // This function enqueues all the children of claw TARGETS to clawTargetsQueue. This automatically adjusts the array size to fit all the targets.
    private void SetClawTargetsForFirstPhase()
    {

        for (int i = 0; i < clawTargets.childCount; i++)
        {
            clawTargetsQueue.Enqueue(clawTargets.GetChild(i));
        }

        clawTargetsArray = new Transform[clawTargets.childCount];

        clawTargetsQueue.CopyTo(clawTargetsArray, 0);
    }

    // Entire Claw should first move from its start position to the target's position.
    void MoveFullClawToTarget() 
    {

        Vector3 positionAboveTarget;

        if (currentState == StateOfClaw.Hunting)
        {
            positionAboveTarget = new Vector3(clawTargetsArray[clawTargetIndex].position.x, fullClaw.position.y, clawTargetsArray[clawTargetIndex].position.z);

            if (clawAnimator.GetBool("isRaised"))
            {
                fullClaw.position = Vector3.MoveTowards(fullClaw.position, positionAboveTarget, speedBetweenTargets);
            }

            if (fullClaw.position.x == clawTargetsArray[clawTargetIndex].position.x && fullClaw.position.z == clawTargetsArray[clawTargetIndex].position.z)
            {
                clawAnimator.SetBool("isOpen", true);

                ClawHeadDescendToTarget();
                SetNextClawTarget();                
            }
        }
        else if (currentState == StateOfClaw.Distracted) 
        {
            positionAboveTarget = new Vector3(hologramArray[hologramIndex].position.x, fullClaw.position.y, hologramArray[hologramIndex].position.z);

            if (clawAnimator.GetBool("isRaised"))
            {
                fullClaw.position = Vector3.MoveTowards(fullClaw.position, positionAboveTarget, speedBetweenTargets);
            }

            if (fullClaw.position.x == hologramArray[hologramIndex].position.x && fullClaw.position.z == hologramArray[hologramIndex].position.z)
            {
                clawAnimator.SetBool("isOpen", true);

                ClawHeadDescendToTarget();
            }

        }

    }

    // Once the Claw reaches the target's coordinates from above, the head should then descend to hit the target.
    void ClawHeadDescendToTarget() 
    {
        //Debug.Log("DESCENT BEGINS NOW!");
        clawAnimator.SetBool("isRaised", false);
        clawAnimator.SetBool("isDescending", true);

        Invoke(nameof(ClawHeadClose), findToDescendDelay * 3f);
    }

    void ClawHeadClose() 
    {
        //Debug.Log("CLAW CLOSES!");
        clawAnimator.SetBool("isDescending", false);
        clawAnimator.SetBool("isOpen", false);

        Invoke(nameof(ClawHeadReturnToIdle), findToDescendDelay);
    }

    void ClawHeadReturnToIdle() 
    {
        //Debug.Log("Claw returns to neutral.");
        clawAnimator.SetBool("isRaised", true);        
    }

    void SetNextClawTarget() 
    {
        if (clawTargetIndex + 1 < clawTargetsArray.Length)
        {
            clawTargetIndex++;
        }
        else
        {
            clawTargetIndex = 0;
        }
    }
}
