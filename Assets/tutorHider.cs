using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorHider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameProcess.State != GameProcess.ModePlay) Destroy(gameObject);
	}
}
