using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RateUsPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject[] lightedStars;
    [SerializeField]
    private Button rateButton;
    [SerializeField]
    private Button reportButton;
    [SerializeField]
    private Text rateText;
    [SerializeField]
    private Text laterText;
    [SerializeField]
    private Text neverText;
    [SerializeField]
    private Text rateWindowText;
    
    public static bool IsRateUsNever
    {
        get { return PlayerPrefs.HasKey("is_rate_us_never"); }
        set
        {
            if (value)
            {
                PlayerPrefs.SetInt("is_rate_us_never", 1);
            }
            else
                PlayerPrefs.DeleteKey("is_rate_us_never");
        }
    }

    private void OnEnable()
    {
        SetStars(0);
    }

    public void SetStars(int stars)
    {
        for (int i = 0; i < lightedStars.Length; i++)
        {
            lightedStars[i].SetActive(i < stars);
        }

        UpdateRateButton(stars);
    }

    private void UpdateRateButton(int stars)
    {
        bool isGoodOpinion = stars > 3 || stars == 0;
        rateButton.gameObject.SetActive(isGoodOpinion);
        reportButton.gameObject.SetActive(!isGoodOpinion);
        rateButton.interactable = stars == 0 ? false : true;
        reportButton.interactable = stars == 0 ? false : true;
    }

    public void OnRateClick()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=" + Application.identifier);
        IsRateUsNever = true;
        gameObject.SetActive(false);
    }

    public void OnReportClick()
    {
        Application.OpenURL("mailto:eschota@gmail.com?subject=Crash%20KnockDown");
        IsRateUsNever = true;
        gameObject.SetActive(false);
    }

    public void OnNeverClick()
    {
        IsRateUsNever = true;
        gameObject.SetActive(false);
    }

    public void OnLaterClick()
    {
        gameObject.SetActive(false);
    }
}
