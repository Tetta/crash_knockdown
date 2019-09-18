using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
 
public class ChangeGameStateOnClick : MonoBehaviour
{
    [SerializeField] int ChangeTo;
    [SerializeField] bool OpenLevelByName=false;
    [SerializeField] bool NextLevelelButton=false;

    public static bool IsTutorialOpened
    {
        get { return PlayerPrefs.HasKey("is_tutorial_opened"); }
        set
        {
            if (value)
            {
                PlayerPrefs.SetInt("is_tutorial_opened", 1);
            }
            else
                PlayerPrefs.DeleteKey("is_tutorial_opened");
        }
    }

    // Use this for initialization
    void Awake()
    {
        GameProcess.EventWin += OnWin;
       gameObject.GetComponent<Button>().onClick.AddListener(delegate { TaskOnClick(ChangeTo); });
    }

    private void OnDestroy()
    {
        GameProcess.EventWin -= OnWin;

    }
    void OnWin()
    {
        if (NextLevelelButton)
        {
            if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "Stars"))
            {
                if(PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "Stars")==0)
             
                Destroy(this.gameObject);
            }

        }
    }
    void TaskOnClick(int someValue)
    {
        if (gameObject.name == "YesQuality")
        {
            int GQ = PlayerPrefs.GetInt("difficulty", 1);
            if (GQ == 1) GameProcess.Difficulty=0;
            if (GQ == 2) GameProcess.Difficulty=1;

            GameProcess.FadeLoadLevel(SceneManager.GetActiveScene().name);




            return;
        }
        if (gameObject.name == "NoQuality")
        {
          
            GameProcess.FadeLoadLevel(SceneManager.GetActiveScene().name);




            return;
        }
        if (someValue == -5) GameProcess.FadeLoadLevel("0001");

        if (!OpenLevelByName)
        {
            GameProcess.State = someValue;
            if (gameObject.name == "Max") simpleFPS.SetResolution(1);
            if (gameObject.name == "Half") simpleFPS.SetResolution(0.5f);
            if (gameObject.name == "Quater") simpleFPS.SetResolution(0.25f);
            if (gameObject.name == "POST") simpleFPS.TurnPostEffects();
        //    if (gameObject.name == "Qual") simpleFPS.ChangeQuality();
            if (gameObject.name == "Winwin")
            {
                GameProcess.MyTimer = 1;
                GameProcess.EndGameSession();
            }
            GameProcess.ControlStates();
            GameGUI.UpdateStates();
            MainMenuCameraController.ChangeCameraPos();
        }
        else
        {
            if (PlayerPrefs.HasKey(name.Substring(name.Length - 4) + "Stars"))
                if (IsTutorialOpened)

                    GameProcess.FadeLoadLevel(   name.Substring(name.Length - 4));
                else
                {
                    GameProcess.FadeLoadLevel("t1");
                    IsTutorialOpened = true;
                }
            Debug.Log( name.Substring(name.Length - 4));
        }
        if (NextLevelelButton)
        {
            if (SceneManager.GetActiveScene().name  == "t1")
            {
                GameProcess.FadeLoadLevel("t2");
                return;
            }
            if (SceneManager.GetActiveScene().name  == "t2")
            {
                GameProcess.FadeLoadLevel("t3");
                return;

            }
            if (SceneManager.GetActiveScene().name  == "t3")
            {
                GameProcess.FadeLoadLevel("t4");
                return;

            }

            if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name+ "Stars"))
                if(PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "Stars") > 0)
                {
                    int lvl = 0;
                    int.TryParse( SceneManager.GetActiveScene().name ,out lvl) ;

                    string sceneName = SceneManager.GetActiveScene().name;
                    if (sceneName.Substring(0,1) == "t")
                    {
                        if (sceneName != "t4")
                        {
                            GameProcess.FadeLoadLevel("t" + (int.Parse(sceneName.Substring(1, 1)) + 1));
                        }
                        else
                        {
                            GameProcess.FadeLoadLevel("0001");
                        }
                        return;
                    }

                    if ((lvl == 10) || (lvl == 20) || (lvl == 30) || (lvl == 40)) {
                        GameProcess.FadeLoadLevel("mainmenu");
                        return;
                    }
                    if (lvl < 9)
                        GameProcess.FadeLoadLevel( "000" + (lvl + 1).ToString());
                    else
                    if (lvl < 99)
                        GameProcess.FadeLoadLevel("00" + (lvl + 1).ToString());


                }
        }
        if (someValue == 5)
        {
            GameProcess.FadeLoadLevel("mainmenu");
        }
    }

}
