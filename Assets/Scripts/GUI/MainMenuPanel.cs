using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenuPanel : MonoBehaviour {
    [SerializeField]
    private RectTransform rectTransform;

    public void Show(bool isShow, float delay = 0)
    {
        rectTransform.DOAnchorPosX(isShow ? 0 : -rectTransform.rect.width, .25f).SetDelay(delay).SetEase(Ease.InOutQuart).Play();
    }

    public void StartTutorial()
    {
        GameProcess.FadeLoadLevel("t1");
        ChangeGameStateOnClick.IsTutorialOpened = true;
    }
}
