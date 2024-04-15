using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    [SerializeField] private GameObject claw;
    [SerializeField] private GameObject bossMusic;

    // Start is called before the first frame update
    void Start()
    {
        claw.gameObject.SetActive(false);
        bossMusic.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider enemy)
    {
        claw.gameObject.SetActive(true);
        bossMusic.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
