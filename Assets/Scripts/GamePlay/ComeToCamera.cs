using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeToCamera : MonoBehaviour {

    float T;
    
	// Use this for initialization
	void Awake () {
        T = 0;
    }
	
	// Update is called once per frame
	void Update () {
        T += Time.deltaTime*1;
        if (T > 0.85f)
        {
            Destroy(this);
        }else 
       gameObject.transform.position = Vector3.Lerp(  gameObject.transform.position, Camera.main.transform.position+Vector3.up*-0.2f, T);
	}
}
