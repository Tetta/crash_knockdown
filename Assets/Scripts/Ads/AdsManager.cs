using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour {
    
    private static bool isEnabled = false;
    [SerializeField]
    private string gameId;

    private const string NO_ADS_BOUGHT_KEY = "no_ads_bought";

    public static bool IsBought
    {
        get { return PlayerPrefs.HasKey(NO_ADS_BOUGHT_KEY); }
        set
        {
            if (value)
                PlayerPrefs.SetInt(NO_ADS_BOUGHT_KEY, 1);
            else
                PlayerPrefs.DeleteKey(NO_ADS_BOUGHT_KEY);
        }
    }

	void Awake () {
		if (Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, false);
        }
        else
        {
            Debug.Log("Platform is not supported for UnityAds");
        }
    }

    public static void ShowRewarded()
    {
        if (!IsBought && isEnabled)
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show("rewardedVideo");
            }
        }
    }

    public static void Show()
    {
        if (!IsBought && isEnabled)
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show();
            }
        }
    }
}
