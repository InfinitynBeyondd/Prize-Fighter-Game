using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class iDogDialogueSwitch : MonoBehaviour
{
    public GameManager GameManager;

    //[SerializeField] private GameObject iDog;

    [SerializeField] private GameObject iDog0;
    [SerializeField] private GameObject iDog1;
    [SerializeField] private GameObject iDog2;
    [SerializeField] private GameObject iDog3;
    [SerializeField] private GameObject button;


    //public DialogueText dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        //DialogueText dialogueText = iDog.GetComponent<DialogueText>();
        //iDogs = GameObject.FindWithTag("iDogs").SetActive(true);
        iDog0.gameObject.SetActive(false);
        iDog1.gameObject.SetActive(false);
        iDog2.gameObject.SetActive(false);
        iDog3.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.hasBeatTutorial)
        {
            //SwitchDialogue(convo[0]);
            iDog0.gameObject.SetActive(true);
        }
        if (GameManager.hasBeatTutorial)
        {
            //SwitchDialogue(convo[1]);
            iDog0.gameObject.SetActive(false);
            iDog1.gameObject.SetActive(true);
            button.gameObject.SetActive(true);
        }
        if (GameManager.hasBeatCoinPusher)
        {
            //SwitchDialogue(convo[2]);
            iDog0.gameObject.SetActive(false);
            iDog1.gameObject.SetActive(false);
            iDog2.gameObject.SetActive(true);
            button.gameObject.SetActive(true);
        }
        if (GameManager.hasBeatPachinko)
        {
            //SwitchDialogue(convo[3]);
            iDog0.gameObject.SetActive(false);
            iDog1.gameObject.SetActive(false);
            iDog2.gameObject.SetActive(false);
            iDog3.gameObject.SetActive(true);
            button.gameObject.SetActive(true);
        }
    }

    void SwitchDialogue(DialogueText convo)
    {
        //switch the dialoguetext gameobject of an npc to the selected conversation object
        //dialogueText = convo;
    }
}
