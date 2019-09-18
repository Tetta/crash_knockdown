using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QualityTextInMenu : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        ButtonClick.ChangeQualityEvent += OnChange;
        OnChange();
    }
    void OnDestroy()
    {
        ButtonClick.ChangeQualityEvent -= OnChange;
    }
    private void OnChange()
    {
        gameObject.GetComponent<Text>().text = simpleFPS.MyQuality.ToString() + "0%";
    }
   
}
