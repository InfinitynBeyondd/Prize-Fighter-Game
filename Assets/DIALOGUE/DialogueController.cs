using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NPCNameText;
    [SerializeField] private TextMeshProUGUI NPCDialogueText;
    [SerializeField] private float typeSpeed = 10;
    [SerializeField] private AudioClip[] NPCDialogueAudioClip;
    [SerializeField] private AudioClip[] NPCGreeting;
    [SerializeField] private Animator animationController;


    public GameManager GameManager;

    private Queue<string> paragraphs = new Queue<string>();

    private bool conversationEnded;
    private bool isTyping;

    private string p;

    private Coroutine typeDialogueCoroutine;

    private const string HTML_ALPHA = "<color=#00000000>";
    private const float MAX_TYPE_TIME = 0.1f;

    private void Start()
    {
        animationController = gameObject.GetComponent<Animator>();
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

    }

    public void DisplayNextParagraph(DialogueText dialogueText)
    {
        if (paragraphs.Count == 0) 
        { 
        if (!conversationEnded)
            {
                GameObject.Find("HexDogPlayer").GetComponent<PlayerBehavior>().enabled = false;
                StartConverstaion(dialogueText);
            }

            else if (conversationEnded && !isTyping)
            {
                GameObject.Find("HexDogPlayer").GetComponent<PlayerBehavior>().enabled = true;
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
            SoundFXManager.Instance.PlayRandomSoundFXClip(NPCGreeting, transform, 0.7f, 0.2f);

            //invoke, play NPC interaction Anim if exists

            gameObject.SetActive(true);
            animationController.SetTrigger("Interact");

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

        ConversationEvent();
    }

    private IEnumerator TypeDialogueText(string p)
    {
        isTyping = true;

        NPCDialogueText.text = "";

        string originalText = p;
        string displayedText = "";
        int alphaIndex = 0;

        foreach (char c in p.ToCharArray())
        {
            alphaIndex++;
            NPCDialogueText.text = originalText;

            displayedText = NPCDialogueText.text.Insert(alphaIndex, HTML_ALPHA);
            NPCDialogueText.text = displayedText;

            if (alphaIndex % 2 == 0)
            {
                SoundFXManager.Instance.PlayRandomSoundFXClip(NPCDialogueAudioClip, transform, 0.1f, 0f);
            }

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

    private void ConversationEvent()
    {
        if (!GameManager.hasBeatTutorial)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(5);
        }
    }
}
