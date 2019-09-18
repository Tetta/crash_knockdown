using GameAnalyticsSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour {
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Text progressText;

	void Start () {
        StartCoroutine(Load());
        GameAnalytics.Initialize();
    }

    private IEnumerator Load()
    {
        yield return new WaitForEndOfFrame();
        AsyncOperation AO = SceneManager.LoadSceneAsync(!simpleFPS.IsDontNeedCalibration ? "calibration_scene" : "mainmenu");
        AO.allowSceneActivation = false;
        while (!AO.isDone)
        {
            slider.value = AO.progress * 1.11f;
            progressText.text = (int)(AO.progress * 111.11f) + "%";

            if (AO.progress >= 0.89f)
                AO.allowSceneActivation = true;

            yield return null;
        }
    }
}
