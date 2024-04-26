using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    [SerializeField] private GameObject claw;
    [SerializeField] private GameObject plats;
    [SerializeField] private GameObject bossMusic;
    [SerializeField] private Animator phaseAC;

    // Start is called before the first frame update
    void Start()
    {
        claw.gameObject.SetActive(false);
        bossMusic.gameObject.SetActive(false);
        plats.gameObject.SetActive(false);
        //phaseAC.SetBool("phaseEnd", false);
        //phaseAC.SetBool("phaseStart", false);
    }

    private void OnTriggerEnter(Collider enemy)
    {
        claw.gameObject.SetActive(true);
        bossMusic.gameObject.SetActive(true);
        plats.gameObject.SetActive(true);
        //phaseAC.SetBool("phaseStart", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
