using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class TRIGGER : MonoBehaviour
{

    [SerializeField] private AudioClip diagPopup;

    [SerializeField] private BoxCollider Tcollider;

    [SerializeField] private TriggerDialogueController TriggerDialogueController;

    //private bool conversationEnded;

    private void Start()
    {
        //dialogueText = TriggerDialogueController.GetComponent<DialogueText>();
    }

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && !ConversationEnded())
        {
            Trigger();
    
        }
    }

    private void OnTriggerEnter(Collider enemy)
    {
        Trigger();
        
        SoundFXManager.Instance.PlaySoundFXClip(diagPopup, transform, 0.8f);
        Tcollider.enabled = false;
    }

    public abstract void Trigger();

    private bool ConversationEnded()
    {
        if (TriggerDialogueController.conversationEnded)
        {
            return true;
        }
        else
        {
            return false;
        }
                
    }
}
