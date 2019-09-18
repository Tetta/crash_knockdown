using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepOnStart : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        GetComponent<Rigidbody>().Sleep();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
