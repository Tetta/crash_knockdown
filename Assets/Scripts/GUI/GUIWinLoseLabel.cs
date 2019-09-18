using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GUIWinLoseLabel : MonoBehaviour {

    private bool isGameOver;

    // Use this for initialization
    void Awake()
    {

        GameProcess.EventWin += SetWinLoseLabel;

        isGameOver = false;

    }

    private void OnDestroy()
    {

        GameProcess.EventWin -= SetWinLoseLabel;


    }

    void SetWinLoseLabel()
    {

        if (!isGameOver && GameProcess.State == GameProcess.ModeWin)
        {

            if (GameProcess.Stars[2] < GameProcess.MyTimer)
            {

                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.SetActive(false);
                SoundAndMusik.Instance.GetLooser();
                isGameOver = true;

            }
            else
            {

                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);                
                SoundAndMusik.Instance.GetWinner();
                isGameOver = true;

            }

        }

    }
	
    
    // Update is called once per frame
	void Update ()
    {

        SetWinLoseLabel();

    }
}
