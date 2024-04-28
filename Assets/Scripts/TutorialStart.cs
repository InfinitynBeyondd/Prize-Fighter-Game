using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStart : MonoBehaviour
{
    //sets a flag in the GM to make sure player doesn't get teleported to the tutorial after every convo (scuffed i know)

    public GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.hasBeatTutorial = true;
        Debug.Log("tut start");
    }

}
