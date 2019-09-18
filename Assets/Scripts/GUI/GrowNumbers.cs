using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowNumbers : MonoBehaviour {

    Text t;
    float time;
    int target;

    private int myMin;
    private int mySec;
    private float tempSec;

    // Use this for initialization
    void Awake () {
        time = -1;
        t = GetComponent<Text>();
       int.TryParse(t.text,out target);
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        //t.text =Mathf.RoundToInt( Mathf.Lerp(0, target, time)).ToString();

        myMin = Mathf.RoundToInt(GameProcess.MyTimer) / 60;
        tempSec = GameProcess.MyTimer - (myMin * 60);
        mySec = Mathf.RoundToInt(tempSec);
        if (t != null)
        {

            if (mySec < 10) t.text = myMin + " : 0" + mySec;
            else t.text = myMin + " : " + mySec;

        }

    }
}
