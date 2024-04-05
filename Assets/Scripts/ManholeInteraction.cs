using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManholeInteraction : MonoBehaviour
{
    [SerializeField] Collider manholeMeshCollider; // Mesh collider for the manhole; will be turned off on hit to prevent it potentially flinging the player.
    [SerializeField] float zAxisMaxRotationSpeed; // Max speed the manhole will rotate around the Z Axis by.
    [SerializeField] float zAxisRotationSpeed; // Speed the manhole will rotate around the Z Axis by; it should eventually slow to a stop.
    [SerializeField] bool spinInitiated; // Boolean that controls when the manhole spins or not.

    // When the manhole is punched, set the boolean that makes it spin on its Z Axis to true.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            spinInitiated = true;
            zAxisRotationSpeed = zAxisMaxRotationSpeed;
            Debug.Log("Manhole Punched. Spin Begin!");
        }
    }

    // Function that makes the manhole spin on its Z axis.
    void ManholeSpin(float speedOfSpin) 
    {
        //transform.Rotate(new Vector3(0, 0f, speedOfSpin), zAxisRotationSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, speedOfSpin));
        manholeMeshCollider.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {        
        zAxisMaxRotationSpeed = 90f;
        zAxisRotationSpeed = zAxisMaxRotationSpeed;
        manholeMeshCollider = GetComponent<Collider>();
        spinInitiated = false;
    }


    // FixedUpdate is called once per physics change
    void FixedUpdate()
    {
        if (spinInitiated) 
        {
            ManholeSpin(zAxisRotationSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Within 2 seconds upon being punched, the manhole slows to a stop.
        if (zAxisRotationSpeed != 0f) 
        {
            zAxisRotationSpeed-= (zAxisMaxRotationSpeed / 2f) * Time.deltaTime;
        }

        // Prevents the manhole from spinning in reverse.
        if (zAxisRotationSpeed < 0f) 
        {
            zAxisRotationSpeed = 0f;
        }

        if (zAxisRotationSpeed == 0f) 
        {
            manholeMeshCollider.enabled = true;
        }

    }
}
