using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TUtorial1 : MonoBehaviour {
    float t;
    bool test;
    Image img;
	// Use this for initialization
	void Awake () {

        if (SceneManager.GetActiveScene().name != "t1") Destroy(this.gameObject);
        GameProcess.EventShoot += AfterShot;
        GameProcess.EventWin += AfterWin;
        t = 0;
        test = false;

        img = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
    }
    void OnDestroy()
    {
        GameProcess.EventShoot -= AfterShot;
        GameProcess.EventWin -= AfterWin;
    }
 
    // Update is called once per frame
    void Update () {
		 if (test == true)
        {
            t += Time.deltaTime;

        }
         if ((t > 5)&&(GameProcess.State==GameProcess.ModePlay))
        {
            test = false;
            img.enabled = true;
        }
	}

    void AfterShot(GameObject go)
    {
        test = true;
        img  .enabled = false;
    }
    void AfterWin()
    {
        Destroy(gameObject);
    }
}
