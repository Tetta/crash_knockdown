using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorial7 : MonoBehaviour {
    bool show;
	// Use this for initialization
	void Awake () {
        if (SceneManager.GetActiveScene().name != "0005") Destroy(this.gameObject);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        if (GameProcess.MyTimer > 5) gameObject.transform.GetChild(0).gameObject.SetActive(true);
        if (GameProcess.CurrentCamera == 1) Destroy(this.gameObject);
    }
}
