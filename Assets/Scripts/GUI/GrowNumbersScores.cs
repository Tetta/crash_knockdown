using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowNumbersScores : MonoBehaviour
{

    Text t;
    float time;
    int target;

    private int myMin;
    private int mySec;
    private float tempSec;

    // Use this for initialization
    void Awake()
    {
        time = -1;
        t = GetComponent<Text>();
        int.TryParse(t.text, out target);
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 1) { 
        time += Time.deltaTime;
            t.text = Mathf.RoundToInt(Mathf.Lerp(0, target, time)).ToString();
        }
        
    }
}
