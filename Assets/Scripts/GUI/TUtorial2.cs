using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TUtorial2 : MonoBehaviour
{
    float t;
    bool test;
    Image img1,img2;
    // Use this for initialization
    void Awake()
    {

        if (SceneManager.GetActiveScene().name != "t2") Destroy(this.gameObject);
        GameProcess.EventTake += AfterShot;
        t = 0;
        test = false;

        img1 = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        img2 = gameObject.transform.GetChild(1).gameObject.GetComponent<Image>();
    }
    void OnDestroy()
    {
        GameProcess.EventTake -= AfterShot;

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void AfterShot( )
    {
       
        img1.enabled = false;
        img2.enabled = false;
    }
}
