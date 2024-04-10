using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class WallJumpCheck : MonoBehaviour
{

    [Header("Wall Jump Check")]
    [SerializeField] private bool wallFaceCheck;
    [SerializeField] private bool wallJumping;
    [SerializeField] private PlayerAnims pA;
    [SerializeField] private PlayerBehavior pB;
    [SerializeField] private Rigidbody rB;
    [SerializeField] private Collider faceCollider;
    [SerializeField] private float vectorForceConstant = 4.0f;
    [SerializeField] private float vectorForceZConstant;
    [SerializeField] private LayerMask wallJumpable;
    Vector3 wallJumpCheckPos;

    // Start is called before the first frame update
    void Start()
    {
        pB = GetComponent<PlayerBehavior>();
        //pA = GetComponent<PlayerAnims>();

        rB = GetComponent<Rigidbody>();
        vectorForceZConstant = vectorForceConstant / 3.0f; 
        
    }

    // Update is called once per frame
    void Update()
    {        
        // Casts a ray in front of the player to see if the player is facing a wall.
        wallJumpCheckPos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
        
        Debug.DrawRay(wallJumpCheckPos, transform.forward, Color.red, vectorForceConstant);

        RaycastHit hit;
        
        if (Physics.Raycast(wallJumpCheckPos, transform.forward, out hit, 2f, wallJumpable) && !wallJumping)
        {
            wallFaceCheck = true;
            pB.m_Animator.SetBool("isWallSliding", wallFaceCheck);
        }
        else
        {
            wallFaceCheck = false;
            pB.m_Animator.SetBool("isWallSliding", wallFaceCheck);
        }               
    }

    void FixedUpdate()
    {
        if (wallJumping)
        {
            // Depending on how the ray collides with the wall, the positions along the x axis and z axis change.
            rB.velocity = new Vector3(rB.velocity.x, 0f, rB.velocity.z);
            rB.AddForce(transform.up * 4f * (vectorForceConstant), ForceMode.VelocityChange);

            if (Physics.Raycast(transform.position, transform.forward, vectorForceConstant, wallJumpable))
            {
                rB.AddForce(transform.forward * -vectorForceZConstant, ForceMode.VelocityChange);
            }
            else if (Physics.Raycast(transform.position, -transform.forward, vectorForceConstant, wallJumpable))
            {
                rB.AddForce(transform.forward * vectorForceZConstant, ForceMode.VelocityChange);
            }
           
            pB.m_Animator.SetBool("isWallSliding", !wallJumping);

        }
    }

    public void WallJump(InputAction.CallbackContext context)
    {
        if (!pB.IsGrounded() && wallFaceCheck && context.performed) // When hugging the wall in midair and the Jump Key is pressed, activate the wall jump.
        {
            wallJumping = true;
            Invoke(nameof(WallJumpEnd), 0.2f);
        }
    }
    void WallJumpEnd()
    {        
        wallJumping = false;
    }


}
