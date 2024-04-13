using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressing : MonoBehaviour
{
    [SerializeField] Animator thisButtonAnimator;
    public bool buttonIsPressed;

    [Header("SET THIS IN THE INSPECTOR!")]
    [SerializeField] Transform partneredAsset; // Transform affected by whichever button this is. SET THIS IN THE INSPECTOR!
    
    // Start is called before the first frame update
    void Start()
    {
        thisButtonAnimator = GetComponentInParent<Animator>();
        SetPartneredAssetAtStart();
    }

    // Update is called once per frame
    void Update()
    {
        thisButtonAnimator.SetBool("isPressed", buttonIsPressed);
        ToggleOnOrOff();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Attack")) 
        {
            buttonIsPressed = true;            
        }
    }

    void SetPartneredAssetAtStart()
    {
        if (partneredAsset.CompareTag("ButtonActivated"))
        {
            partneredAsset.gameObject.SetActive(false);
        }
        else if (partneredAsset.CompareTag("ButtonDeactivated"))
        {
            partneredAsset.gameObject.SetActive(true);
        }
    }

    void ToggleOnOrOff() 
    {
        if (buttonIsPressed && partneredAsset.CompareTag("ButtonActivated"))
        {
            //Debug.Log("Button pressed - TURN ON THIS TRANSFORM!");
            partneredAsset.gameObject.SetActive(buttonIsPressed);
        }
        else if (buttonIsPressed && partneredAsset.CompareTag("ButtonDeactivated"))
        {
            //Debug.Log("Button pressed - TURN OFF THIS TRANSFORM!");
            partneredAsset.gameObject.SetActive(!buttonIsPressed);
        }
        else if (buttonIsPressed && partneredAsset.CompareTag("ButtonAnimated")) 
        {
            partneredAsset.GetComponent<Animator>().SetBool("isButtonPressed", buttonIsPressed);
        }
    }

}
