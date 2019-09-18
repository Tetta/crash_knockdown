using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Topping : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        GameProcess.EventWin += OnChange;
        OnChange();
    }
    private void OnDestroy()
    {
        GameProcess.EventWin -= OnChange;

    }
    private void OnChange()
    {
        string str = gameObject.transform.parent.gameObject.name;
        str = str.Substring(str.Length - 4, 4);
         if (PlayerPrefs.HasKey(str + "Stars"))
        {
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
