using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [Header("Out of Bounds")]
    public GameObject respawnPoint;
    public Vector3 pathToRespawn;

    [SerializeField] private AudioClip hexdogFall;
    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn"); // Finds the first object with the respawn tag.
        pathToRespawn = respawnPoint.transform.position;
    }    

    // Once the player collides with anything forcing a respawn, it will be sent back to the level's designated spawn point.
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("ForceRespawn"))
        {
            Debug.Log("OUT OF BOUNDS - Moving back to spawn point!");
            transform.position = pathToRespawn;
            SoundFXManager.Instance.PlaySoundFXClip(hexdogFall, transform, 0.3f);
        }
    }

    // Detect when a checkpoint is crossed, then set the respawn coordinates to that checkpoint's position.
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Checkpoint"))
        {
            respawnPoint = other.gameObject;
            pathToRespawn = other.transform.position;
            Debug.Log("CHECKPOINT CROSSED - Respawn position has been set to: " + other.transform.position);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
