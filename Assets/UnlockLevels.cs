using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockLevels : MonoBehaviour
{
    public static event Action OnUnlockLevel;
    // Use this for initialization
    void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { TaskOnClick(); });

    }
    public void TaskOnClick()
    {
        if (gameObject.name == "UnlockBar")
        {
            MainMenu.IsBarUnlocked = true;
            for (int i = 1; i < 11; i++)
            {
                if (i != 10)
                    PlayerPrefs.SetInt("000" + i.ToString() + "Stars", 3);
                else
                    PlayerPrefs.SetInt("0010" + "Stars", 3);
            }
            if (!PlayerPrefs.HasKey("0011Stars"))
                PlayerPrefs.SetInt("0011" + "Stars", 0);
        }
        else 
        if (gameObject.name == "UnlockLab")
        {
            MainMenu.IsLaboratoryUnlocked = true;
            for (int i = 11; i < 21; i++)
            {
                if (i != 20)
                    PlayerPrefs.SetInt("00" + i.ToString() + "Stars", 3);
                else
                    PlayerPrefs.SetInt("0020" + "Stars", 3);
            }
            if (!PlayerPrefs.HasKey("0021Stars"))
                PlayerPrefs.SetInt("0021" + "Stars", 0);
        }
        else
        if (gameObject.name == "UnlockFactory")
        {
            MainMenu.IsFactoryUnlocked = true;
            for (int i = 21; i < 31; i++)
            {
                if (i != 30)
                    PlayerPrefs.SetInt("00" + i.ToString() + "Stars", 3);
                else
                    PlayerPrefs.SetInt("0030" + "Stars", 3);
            }
            if (!PlayerPrefs.HasKey("0031Stars"))
                PlayerPrefs.SetInt("0031" + "Stars", 0);
        }
        else
        if (gameObject.name == "PassFactory")
        {
            for (int i = 31; i < 41; i++)
            {
                if (i != 40)
                    PlayerPrefs.SetInt("00" + i.ToString() + "Stars", 3);
                else
                    PlayerPrefs.SetInt("0040" + "Stars", 3);
            }
        }
        else
        if (gameObject.name == "DeleteAll")
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("0001" + "Stars", 0);
        }

        PlayerPrefs.Save();
        OnUnlockLevel();
    }
}
