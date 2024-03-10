using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
       // if(other.CompareTag("Player"))
       // {
       //     if(Input.GetKeyDown(KeyCode.Q))
       //         
       // }
       UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    
}
