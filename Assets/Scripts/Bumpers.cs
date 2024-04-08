using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumpers : MonoBehaviour
{    
    GravityScalePhysX playerGSPX;
    Rigidbody playerBody;
    DivePunch playerDP;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GameObject.Find("HexDogPlayer").GetComponent<Rigidbody>();
        playerGSPX = GameObject.Find("HexDogPlayer").GetComponent<GravityScalePhysX>();
        playerDP = GameObject.Find("HexDogPlayer").GetComponent<DivePunch>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            playerBody.AddForce(Vector3.up * playerGSPX.gravityScale * 3, ForceMode.VelocityChange);

            Debug.Log("Bumper Bounce!");
        }        
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack")) 
        {
            playerBody.AddForce(Vector3.up * 50, ForceMode.VelocityChange);

            Debug.Log("ZOOM!");
        }
    }*/

    // Update is called once per frame
    void Update()
    {
        
    }
}
