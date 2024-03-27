using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogGroundCheck : MonoBehaviour
{

    [Header("Ground Check")]
    [SerializeField] Rigidbody frogBody;
    [SerializeField] float frogJumpTimer;
    [SerializeField] float frogJumpTimerMax = 300f;
    [SerializeField] Transform frogGroundCheck; // Empty GameObject below the frog that casts a sphere, detecting if the ground layer overlaps.
    [SerializeField] LayerMask groundLayer; // Layer mask determining what is a ground layer.    
    [SerializeField] float gCR; // Ground Check Radius    
    [SerializeField] bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        frogBody = GetComponentInParent<Rigidbody>();
        frogJumpTimer = frogJumpTimerMax;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            frogJumpTimer--;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            frogJumpTimer = frogJumpTimerMax;
        }
    }

    void Update()
    {
        FrogJump();
    }

    private void FrogJump()
    {
        if (frogJumpTimer <= 0)
        {
            frogBody.AddForce(Vector3.up, ForceMode.VelocityChange);
        }
        else 
        {
            frogBody.AddForce(Vector3.zero, ForceMode.VelocityChange);
        }
    }

    /*public bool IsGrounded() // Bool that says when the frog is on a ground layer- a small sphere is cast at the frog's feet to determine if they're standing on solid ground.
    {
        return Physics.CheckSphere(frogGroundCheck.position, gCR, groundLayer);
    }

    OLD
    void Start()
    {
        frogBody = GetComponentInParent<Rigidbody>();
        frogJumpTimer = frogJumpTimerMax;
        frogGroundCheck = this.transform;
    }

    // Update is called once per frame
    void Update()
    {        
        if (frogJumpTimer <= 0)
        {
            frogJumpTimer = 0;            
        }

        if (IsGrounded())
        {
            frogJumpTimer--;
            onGround = true;
        }
        else if (!IsGrounded())
        {
            frogJumpTimer = frogJumpTimerMax;
            onGround = false;
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(frogGroundCheck.position, gCR);
        Gizmos.color = Color.green;
    }

    private void FrogJump()
    {
        if (frogJumpTimer <= 0)
        {
            frogBody.AddForce(new Vector3(0, .5f, 0), ForceMode.VelocityChange);
        }
        else 
        {
            frogBody.AddForce(Vector3.zero);
        }
    }
    */
}
