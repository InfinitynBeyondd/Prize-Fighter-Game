using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivePunch : MonoBehaviour
{
    PlayerBehavior pB;
    Rigidbody rB;
    GravityScalePhysX gSPX;

    [SerializeField] private bool divePunchCall = false; // Boolean detecting when dive punch function is called.
    [SerializeField] private float divePunchAirPause = .5f; // Player pauses midair once the dive punch is called.
    //[SerializeField] private float dropGrav = 150f; // Float determining player's speed of descent in the dive punch.
    [SerializeField] private bool isDivePunchActive = false; // Boolean value that detects if the player is doing a dive punch already.

    // Start is called before the first frame update
    void Start()
    {
        pB = GetComponent<PlayerBehavior>();
        rB = GetComponent<Rigidbody>();
        gSPX = GetComponent<GravityScalePhysX>();
    }

    // Update is called once per frame
    void Update()
    {
        // If Left Shift is pressed down and the player isn't grounded, this cues the dive punch. 
        if (Input.GetKeyDown(pB.attackKey) && !pB.IsGrounded())
        {
            divePunchCall = true;
        }
    }

    // FixedUpdate is called once per physics change
    void FixedUpdate()
    {
        // When divePunchCall is true and a dive punch isn't already in action, it sets the dive punch in action.
        if (divePunchCall && !isDivePunchActive)
        {
            DivePunchCalled();
        }
        divePunchCall = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        // Upon colliding with something, the player's dive punch ends. If this becomes incongruent with wall jump code later on, fill out the method below.
        if (other.GetContact(0).normal.y >= 0.5f) 
        {
            DivePunchEnd();
        }
    }

    /* void GroundCheckForDivePunch() 
    {
        if (pB.IsGrounded()) 
        {

        }
    } 
    */

    // Set the dive punch to be active so that it can't be called repeatedly, cue the midair pause, then begin to fall with faster gravity applied.
    void DivePunchCalled()
    {
        isDivePunchActive = true;
        MidairPause();
        StartCoroutine("DivePunchDrop");
    }

    // As the dive punch activates, the player pauses briefly in midair, and the player's influence over input is halved.
    void MidairPause()
    {
        DivePunchMovementChange();
        gSPX.gravityScale = 0;
    }
    
    // Coroutine that forces the descent to be faster than the usual falling speed.
    IEnumerator DivePunchDrop()
    {
        yield return new WaitForSeconds(divePunchAirPause);
        gSPX.gravityScale = 15f;
        //rB.AddForce(Vector3.down * dropGrav, ForceMode.VelocityChange);        
    }

    // Player has less control over the character while dive punching.
    void DivePunchMovementChange()
    {
        pB.movementSpeed = (pB.movementSpeed/2);
        //rB.angularVelocity = Vector3.zero;
    }

    // Sets movement variables back to their usual state when a dive punch is over.
    void DivePunchEnd()
    {        
        pB.movementSpeed = 10f;
        gSPX.gravityScale = 1f;
        isDivePunchActive = false;
    }

}
