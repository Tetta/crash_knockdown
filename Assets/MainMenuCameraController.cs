using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCameraController : MonoBehaviour {
  public  static bool _change;
  public static  float t;
  public static  int Current;
    public static bool isFast;
    public float moveKoef = 1f;
    // Use this for initialization
    void Awake () {
        t = 0;
        Current = 0;
        MainMenu.EventChangeCamera += ChangeCameraPos;
        MainMenu.EventFastChangeCamera += ChangeCameraPosFast;
    }

    private void OnDestroy()
    {
        MainMenu.EventChangeCamera -= ChangeCameraPos;
        MainMenu.EventFastChangeCamera -= ChangeCameraPosFast;

    }

    public static void ChangeCameraPosFast()
    {
        isFast = true;
        ChangeCameraPos();
    }

    public static void ChangeCameraPos()
    {
        if (SceneManager.GetActiveScene().name == "mainmenu")
        {
            t = 0;
            _change = true;
            if (GameProcess.State == GameProcess.ModeMainMenu) Current = 0;
            if (GameProcess.State == GameProcess.ModeMainMenuKitchen || GameProcess.State == GameProcess.ModeSelectLevelKitchen || GameProcess.State == GameProcess.ModeDevInfo) Current = 1;
            if (GameProcess.State == GameProcess.ModeMainMenuBar || GameProcess.State == GameProcess.ModeSelectLevelBar) Current = 2;
            if (GameProcess.State == GameProcess.ModeMainMenuLab || GameProcess.State == GameProcess.ModeSelectLevelLab) Current = 3;
            if (GameProcess.State == GameProcess.ModeMainMenuFactory || GameProcess.State == GameProcess.ModeSelectLevelFactory) Current = 4;
            if (GameProcess.State == GameProcess.ModeMainMenuDonates || GameProcess.State == GameProcess.ModeSelectDonatesMenu) Current = 5;
        }
    }
	// Update is called once per frame
	void Update ()
    {
        if (_change)
        {
            if (GameProcess.State == GameProcess.ModeDevInfo && moveKoef == 1f)
            {
                return;
            }

            t += isFast ? 1 : Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, MainMenu.CameraPositions[Current], t * moveKoef);
            if (t > 1)
            {
                _change = false;
                isFast = false;
            }
        }
	}
}
