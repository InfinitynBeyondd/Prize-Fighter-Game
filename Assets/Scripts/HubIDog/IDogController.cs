using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IDogController : MonoBehaviour
{
    public GameObject IDogDialogueScreen;
    public GameObject ShopScreen;

    public bool IsInShop = false;

    void Start()
    {
        
    }

    public void GoToShopScreen()
    {
        IsInShop = true;
        ShopScreen.SetActive(true);
        IDogDialogueScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IDogDialogueScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true; // Make the cursor visible
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IDogDialogueScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined; // Unlock the cursor
        Cursor.visible = false; // Make the cursor visible
    }

   public void GoToTutorialLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }

}
