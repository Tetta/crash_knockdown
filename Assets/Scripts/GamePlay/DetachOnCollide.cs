using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachOnCollide : MonoBehaviour {
    Rigidbody rb;
    bool _destroy;
    [SerializeField] float Speed = 1;
	// Use this for initialization
	void Awake() {
        _destroy = false;
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(_destroy==false)
        if (rb.velocity.sqrMagnitude > Speed)
        {
                _destroy = true;
            Destroy(this.gameObject, 2);
        }
	}
}
