using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableCounters : MonoBehaviour
{
    public GameManager gM;
    public TMP_Text stickerCount;
    public TMP_Text coinCount;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();
        stickerCount.text = "Stickers: " + gM.stickersCollected.ToString();
        coinCount.text = "Coins: " + gM.coinsCollected.ToString();
    }

    public void UpdateCollectedCount() 
    {
        stickerCount.text = "Stickers: " + gM.stickersCollected.ToString();
        coinCount.text = "Coins: " + gM.coinsCollected.ToString();
    }
}
