using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogHitbox : MonoBehaviour
{
    Rigidbody PlayerBody;
    Vector3 PlayerVelocity;
    public float dispelForce = 100f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBody = other.GetComponent<Rigidbody>();
            PlayerVelocity = PlayerBody.velocity;

            PlayerBody.AddForce(new Vector3(600, 600,600) + PlayerVelocity, ForceMode.Impulse);

            Debug.LogWarning("Hitbox is working" + PlayerVelocity);
        }
    }
}
