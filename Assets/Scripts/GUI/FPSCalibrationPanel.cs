using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FPSCalibrationPanel : MonoBehaviour {
    [SerializeField]
    private RectTransform panel;
    [SerializeField]
    private Text progressText;

    [SerializeField]
    private float progressTextPeriod;
    [SerializeField]
    private int maxDotsNumber;

    private Coroutine animatedDrawProgressCoroutine;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        StartCoroutine(AnimatedDrawProgress());
    }

    private IEnumerator AnimatedDrawProgress()
    {
        int currentDotsNumber = 0;
        while(true)
        {
            yield return new WaitForSeconds(progressTextPeriod);

            currentDotsNumber = ++currentDotsNumber % (maxDotsNumber + 1);
            progressText.text = "";
            for (int i = 0; i < currentDotsNumber; i++)
            {
                progressText.text += " . ";
            }
        }
    }

    private IEnumerator Hiding()
    {
        AsyncOperation AO = SceneManager.LoadSceneAsync("mainmenu");
        AO.allowSceneActivation = false;
        while (AO.progress < 0.9f)
        {
            yield return null;
        }

        AO.allowSceneActivation = true;

        panel.DOMoveY(- panel.rect.height / 2, 2f).SetEase(Ease.InSine).OnComplete(() => 
        {
            FindObjectOfType<MenuCamera>().ZoomOut();
            Destroy(gameObject);
        }).Play();
    }

    [ContextMenu("Hide")]
    public void Hide()
    {
        StartCoroutine(Hiding());
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}