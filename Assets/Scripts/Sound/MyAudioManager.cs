using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAudioManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ContinueNext()
    {

        SoundAndMusik.Instance.GetClickContinue();

    }

    public void Reboot()
    {

        SoundAndMusik.Instance.GetClickReboot();

    }

    public void MainMenu()
    {

        SoundAndMusik.Instance.GetClickMenu();

    }

    public void ClickClick()
    {

        SoundAndMusik.Instance.GetAnyButtonClick();

    }

    public void LoadingScoreBoardWindow()
    {

        SoundAndMusik.Instance.LoadingScoreBoardWindow();

    }

    public void PauseWindow()
    {

        SoundAndMusik.Instance.PauseWindow();

    }

    public void PauseWindowOff()
    {

        SoundAndMusik.Instance.PauseWindowOff();

    }

}
