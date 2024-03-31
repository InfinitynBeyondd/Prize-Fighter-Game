using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuScreen;
    public GameObject controlScreen;
    public GameObject gameCreditsScreen;
    public GameObject gameOptionsScreen;
    public GameObject gameGalleryScreen;

    public Button GoToLevelButton;
    // Start is called before the first frame update


    public void GoToControlsScreen()
    {
        mainMenuScreen.SetActive(false);
        controlScreen.SetActive(true);
    }
    public void GoToCreditsScreen()
    {
        mainMenuScreen.SetActive(false);
        gameCreditsScreen.SetActive(true);
    }
    public void GoToMainFromCredits()
    {
        gameCreditsScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
    }

    public void GoToLevelOne()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        GoToLevelButton = GameObject.Find("GoControlButton").GetComponent<Button>();
        GoToLevelButton.Select();

    }


}
