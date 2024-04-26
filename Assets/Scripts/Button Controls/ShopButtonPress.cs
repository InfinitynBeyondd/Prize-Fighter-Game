using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopButtonPress : MonoBehaviour
{

    // THIS IS THE SHOP ONLY BUTTON

    [SerializeField] Animator thisButtonAnimator;
    public bool buttonIsPressed = false;

    [Header("SET THIS IN THE INSPECTOR!")]
    [SerializeField] Transform partneredAsset; // Transform affected by whichever button this is. SET THIS IN THE INSPECTOR!

    [SerializeField] private AudioClip buttonPressed;

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
        if (other.CompareTag("Attack") && buttonIsPressed == false) 
        {
            buttonIsPressed = true;
            SoundFXManager.Instance.PlaySoundFXClip(buttonPressed, transform, 0.4f, 0.2f);

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

    public void ToggleOnOrOff() 
    {
        if (buttonIsPressed && partneredAsset.CompareTag("ButtonActivated"))
        {
            //Debug.Log("Button pressed - TURN ON THIS TRANSFORM!");
            partneredAsset.gameObject.SetActive(buttonIsPressed);
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true; // Make the cursor visible
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

    public void TurnOffTheGame()
    {
        partneredAsset.gameObject.SetActive(false);
        buttonIsPressed = false;

    }

}
