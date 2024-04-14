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

    [Header("Buy Checks")]
    public bool BoughtSkin1;
    public bool BoughtSkin2;
    public bool BoughtSkin3;
    public bool BoughtSkin4;

    [Header("SkinBuyButtons")]
    public TextMeshProUGUI Skin1Text;
    public TextMeshProUGUI Skin2Text;
    public TextMeshProUGUI Skin3Text;
    public TextMeshProUGUI Skin4Text;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();    
        PlayerMeshRenderer = GameObject.FindWithTag("CharacterBody").GetComponent<SkinnedMeshRenderer>();
    }

    public void ExitShop()
    {
        SkinShopScreen.SetActive(false);
        IDogDialogue.SetActive(true);
    }


    public void BuySkin1()
    {
        if (BoughtSkin1 == false)
        {
            //If the player has the minimum amount of coins tyhen it subtract them
            if (GameManager.coinsCollected >= 10)
            {
                GameManager.coinsCollected -= 10;
                BoughtSkin1 = true;
                Skin1Text.text = "Change to Skin 1";
            }
            else
            {
                Debug.Log("You need more money!!!");
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
        if (BoughtSkin2 == false)
        {
            if (GameManager.coinsCollected >= 20)
            {
                GameManager.coinsCollected -= 20;
                BoughtSkin2 = true;
                Skin2Text.text = "Change to Skin 2";
            }
            else
            {
                Debug.Log("You need more money!!!");
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
        if (BoughtSkin3 == false)
        {
            if (GameManager.coinsCollected >= 30)
            {
                GameManager.coinsCollected -= 30;
                BoughtSkin3 = true;
                Skin3Text.text = "Change to Skin 3";
            }
            else
            {
                Debug.Log("You need more money!!!");
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
        if (BoughtSkin4 == false)
        {
            if (GameManager.coinsCollected >= 40)
            {
                GameManager.coinsCollected -= 40;
                BoughtSkin4 = true;
                Skin4Text.text = "Change to Skin 4";
            }
            else
            {
                Debug.Log("You need more money!!!");
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
