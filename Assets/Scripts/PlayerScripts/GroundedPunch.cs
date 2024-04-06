using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroundedPunch : MonoBehaviour
{
    //PlayerAnims pA;
    PlayerBehavior pB;    
    
    [SerializeField] GameObject groundAttackHitbox;
    [SerializeField] GameObject playerModel;    
    [SerializeField] private AudioClip[] hexdogPunch;
    bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        //pA = GetComponent<PlayerAnims>();
        pB = GetComponent<PlayerBehavior>();        
        groundAttackHitbox.SetActive(false);
    }


    // For now, the attack will be translated through the animator. This does crate a slight bug of the attack being displaced, but the hitbox should be wide enough to cover for it.
    public void Attack(InputAction.CallbackContext context)
    {        
        if (pB.IsGrounded() && !groundAttackHitbox.activeSelf && context.performed) //Checks if player is grounded, Attack anim is not active, and if button was pressed
        {            
            groundAttackHitbox.SetActive(true);
            attacking = groundAttackHitbox.activeSelf;
            pB.m_Animator.SetBool("isPunching", attacking);
            StartCoroutine("AttackReset");
            SoundFXManager.Instance.PlayRandomSoundFXClip(hexdogPunch, transform, 0.7f);
        }
    }

    // Coroutine that resets the attack status.
    IEnumerator AttackReset() 
    {
        yield return new WaitForSeconds(0.5f);        
        groundAttackHitbox.SetActive(false);
        attacking = groundAttackHitbox.activeSelf;
        pB.m_Animator.SetBool("isPunching", attacking);
    }

}
