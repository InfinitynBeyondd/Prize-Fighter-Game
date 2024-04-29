using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class HubLevelSelectController : MonoBehaviour
{
    [SerializeField] private GameObject levelPopup;
    [SerializeField] private GameObject transitionObject;
    [SerializeField] private Animator transitionObjectAC;
    [SerializeField] private SpriteRenderer[] levelLocks;
    //[SerializeField] private AudioClip uiPopup;
    [SerializeField] private AudioClip levelSelect;

    public int SpawnID;
    public GameManager GameManager;
    public SceneSwitch SceneSwitch;

    public bool LevelStart;

    private Transform hexdogTransform;

    private const float interactDistance = 8f;

    //private bool levelUnlocked;

    private void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        hexdogTransform = GameObject.FindGameObjectWithTag("Player").transform;

        //levelUnlocked = GameManager.hasBeatTutorial;
    }

    //void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
            //Goes to Coin Pusher
    //        if (SpawnID == 1 && GameManager.hasBeatTutorial)
    //        {
    //            if (SceneSwitch.LevelStart == true)
    //            {
    //                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    //            }
    //        }

            //Goes to Pachinko
    //       if (SpawnID == 2 && GameManager.hasBeatCoinPusher)
    //        {
    //            if (SceneSwitch.LevelStart == true)
    //            {
    //                UnityEngine.SceneManagement.SceneManager.LoadScene(4);
    //            }
    //        }

            //Goes to Claw
    //        if (SpawnID == 3 && GameManager.hasBeatPachinko)
    //        {
    //            if (SceneSwitch.LevelStart == true)
    //            {
    //               UnityEngine.SceneManagement.SceneManager.LoadScene(6);
    //            }
    //       }
    //   }
    //}

    private void Update()
    {
        if (levelPopup.gameObject.activeSelf && !WithinInteract())
        {
            levelPopup.gameObject.SetActive(false);

        }

        else if (!levelPopup.gameObject.activeSelf && WithinInteract())
        {
            levelPopup.gameObject.SetActive(true);

            
            if (GameManager.hasBeatTutorial)
            {
                levelLocks[0].gameObject.SetActive(false);
            }
            if (GameManager.hasBeatCoinPusher)
            {
                levelLocks[2].gameObject.SetActive(false);
            }
            if (GameManager.hasBeatPachinko)
            {
                levelLocks[1].gameObject.SetActive(false);
            }
        }

        if (Keyboard.current.eKey.wasPressedThisFrame && WithinInteract())
        {
            LevelTransition();
            SoundFXManager.Instance.PlaySoundFXClip(levelSelect, transform, 0.7f, 0f);
            //scenetransitionAnim
            Debug.Log("level selected");
        }
    }

    void LevelTransition()
    {
        StartCoroutine(SceneTransition());
    }

    private bool WithinInteract()
    {
        if (Vector3.Distance(hexdogTransform.position, transform.position) < interactDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator SceneTransition()
    {
        if (SpawnID == 1 && GameManager.hasBeatTutorial)
        {

            transitionObject.SetActive(true);
            transitionObjectAC.SetTrigger("Start");
            yield return new WaitForSeconds(2f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);

        }

        //Goes to Pachinko
        if (SpawnID == 2 && GameManager.hasBeatCoinPusher)
        {
            transitionObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(4);

        }

        //Goes to Claw
        if (SpawnID == 3 && GameManager.hasBeatPachinko)
        {
            transitionObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(6);

        }
    }

}
