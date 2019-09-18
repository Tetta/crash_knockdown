using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemOnAwake : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        int k = Random.Range(0, transform.childCount);
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);
            transform.GetChild(k).gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
