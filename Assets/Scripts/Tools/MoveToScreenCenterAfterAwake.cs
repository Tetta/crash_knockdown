using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToScreenCenterAfterAwake : MonoBehaviour
{
    RectTransform rect;
    float time;
    // Use this for initialization
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time < 0.05f)
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, new Vector2(0, 0), time);
        else {  
        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, new Vector2(0, Screen.height/2) * GameProcess.ScalerUICoords, time);
    }
           if(time>1)
        {
            Destroy(this);
        }
    }
   
}
