using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomForceOnAwake : MonoBehaviour {
    float t,rand;
    Rigidbody rb;
	// Use this for initialization
	void Awake () {
        rand = Random.Range(0, 1);
        rb=GetComponent<Rigidbody>();
        if (rb == null) Destroy(this);
        if (rb != null) rb.isKinematic = false;
    }
	
	// Update is called once per frame
	void Update () {
        rand -= Time.deltaTime;
        if (rand < 0)
        {
            rb.AddExplosionForce(1000,gameObject.transform.position,0.1f);
            Destroy(this);
        }
	}
}
