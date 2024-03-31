using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroundedPunch : MonoBehaviour
{
    PlayerBehavior pB;
    
    //Rigidbody rB;
    //GravityScalePhysX gSPX;
    
    [SerializeField] GameObject groundAttackHitbox;
    [SerializeField] GameObject playerModel;
    public Animator attackAnim;
    bool attacking;
    
    //public float currentPlayerXPos;
    //public float currentPlayerYPos;
    //public float currentPlayerZPos;

    // Start is called before the first frame update
    void Start()
    {
        //rB = GetComponent<Rigidbody>();
        pB = GetComponent<PlayerBehavior>();
        attackAnim.GetComponentInParent<Animator>();
        groundAttackHitbox.SetActive(false);
    }


    // For now, the attack will be translated through the animator. This does crate a slight bug of the attack being displaced, but the hitbox should be wide enough to cover for it.
    public void Attack(InputAction.CallbackContext context)
    {
        //if (pB.IsGrounded() && !groundAttackHitbox.activeSelf && context.performed)

        if (pB.IsGrounded() && !groundAttackHitbox.activeSelf && context.performed) //Checks if player is grounded, Attack anim is not active, and if button was pressed
        {            
            groundAttackHitbox.SetActive(true);
            attacking = groundAttackHitbox.activeSelf;
            attackAnim.SetBool("isPunching", attacking);
            StartCoroutine("AttackReset");            
        }
    }

    // Coroutine that resets the attack status.
    IEnumerator AttackReset() 
    {
        yield return new WaitForSeconds(0.5f);        
        groundAttackHitbox.SetActive(false);
        attacking = groundAttackHitbox.activeSelf;
        attackAnim.SetBool("isPunching", attacking);
    }

}
