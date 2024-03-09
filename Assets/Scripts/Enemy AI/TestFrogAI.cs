using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestFrogAI : MonoBehaviour
{
    public NavMeshAgent testFrog;

    // Start is called before the first frame update
    void Start()
    {
        testFrog = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            Debug.Log("TAB CLICKED");
            Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(movePosition, out var hitInfo)) 
            {
                testFrog.SetDestination(hitInfo.point);
                //transform.Translate(Vector3.up);
            }
        }
    }
}
