using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class FadeLoad : MonoBehaviour {
    float t;
    Image Img;
    Text T1, T2;
    bool _fade;
    // Use this for initialization
    void Awake () {
        t = 0;
        _fade = false;
        Img = gameObject.GetComponent<Image>();
        Color c = Color.white;
        c.a = 0;
        Img.color = c;
        T1 = transform.GetChild(0). GetComponent<Text>();
        T2 = transform.GetChild(0).transform.GetChild(0). GetComponent<Text>();
        GameProcess.EventFadeIn += FadeIn;
        Img.enabled = false;
    }
    private void OnDestroy()
    {
        GameProcess.EventFadeIn -= FadeIn;
    }
    void FadeIn()
    {
        
        Img.enabled = true;
        _fade = true;
        transform.GetChild(0).transform.GetChild(0).gameObject.AddComponent<TestLevelNameOnWin>();

    }

    // Update is called once per frame
    void Update () {
        if (_fade == true)
        {
            t += Time.deltaTime;
            Color clr = Color.black;
            Color clr2 = Color.white;

            clr.a = Mathf.Lerp(0, 1, t * 3.33f);
            clr2.a = Mathf.Lerp(0, 1, t * 3.33f);
            Img.color = clr2;
            T1.color = clr2;
            T2.color = clr2;

            if (t > 0.3f)
            {
                if (simpleFPS.AverageFPS <35)
                {
                    simpleFPS.FPSCalibrationQuality--;
                    simpleFPS.MyQuality--;
                }
                SceneManager.LoadScene(GameProcess.FadeLevelName);
            }
        }
	}
}
