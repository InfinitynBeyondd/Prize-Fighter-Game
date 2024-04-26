using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogHitbox : MonoBehaviour
{
    Rigidbody playerBody;
    Vector3 playerVelocity;
    public float dispelForce = 100f;

    [SerializeField] private AudioClip playerHit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerBody = other.GetComponent<Rigidbody>();
            playerVelocity = playerBody.velocity;

            playerBody.AddForce(new Vector3(dispelForce, dispelForce, dispelForce) + playerVelocity, ForceMode.Impulse);
            SoundFXManager.Instance.PlaySoundFXClip(playerHit, transform, 0.4f, 0.8f);
            Debug.Log("Frog Hitbox Entered: Flinging back!");
        }
    }

}
