using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogGroundCheck : MonoBehaviour
{

    [Header("CHECK PRE-BUILD!")]
    [SerializeField] Rigidbody frogBody; // Frog's jump timer. Once it hits zero, the frog will jump.
    [SerializeField] float frogJumpTimer; // A timer that controls when the frog will jump.
    [SerializeField] float frogJumpTimerMax = 300f; // Max value of the frog's jump timer. SET IN THE INSPECTOR BECAUSE IT WILL VARY BY FROG!
    [SerializeField] float frogJumpForce; // Distance that the frog jumps up with. SET IN THE INSPECTOR BECAUSE IT WILL VARY BY FROG!
    [SerializeField] int buildMultiple = 2; // Frogs have different physics in engine than in build. Use this int to debug depending on version.

    // Start is called before the first frame update
    void Start()
    {
        frogBody = GetComponentInParent<Rigidbody>();
        frogJumpTimer = frogJumpTimerMax;
    }

    // Update is called once per frame
    void Update()
    {
        FrogJump();
    }

    // When the frog's hitbox intersects with a ground or default layer, its timer will decrease until it hits 0.
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") || other.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            frogJumpTimer--;
        }
    }

    // If a player gets caught under a frog, the frog will jump again prevent a softlock.
    private void OnCollisionStay(Collision collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player") && frogJumpTimer == 0)
        {
            FrogJump();
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


    // The frog will jump based on the force assigned to it in the inspector. Once the timer resets, the force will stop being applied.
    private void FrogJump()
    {
        if (frogJumpTimer <= 0)
        {
            //IN-BUILD FORCES (MULTIPLY THE ENGINE FORCES!)
            frogBody.AddForce(new Vector3(0, buildMultiple * frogJumpForce, 0), ForceMode.VelocityChange);

            //IN-ENGINE FORCES:
            //frogBody.AddForce(new Vector3(0, frogJumpForce, 0), ForceMode.VelocityChange);
        }
        else 
        {
            frogBody.AddForce(Vector3.zero, ForceMode.VelocityChange);
        }
    }

    // If it feels like a frog falls too slowly, edit the gravity value of the frog using the GravityScalePhysX script.

}
