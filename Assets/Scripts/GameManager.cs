using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int stickersCollected;
    public int coinsCollected;
    private bool skinCheck;

    [Header("Check if level beaten")]
    public bool hasBeatTutorial;
    public bool hasBeatCoinPusher;
    public bool hasBeatPachinko;
    public bool hasBeatClaw;

    [Header("PlayerSkins")]
    public Material currentPlayerSkin;
    public Material DefaultSkin;
    public Material GreenSkin;
    public Material PurpleSkin;
    public Material TransSkin;
    public Material GreySkin;
    public Material GundamSkin;


    // Start is called before the first frame update

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        currentPlayerSkin = DefaultSkin;
    }

    public void FixedUpdate()
    {
        //Test degug that gets you to other scenes
        //if (Input.GetKeyUp(KeyCode.O)) {UnityEngine.SceneManagement.SceneManager.LoadScene(2);}
        //if (Input.GetKeyUp(KeyCode.T)){UnityEngine.SceneManagement.SceneManager.LoadScene(4);}

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // This function will be called every time a new scene is loaded
        Debug.Log("Scene loaded: " + scene.name);

        // Call your function here
       SkinnedMeshRenderer PlayerMeshRenderer = GameObject.FindWithTag("CharacterBody").GetComponent<SkinnedMeshRenderer>();
        PlayerMeshRenderer.material = currentPlayerSkin;

        skinCheck = true;
    }
}
