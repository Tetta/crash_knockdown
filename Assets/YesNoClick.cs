using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YesNoClick : MonoBehaviour {
    [SerializeField] int State;
    [SerializeField] int StateNo;
    private void Awake()
    {

        gameObject.GetComponent<Button>().onClick.AddListener(delegate { TaskOnClick(); });
    }

    // Update is called once per frame
    void Update()
    {

    }
    void TaskOnClick()
    {
        if (GameProcess.State == State)
        {

            if (gameObject.name == "No")
            {
                GameProcess.State = StateNo;
                Debug.Log("No Clicked, Changed State To " + GameProcess.State);

                MainMenu.ChangeState();

            }
            else
            {
                if (GameProcess.ModeShopBar == GameProcess.State)

                {


                    Debug.Log("Buy Bar Theme!");
                    PlayerPrefs.SetInt("Bar", MainMenu.UnlockCosts[0]);
                    PlayerPrefs.SetInt("0011Stars", 0);
                    GameProcess.State = GameProcess.ModeMainMenuBar;
                    PlayerPrefs.Save();

                }
                if (GameProcess.ModeUnlockBarYesNo == GameProcess.State)
                {
                    if (MainMenu.UnlockCosts[0] <= MenuScoresCounter.TotalStarsCounter())
                    {
                        Debug.Log("Unlock Bar Theme!!");
                        PlayerPrefs.SetInt("Bar", MainMenu.UnlockCosts[0]);
                        PlayerPrefs.SetInt("0011Stars", 0);

                        GameProcess.State = GameProcess.ModeMainMenuBar;
                        PlayerPrefs.Save();
                    }
                    else
                    {

                        Debug.Log("Player does not have Stars -> Go to Shop!");
                        GameProcess.State = GameProcess.ModeShopBar;
                    }
                }
                if (GameProcess.ModeShopLab == GameProcess.State)

                {


                    PlayerPrefs.SetInt("0021Stars", 0);
                    Debug.Log("Buy Lab Theme!");
                    PlayerPrefs.SetInt("Lab", MainMenu.UnlockCosts[1]);
                    GameProcess.State = GameProcess.ModeMainMenuLab;
                    PlayerPrefs.Save();

                }
                if (GameProcess.ModeUnlockLabYesNo == GameProcess.State)
                {
                    if (MainMenu.UnlockCosts[0] <= MenuScoresCounter.TotalStarsCounter())
                    {
                        Debug.Log("Unlock Lab Theme!!");
                        PlayerPrefs.SetInt("Lab", MainMenu.UnlockCosts[0]);
                        GameProcess.State = GameProcess.ModeMainMenuBar;
                    PlayerPrefs.SetInt("0021Stars", 0);
                        PlayerPrefs.Save();
                    }
                    else
                    {

                        Debug.Log("Player does not have Stars -> Go to Shop!");
                        GameProcess.State = GameProcess.ModeShopLab;
                    }
                }
                if (GameProcess.ModeShopFactory == GameProcess.State)

                {

                    PlayerPrefs.SetInt("0031Stars", 0);

                    Debug.Log("Buy Factory Theme!");
                    PlayerPrefs.SetInt("Factory", MainMenu.UnlockCosts[2]);
                    GameProcess.State = GameProcess.ModeMainMenuFactory;
                    PlayerPrefs.Save();

                }
                if (GameProcess.ModeUnlockFactoryYesNo == GameProcess.State)
                {
                    if (MainMenu.UnlockCosts[2] <= MenuScoresCounter.TotalStarsCounter())
                    {
                        Debug.Log("Unlock Factory Theme!!");
                        PlayerPrefs.SetInt("0031Stars", 0);
                        PlayerPrefs.SetInt("Factory", MainMenu.UnlockCosts[2]);
                        GameProcess.State = GameProcess.ModeMainMenuFactory;
                        PlayerPrefs.Save();
                    }
                    else
                    {

                        Debug.Log("Player does not have Stars -> Go to Shop!");
                        GameProcess.State = GameProcess.ModeShopFactory;
                    }
                }


                MainMenu.ChangeState();


            }
        }
    }
}
