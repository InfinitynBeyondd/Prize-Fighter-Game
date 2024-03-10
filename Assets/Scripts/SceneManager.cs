using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // TO DO:
    // Scene Management, Respawn

    //~~~Chelle's Contribution~~~
    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit Game.");
            Application.Quit();
        }      

    }
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~
}
