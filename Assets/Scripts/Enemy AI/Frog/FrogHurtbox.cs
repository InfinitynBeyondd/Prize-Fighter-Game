using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogHurtbox : MonoBehaviour
{
    [SerializeField] Animator frogAnim;
    //[SerializeField] GameObject frogEnemyParent;

    private void Awake()
    {
        //frogEnemyParent = GetComponentInParent<GameObject>();
        //frogAnim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            //Destroy(transform.parent.gameObject);
            frogAnim.SetBool("isAttacked", true);
            Debug.Log("PACKWATCH");
        }
    }
}
