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

    [Header("Sticker Sprites")]
    public Image Sticker1Image;
    public Sprite CoinPusherSticker;
    public Image Sticker2Image;
    public Sprite PachinkoSticker;
    public Image Sticker3Image;
    public Sprite ClawSticker;

    [Header("Game Manager")]
    public GameManager GameManager;
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

    private void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }
    //Gallery Functions
    public void GoToGalleryScreen()
    {
        mainMenuScreen.SetActive(false);
        gameGalleryScreen.SetActive(true);
    }
    public void GoToMainFromGallery()
    {
        gameGalleryScreen.SetActive(false);
        mainMenuScreen.SetActive(true);

        //Checks how many stickers are collected and changes the sticker showed depending on that
        if (GameManager.stickersCollected > 0 )
        {
            Sticker1Image.sprite = CoinPusherSticker;
            if (GameManager.stickersCollected > 1)
            {
                Sticker2Image.sprite = PachinkoSticker;
                if (GameManager.stickersCollected > 2)
                {
                    Sticker3Image.sprite = ClawSticker;
                }
            }
        }
    }

    public void GoToHub()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        GoToLevelButton = GameObject.Find("GoControlButton").GetComponent<Button>();
        GoToLevelButton.Select();

    }


}
