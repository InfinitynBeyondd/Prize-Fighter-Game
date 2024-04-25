using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;

public class TriggerDialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NPCNameText;
    [SerializeField] private TextMeshProUGUI NPCDialogueText;
    [SerializeField] private float typeSpeed = 10;
    [SerializeField] private AudioClip[] NPCDialogueAudioClip;
    [SerializeField] private Animator animationController;

    private Queue<string> paragraphs = new Queue<string>();

    public bool conversationEnded;
    //public int paragraphcount = dialogueText.paragraphs.Length;
    private bool isTyping;

    private string p;

    private Coroutine typeDialogueCoroutine;

    private const string HTML_ALPHA = "<color=#00000000>";
    private const float MAX_TYPE_TIME = 0.1f;

    private void Start()
    {
        animationController = gameObject.GetComponent<Animator>();

    }

    public void DisplayNextParagraph(DialogueText dialogueText)
    {
        if (paragraphs.Count == 0) 
        { 
        if (!conversationEnded)
            {
                StartConverstaion(dialogueText);
            }

            else if (conversationEnded && !isTyping)
            {
                EndConverstaion();
                return;
            }
        }

        if (!isTyping)
        {
            p = paragraphs.Dequeue();

            typeDialogueCoroutine = StartCoroutine(TypeDialogueText(p));
        }

        else
        {
            FinishParagraphEarly();
        }



        if (paragraphs.Count == 0)
        {
            conversationEnded = true;
        }
    }
    
    private void StartConverstaion(DialogueText dialogueText)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);

            animationController.SetTrigger("Interact");
            Debug.Log("playing anim");
        }


        NPCNameText.text = dialogueText.speakerName;


        for (int i = 0; i < dialogueText.paragraphs.Length; i++)
        {
            paragraphs.Enqueue(dialogueText.paragraphs[i]);
        }
    }
    private void EndConverstaion()
    {
        paragraphs.Clear();

        conversationEnded = false;

        if (gameObject.activeSelf)
        {
            gameObject.SetActive (false);
            
        }
    }



    private IEnumerator TypeDialogueText(string p)
    {
        isTyping = true;

        int maxVisibleChars = 0;

        NPCDialogueText.text = p;
        NPCDialogueText.maxVisibleCharacters = maxVisibleChars;

        foreach (char c in p.ToCharArray())
        {

            maxVisibleChars++;
            NPCDialogueText.maxVisibleCharacters = maxVisibleChars;

            SoundFXManager.Instance.PlayRandomSoundFXClip(NPCDialogueAudioClip, transform, 0.1f, 0f);

            yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
        }

        isTyping = false;
    }
    private void FinishParagraphEarly()
    {
        StopCoroutine(typeDialogueCoroutine);

        NPCDialogueText.text = p;

        isTyping = false;
    }
}
