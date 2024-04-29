using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [Header("Out of Bounds")]
    public GameObject respawnPoint;
    public Vector3 pathToRespawn;

    [SerializeField] private AudioClip hexdogFall;
    [SerializeField] private AudioClip checkpoint;

    [SerializeField] private GameObject respawnTransition;
    [SerializeField] private Animator respawnTransitionAC;
    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn"); // Finds the first object with the respawn tag.
        pathToRespawn = respawnPoint.transform.position;
    }    

    // Once the player collides with anything forcing a respawn, it will be sent back to the level's designated spawn point.
    private void OnCollisionEnter(Collision other)
    {

        StartCoroutine(RespawnTransition(other));
    }

    // Detect when a checkpoint is crossed, then set the respawn coordinates to that checkpoint's position.
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Checkpoint"))
        {
            respawnPoint = other.gameObject;
            pathToRespawn = other.transform.position;
            SoundFXManager.Instance.PlaySoundFXClip(checkpoint, transform, 0.3f, 0.1f);
            Debug.Log("CHECKPOINT CROSSED - Respawn position has been set to: " + other.transform.position);
            //other.GetComponent<Collider>().enabled = false;
            other.gameObject.SetActive(false);
        }
        
    }

    //respawn transition

    IEnumerator RespawnTransition(Collision other)
    {
        if (other.transform.CompareTag("ForceRespawn"))
        {
            Debug.Log("OUT OF BOUNDS - Moving back to spawn point!");
            SoundFXManager.Instance.PlaySoundFXClip(hexdogFall, transform, 0.6f, 0f);
            respawnTransition.SetActive(true);
            respawnTransitionAC.SetTrigger("Start");
            yield return new WaitForSeconds(0.8f);
            transform.position = pathToRespawn;
            yield return new WaitForSeconds(1f);
            respawnTransition.SetActive(false);

        }
    }
}
