using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtOnStart : MonoBehaviour {

    [SerializeField]
    private Transform target;

	void Start () {
        transform.LookAt(target);
	}
}
