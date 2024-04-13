using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int stickersCollected;
    public int coinsCollected;

    [Header("Check if level beaten")]
    public bool hasBeatTutorial;
    public bool hasBeatCoinPusher;
    public bool hasBeatPachinko;
    public bool hasBeatClaw;


    // Start is called before the first frame update

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
