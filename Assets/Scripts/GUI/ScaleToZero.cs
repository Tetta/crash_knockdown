using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleToZero : MonoBehaviour
{
   private float t = 0;
    void Awake()
    {
        t = 0;
       
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObject.transform.position-Vector3.up, t);
    }
}
