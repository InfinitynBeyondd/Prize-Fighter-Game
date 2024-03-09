using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpCheck : MonoBehaviour
{

    [Header("Wall Jump Check")]
    [SerializeField] private bool wallFaceCheck;
    [SerializeField] private bool wallJumping;
    [SerializeField] private PlayerBehavior pB;
    [SerializeField] private Rigidbody rB;
    [SerializeField] private Collider faceCollider;
    [SerializeField] private float vectorForceConstant = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        pB = GetComponent<PlayerBehavior>();
        rB = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //Physics.Raycast(transform.position, Vector3.forward, 3.0f);
        Debug.DrawRay(faceCollider.transform.position, Vector3.forward, Color.red, 3.0f);

        // When hugging the wall in midair and the Jump Key is pressed, activate the wall jump.
        if (wallFaceCheck && !pB.IsGrounded() && Input.GetKeyDown(pB.jumpKey)) 
        {
            wallJumping = true;
            Invoke(nameof(WallJumpEnd), 0.2f);
        }

        if (wallJumping) 
        {
            // TO DO: Figure out how vectors work with this wall jump implementation to properly apply force to X or Z axis depending on where the player is facing.
            rB.velocity = new Vector3(rB.velocity.x, 0f, rB.velocity.z);
            rB.AddForce(transform.up * vectorForceConstant, ForceMode.VelocityChange);
        }
    }    

    private void OnCollisionStay(Collision other)
    {
        if (other.transform.tag == "Wall" && !pB.IsGrounded())
        {
            wallFaceCheck = true;
        }

        else 
        {
            wallFaceCheck = false;
        }
    }

    void WallJumpEnd()
    {
        wallFaceCheck = false;
        wallJumping = false;
    }

}
