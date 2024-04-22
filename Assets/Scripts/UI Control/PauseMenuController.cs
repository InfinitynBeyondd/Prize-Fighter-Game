using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject GalleryMenuUI;

    [Header("Sticker Sprites")]
    public Image Sticker1Image;
    public Sprite CoinPusherSticker;
    public Image Sticker2Image;
    public Sprite PachinkoSticker;
    public Image Sticker3Image;
    public Sprite ClawSticker;

    public GameManager GameManager;

    private void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }


    public void ResumeOrPause(InputAction.CallbackContext context)
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        Cursor.lockState = CursorLockMode.Confined; // Unlock the cursor
        Cursor.visible = false; // Make the cursor visible
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoBackToHubOrMainMenu()
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        // If we're in the hub go back to main menu
        if(currentSceneIndex == 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }

    public void GoToGallery()
    {
        PauseMenuUI.SetActive(false);
        GalleryMenuUI.SetActive(true);

        //Checks how many stickers are collected and changes the sticker showed depending on that
        if (GameManager.stickersCollected > 0)
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

    public void ExitGallery()
    {
        PauseMenuUI.SetActive(true);
        GalleryMenuUI.SetActive(false);
    }
}
