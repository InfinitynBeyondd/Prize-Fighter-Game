using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class WallJumpCheck : MonoBehaviour
{

    [Header("Wall Jump Check")]
    [SerializeField] private bool wallFaceCheck;
    [SerializeField] private bool wallJumping;
    [SerializeField] private PlayerBehavior pB;
    [SerializeField] private Rigidbody rB;
    [SerializeField] private Collider faceCollider;
    [SerializeField] private float vectorForceConstant = 4.0f;
    [SerializeField] private float vectorForceZConstant;
    [SerializeField] private LayerMask wallJumpable;

    // Start is called before the first frame update
    void Start()
    {
        pB = GetComponent<PlayerBehavior>();
        rB = GetComponent<Rigidbody>();
        vectorForceZConstant = vectorForceConstant / 16.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Physics.Raycast(transform.position, Vector3.forward, 3.0f);
        //Debug.DrawRay(faceCollider.transform.position, Vector3.forward, Color.red, 3.0f);

        // Casts a ray in front of the player to see if the player is facing a wall.
        Debug.DrawRay(transform.position, transform.forward, Color.red, vectorForceConstant);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f, wallJumpable) || Physics.Raycast(transform.position, -transform.forward, out hit, 1f, wallJumpable))
        {
            wallFaceCheck = true;
        }
        else
        {
            wallFaceCheck = false;
        }

        // When hugging the wall in midair and the Jump Key is pressed, activate the wall jump.
        if (Input.GetKeyDown(pB.jumpKey) && !pB.IsGrounded() && wallFaceCheck) 
        {
            wallJumping = true;
            Invoke(nameof(WallJumpEnd), 0.2f);            
        }        

        if (wallJumping) 
        {
            // Depending on how the ray collides with the wall, the position along the z axis changes.
            rB.velocity = new Vector3(rB.velocity.x, 0f, rB.velocity.z);
            rB.AddForce(transform.up * vectorForceConstant, ForceMode.VelocityChange);

            if (Physics.Raycast(transform.position, transform.forward, vectorForceConstant, wallJumpable))
            {
                rB.AddForce(transform.forward * -vectorForceZConstant, ForceMode.VelocityChange);
            }
            else if (Physics.Raycast(transform.position, -transform.forward, vectorForceConstant, wallJumpable))
            {
                rB.AddForce(transform.forward * vectorForceZConstant, ForceMode.VelocityChange);
            }

        }
    }    

    void WallJumpEnd()
    {        
        wallJumping = false;
    } 
}
