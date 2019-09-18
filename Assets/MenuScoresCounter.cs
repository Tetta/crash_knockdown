using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScoresCounter : MonoBehaviour {
    [SerializeField] int ThemeNumber;
    TextMesh T;
    [SerializeField] TextMesh statusText;
    [SerializeField] TextMesh toUnlockText;

    private void Awake()
    {
        MainMenu.EventOpenTheme += UpdateLabels;
        UnlockLevels.OnUnlockLevel += UpdateLabels;
        DifficultyPanel.OnRemoveAllLevelData += UpdateLabels;
    }

    private void OnDestroy()
    {
        MainMenu.EventOpenTheme -= UpdateLabels;
        UnlockLevels.OnUnlockLevel -= UpdateLabels;
        DifficultyPanel.OnRemoveAllLevelData -= UpdateLabels;
    }

    // Use this for initialization
    void Start () {
        UpdateLabels();
    }
	
	void UpdateLabels () {
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

        T.text = isLocked ? "" : TotalScoresCounter().ToString();
        if (toUnlockText)
            toUnlockText.gameObject.SetActive(isLocked);
        statusText.gameObject.SetActive(!isLocked);
    }

    public int TotalScoresCounter()
    {
        int scores = 0;
        if (ThemeNumber == 0)
        {
            for (int i = 1; i <= 10; i++)
            {

                if (PlayerPrefs.HasKey((i < 10 ? "000" : "00") + i.ToString() + "Scores"))
                {
                    scores += PlayerPrefs.GetInt((i < 10 ? "000" : "00") + i.ToString() + "Scores");

                }
            }
            for (int i = 11; i <= 20; i++)
            {
                if (PlayerPrefs.HasKey("00" + i.ToString() + "Scores"))
                {
                    scores += PlayerPrefs.GetInt("00" + i.ToString() + "Scores");
                }
            }
        }

        if (ThemeNumber == 1)
        {
            for (int i = 21; i <= 30; i++)
            {
                if (PlayerPrefs.HasKey("00" + i.ToString() + "Scores"))
                {
                    scores += PlayerPrefs.GetInt("00" + i.ToString() + "Scores");
                }
            }
            for (int i = 31; i <= 40; i++)
            {
                if (PlayerPrefs.HasKey("00" + i.ToString() + "Scores"))
                {
                    scores += PlayerPrefs.GetInt("00" + i.ToString() + "Scores");
                }
            }
        }
        if (ThemeNumber == 2)
        {
            for (int i = 41; i <= 50; i++)
            {
                if (PlayerPrefs.HasKey("00" + i.ToString() + "Scores"))
                {
                    scores += PlayerPrefs.GetInt("00" + i.ToString() + "Scores");
                }
            }
            for (int i = 51; i <= 60; i++)
            {
                if (PlayerPrefs.HasKey("00" + i.ToString() + "Scores"))
                {
                    scores += PlayerPrefs.GetInt("00" + i.ToString() + "Scores");
                }
            }
        }
        if (ThemeNumber == 3)
        {
            for (int i = 61; i <= 70; i++)
            {
                if (PlayerPrefs.HasKey("00" + i.ToString() + "Scores"))
                {
                    scores += PlayerPrefs.GetInt("00" + i.ToString() + "Scores");
                }
            }
            for (int i = 71; i <= 80; i++)
            {
                if (PlayerPrefs.HasKey("00" + i.ToString() + "Scores"))
                {
                    scores += PlayerPrefs.GetInt("00" + i.ToString() + "Scores");
                }
            }
        }
        return scores;
    }

    public static int TotalStarsCounter()
    {
        int TotalStars = 0;
            for (int i = 1; i <= 10; i++)
            {

                if (PlayerPrefs.HasKey((i < 10 ? "000" : "00") + i.ToString() + "Stars"))
                {
                TotalStars += PlayerPrefs.GetInt((i < 10 ? "000" : "00") + i.ToString() + "Stars");

                }
            }
            for (int i = 11; i <= 20; i++)
            {
                if (PlayerPrefs.HasKey("00" + i.ToString() + "Stars"))
                {
                TotalStars += PlayerPrefs.GetInt("00" + i.ToString() + "Stars");
                }
            }
        
        
            for (int i = 21; i <= 30; i++)
            {
                if (PlayerPrefs.HasKey("00" + i.ToString() + "Stars"))
                {
                TotalStars += PlayerPrefs.GetInt("00" + i.ToString() + "Stars");
                }
            }
            for (int i = 31; i <= 40; i++)
            {
                if (PlayerPrefs.HasKey("00" + i.ToString() + "Stars"))
                {
                TotalStars += PlayerPrefs.GetInt("00" + i.ToString() + "Stars");
                }
            }
        
            for (int i = 41; i <= 50; i++)
            {
                if (PlayerPrefs.HasKey("00" + i.ToString() + "Stars"))
                {
                TotalStars += PlayerPrefs.GetInt("00" + i.ToString() + "Stars");
                }
            }
            for (int i = 51; i <= 60; i++)
            {
                if (PlayerPrefs.HasKey("00" + i.ToString() + "Stars"))
                {
                TotalStars += PlayerPrefs.GetInt("00" + i.ToString() + "Stars");
                }
            }
         
      
            for (int i = 61; i <= 70; i++)
            {
                if (PlayerPrefs.HasKey("00" + i.ToString() + "Stars"))
                {
                TotalStars += PlayerPrefs.GetInt("00" + i.ToString() + "Stars");
                }
            }
            for (int i = 71; i <= 80; i++)
            {
                if (PlayerPrefs.HasKey("00" + i.ToString() + "Stars"))
                {
                TotalStars += PlayerPrefs.GetInt("00" + i.ToString() + "Stars");
                }
            }
        Debug.Log("TotalStars = " + TotalStars);
        GameObject.Find("TotalStars").GetComponent<Text>().text = TotalStars.ToString();
        return TotalStars;
    }
}
