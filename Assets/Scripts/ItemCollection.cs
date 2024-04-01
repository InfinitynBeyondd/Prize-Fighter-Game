using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    [SerializeField] GameManager gM;
    CollectableCounters cCounters;

    [SerializeField] private AudioClip coinCollect;
    [SerializeField] private AudioClip stickerCollect;

    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();
        cCounters = GameObject.Find("Canvas").GetComponent<CollectableCounters>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") && this.transform.CompareTag("Sticker"))
        {
            Debug.Log("Got a Sticker!");
            SoundFXManager.Instance.PlaySoundFXClip(stickerCollect, transform, 0.6f);
            this.gameObject.SetActive(false);
            gM.stickersCollected++;
            cCounters.UpdateCollectedCount();
        }

        if (other.transform.CompareTag("Player") && this.transform.CompareTag("Coin"))
        {
            Debug.Log("Coin Collected!");
            this.gameObject.SetActive(false);
            gM.coinsCollected++;
            cCounters.UpdateCollectedCount();
            SoundFXManager.Instance.PlaySoundFXClip(coinCollect, transform, 0.7f);
        }
    }
}
