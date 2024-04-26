using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class iDogDialogueSwitch : MonoBehaviour
{
    public GameManager GameManager;

    //[SerializeField] private GameObject iDog;
    [SerializeField] private GameObject iDogs;

    public DialogueText dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        //DialogueText dialogueText = iDog.GetComponent<DialogueText>();
        //iDogs = GameObject.FindWithTag("iDogs").SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.hasBeatTutorial)
        {
            //SwitchDialogue(convo[0]);
        }
        if (GameManager.hasBeatTutorial)
        {
           //SwitchDialogue(convo[1]);
        }
        if (GameManager.hasBeatCoinPusher)
        {
            //SwitchDialogue(convo[2]);
        }
        if (GameManager.hasBeatPachinko)
        {
            //SwitchDialogue(convo[3]);
        }
    }

    void SwitchDialogue(DialogueText convo)
    {
        //switch the dialoguetext gameobject of an npc to the selected conversation object
        dialogueText = convo;
    }
}
