using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private GameObject levelPopup;
    [SerializeField] private SpriteRenderer levelLocked;
    //[SerializeField] private AudioClip uiPopup;
    [SerializeField] private AudioClip levelSelect;

    private Transform hexdogTransform;

    private const float interactDistance = 8f;

    private bool levelUnlocked;

    private void Start()
    {
        hexdogTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //levelUnlocked = get level progression component from game manager!!!
    }

    private void Update()
    {
        if (levelPopup.gameObject.activeSelf && !WithinInteract())
        {
            levelPopup.gameObject.SetActive(false);
            levelLocked.gameObject.SetActive(false);
        }

        else if (!levelPopup.gameObject.activeSelf && WithinInteract())
        {
            levelPopup.gameObject.SetActive(true);
            //if level is unlocked, show or hide locked icon
            if (!levelUnlocked) 
            {
                levelLocked.gameObject.SetActive(true);
            }
            else
            {
                levelLocked.gameObject.SetActive(false);
            }
        }

        if (Keyboard.current.eKey.wasPressedThisFrame && WithinInteract())
        {
            //Scene transition
            SoundFXManager.Instance.PlaySoundFXClip(levelSelect, transform, 0.4f, 0f);
            Debug.Log("level selected");
        }

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
}
