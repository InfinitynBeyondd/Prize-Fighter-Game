using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagTouch : MonoBehaviour
{
    public GameManager GameManager;
    private void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

            //Checks if we're in tutorial
            if (currentSceneIndex == 5)
            {
                GameManager.hasBeatTutorial = true;
                //Change later to a little screen that pops that you say you beat the specific level
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
            //Checks if we're in CoinPusher
            if (currentSceneIndex == 2)
            {
                GameManager.hasBeatCoinPusher = true;
                //Change later to a little screen that pops that you say you beat the specific level
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }

            //Checks if we're in Pachinko
            if (currentSceneIndex == 4)
            {
                GameManager.hasBeatPachinko = true;
                //Change later to a little screen that pops that you say you beat the specific level
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }

            //Checks if we're in Claw
            if (currentSceneIndex == 6)
            {
                GameManager.hasBeatClaw = true;
                //Change later to a little screen that pops that you say you beat the specific level
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
        }
    }
}
