using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour {
    [SerializeField]
    private Camera camera;

    private bool isMoving;

    private bool isZoomed = true;

    private void Awake()
    {
        MainMenu.EventChangeState += ChangeState;
    }

    private void OnDestroy()
    {
        MainMenu.EventChangeState -= ChangeState;
    }

    private void Start()
    {
        if (Application.isEditor && GameProcess.lastStateToMenu == 0)
        {
            FPSCalibrationPanel fcp = FindObjectOfType<FPSCalibrationPanel>();
            if (!fcp)
                ZoomOut();
        }
    }

    private bool IsNeedToZoom
    {
        get
        {
            return GameProcess.State == GameProcess.ModeSelectLevelKitchen || GameProcess.State == GameProcess.ModeSelectLevelBar || GameProcess.State == GameProcess.ModeSelectLevelFactory || 
                GameProcess.State == GameProcess.ModeSelectLevelLab || GameProcess.State == GameProcess.ModeDevInfo || GameProcess.State == GameProcess.ModeSelectDonatesMenu;
        }
    }

    public void ChangeState()
    {
        if (!isZoomed && IsNeedToZoom)
        {
            ZoomIn();
        }
        else if (isZoomed && !IsNeedToZoom)
        {
            ZoomOut();
        }
    }

    public void ZoomOut()
    {
        isZoomed = false;
        DOTween.To(() => camera.fieldOfView, x => camera.fieldOfView = x, 79.8f, .4f).SetEase(Ease.InOutCubic).Play();
    }

    public void ZoomIn()
    {
        isZoomed = true;
        DOTween.To(() => camera.fieldOfView, x => camera.fieldOfView = x, 1f, .4f).SetEase(Ease.InOutCubic).Play();
    }
}
