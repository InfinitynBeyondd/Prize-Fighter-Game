using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class HubLevelSelectController : MonoBehaviour
{
    public int SpawnID;
    public GameManager GameManager;
    public SceneSwitch SceneSwitch;

    public bool LevelStart;
    

    private void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();    
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Goes to Coin Pusher
            if (SpawnID == 1 && GameManager.hasBeatTutorial)
            {
                if (SceneSwitch.LevelStart == true)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                }
            }

            //Goes to Pachinko
            if (SpawnID == 2 && GameManager.hasBeatCoinPusher)
            {
                if (SceneSwitch.LevelStart == true)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(4);
                }
            }

            //Goes to Claw
            if (SpawnID == 3 && GameManager.hasBeatPachinko)
            {
                if (SceneSwitch.LevelStart == true)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(6);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SceneSwitch.LevelStart = false;
    }

}
