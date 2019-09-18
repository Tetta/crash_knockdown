using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DifficultyPanel : MonoBehaviour
{
    [SerializeField]
    private DifficultyButton[] difficultyButtons;
    [SerializeField]
    private RectTransform areYouSurePopup;
    [SerializeField]
    private Sprite lockedSprite;
    [SerializeField]
    private Sprite unlockedSprite;
    [SerializeField]
    private Sprite selectedSprite;

    public static event Action OnRemoveAllLevelData;
    public static event Action OnChangeDifficulty;

    private int nextDifficulty;

    void Awake()
    {
        UpdatePanel();

        UnlockLevels.OnUnlockLevel += UpdatePanel;

        for (int i = 0; i < difficultyButtons.Length; i++)
        {
            int arg = i;
            difficultyButtons[i].button.onClick.AddListener(() => ChooseDifficulty(arg));
        }
    }

    private void OnDestroy()
    {
        UnlockLevels.OnUnlockLevel -= UpdatePanel;
    }

    private void UpdatePanel()
    {
        if (PlayerPrefs.GetInt("0040Stars", -1) > 0 || PlayerPrefs.HasKey("isCanHard"))
        {
            PlayerPrefs.SetInt("isCanHard", 1);
            difficultyButtons[2].isLocked = false;
        }
        else
            difficultyButtons[2].isLocked = true;

        DrawDifficulty();
    }

    private void DrawDifficulty()
    {
        for (int i = 0; i < difficultyButtons.Length; i++)
        {
            if (difficultyButtons[i].isLocked)
                difficultyButtons[i].image.sprite = lockedSprite;
            else if (i == GameProcess.Difficulty)
                difficultyButtons[i].image.sprite = selectedSprite;
            else
            {
                difficultyButtons[i].image.sprite = unlockedSprite;
            }
        }
    }

    private void ChooseDifficulty(int arg)
    {
        if (difficultyButtons[arg].isLocked)
            return;
        if (GameProcess.Difficulty != arg)
        {
            if (arg == 2 || arg < 3 && GameProcess.Difficulty == 2)
            {
                nextDifficulty = arg;
                ShowAreYouSurePopup(true);
            }
            else
            {
                GameProcess.Difficulty = arg;
                OnChangeDifficulty();
                DrawDifficulty();
            }
        }
    }

    public void ShowAreYouSurePopup(bool isShow)
    {
        areYouSurePopup.DOAnchorPosX(isShow ? 0 : -areYouSurePopup.rect.width * 1.5f, 0.25f).SetEase(Ease.InOutQuart).Play();
    }

    public void SelectPowerDifficulty()
    {
        for (int i = 1; i <= 40; i++)
        {
            PlayerPrefs.SetInt((i < 10 ? "000" : "00") + i + "Stars", 0);
        }
        GameProcess.Difficulty = nextDifficulty;
        OnChangeDifficulty();
        OnRemoveAllLevelData();
        DrawDifficulty();
        ShowAreYouSurePopup(false);
    }

    [Serializable]
    private struct DifficultyButton
    {
        public Button button;
        public Image image;
        public bool isLocked;
    }
}
