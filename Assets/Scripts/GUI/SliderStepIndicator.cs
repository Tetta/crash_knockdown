using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderStepIndicator : MonoBehaviour {
    [Header("Init")]
    [SerializeField]
    private SliderStep[] sliderSteps;
    [SerializeField]
    private CanvasGroup canvasGroup;

    [Header("Settings")]
    [SerializeField]
    private float activeScale;
    [SerializeField]
    private float inactiveScale;

    private bool isShown;

    private void Awake()
    {
        SetStep();
        MainMenu.EventChangeState += SetStep;
    }

    private void OnDestroy()
    {
        MainMenu.EventChangeState -= SetStep;
    }

    public void SetStep()
    {
        if (Array.FindIndex(sliderSteps, x => x.state == GameProcess.State) != -1)
        {
            if (!isShown)
                Show(true);

            foreach (var sliderStep in sliderSteps)
            {
                if (sliderStep.state == GameProcess.State)
                {
                    sliderStep.circle.transform.DOScale(activeScale, .5f).Play();
                }
                else if (sliderStep.state != GameProcess.State && sliderStep.circle.transform.localScale.x != inactiveScale)
                {
                    sliderStep.circle.transform.DOScale(inactiveScale, .5f).Play();
                }
            }
        }
        else if (isShown)
            Show(false);
    }

    private void Show(bool isShow)
    {
        canvasGroup.DOFade(isShow ? 1 : 0, 1).Play();
        isShown = isShow;
    }

    [Serializable]
	private struct SliderStep
    {
        public RectTransform circle;
        public int state;
    }
}
