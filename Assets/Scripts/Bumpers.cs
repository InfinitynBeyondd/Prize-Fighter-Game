using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumpers : MonoBehaviour
{
    [SerializeField] GravityScalePhysX playerGSPX;
    [SerializeField] Rigidbody playerBody;
    [SerializeField] DivePunch playerDP;
    [SerializeField] int constantFactor; // Factor to multiply upwards force by. SET IN THE INSPECTOR!
    [SerializeField] private AudioClip[] carbump;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GameObject.Find("HexDogPlayer").GetComponent<Rigidbody>();
        playerGSPX = GameObject.Find("HexDogPlayer").GetComponent<GravityScalePhysX>();
        playerDP = GameObject.Find("HexDogPlayer").GetComponent<DivePunch>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" || collision.transform.tag == "PlayerFeet")
        {
            playerBody.AddForce(Vector3.up * playerGSPX.gravityScale * constantFactor, ForceMode.VelocityChange);
            SoundFXManager.Instance.PlayRandomSoundFXClip(carbump, transform, 0.6f);
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
