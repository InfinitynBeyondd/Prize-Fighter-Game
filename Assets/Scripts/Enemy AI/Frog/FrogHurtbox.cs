using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogHurtbox : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
