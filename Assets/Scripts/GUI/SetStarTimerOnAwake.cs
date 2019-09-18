using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetStarTimerOnAwake : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        if(GameProcess.State==GameProcess.ModePlay)
        GetComponent<Text>().text = GameProcess.Stars[0].ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
