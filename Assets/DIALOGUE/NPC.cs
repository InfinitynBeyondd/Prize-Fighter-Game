using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class NPC : MonoBehaviour, IInteractable
{


    [SerializeField] private SpriteRenderer interact;

    private Transform hexdogTransform;

    //public Transform hexdogFace;

    //add the hexdog char AC somewhere here to reset to idle when interact is active :)

    private const float interactDistance = 8f;

    public LayerMask isNPC;

    private void Start()
    {
        hexdogTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Update()
    {
        if (interact.gameObject.activeSelf && !WithinInteract())
        {
            interact.gameObject.SetActive(false);
        }

        else if (!interact.gameObject.activeSelf && WithinInteract())
        {
            interact.gameObject.SetActive(true);
        }

        if (Keyboard.current.eKey.wasPressedThisFrame && WithinInteract())
        {
            Interact();
            
            //RaycastHit hit;
            //if (Physics.Raycast(hexdogFace.position, hexdogFace.forward, out hit, interactDistance, isNPC))
            //{

            //Debug.Log("interacted");
            //Interact();
            //}
        }
    }

    public abstract void Interact();

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
