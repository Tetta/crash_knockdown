using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomKiller : MonoBehaviour
{
    private void Awake()
    {
        gameObject.transform.parent = null;
    }
    private void OnTriggerEnter(Collider other)
    {

        //   other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        other.gameObject.GetComponent<Rigidbody>().isKinematic=true;
        other.gameObject.transform.position = Vector3.forward * -10;
        Debug.Log("Destroyed Object Falled Down : " + other.gameObject.name);
    }
}
