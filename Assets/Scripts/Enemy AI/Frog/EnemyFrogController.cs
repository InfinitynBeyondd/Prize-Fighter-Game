using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrogController : MonoBehaviour
{
    [Header("JUMPING - DON'T SET BUILD MULTIPLE UNDER 1")]
    [SerializeField] Rigidbody frogBody; // Frog's RB.
    [SerializeField] float frogJumpTimer; // A timer that controls when the frog will jump.
    [SerializeField] float frogJumpTimerMax = 300f; // Max value of the frog's jump timer. SET IN THE INSPECTOR BECAUSE IT WILL VARY BY FROG!
    [SerializeField] float frogJumpForce; // Distance that the frog jumps up with. SET IN THE INSPECTOR BECAUSE IT WILL VARY BY FROG!
    [SerializeField] int buildMultiple = 2; // Frogs have different physics in engine than in build. Use this int to debug depending on version.
    [SerializeField] private AudioClip[] frogJumps;

    [SerializeField] Transform[] jumpTargetsArray;
    Queue<Transform> jumpTargetsQueue = new Queue<Transform>();

    [SerializeField] Transform thisFrog; // Parent transform
    [SerializeField] Transform frogTargets; // Target spots the frog will jump between. 
    //[SerializeField] Transform nextJumpTarget; // 
    [SerializeField] int jumpTargetIndex; // Index of the array that the frog is headed towards.
    public float speedBetweenTargets; // The speed at which the frog travels between its targets.

    // Start is called before the first frame update
    void Start()
    {        
        frogBody = transform.parent.GetComponentInParent<Rigidbody>();
        frogJumpTimer = frogJumpTimerMax;

        thisFrog = gameObject.transform.parent.parent;
        frogTargets = frogBody.transform.parent.GetChild(1);

        jumpTargetIndex = 0;
        speedBetweenTargets = 25f;

        SetJumpTargets();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // FixedUpdate is called once per physics change
    void FixedUpdate()
    {
        if (frogJumpTimer <= 0)
        {
            FrogJumpToTarget();
        }

        if (frogBody.velocity.y != 0)
        {
            FrogMoveToTarget();
        }
    }

    // When the frog's hitbox intersects with a ground or default layer, its timer will decrease until it hits 0.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.gameObject.layer == LayerMask.NameToLayer("Default")
            || other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            frogJumpTimer--;
        }
    }

    // If a player gets caught under a frog, the frog will jump again to prevent a softlock.
    private void OnCollisionStay(Collision collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            frogJumpTimer--;
        }
    }

    // When the frog's hitbox no longer intersects with a ground or default layer, its timer will reset to the max value.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            frogJumpTimer = frogJumpTimerMax;
        }
    }

    // This function enqueues all the children of JUMP TARGETS to jumpTargetsQueue. This automatically adjusts the array size to fit all the targets.
    private void SetJumpTargets()
    {

        for (int i = 0; i < frogTargets.childCount; i++)
        {
            jumpTargetsQueue.Enqueue(frogTargets.GetChild(i));
        }

        jumpTargetsArray = new Transform[frogTargets.childCount];

        jumpTargetsQueue.CopyTo(jumpTargetsArray, 0);
    }

    // Controls frog's movement to the targets. Some oddities exist where the frog stops briefly midair.
    void FrogMoveToTarget()
    {
        frogBody.position = Vector3.MoveTowards(frogBody.position, jumpTargetsArray[jumpTargetIndex].position, speedBetweenTargets * Time.deltaTime);

        FrogFaceTarget();

        if (frogBody.position == jumpTargetsArray[jumpTargetIndex].position)
        {
            SetNextJumpTarget();
        }
    }

    // The frog will jump towards the enqueued target based on the force assigned to it in the inspector. Once the timer resets, the force will stop being applied.
    private void FrogJumpToTarget()
    {

        // MULTIPLY THE ENGINE FORCES BY A PROPER VALUE - DO NOT SET IT TO ANYTHING UNDER 1!

        frogBody.AddForce(new Vector3(0, buildMultiple * frogJumpForce, 0), ForceMode.VelocityChange);

        SoundFXManager.Instance.PlayRandomSoundFXClip(frogJumps, transform, 0.2f);

    }

    // Snaps frog's rotation to the target it is headed for. A smoother transition would be nice, but this still works.
    private void FrogFaceTarget()
    {
        Vector3 targetPosition = new Vector3(jumpTargetsArray[jumpTargetIndex].position.x, 0, jumpTargetsArray[jumpTargetIndex].position.z);
        Vector3 thisFrogsPosition = new Vector3(thisFrog.position.x, 0, thisFrog.position.z);
        Vector3 frogFacingDirection = (thisFrogsPosition - targetPosition);

        Quaternion frogRotation = Quaternion.LookRotation(frogFacingDirection);

        thisFrog.rotation = frogRotation;
    }

    // This function should theoretically create more consistency in the frog jumps to not immediately snap to the newest target.
    private void SetNextJumpTarget() 
    {
        if (jumpTargetIndex + 1 < jumpTargetsArray.Length)
        {
            jumpTargetIndex++;
        }
        else
        {
            jumpTargetIndex = 0;
        }
    }

    // If it feels like a frog falls too slowly, edit the gravity value of the frog using the GravityScalePhysX script.
}
