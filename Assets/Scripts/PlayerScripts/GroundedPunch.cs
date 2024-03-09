using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedPunch : MonoBehaviour
{
    PlayerBehavior pB;
    
    //Rigidbody rB;
    //GravityScalePhysX gSPX;

    [SerializeField] GameObject playerModel;
    [SerializeField] GameObject groundAttackFX;
    public Animator attackAnim;
    bool attacking;
    
    //public float currentPlayerXPos;
    //public float currentPlayerYPos;
    //public float currentPlayerZPos;

    // Start is called before the first frame update
    void Start()
    {
        pB = GetComponent<PlayerBehavior>();
        //rB = GetComponent<Rigidbody>();
        
        groundAttackFX.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (pB.IsGrounded() && Input.GetKeyDown(pB.attackKey) && !groundAttackFX.activeSelf) 
        {
            Attack();            
        }        
    }

    // For now, the attack will be translated through the animator. This does crate a slight bug of the attack being displaced, but the hitbox should be wide enough to cover for it.
    void Attack()
    {
        //groundAttackFX.transform.Translate(Vector3.forward);
        groundAttackFX.SetActive(true);
        attacking = groundAttackFX.activeSelf;
        attackAnim.SetBool("Attacking", attacking);
        StartCoroutine("AttackReset");
        //groundAttackFX.transform.position += new Vector3(playerModel.transform.position.x, playerModel.transform.position.y, 10f);
        //groundAttackFX.transform.Translate(new Vector3(playerModel.transform.position.x, playerModel.transform.position.y, 10f));
    }

    // Coroutine that resets the attack status.
    IEnumerator AttackReset() 
    {
        yield return new WaitForSeconds(0.5f);
        groundAttackFX.SetActive(false);
        groundAttackFX.transform.position = new Vector3(playerModel.transform.position.x, playerModel.transform.position.y, playerModel.transform.position.z + 1.5f);
        attacking = groundAttackFX.activeSelf;
        attackAnim.SetBool("Attacking", attacking);
    }

}
