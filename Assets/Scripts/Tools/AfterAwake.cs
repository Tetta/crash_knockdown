using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterAwake : MonoBehaviour {
    [SerializeField] bool NoRenderable = true;
	// Use this for initialization
	void Awake () {
        if (NoRenderable) gameObject.GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
