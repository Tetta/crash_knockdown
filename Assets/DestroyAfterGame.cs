using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameProcess.State == GameProcess.ModeWin) Destroy(this.gameObject);
	}
}
