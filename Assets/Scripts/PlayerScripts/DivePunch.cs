using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DivePunch : MonoBehaviour
{
    PlayerBehavior pB;
    Rigidbody rB;
    GravityScalePhysX gSPX;

    [SerializeField] private float divePunchAirPause = .5f; // Player pauses midair once the dive punch is called.
    public float gravConstant = 5f; // The default value of the gravity scale for the object.
    [SerializeField] private float dropGravMult = 3f; // Factor determining player's speed of descent in the dive punch.

    public Animator attackAnim;
    [SerializeField] private bool divePunchCall = false; // Boolean detecting when dive punch function is called.
    [SerializeField] private bool isDivePunchActive = false; // Boolean value that detects if the player is doing a dive punch already.

    // Start is called before the first frame update
    void Start()
    {
        pB = GetComponent<PlayerBehavior>();
        rB = GetComponent<Rigidbody>();
        gSPX = GetComponent<GravityScalePhysX>();
        attackAnim = GetComponentInChildren<Animator>();
        gSPX.gravityScale = gravConstant;
    }

    private void OnCollisionEnter(Collision other)
    {
        // Upon colliding with something, the player's dive punch ends. 
        if (other.GetContact(0).normal.y >= 0.5f) 
        {
            DivePunchEnd();
        }
    }

    // Set the dive punch to be active so that it can't be called repeatedly, cue the midair pause, then begin to fall with faster gravity applied.
    public void DivePunchCalled(InputAction.CallbackContext context)
    {
        if (!isDivePunchActive && !pB.IsGrounded() && context.performed) //If divepunch is not active, and the player is not grounded, and button is pressed then do divepunch
        {
            isDivePunchActive = true;
            MidairPause();
            StartCoroutine("DivePunchDrop");
        }
    }

    // As the dive punch activates, the player pauses briefly in midair, and the player's influence over input is halved.
    void MidairPause()
    {
        DivePunchMovementChange();
        gSPX.gravityScale = 0;
        divePunchCall = true;
        attackAnim.SetBool("isDiveHolding", divePunchCall);
    }
    
    // Coroutine that forces the descent to be faster than the usual falling speed.
    IEnumerator DivePunchDrop()
    {
        yield return new WaitForSeconds(divePunchAirPause);
        gSPX.gravityScale = gravConstant * dropGravMult;
        divePunchCall = false;
        attackAnim.SetBool("isDiving", isDivePunchActive);
    }

    // Player has less control over the character while dive punching.
    void DivePunchMovementChange()
    {
        pB.movementSpeed = (pB.defaultMovementSpeed/dropGravMult);
        //rB.angularVelocity = Vector3.zero;
    }

    // Sets movement variables back to their usual state when a dive punch is over.
    void DivePunchEnd()
    {        
        pB.movementSpeed = pB.defaultMovementSpeed;
        gSPX.gravityScale = gravConstant;
        isDivePunchActive = false;
        attackAnim.SetBool("isDiving", isDivePunchActive);
    }

}
