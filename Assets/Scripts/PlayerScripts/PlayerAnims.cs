using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnims : MonoBehaviour
{
    public Animator m_Animator; // Player animator.
    [SerializeField] PlayerBehavior pB;

    // ANIMATION STATES:
    const string ani_Idle = "ANI_idle";
    const string ani_Walk = "ANI_walk";
    const string ani_Fall = "ANI_fall";
    const string ani_Jump = "ANI_jump";
    const string ani_JumpTwo = "ANI_jumpTwo";
    const string ani_WallJumpL = "ANI_wallJumpL";
    const string ani_DiveWindup = "ANI_diveWindup";
    const string ani_Dive = "ANI_dive";
    const string ani_Punch = "ANI_punch";
    const string ani_Grapple = "ANI_grapple";

    // Start is called before the first frame update
    void Start()
    {
        m_Animator.GetComponentInChildren<Animator>();
        pB = GetComponent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // FixedUpdate is called once per physics change
    void FixedUpdate()
    {

    }


    /* 
        if (IsGrounded() && isWalking)
        {
            // m_Animator.SetBool("isWalking", isWalking);
            // m_Animator.SetBool("isStopped", false);
        }
    */

    void ChangeAnimationState(string newState) 
    {
        
    }
}
