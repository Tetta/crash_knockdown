using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestLevelNameOnWin : MonoBehaviour {

    Text T;
        void Awake ()
    {
        T = GetComponent<Text>();
        GameProcess.EventFadeIn += OnFadeIn;
	}
    private void OnDestroy()
    {
        GameProcess.EventFadeIn -= OnFadeIn;

    }
    void OnFadeIn()
    {
        if (GameProcess.FadeLevelName.Length == 4)
            T.text = GameProcess.FadeLevelName.Substring(2, 2);
        else {
            T.text = GameProcess.FadeLevelName.Substring(1, 1);
            }
        if (GameProcess.FadeLevelName.Length > 4)
        {
            T.text = "MENU";
        }

    }
	// Update is called once per frame
	void Update () {
		
	}
}
