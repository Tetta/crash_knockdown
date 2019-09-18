using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonClick : MonoBehaviour {
    public static event Action ChangeQualityEvent;

    void Awake () {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { TaskOnClick(); });
    }

    void TaskOnClick()
    {
      
        if (gameObject.name == "+Quality")
        {
            simpleFPS.FPSCalibrationQuality++;
            simpleFPS.MyQuality = simpleFPS.FPSCalibrationQuality;
            simpleFPS.ChangeMyQuality(simpleFPS.MyQuality);
            simpleFPS.IsDontNeedCalibration = true;
        }
        if (gameObject.name == "-Quality")
        {
            simpleFPS.FPSCalibrationQuality--;
            simpleFPS.MyQuality = simpleFPS.FPSCalibrationQuality;
            simpleFPS.ChangeMyQuality(simpleFPS.MyQuality);
            simpleFPS.IsDontNeedCalibration = true;
        }
        if (gameObject.name == "+Resolution") simpleFPS.ChangeResolution(1);
        if (gameObject.name == "-Resolution") simpleFPS.ChangeResolution(-1);
        if (gameObject.name == "-AA") simpleFPS.ChangeAA(1);
        if (gameObject.name == "+AA") simpleFPS.ChangeAA(-1);
        if (gameObject.name == "AO+") simpleFPS.AOprocess(true);
        if (gameObject.name == "AO-") simpleFPS.AOprocess(false);
        if (gameObject.name == "CC+") simpleFPS.CCprocess(true);
        if (gameObject.name == "CC-") simpleFPS.CCprocess(false);
        if (gameObject.name == "B-") simpleFPS.BLoomProcess(false);
        if (gameObject.name == "B+") simpleFPS.BLoomProcess(true);
        if (gameObject.name == "V+") simpleFPS.VignetteProcess(true);
        if (gameObject.name == "V-") simpleFPS.VignetteProcess(false);
        
        
        ChangeQualityEvent();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
