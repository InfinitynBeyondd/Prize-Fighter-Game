using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogHitbox : MonoBehaviour
{
    Rigidbody playerBody;
    Vector3 playerVelocity;
    public float dispelForce = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerBody = other.GetComponent<Rigidbody>();
            playerVelocity = playerBody.velocity;

            playerBody.AddForce(new Vector3(dispelForce, dispelForce, dispelForce) + playerVelocity, ForceMode.Impulse);

            Debug.Log("Frog Hitbox Entered: Flinging back!");
        }
    }

}
