using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    [SerializeField] GameManager gM;
    CollectableCounters cCounters;

    [SerializeField] private AudioClip coinCollect;
    [SerializeField] private AudioClip stickerCollect;
    [SerializeField] private AudioSource levelMusic;
    private GameObject GotStickerImageGO;

    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();
        cCounters = GameObject.Find("LEVEL UI").GetComponentInChildren<CollectableCounters>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.K)) 
        {
            //StartCoroutine(WaitForXSeconds(5));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") && this.transform.CompareTag("Sticker"))
        {
            Debug.Log("Got a Sticker!");
            levelMusic.volume = 0f;
            SoundFXManager.Instance.PlaySoundFXClip(stickerCollect, transform, 0.7f, 0f);
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.GetComponent<MeshCollider>().enabled = false;
            gM.stickersCollected++;
            cCounters.UpdateCollectedCount();

            GotStickerImageGO = GameObject.FindWithTag("StickerImage");
            Image GotStickerImage = GotStickerImageGO.GetComponent<Image>();
            StartCoroutine(WaitForXSeconds(3,GotStickerImage));


        }

        if (other.transform.CompareTag("Player") && this.transform.CompareTag("Coin"))
        {
            Debug.Log("Coin Collected!");
            this.gameObject.SetActive(false);
            gM.coinsCollected++;
            cCounters.UpdateCollectedCount();
            SoundFXManager.Instance.PlaySoundFXClip(coinCollect, transform, 0.5f, 0f);
        }
    }

    IEnumerator WaitForXSeconds(int seconds, Image GotStickerImage)
    {
        GotStickerImage.color = new Color(255, 255, 255, 255);
        yield return new WaitForSeconds(seconds);
        GotStickerImage.color = new Color(255, 255, 255, 0);
        levelMusic.volume = 0.5f;
        this.gameObject.SetActive(false) ;
    }
}
