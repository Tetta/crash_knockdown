using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffKinematicCheldrens : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider!");
        if (other.tag == "Bullet")
            transform.GetChild(0).gameObject.GetComponent<Rigidbody>().isKinematic = false;

    }
    // Update is called once per frame
    void Update () {
		
	}
}
