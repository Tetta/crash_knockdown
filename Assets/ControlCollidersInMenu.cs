using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

public class ControlCollidersInMenu : MonoBehaviour {
    float Once;
    bool isOnce;
	// Use this for initialization
	void Awake () {
        isOnce = false;
        Once = 1;
        MainMenu.EventChangeState += OnchangeState;
	}
	void OnchangeState()
    {
       
        if ((GameProcess.State == GameProcess.ModeMainMenu) || (GameProcess.State == GameProcess.ModeMainMenuKitchen) || (GameProcess.State == GameProcess.ModeMainMenuBar) || (GameProcess.State == GameProcess.ModeMainMenuLab) || (GameProcess.State == GameProcess.ModeMainMenuFactory))
        {
            isOnce = true;
            Once = 1;
        }
        else
            gameObject.GetComponent<SphereCollider>().enabled = false;
    }
    // Update is called once per frame
    void Update () {
        if (isOnce)
        {
            Once -= Time.deltaTime;


            if (Once < 0)
            {
                isOnce = false;
                if ((GameProcess.State == GameProcess.ModeMainMenu) || (GameProcess.State == GameProcess.ModeMainMenuKitchen) || (GameProcess.State == GameProcess.ModeMainMenuBar) || (GameProcess.State == GameProcess.ModeMainMenuLab) || (GameProcess.State == GameProcess.ModeMainMenuFactory))
                {
                    gameObject.GetComponent<SphereCollider>().enabled = true;

                }
             
            }
        }
	}
}
