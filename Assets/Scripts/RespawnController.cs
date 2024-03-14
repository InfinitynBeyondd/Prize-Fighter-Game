using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [Header("Out of Bounds")]
    [SerializeField] GameObject respawnPoint;
    public Vector3 pathToRespawn;


    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn"); // Finds the first object with the respawn tag.
        pathToRespawn = respawnPoint.transform.position;
    }    

    // Once the player collides with anything forcing a respawn, it will be sent back to the level's designated spawn point.
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("OUT OF BOUNDS - Moving back to spawn point!");
            other.transform.position = pathToRespawn;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
