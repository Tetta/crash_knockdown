using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class DonatePanel : MonoBehaviour {
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Text donateText;

    private int DonateSum
    {
        get { return PlayerPrefs.GetInt("donate_sum", Random.Range(0, 700)); }
        set
        {
            PlayerPrefs.SetInt("donate_sum", value);
            UpdateDonateLabels();
        }
    }

    [ContextMenu("ResetSum")]
    private void ResetSum()
    {
        PlayerPrefs.DeleteKey("donate_sum");
    }

    private void UpdateDonateLabels()
    {
        slider.value = DonateSum;
        donateText.text = DonateSum + "$";
    }

    private void Awake()
    {
        MainMenu.EventChangeState += OnChangeState;

        DonateSum = DonateSum;
    }

    void Start()
    {
        PurchaseManager.OnPurchaseConsumable += PurchaseManager_OnPurchaseConsumable;
    }

    private void PurchaseManager_OnPurchaseConsumable(PurchaseEventArgs args)
    {
        switch (args.purchasedProduct.definition.id)
        {
            case "donate_1":
                DonateSum += 1;
                break;
            case "donate_5":
                DonateSum += 5;
                break;
            case "donate_10":
                DonateSum += 10;
                break;
        }
    }

    private void OnDestroy()
    {
        MainMenu.EventChangeState -= OnChangeState;
    }

    private void OnChangeState()
    {
        if (GameProcess.State == GameProcess.ModeSelectDonatesMenu)
            Show(true, 1);
        else if (GameProcess.LastState == GameProcess.ModeSelectDonatesMenu)
            Show(false);
    }

    public void OnBackButtonClick()
    {
        Show(false);
        GameProcess.LastState = GameProcess.State;
        GameProcess.State = GameProcess.ModeMainMenuDonates;
    }

    private IEnumerator OnlineUsersDonate()
    {
        yield return new WaitForSeconds(1.1f);
        float chance = .7f;
        while (canvasGroup.alpha > 0)
        {
            yield return new WaitForSeconds(1f);
            chance -= chance > .1f ? .05f : 0;
            DonateSum += Random.value < chance ? Random.Range(1, 10) : 0;
        }
    }

    private void Show(bool isShow, float delay = 0)
    {
        if (!isShow)
            canvasGroup.blocksRaycasts = false;
        else
            StartCoroutine(OnlineUsersDonate());

        canvasGroup.DOFade(isShow ? 1 : 0, 1).OnComplete(() => canvasGroup.blocksRaycasts = isShow).SetDelay(delay).Play();
    }
}
