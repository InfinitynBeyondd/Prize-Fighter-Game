using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopController : MonoBehaviour
{
    public GameObject SkinShopScreen;
    public GameObject IDogDialogue;
    public GameManager GameManager;
    public SkinnedMeshRenderer PlayerMeshRenderer;

    [Header("Player Skins")]
    public Material DefaultSkin;
    public Material GreenSkin;
    public Material PurpleSkin;
    public Material TransSkin;
    public Material GreySkin;
    public Material GundamSkin;

    [Header("SkinBuyButtons")]
    public TextMeshProUGUI Skin1Text;
    public TextMeshProUGUI Skin2Text;
    public TextMeshProUGUI Skin3Text;
    public TextMeshProUGUI Skin4Text;

    [Header("SFX")]
    [SerializeField] private AudioClip shopBuy;
    [SerializeField] private AudioClip shopPoor;

    [Header("Coin Counter")]
    public GameObject CoinCounter;
    public TMP_Text CoinText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();    
        PlayerMeshRenderer = GameObject.FindWithTag("CharacterBody").GetComponent<SkinnedMeshRenderer>();
        CoinText = CoinCounter.GetComponent<TMP_Text>();

        if(GameManager.BoughtSkin1 == true) { Skin1Text.text = "Equip"; }
        if (GameManager.BoughtSkin2 == true) { Skin2Text.text = "Equip"; }
        if (GameManager.BoughtSkin3 == true) { Skin3Text.text = "Equip"; }
        if (GameManager.BoughtSkin4 == true) { Skin4Text.text = "Equip"; }
    }

    public void ExitShop()
    {
        SkinShopScreen.SetActive(false);
        IDogDialogue.SetActive(true);
    }


    public void BuySkin1()
    {
        if (GameManager.BoughtSkin1 == false)
        {
            //If the player has the minimum amount of coins tyhen it subtract them
            if (GameManager.coinsCollected >= 25)
            {
                GameManager.coinsCollected -= 25;
                GameManager.BoughtSkin1 = true;
                Skin1Text.text = "Equip";
                SoundFXManager.Instance.PlaySoundFXClip(shopBuy, transform, 0.5f, 0f);
                CoinText.text = GameManager.coinsCollected.ToString();
            }
            else
            {
                Debug.Log("You need more money!!!");
                SoundFXManager.Instance.PlaySoundFXClip(shopPoor, transform, 0.5f, 0f);
            }
        }
        else
        {
            PlayerMeshRenderer.material = GreenSkin;
            GameManager.currentPlayerSkin = GreenSkin;
        }
    }

    public void BuySkin2()
    {
        if (GameManager.BoughtSkin2 == false)
        {
            if (GameManager.coinsCollected >= 30)
            {
                GameManager.coinsCollected -= 30;
                GameManager.BoughtSkin2 = true;
                Skin2Text.text = "Equip";
                SoundFXManager.Instance.PlaySoundFXClip(shopBuy, transform, 0.7f, 0f);
                CoinText.text = GameManager.coinsCollected.ToString();
            }
            else
            {
                Debug.Log("You need more money!!!");
                SoundFXManager.Instance.PlaySoundFXClip(shopPoor, transform, 0.5f, 0f);
            }
        }
        else
        {
            PlayerMeshRenderer.material = PurpleSkin;
            GameManager.currentPlayerSkin = PurpleSkin;
        }

    }
    public void BuySkin3()
    {
        if (GameManager.BoughtSkin3 == false)
        {
            if (GameManager.coinsCollected >= 35)
            {
                GameManager.coinsCollected -= 35;
                GameManager.BoughtSkin3 = true;
                Skin3Text.text = "Equip";
                SoundFXManager.Instance.PlaySoundFXClip(shopBuy, transform, 0.5f, 0f);
                CoinText.text = GameManager.coinsCollected.ToString();
            }
            else
            {
                Debug.Log("You need more money!!!");
                SoundFXManager.Instance.PlaySoundFXClip(shopPoor, transform, 0.5f, 0f);
            }
        }
        else
        {
            PlayerMeshRenderer.material = TransSkin;
            GameManager.currentPlayerSkin = TransSkin;
        }

    }

    public void BuySkin4()
    {
        if (GameManager.BoughtSkin4 == false)
        {
            if (GameManager.coinsCollected >= 40)
            {
                GameManager.coinsCollected -= 40;
                GameManager.BoughtSkin4 = true;
                Skin4Text.text = "Equip";
                SoundFXManager.Instance.PlaySoundFXClip(shopBuy, transform, 0.5f, 0f);
                CoinText.text = GameManager.coinsCollected.ToString();
            }
            else
            {
                Debug.Log("You need more money!!!");
                SoundFXManager.Instance.PlaySoundFXClip(shopPoor, transform, 0.5f, 0f);
            }
        }
        else
        {
            PlayerMeshRenderer.material = GreySkin;
            GameManager.currentPlayerSkin = GreySkin;
        }

    }

    public void ChangeSkinToDefault()
    {
        PlayerMeshRenderer.material = DefaultSkin;
        GameManager.currentPlayerSkin = DefaultSkin;
    }
}
