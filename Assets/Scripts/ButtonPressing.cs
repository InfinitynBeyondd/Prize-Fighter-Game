using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressing : MonoBehaviour
{
    [SerializeField] Animator thisButtonAnimator;
    public bool buttonIsPressed;

    // Start is called before the first frame update
    void Start()
    {
        thisButtonAnimator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        thisButtonAnimator.SetBool("isPressed", buttonIsPressed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack")) 
        {
            buttonIsPressed = true;            
        }
    }
}
