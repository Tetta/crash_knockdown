using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUILevelNaming : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        int z = 0;
        int.TryParse(SceneManager.GetActiveScene().name,out z);
        GetComponent<Text>().text = z.ToString();
        /*
        if (z < 10) GetComponent<Text>().text = "1-" + z.ToString();
        else
        if (z < 20) GetComponent<Text>().text = "2-" + z.ToString();
        else
        if (z < 30) GetComponent<Text>().text = "3-" + z.ToString();
        else
        if (z < 40) GetComponent<Text>().text = "4-" + z.ToString();
        else
        if (z < 50) GetComponent<Text>().text = "5-" + z.ToString();
        else
        if (z < 60) GetComponent<Text>().text = "6-" + z.ToString();
        else
        if (z < 70) GetComponent<Text>().text = "7-" + z.ToString();
        else
        if (z < 80) GetComponent<Text>().text = "8-" + z.ToString();
        else
        if (z < 90) GetComponent<Text>().text = "9-" + z.ToString();

        */
    }
    
}