using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoresCounter : MonoBehaviour {
   public static int Scores;
   public static int RecordScores;

	// Use this for initialization
	void Awake () {
        Scores = 0;
        GameProcess.EvectCrack += OnCrack ;
        GameProcess.EventWin += OnWin;
        GameProcess.EventCatch += OnCatchInSlowMo;

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "Scores"))
            RecordScores = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "Scores");
        else RecordScores = 0;


    }
    private void OnDestroy()
    {
          GameProcess.EvectCrack -= OnCrack ;
        GameProcess.EventWin -= OnWin;

    }
    void OnCrack(Vector2 _v,int _scores)
    {
        Scores += _scores;
    }

    void OnWin()
    {
        Scores += GameProcess.GrabbedBullets.Count * 50;
   

        if(Scores>RecordScores)
        {
            Debug.Log("New Record Scores!");
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Scores", Scores);
        }
        gameObject.AddComponent<BulletsToScores>();
    }

    void OnCatchInSlowMo(Vector2 v, float f)
    {
        if(TimeManager.SlowMo)
        Scores +=  50;

    }
    // Update is called once per frame
    void Update () {
		
	}
}
