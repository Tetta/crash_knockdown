using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuStarsCounter : MonoBehaviour
{
    [SerializeField] int ThemeNumber;
    TextMesh T;

    [SerializeField]
    private Color lockedColor = Color.red;
    [SerializeField]
    private Color unlockedColor = Color.green;
    // Use this for initialization

    private void Awake()
    {
        MainMenu.EventOpenTheme += UpdateLabels;
        UnlockLevels.OnUnlockLevel += UpdateLabels;
        DifficultyPanel.OnRemoveAllLevelData += UpdateLabels;
        DifficultyPanel.OnChangeDifficulty += UpdateLabelsInFrame;
    }

    private void OnDestroy()
    {
        MainMenu.EventOpenTheme -= UpdateLabels;
        UnlockLevels.OnUnlockLevel -= UpdateLabels;
        DifficultyPanel.OnRemoveAllLevelData -= UpdateLabels;
        DifficultyPanel.OnChangeDifficulty -= UpdateLabelsInFrame;
    }

    void Start()
    {
        UpdateLabels();
    }

    private void UpdateLabelsInFrame()
    {
        Invoke("UpdateLabels",0.03f);
    }

    private void UpdateLabels()
    {
        T = GetComponent<TextMesh>();

        bool isLocked = false;
        switch (ThemeNumber)
        {
            case 0:
                isLocked = false;
                break;
            case 1:
                isLocked = !MainMenu.IsBarUnlocked;
                break;
            case 2:
                isLocked = !MainMenu.IsLaboratoryUnlocked;
                break;
            case 3:
                isLocked = !MainMenu.IsFactoryUnlocked;
                break;
        }

        T.text = (isLocked ? TotalStarsCount() : TotalStarsCount(ThemeNumber)) + "/" + (isLocked ? MainMenu.UnlockCosts[ThemeNumber - 1] : 30);
        if (isLocked)
        {
            bool isReadyToUnlock = TotalStarsCount() >= MainMenu.UnlockCosts[ThemeNumber - 1];
            T.color = isReadyToUnlock ? unlockedColor : lockedColor;
        }
        else
        {
            T.color = Color.white;
        }
    }

    public static int TotalStarsCount()
    {
        int scores = 0;
        for (int i = 1; i <= 10; i++)
        {
            if (PlayerPrefs.HasKey((i < 10 ? "000" : "00") + i.ToString() + "Stars"))
            {
                scores += PlayerPrefs.GetInt((i < 10 ? "000" : "00") + i.ToString() + "Stars");

            }
        }
        for (int i = 11; i <= 20; i++)
        {
            if (PlayerPrefs.HasKey("00" + i.ToString() + "Stars"))
            {
                scores += PlayerPrefs.GetInt("00" + i.ToString() + "Stars");
            }
        }
        for (int i = 21; i <= 30; i++)
        {
            if (PlayerPrefs.HasKey("00" + i.ToString() + "Stars"))
            {
                scores += PlayerPrefs.GetInt("00" + i.ToString() + "Stars");
            }
        }
        for (int i = 31; i <= 40; i++)
        {
            if (PlayerPrefs.HasKey("00" + i.ToString() + "Stars"))
            {
                scores += PlayerPrefs.GetInt("00" + i.ToString() + "Stars");
            }
        }
        for (int i = 41; i <= 50; i++)
        {
            if (PlayerPrefs.HasKey("00" + i.ToString() + "Stars"))
            {
                scores += PlayerPrefs.GetInt("00" + i.ToString() + "Stars");
            }
        }
        for (int i = 51; i <= 60; i++)
        {
            if (PlayerPrefs.HasKey("00" + i.ToString() + "Stars"))
            {
                scores += PlayerPrefs.GetInt("00" + i.ToString() + "Stars");
            }
        }
        for (int i = 61; i <= 70; i++)
        {
            if (PlayerPrefs.HasKey("00" + i.ToString() + "Stars"))
            {
                scores += PlayerPrefs.GetInt("00" + i.ToString() + "Stars");
            }
        }
        for (int i = 71; i <= 80; i++)
        {
            if (PlayerPrefs.HasKey("00" + i.ToString() + "Stars"))
            {
                scores += PlayerPrefs.GetInt("00" + i.ToString() + "Stars");
            }
        }
        return scores;
    }

    public int TotalStarsCount(int theme)
    {
        int scores = 0;
        for (int i = theme * 10 + 1; i <= theme * 10 + 10; i++)
        {
            if (PlayerPrefs.HasKey((i < 10 ? "000" : "00") + i.ToString() + "Stars"))
            {
                scores += PlayerPrefs.GetInt((i < 10 ? "000" : "00") + i.ToString() + "Stars");

            }
        }
        
        return scores;
    }
}
