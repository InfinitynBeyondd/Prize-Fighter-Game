using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClawNPCSpawn : MonoBehaviour
{
    [SerializeField] private GameObject clawNPC;
    [SerializeField] private GameObject bossWalls;
    [SerializeField] private GameObject clawInteract;
    [SerializeField] private Animator clawAC;

    // Start is called before the first frame update
    void Start()
    {
        clawNPC.gameObject.SetActive(false);
        bossWalls.gameObject.SetActive(false);
        clawAC.SetBool("isIdle", false);
        clawAC.SetBool("isEntering", false);
        clawAC.SetBool("isLeaving", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            clawInteract.gameObject.SetActive(true);
            clawNPC.gameObject.SetActive(true);
            clawAC.SetBool("isEntering", true);
            Invoke("ClawIdle", 0.5f);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            clawAC.SetBool("isLeaving", true);
            StartCoroutine("ClawLeave", 0.2f);
        }
    }
  
    void ClawIdle()
    {
        bossWalls.gameObject.SetActive(true);
        //clawNPC.transform.position = new Vector3(0f, -5f, 0f);

        clawAC.SetBool("isIdle", true);
    }
    private IEnumerator ClawLeave()
    {
        //clawNPC.transform.position = new Vector3(0f, 0f, 0f);
        bossWalls.gameObject.SetActive(false);
        clawInteract.gameObject.GetComponent<idofgdialoggue>().enabled = false;
        clawInteract.gameObject.SetActive(false);
        yield return new WaitForSeconds((float)2.5f);
        clawNPC.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
