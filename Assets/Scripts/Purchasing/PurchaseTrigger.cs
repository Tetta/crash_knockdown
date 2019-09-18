using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Purchasing;

public class PurchaseTrigger : MonoBehaviour
{
    public static event Action OnPurchaseChapter;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PurchaseManager.OnPurchaseNonConsumable += PurchaseManager_OnPurchaseNonConsumable;
    }

    private void LateUpdate()
    {
        if (PurchaseManager.IsInitialized())
        {
            if (PurchaseManager.CheckBuyState("no_ads"))
                AdsManager.IsBought = true;
            else
                AdsManager.IsBought = false;

            if (!MainMenu.IsBarUnlocked && PurchaseManager.CheckBuyState("unlock_bar"))
            {
                MainMenu.IsBarUnlocked = true;
                if (!PlayerPrefs.HasKey("0011Stars"))
                {
                    PlayerPrefs.SetInt("0011Stars", 0);
                    OnPurchaseChapter();
                }
            }
            if (!MainMenu.IsLaboratoryUnlocked && PurchaseManager.CheckBuyState("unlock_laboratory"))
            {
                MainMenu.IsLaboratoryUnlocked = true;
                if (!PlayerPrefs.HasKey("0021Stars"))
                {
                    PlayerPrefs.SetInt("0021Stars", 0);
                    OnPurchaseChapter();
                }
            }
            if (!MainMenu.IsFactoryUnlocked && PurchaseManager.CheckBuyState("unlock_factory"))
            {
                MainMenu.IsFactoryUnlocked = true;
                if (!PlayerPrefs.HasKey("0031Stars"))
                { 
                    PlayerPrefs.SetInt("0031Stars", 0);
                    OnPurchaseChapter();
                }
            }
        }
    }

    private void PurchaseManager_OnPurchaseNonConsumable(PurchaseEventArgs args)
    {
        switch (args.purchasedProduct.definition.id)
        {
            case "no_ads":
                AdsManager.IsBought = true;
                break;
            case "unlock_bar":
                MainMenu.IsBarUnlocked = true;
                PlayerPrefs.SetInt("0011Stars", 0);
                OnPurchaseChapter();
                break;
            case "unlock_laboratory":
                MainMenu.IsLaboratoryUnlocked = true;
                PlayerPrefs.SetInt("0021Stars", 0);
                OnPurchaseChapter();
                break;
            case "unlock_factory":
                MainMenu.IsFactoryUnlocked = true;
                PlayerPrefs.SetInt("0031Stars", 0);
                OnPurchaseChapter();
                break;
        }
    }
}
