using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogJumpbox : MonoBehaviour
{

    [SerializeField] private AudioClip frogJump;

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SoundFXManager.Instance.PlaySoundFXClip(frogJump, transform, 0.9f, 0.1f);
            Debug.Log("bounced");

        }
    } 
}
