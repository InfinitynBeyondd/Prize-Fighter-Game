using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    [Header("Out of Bounds")]
    [SerializeField] GameObject respawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = GameObject.FindGameObjectWithTag("Respawn"); // Finds the first object with the respawn tag.
    }    

    // Once the player collides with anything forcing a respawn, it will be sent back to the level's designated spawn point.
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("OUT OF BOUNDS - Moving back to spawn point!");
            other.transform.position = respawnPoint.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
