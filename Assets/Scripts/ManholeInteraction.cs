using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManholeInteraction : MonoBehaviour
{
    [SerializeField] Collider manholeMeshCollider; // Mesh collider for the manhole; will be turned off on hit to prevent it potentially flinging the player.
    [SerializeField] float yAxisMaxRotationSpeed; // Max speed the manhole will rotate around the Y Axis by.
    [SerializeField] float yAxisRotationSpeed; // Speed the manhole will rotate around the Y Axis by; it should eventually slow to a stop.
    [SerializeField] bool spinInitiated; // Boolean that controls when the manhole spins or not.
    [SerializeField] private AudioClip manholeSpin;

    public GameObject coinPrefab;
    public int coinsToSpawn;

    // When the manhole is punched, set the boolean that makes it spin on its Y Axis to true.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack"))
        {
            spinInitiated = true;
            yAxisRotationSpeed = yAxisMaxRotationSpeed;
            SoundFXManager.Instance.PlaySoundFXClip(manholeSpin, transform, 0.4f, 0.2f);
            Debug.Log("Manhole Punched. Spin Begin!");
            SpawnCoin();
        }
    }

    // Function that makes the manhole spin on its Y Axis.
    void ManholeSpin(float speedOfSpin) 
    {
        //transform.Rotate(new Vector3(0, 0f, speedOfSpin), zAxisRotationSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, speedOfSpin, 0));
        manholeMeshCollider.enabled = false;                
    }

    // This function should reset the manhole to the neutral position.
    void ManholeReturnToNeutral() 
    {
        if (transform.rotation.y % 360 != 0) 
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        spinInitiated = false;        
    }

    void SpawnCoin()
    {
        for (int i = 0; i < 1; i++) 
        {            
            coinPrefab.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {        
        yAxisMaxRotationSpeed = 90f;
        yAxisRotationSpeed = yAxisMaxRotationSpeed;
        manholeMeshCollider = GetComponent<Collider>();
        spinInitiated = false;
        coinPrefab.SetActive(false);
    }


    // FixedUpdate is called once per physics change
    void FixedUpdate()
    {
        if (spinInitiated) 
        {
            ManholeSpin(yAxisRotationSpeed);
            Invoke(nameof(ManholeReturnToNeutral), 3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Within 2 seconds upon being punched, the manhole slows to a stop.
        if (yAxisRotationSpeed != 0f) 
        {
            yAxisRotationSpeed-= (yAxisMaxRotationSpeed / 2f) * Time.deltaTime;
        }

        // Prevents the manhole from spinning in reverse.
        if (yAxisRotationSpeed < 0f) 
        {
            yAxisRotationSpeed = 0f;
        }

        /*if (yAxisRotationSpeed == 0f) 
        {
            manholeMeshCollider.enabled = true;
        }*/

    }

}
