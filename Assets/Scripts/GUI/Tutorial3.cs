using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Tutorial3 : MonoBehaviour
{
    float t;
    bool test;
    Image img1;
    Image img2;
    // Use this for initialization
    void Awake()
    {

        if (SceneManager.GetActiveScene().name != "t3") Destroy(this.gameObject);
        GameProcess.EventShoot += AfterShot;
        GameProcess.EventWin += AfterWin;
        t = 0;
        test = false;

        img1 = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        img2 = img1.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
    }
    void OnDestroy()
    {
        GameProcess.EventShoot -= AfterShot;
        GameProcess.EventWin -= AfterWin;

    }

    // Update is called once per frame
    void Update()
    {
        if (test == true)
        {
            t += Time.deltaTime;

        }
        if ((t > 5) && (GameProcess.State == GameProcess.ModePlay))
        {
            test = false;
            img1.enabled = true;
            img2.enabled = true;
            

        }
    }
    void AfterWin()
    {
        Destroy(gameObject);
    }
    void AfterShot(GameObject go)
    {
        test = true;
        img1.enabled = false;
        img2.enabled = false;
    }
}
