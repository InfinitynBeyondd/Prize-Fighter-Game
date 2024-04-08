using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DivePunch : MonoBehaviour
{
    PlayerAnims pA;
    PlayerBehavior pB;
    Rigidbody rB;
    GravityScalePhysX gSPX;

    [SerializeField] private float divePunchAirPause = .5f; // Player pauses midair once the dive punch is called.
    public float gravConstant = 5f; // The default value of the gravity scale for the object.
    [SerializeField] private float dropGravMult = 3f; // Factor determining player's speed of descent in the dive punch.

    [SerializeField] GameObject divePunchHitbox;
    [SerializeField] private bool divePunchCall = false; // Boolean detecting when dive punch function is called.
    public bool isDivePunchActive = false; // Boolean value that detects if the player is doing a dive punch already.
    [SerializeField] private AudioClip hexdogdivePunch1;
    [SerializeField] private AudioClip hexdogdivePunch2;
    // Start is called before the first frame update
    void Start()
    {
        // pA = GetComponent<PlayerAnims>();
        pB = GetComponent<PlayerBehavior>();
        rB = GetComponent<Rigidbody>();
        gSPX = GetComponent<GravityScalePhysX>();        
        gSPX.gravityScale = gravConstant;        
        divePunchHitbox.SetActive(false);
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

            pB.airTimeJumps = pB.airTimeJumpsMax;
        }
    }

    // As the dive punch activates, the player pauses briefly in midair, and the player's influence over input is halved.
    void MidairPause()
    {
        DivePunchMovementChange();
        gSPX.gravityScale = 0;
        divePunchCall = true;
        pB.m_Animator.SetBool("isDiveHolding", divePunchCall);
        SoundFXManager.Instance.PlaySoundFXClip(hexdogdivePunch1, transform, 0.5f);
    }
    
    // Coroutine that forces the descent to be faster than the usual falling speed.
    IEnumerator DivePunchDrop()
    {
        yield return new WaitForSeconds(divePunchAirPause);
        gSPX.gravityScale = gravConstant * dropGravMult;
        divePunchCall = false;
        pB.m_Animator.SetBool("isDiveHolding", divePunchCall);
        pB.m_Animator.SetBool("isDiving", true);
        divePunchHitbox.SetActive(true);
        SoundFXManager.Instance.PlaySoundFXClip(hexdogdivePunch2, transform, 0.5f);
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
        pB.m_Animator.SetBool("isDiveHolding", isDivePunchActive);
        pB.m_Animator.SetBool("isDiving", isDivePunchActive);
        divePunchHitbox.SetActive(false);
    }

}
