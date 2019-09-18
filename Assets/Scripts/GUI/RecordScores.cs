using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RecordScores : MonoBehaviour {
    Text t;
	// Use this for initialization
	void Awake () {
        t = GetComponent<Text>();
        GameProcess.EventWin += GetRecordScores;
	}
    private void OnDestroy()
    {
        GameProcess.EventWin -= GetRecordScores;

    }
    void GetRecordScores()
    {
        t.text = ScoresCounter.RecordScores.ToString();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
