using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrogController : MonoBehaviour
{
    [Header("JUMPING - DON'T SET BUILD MULTIPLE UNDER 1")]
    [SerializeField] Rigidbody frogBody; // Frog's jump timer. Once it hits zero, the frog will jump.
    [SerializeField] float frogJumpTimer; // A timer that controls when the frog will jump.
    [SerializeField] float frogJumpTimerMax = 300f; // Max value of the frog's jump timer. SET IN THE INSPECTOR BECAUSE IT WILL VARY BY FROG!
    [SerializeField] float frogJumpForce; // Distance that the frog jumps up with. SET IN THE INSPECTOR BECAUSE IT WILL VARY BY FROG!
    [SerializeField] int buildMultiple = 2; // Frogs have different physics in engine than in build. Use this int to debug depending on version.
    [SerializeField] private AudioClip[] frogJumps;

    [SerializeField] Transform[] jumpTargetsArray;
    Queue<Transform> jumpTargetsQueue = new Queue<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        frogBody = GetComponentInParent<Rigidbody>();
        frogJumpTimer = frogJumpTimerMax;
        SetJumpTargets();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FrogJumpToTarget();
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

    // This function theoretically should enqueue all the children of JUMP TARGETS to jumpTargetsQueue.
    private void SetJumpTargets() 
    {
        GameObject thisFrog = transform.parent.gameObject;

        for (int i = 0; i < thisFrog.transform.GetChild(4).childCount; i++) 
        {
            jumpTargetsQueue.Enqueue(thisFrog.transform.GetChild(4).GetChild(i));
        }

        jumpTargetsQueue.CopyTo(jumpTargetsArray, 0);
    }

    // The frog will jump towards the enqueued target based on the force assigned to it in the inspector. Once the timer resets, the force will stop being applied.
    private void FrogJumpToTarget()
    {
        if (frogJumpTimer <= 0)
        {
            Transform nextJumpTarget = jumpTargetsQueue.Dequeue();

            //frogBody.transform.Translate(new Vector3(nextJumpTarget.position.x, 0, nextJumpTarget.position.x));

            jumpTargetsQueue.Enqueue(nextJumpTarget);

            // MULTIPLY THE ENGINE FORCES BY A PROPER VALUE - DO NOT SET IT TO ANYTHING UNDER 1!
            frogBody.AddForce(new Vector3(0, buildMultiple * frogJumpForce, 0), ForceMode.VelocityChange);
            
            SoundFXManager.Instance.PlayRandomSoundFXClip(frogJumps, transform, 0.2f);
        }
        else
        {
            frogBody.AddForce(Vector3.zero, ForceMode.VelocityChange);
        }
    }

    // If it feels like a frog falls too slowly, edit the gravity value of the frog using the GravityScalePhysX script.
}
