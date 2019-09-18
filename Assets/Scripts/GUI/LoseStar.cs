using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseStar : MonoBehaviour {
    [SerializeField] GameObject[] Starss;
   public static GameObject[] Starzz;

	void Awake () {
        Starzz = Starss;
        GameGUI.EventOnceASec += CheckStars;
    
	}
    private void OnDestroy()
    {
    

        GameGUI.EventOnceASec -= CheckStars;

    }
   public static void SetStars()
    { int k = 0;



        if (GameProcess.Stars[2] < GameProcess.MyTimer) k = 0;
        else
            if (GameProcess.Stars[1] < GameProcess.MyTimer) k = 1;
        else
            if (GameProcess.Stars[0] < GameProcess.MyTimer) k = 2;
        else k = 3;
        float RecordTime = GameProcess.MyTimer;
        //--> Check Record Time and Update it if it's New Record Time
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name))
        {
            RecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name);
            if (RecordTime > GameProcess.MyTimer)
            {
                RecordTime = GameProcess.MyTimer;
                Debug.Log("New Record Achieved!: " + RecordTime);
            }
        }
        //<--
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name, RecordTime);
        if (k == 3)
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Stars", 3);
        else
            if (k == 2)
        {
            if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "Stars") <= 2)
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Stars", 2);
        }
        else
           if (k == 1)
        {
            if (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "Stars") <= 1)
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Stars", 1);
          
        }

        if (k == 0)
        {
            if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "Stars"))
            {
            }
            else
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "Stars", 0);
           
        }
        Debug.Log("Set Stars for this Level: " + PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "Stars"));
    }
    void CheckStars()
    {

        int    timer = Mathf.RoundToInt(GameProcess.MyTimer);
        int temp = Mathf.RoundToInt(GameProcess.Stars[0] - timer);
        int temp2 = Mathf.RoundToInt(GameProcess.Stars[1] - timer);
        int temp3 = Mathf.RoundToInt(GameProcess.Stars[2] - timer);
        //int temp4 = Mathf.RoundToInt(Game.Stars[3] - timer);           
        //int temp5 = Mathf.RoundToInt(Game.Stars[4] - timer);

        if (temp == 0)
        {

            if (Starzz[0] != null)
            {

                Starzz[0].GetComponent<Animator>().enabled = true;
                if (GameProcess.State != GameProcess.ModeWin) SoundAndMusik.Instance.GetStarLose(0);

            }

        }

        if (temp2 == 0)
        {

            if (Starzz[1] != null)
            {

                Starzz[1].GetComponent<Animator>().enabled = true;
                if (GameProcess.State != GameProcess.ModeWin) SoundAndMusik.Instance.GetStarLose(1);

            }

        }

        if (temp3 == 0)
        {

            if (Starzz[2] != null)
            {

                Starzz[2].GetComponent<Animator>().enabled = true;
                if (GameProcess.State != GameProcess.ModeWin) SoundAndMusik.Instance.GetStarLose(2);

            }

        }

                 
        /*
        if (temp4 == 0)
        if (Starzz[3] != null) Starzz[3].GetComponent<Animator>().enabled = true;
                                
        if (temp5 == 0)  
        if (Starzz[4] != null) Starzz[4].GetComponent<Animator>().enabled = true;            
         */

 
    }
	// Update is called once per frame
	void Update () {
      
    }
}
