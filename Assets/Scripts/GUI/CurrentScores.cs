using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentScores : MonoBehaviour {

    Text t;
	void Awake () {
        t = GetComponent<Text>();
        GameProcess.EventWin += ChangeTextScores;
	}
    private void OnDestroy()
    {
        GameProcess.EventWin -= ChangeTextScores;

    }
    // Update is called once per frame
    
    void ChangeTextScores()
    {
        t.text = ScoresCounter.Scores.ToString();
        gameObject.AddComponent<GrowNumbersScores>(); 
    }
}
