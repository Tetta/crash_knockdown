using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    [SerializeField] public static int[] UnlockCosts;

    [SerializeField] float CrackAnimationDelay;
    [SerializeField] MenuCamera zoomMenuCamera;
    [SerializeField] GameObject logoCamera;
    [SerializeField] GameObject[] CameraPos;
    public static Vector3[] CameraPositions;
    [SerializeField] float CamSpeed = 1;
    [SerializeField] GameObject ButtonToKitchen;
    [SerializeField] GameObject ButtonKitchen;
    [SerializeField] GameObject ButtonBar;
    [SerializeField] GameObject ButtonLab;
    [SerializeField] GameObject ButtonFactory;
    [SerializeField] GameObject ButtonUnlockBar;
    [SerializeField] GameObject ButtonUnlockLab;
    [SerializeField] GameObject ButtonUnlockFactory;
    [SerializeField] GameObject ButtonDonate;
    [SerializeField] GameObject rateUsCanvas;
    public static GameObject RateUsCanvas;
    [SerializeField] CanvasGroup devInfoText;
    [SerializeField] MainMenuPanel mainMenuPanel;

    [SerializeField] ChapterVisuals[] chaptersVisuals;

    bool delay;
    public static event Action EventChangeCamera;
    public static event Action EventChangeState;
    public static event Action EventFastChangeCamera;
    public static event Action EventOpenTheme;

    [SerializeField]
    private float maxTouchTime;
    [SerializeField]
    private float minTouchSlideMagnitude;
    private Vector3 startTouchPosition;
    private float startTouchTime;
    [SerializeField]
    private SliderStepIndicator sliderStepIndicator;

    [SerializeField]
    private ParticleSystem unlockEffect;

    private static PurchaseManager _purchaseManager;

    public static PurchaseManager purchaseManager
    {
        get
        {
            if (_purchaseManager == null)
            {
                _purchaseManager = FindObjectOfType<PurchaseManager>();
            }
            return _purchaseManager;
        }
    }

    public static bool IsBarUnlocked
    {
        get { return PlayerPrefs.HasKey("bar_unlocked"); }
        set
        {
            if (value)
            {
                PlayerPrefs.SetInt("bar_unlocked", 1);
                EventOpenTheme();
            }
            else
                PlayerPrefs.DeleteKey("bar_unlocked");
        }
    }
    public static bool IsLaboratoryUnlocked
    {
        get { return PlayerPrefs.HasKey("laboratory_unlocked"); }
        set
        {
            if (value) { 
                PlayerPrefs.SetInt("laboratory_unlocked", 1);
                EventOpenTheme();
            }
            else
                PlayerPrefs.DeleteKey("laboratory_unlocked");
        }
    }
    public static bool IsFactoryUnlocked
    {
        get { return PlayerPrefs.HasKey("factory_unlocked"); }
        set
        {
            if (value)
            {
                PlayerPrefs.SetInt("factory_unlocked", 1);
                EventOpenTheme();
            }
            else
                PlayerPrefs.DeleteKey("factory_unlocked");
        }
    }

    void Awake ()
    {
        DifficultyPanel.OnChangeDifficulty += UpdateNeededStars;

        UpdateNeededStars();
        if (!PlayerPrefs.HasKey("0001Stars")) PlayerPrefs.SetInt("0001Stars", 0);
        CameraPositions = new Vector3[CameraPos.Length];
        for (int i = 0; i < CameraPos.Length; i++) if (CameraPos[i] != null)
            {
                CameraPositions[i] = CameraPos[i].transform.position;
                if (i != 0) CameraPos[i].GetComponent<Camera>().enabled = false; ;
            }
        GameProcess.State = GameProcess.ModeMainMenu;
        
        delay = false;
        Camera.main.gameObject.AddComponent<MainMenuCameraController>();
        MainMenuCameraController logoMenuCameraController = logoCamera.AddComponent<MainMenuCameraController>();
        logoMenuCameraController.moveKoef = 0.5f;
        DontDestroyOnLoad(rateUsCanvas);
        RateUsCanvas = rateUsCanvas;
        CheckLockersOnAwake();

        EventChangeState += ShowChapterVisualsOnChangeState;
        EventOpenTheme += UpdateUnlockButtons;
        UnlockLevels.OnUnlockLevel += UpdateUnlockButtons;
        DifficultyPanel.OnRemoveAllLevelData += UpdateUnlockButtons;
    }

    private void UpdateNeededStars()
    {
        UnlockCosts = new int[3];
        switch (GameProcess.Difficulty)
        {
            case 0:
                UnlockCosts[0] = 15;
                UnlockCosts[1] = 30;
                UnlockCosts[2] = 40;
                break;
            case 1:
                UnlockCosts[0] = 20;
                UnlockCosts[1] = 40;
                UnlockCosts[2] = 60;
                break;
            default:
                UnlockCosts[0] = 26;
                UnlockCosts[1] = 52;
                UnlockCosts[2] = 76;
                break;
        }
    }

    private void OnDestroy()
    {
        EventChangeState -= ShowChapterVisualsOnChangeState;
        EventOpenTheme -= UpdateUnlockButtons;
        UnlockLevels.OnUnlockLevel -= UpdateUnlockButtons;
        DifficultyPanel.OnRemoveAllLevelData -= UpdateUnlockButtons;
    }

    private void Start()
    {
        if (GameProcess.lastStateToMenu != 0)
        {
            GameProcess.State = GameProcess.lastStateToMenu;
            HideFastChapterVisuals(GameProcess.State);
            EventChangeState();
            EventFastChangeCamera();
        }
        else
        {
            mainMenuPanel.Show(true, .5f);
        }

        UpdateUnlockButtons();
    }

    public void OnPlayButtonClick()
    {
        GameProcess.State = GameProcess.ModeMainMenuKitchen;
        mainMenuPanel.Show(false);
        EventChangeCamera();
    }

    public void UpdateUnlockButtons()
    {
        ApplyUnlockEffect();

        ButtonBar.SetActive(IsBarUnlocked);
        ButtonUnlockBar.SetActive(!IsBarUnlocked);

        ButtonLab.SetActive(IsLaboratoryUnlocked);
        ButtonUnlockLab.SetActive(!IsLaboratoryUnlocked);
        
        ButtonFactory.SetActive(IsFactoryUnlocked);
        ButtonUnlockFactory.SetActive(!IsFactoryUnlocked);
    }

    private void ApplyUnlockEffect()
    {
        if (ButtonUnlockBar.activeSelf && IsBarUnlocked)
        {
            unlockEffect.transform.position = ButtonUnlockBar.transform.position + Vector3.up * .5f;
            unlockEffect.Play();
        }
        if (ButtonUnlockLab.activeSelf && IsLaboratoryUnlocked)
        {
            unlockEffect.transform.position = ButtonUnlockLab.transform.position + Vector3.up * .5f;
            unlockEffect.Play();
        }
        if (ButtonUnlockFactory.activeSelf && IsFactoryUnlocked)
        {
            unlockEffect.transform.position = ButtonUnlockFactory.transform.position + Vector3.up * .5f;
            unlockEffect.Play();
        }
    }

    public static void ChangeState()
    {
        EventChangeState();
        Debug.Log("Event Change State: " + GameProcess.State);
    }
   
    void ControlGame()
    {
        if (Input.GetKeyDown("z"))
        {
            Debug.Log("Deleted all PlayerPrefs");
            PlayerPrefs.DeleteAll();
        }
        if (Input.GetKeyDown("="))
        {
            Debug.Log("+10 Stars");
            int buy = 0;
            if (PlayerPrefs.HasKey("Buy"))
            {
                buy = PlayerPrefs.GetInt("Buy");
            }

            PlayerPrefs.SetInt("Buy", buy + 10);
            PlayerPrefs.Save();
            MenuScoresCounter.TotalStarsCounter();
        }
    }
    void Update ()
    {
        ControlGame();
        CrackAnimationFunction();
        TouchControl();
    }
    void TouchControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouchTime = Time.time;
            startTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            float touchTime = Time.time - startTouchTime;
            if (touchTime > maxTouchTime)
                return;

            Vector3 slideDirection = Input.mousePosition - startTouchPosition;

            if (Camera.main.ScreenToViewportPoint(slideDirection).magnitude > minTouchSlideMagnitude)
            {
                float dotProduct = Vector3.Dot(slideDirection.normalized, Vector3.left);

                if (dotProduct < -.8f)
                {
                    switch (GameProcess.State)
                    {
                        case GameProcess.ModeMainMenuKitchen:
                            GameProcess.LastState = GameProcess.State;
                            GameProcess.State = GameProcess.ModeMainMenu;
                            mainMenuPanel.Show(true);
                            EventChangeCamera();
                            break;
                        case GameProcess.ModeMainMenuBar:
                            GameProcess.LastState = GameProcess.State;
                            GameProcess.State = GameProcess.ModeMainMenuKitchen;
                            EventChangeCamera();
                            break;
                        case GameProcess.ModeMainMenuLab:
                            GameProcess.LastState = GameProcess.State;
                            GameProcess.State = GameProcess.ModeMainMenuBar;
                            EventChangeCamera();
                            break;
                        case GameProcess.ModeMainMenuFactory:
                            GameProcess.LastState = GameProcess.State;
                            GameProcess.State = GameProcess.ModeMainMenuLab;
                            EventChangeCamera();
                            break;
                        case GameProcess.ModeMainMenuDonates:
                            GameProcess.LastState = GameProcess.State;
                            GameProcess.State = GameProcess.ModeMainMenuFactory;
                            EventChangeCamera();
                            break;
                    }
                    EventChangeState();
                    return;
                }
                else if (dotProduct > .8f)
                {
                    switch (GameProcess.State)
                    {
                        case GameProcess.ModeMainMenu:
                            GameProcess.LastState = GameProcess.State;
                            mainMenuPanel.Show(false);
                            GameProcess.State = GameProcess.ModeMainMenuKitchen;
                            EventChangeCamera();
                            break;
                        case GameProcess.ModeMainMenuKitchen:
                            GameProcess.LastState = GameProcess.State;
                            GameProcess.State = GameProcess.ModeMainMenuBar;
                            EventChangeCamera();
                            break;
                        case GameProcess.ModeMainMenuBar:
                            GameProcess.LastState = GameProcess.State;
                            GameProcess.State = GameProcess.ModeMainMenuLab;
                            EventChangeCamera();
                            break;
                        case GameProcess.ModeMainMenuLab:
                            GameProcess.LastState = GameProcess.State;
                            GameProcess.State = GameProcess.ModeMainMenuFactory;
                            EventChangeCamera();
                            break;
                        case GameProcess.ModeMainMenuFactory:
                            GameProcess.LastState = GameProcess.State;
                            GameProcess.State = GameProcess.ModeMainMenuDonates;
                            EventChangeCamera();
                            break;
                    }
                    EventChangeState();
                    return;
                }
            }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
          
            if (GameProcess.State != GameProcess.ModeSelectDonatesMenu && Physics.Raycast(Camera.main.transform.position, ray.direction, out hit, Mathf.Infinity))
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);
                GameProcess.LastState = GameProcess.State;

                if (hit.collider.gameObject == ButtonToKitchen)
                {
                    if (GameProcess.State == GameProcess.ModeDevInfo)
                    {
                        devInfoText.DOFade(0, .3f).OnComplete(() => {
                            GameProcess.State = GameProcess.ModeMainMenu;
                            mainMenuPanel.Show(true);
                            EventChangeCamera();
                            EventChangeState();
                        }).Play();
                    }
                    else
                    {
                        GameProcess.State = GameProcess.ModeMainMenuKitchen;
                        mainMenuPanel.Show(false);
                        EventChangeCamera();
                    }
                }
                if (hit.collider.gameObject == ButtonKitchen)
                {
                    GameProcess.State = GameProcess.ModeSelectLevelKitchen;
                    zoomMenuCamera.ZoomIn();
                }
                if (hit.collider.gameObject == ButtonBar)
                {
                    GameProcess.State = GameProcess.ModeSelectLevelBar;
                    zoomMenuCamera.ZoomIn();
                }
                if (hit.collider.gameObject == ButtonLab)
                {
                    GameProcess.State = GameProcess.ModeSelectLevelLab;
                    zoomMenuCamera.ZoomIn();
                }
                if (hit.collider.gameObject == ButtonFactory)
                {
                    GameProcess.State = GameProcess.ModeSelectLevelFactory;
                    zoomMenuCamera.ZoomIn();
                }
                if (hit.collider.gameObject == ButtonDonate)
                {
                    GameProcess.State = GameProcess.ModeSelectDonatesMenu;
                    zoomMenuCamera.ZoomIn();
                }
                if (hit.collider.gameObject == ButtonUnlockBar)
                {
                    if (MenuStarsCounter.TotalStarsCount() >= UnlockCosts[0]) {
                        IsBarUnlocked = true;
                        if (!PlayerPrefs.HasKey("0011Stars"))
                        {
                            PlayerPrefs.SetInt("0011Stars", 0);
                        }
                    }
                    else
                    {
                        PurchaseManager.instance.BuyNonConsumable(0);
                    }
                 }
                if (hit.collider.gameObject == ButtonUnlockLab)
                {
                    if (MenuStarsCounter.TotalStarsCount() >= UnlockCosts[1])
                    {
                        IsLaboratoryUnlocked = true;
                        if (!PlayerPrefs.HasKey("0021Stars"))
                        {
                            PlayerPrefs.SetInt("0021Stars", 0);
                        }
                    }
                    else
                    {
                        PurchaseManager.instance.BuyNonConsumable(1);
                    }
                }
                if (hit.collider.gameObject == ButtonUnlockFactory)
                {
                    if (MenuStarsCounter.TotalStarsCount() >= UnlockCosts[2])
                    {
                        IsFactoryUnlocked = true;
                        if (!PlayerPrefs.HasKey("0031Stars"))
                        {
                            PlayerPrefs.SetInt("0031Stars", 0);
                        }
                    }
                    else
                    {
                        PurchaseManager.instance.BuyNonConsumable(2);
                    }
                }
                Debug.Log("MainMenu Change State: " + GameProcess.State);
                EventChangeState();
            }
        }
    }
    void CheckLockersOnAwake()
    {
        if (PlayerPrefs.HasKey("Bar")) Destroy(ButtonUnlockBar);
        if (PlayerPrefs.HasKey("Lab")) Destroy(ButtonUnlockLab);
        if (PlayerPrefs.HasKey("Factory")) Destroy(ButtonUnlockFactory);
    }
    void CrackAnimationFunction()
    {
        if (delay == false)
        {
            CrackAnimationDelay -= Time.deltaTime;

            if (CrackAnimationDelay < 0)
            {
                delay = true;
            }
        }
    }

    public void ShowDevInfo()
    {
        GameProcess.State = GameProcess.ModeDevInfo;
        mainMenuPanel.Show(false);
        devInfoText.DOFade(1, .3f).SetDelay(1).Play();
        EventChangeCamera();
    }

    private void ShowChapterVisualsOnChangeState()
    {
        ShowChapterVisuals(GameProcess.State);
    }

    private void ShowChapterVisuals(int stateNumber)
    {
        ChapterVisuals chapterVisuals = Array.Find(chaptersVisuals, x => Array.IndexOf(x.stateNumber, stateNumber) != -1);

        if (chapterVisuals.textMeshed != null)
        {
            int indexOf = Array.IndexOf(chapterVisuals.stateNumber, stateNumber);

            foreach (var sprite in chapterVisuals.sprites)
                DOTween.ToAlpha(() => sprite.color, x => sprite.color = x, indexOf == 0 ? 1 : 0, 1f).Play();
            foreach (var textMesh in chapterVisuals.textMeshed)
                DOTween.ToAlpha(() => textMesh.color, x => textMesh.color = x, indexOf == 0 ? 1 : 0, 1f).Play();
        }
    }

    private void HideFastChapterVisuals(int stateNumber)
    {
        ChapterVisuals chapterVisuals = Array.Find(chaptersVisuals, x => Array.IndexOf(x.stateNumber, stateNumber) != -1);

        if (chapterVisuals.textMeshed != null)
        {
            foreach (var sprite in chapterVisuals.sprites)
                DOTween.ToAlpha(() => sprite.color, x => sprite.color = x, 0, 0).Play();
            foreach (var textMesh in chapterVisuals.textMeshed)
                DOTween.ToAlpha(() => textMesh.color, x => textMesh.color = x, 0, 0).Play();
        }
    }

    [Serializable]
    private struct ChapterVisuals
    {
        public int[] stateNumber;
        public TextMesh[] textMeshed;
        public SpriteRenderer[] sprites;
    }
}
