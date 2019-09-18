using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OncollisionAttachToObject : MonoBehaviour {
    [SerializeField] GameObject AttachableObject;
   
	// Use this for initialization
	void Awake () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
		if (collision.transform.tag == "Conveer")
		{
			transform.parent = AttachableObject.transform;
			Destroy(this);
		}
    }
    // Update is called once per frame
    void Update () {
		
	}
}
