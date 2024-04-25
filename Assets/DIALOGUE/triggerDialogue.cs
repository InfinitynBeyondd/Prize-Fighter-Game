using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerDialogue : TRIGGER, ITalkable
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private TriggerDialogueController TdialogueController;
    //public int dialogueText.paragraphs.Length;
    public override void Trigger()
    {
        Talk(dialogueText);
    }

    public void Talk(DialogueText dialogueText)
    {
        TdialogueController.DisplayNextParagraph(dialogueText);
    }

    }
