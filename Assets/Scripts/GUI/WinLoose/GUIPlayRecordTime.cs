using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIPlayRecordTime : MonoBehaviour {

    private Text myTime;

    private int myMin;
    private int mySec;

    private float tempSec;

    private float recTime;


    
    void Awake()
    {

        Initialized();
        
    }

    void OnEnable()
    {

        Initialized();

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        
    }

    private void Initialized()
    {

        myTime = gameObject.GetComponent<Text>();

        myMin = 0;
        mySec = 0;

        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "RecordTime"))
        {

            recTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "RecordTime");

            myMin = Mathf.RoundToInt(recTime) / 60;
            tempSec = recTime - (myMin * 60);

            mySec = Mathf.RoundToInt(tempSec);

            if (myTime != null)
            {

                if (mySec < 10) myTime.text = myMin + " : 0" + mySec;
                else myTime.text = myMin + " : " + mySec;

            }

        }
        else
        {

            myTime.text = "- : -";

        }

    }

}
