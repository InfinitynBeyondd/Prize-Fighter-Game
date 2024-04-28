using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClawController : MonoBehaviour
{
    public Animator clawAnimator;
    public Transform fullClaw; // Controls the transform for the claw's entire body.
    public Transform clawHead; // Controls the transform for just the claw's head. May need to be strictly controlled through the animator.

    public float speedBetweenTargets;
    public float descentSpeed;
    public int findToDescendDelay;
    //public int clawMatIndex;
    [SerializeField] Material[] clawMaterials;
    [SerializeField] Renderer clawRenderer;

    [Header("TARGET REFERENCES")]
    public Transform clawTargetsParent; // Parent transform of all the target spots.
    public Transform clawTargets; // Target spots the claw will aim between. 
    [SerializeField] int clawTargetIndex; // Index of the target array that the claw is headed towards.    
    [SerializeField] Transform[] clawTargetsArray;
    public Queue<Transform> clawTargetsQueue = new Queue<Transform>();

    public enum StateOfClaw { Hunting, Distracted, Damaged } // Enum that trafcks the state of the claw and controls movement patterns.
    public StateOfClaw currentState;

    [Header("HOLOGRAM POSITIONS")]
    [SerializeField] Transform[] hologramArray = new Transform[2];
    public int bossHitsTaken; // Index of the hologram array that the claw is headed towards.
    [SerializeField] Collider clawHurtbox;
    [SerializeField] Collider clawHitbox;

    [Header("SFX")]
    [SerializeField] private AudioClip clawDamaged;
    [SerializeField] private AudioClip[] audienceCheer;


    // Start is called before the first frame update
    void Start()
    {        
        bossHitsTaken = 0;
        //clawMatIndex = 0;
        clawTargetsParent = GameObject.FindGameObjectWithTag("BossTargets").GetComponent<Transform>();
        SetClawTargets();
    }

    private void Awake()
    {
        clawHurtbox.gameObject.SetActive(false);
        clawHitbox.gameObject.SetActive(false);
        currentState = StateOfClaw.Hunting;
        clawRenderer.sharedMaterial = clawMaterials[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (hologramArray[bossHitsTaken].gameObject.activeSelf)
        {
            currentState = StateOfClaw.Distracted;
        }

        //Checks if the player has beaten the boss
        if (bossHitsTaken > 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(7);
        }
    }

    // FixedUpdate is called once per physics change
    void FixedUpdate()
    {
        MoveFullClawToTarget();
    }

    // This function enqueues all the children of claw TARGETS to clawTargetsQueue. This automatically adjusts the array size to fit all the targets.
    private void SetClawTargets() 
    {
        clawTargets = clawTargetsParent.GetChild(bossHitsTaken);

        for (int i = 0; i < clawTargets.childCount; i++)
        {
            clawTargetsQueue.Enqueue(clawTargets.GetChild(i));
        }

        clawTargetsArray = new Transform[clawTargets.childCount];

        clawTargetsQueue.CopyTo(clawTargetsArray, 0);

    }


    // When the boss is hit, clawTargets is set to the next phase's targets. The clawTargetsQueue is cleared and reset to become the proper size.
    public void SetNewClawTargets()
    {
        clawTargets = clawTargetsParent.GetChild(bossHitsTaken);
        clawTargetsQueue.Clear();

        for (int i = 0; i < clawTargets.childCount; i++)
        {
            clawTargetsQueue.Enqueue(clawTargets.GetChild(i));
            clawTargetsArray[i] = null;
        }

        clawTargetsArray = new Transform[clawTargets.childCount];

        clawTargetsQueue.CopyTo(clawTargetsArray, 0);
    }



    // Entire Claw should first move from its start position to the target's position.
    void MoveFullClawToTarget() 
    {
        Vector3 positionAboveTarget;

        if (bossHitsTaken < 3) 
        {               

            if (currentState == StateOfClaw.Hunting)
            {
                clawRenderer.sharedMaterial = clawMaterials[0];
                positionAboveTarget = new Vector3(clawTargetsArray[clawTargetIndex].position.x, fullClaw.position.y, clawTargetsArray[clawTargetIndex].position.z);

                if (clawAnimator.GetBool("isRaised"))
                {
                    fullClaw.position = Vector3.MoveTowards(fullClaw.position, positionAboveTarget, speedBetweenTargets);
                }

                if (fullClaw.position.x == clawTargetsArray[clawTargetIndex].position.x && fullClaw.position.z == clawTargetsArray[clawTargetIndex].position.z)
                {
                    //clawAnimator.SetBool("isOpen", true);

                    ClawHeadDescendToTarget();
                    SetNextClawTarget();
                }


            }
            else if (currentState == StateOfClaw.Distracted)
            {
                clawRenderer.sharedMaterial = clawMaterials[0];
                positionAboveTarget = new Vector3(hologramArray[bossHitsTaken].position.x, fullClaw.position.y, hologramArray[bossHitsTaken].position.z);

                if (clawAnimator.GetBool("isRaised"))
                {
                    fullClaw.position = Vector3.MoveTowards(fullClaw.position, positionAboveTarget, .5f);
                }

                if (fullClaw.position.x == hologramArray[bossHitsTaken].position.x && fullClaw.position.z == hologramArray[bossHitsTaken].position.z)
                {
                    //clawAnimator.SetBool("isOpen", true);

                    ClawHeadDescendToTarget();
                }

            }
            else if (currentState == StateOfClaw.Damaged) 
            {
                clawRenderer.sharedMaterial = clawMaterials[1];
                TurnOffHitbox();
                clawHurtbox.enabled = false;
                Invoke(nameof(SetStateToHunting), findToDescendDelay);
            }

        }
    }

    // Once the Claw reaches the target's coordinates from above, the head should then descend to hit the target.
    void ClawHeadDescendToTarget() 
    {
        //Debug.Log("DESCENT BEGINS NOW!");
        //speedBetweenTargets = 0f;
        clawAnimator.SetBool("isRaised", false);
        clawAnimator.SetBool("isDescending", true);
        Invoke(nameof(ClawHeadClose), findToDescendDelay * 3f);
    }

    // Once the Claw closes, it detects if it's either in the Hunting state or above the hologram. If neither are true, it returns to idle after closing.
    void ClawHeadClose() 
    {
        //Debug.Log("CLAW CLOSES!");
        Invoke(nameof(TurnOnHitbox), findToDescendDelay / 2f);
        clawAnimator.SetBool("isDescending", false);
        clawAnimator.SetBool("isOpen", false);

        Invoke(nameof(TurnOffHitbox), findToDescendDelay);

        if (currentState == StateOfClaw.Hunting || (fullClaw.position.x != hologramArray[bossHitsTaken].position.x && fullClaw.position.z != hologramArray[bossHitsTaken].position.z))
        {
            //Invoke(nameof(ClawHeadReturnToIdle), findToDescendDelay);
            Invoke(nameof(ClawHeadOpen), findToDescendDelay);
        }
        if (currentState == StateOfClaw.Distracted) 
        {            
            clawHurtbox.gameObject.SetActive(true);
            clawHurtbox.enabled = true;
        }

    }

    void ClawHeadOpen() 
    {
        clawAnimator.SetBool("isOpen", true);
        Invoke(nameof(ClawHeadReturnToIdle), findToDescendDelay);
    }

    void ClawHeadReturnToIdle() 
    {
        //Debug.Log("Claw returns to neutral.");
        clawAnimator.SetBool("isRaised", true);
        clawAnimator.SetBool("isDamaged", false);
        //clawMatIndex = 0;
    }

    public void ClawHeadGetsHit() 
    {
        clawAnimator.SetBool("isDamaged", true);
        SoundFXManager.Instance.PlaySoundFXClip(clawDamaged, transform, 0.7f, 0.2f);
        SoundFXManager.Instance.PlayRandomSoundFXClip(audienceCheer, transform, 0.8f, 0.2f);

        SetNewClawTargets();
        Invoke(nameof(ClawHeadReturnToIdle), findToDescendDelay);
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

    void TurnOnHitbox()
    {
        clawHitbox.gameObject.SetActive(true);
    }

    void TurnOffHitbox() 
    {
        clawHitbox.gameObject.SetActive(false);
    }

    void SetStateToHunting() 
    {
        currentState = StateOfClaw.Hunting;
    }

}
