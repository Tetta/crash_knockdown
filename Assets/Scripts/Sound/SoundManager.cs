using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    //private PartsCollisions partColObj;

    private StudioListener myStudioListener;
    private Camera myCamera;

    private SoundAndMusik proverkaSingleton;

    private EventDescription myEvDes;
    private string tempPath;

    //Меняющиеся звуки для всей игры
    public string menuMusic;
    public string playMusik;
    public string playMusikKitten;
    public string winnerScene;
    public string looserScene;

    //Menu & Клики по кнопкам
    public string onUpMovePanel;
    public string allMovePanel;
    public string clickOnButton;
    public string clickOnButtonContinue;
    public string clickOnButtonReboot;
    public string clickOnButtonEscMenu;

    //Action sound
    public string soundShot;
    public string breakGlass;
    public string breakMetall;
    public string slowMotion;
    public string streakShots01;
    public string streakShots02;
    public string streakShots03;
    public string streakShots04;
    public string streakShots05;

    public string breakGlass01;
    public string breakGlass02;
    public string breakGlass03;

    //Звуки падения
    public string knockMetallToWood;
    public string knockMetallToWoodMore;
    public string knockVeggie;
    public string knockGlass;
    public string knockWood;
    public string knockWoodToWood;

    //Звук подбирания\подхватывания
    public string takeBall;
    public string takeVeggie;
    public string takeWood;
    public string takeMetallTable;

    //Разное    
    public string starOnOne;
    public string starOnTwo;
    public string starOnThree;
    public string starLoose;
    public string aplauseMG;


    void Awake()
    {

        myCamera = Camera.main;
        myStudioListener = FindObjectOfType<StudioListener>();
        if(myStudioListener == null) myStudioListener = myCamera.gameObject.AddComponent<StudioListener>();

        menuMusic = "event:/Music/Menu/Music_menu";
        playMusik = "event:/Music/Kitchen/Music_gameplay";
        playMusikKitten = "event:/Music/Kitchen/Music_gameplay";
        winnerScene = "event:/Music/Kitchen/Fanfares_of_victory";
        looserScene = "event:/Music/Kitchen/Fanfares_of_defeat";

        //Menu & Клики по кнопкам
        //Kitten
        onUpMovePanel = "event:/Scoreboard/Whoosh_scoreboard_down";
        allMovePanel = "event:/Menu/Move_Board";
        clickOnButton = "event:/Menu/TAP_button";
        clickOnButtonContinue = "event:/Scoreboard/CONTINUE_button";
        clickOnButtonReboot = "event:/Scoreboard/RESTART_button";
        clickOnButtonEscMenu = "event:/Scoreboard/MENU_button";

        //Action sound
        soundShot = "event:/Gameplay/Throw";
        breakGlass = "event:/Gameplay/Destruction/Glass";
        breakMetall = "";
        slowMotion = "event:/Gameplay/Destruction/Slowmotion";
        streakShots01 = "event:/Gameplay/5_stadies/1_Encouragement";
        streakShots02 = "event:/Gameplay/5_stadies/2_Encouragement";
        streakShots03 = "event:/Gameplay/5_stadies/3_Encouragement";
        streakShots04 = "event:/Gameplay/5_stadies/4_Encouragement";
        streakShots05 = "event:/Gameplay/5_stadies/5_Encouragement";

        breakGlass01 = "event:/Gameplay/Destruction/Steklo/StrongGlass";
        breakGlass02 = "event:/Gameplay/Destruction/Steklo/MiddleGlass";
        breakGlass03 = "event:/Gameplay/Destruction/Steklo/EasyGlass";

        //Звуки падения
        knockMetallToWood = "event:/Gameplay/Fall/Ball";
        knockMetallToWoodMore = "event:/Gameplay/Fall/Metal";
        knockVeggie = "event:/Gameplay/Fall/Organic";
        knockGlass = "event:/Gameplay/Fall/Glass";
        knockWood = "event:/Gameplay/Fall/Wood";


        //Звук подбирания\подхватывания
        takeBall = "event:/Gameplay/Picking up/Metal_Ball";
        takeVeggie = "event:/Gameplay/Picking up/Organic";
        takeWood = "event:/Gameplay/Picking up/Wood";
        takeMetallTable = "event:/Gameplay/Picking up/Metal";

        //Разное 
        starOnOne = "event:/Scoreboard/Emergence_of_a_star_one";
        starOnTwo = "event:/Scoreboard/Emergence_of_a_star_two";
        starOnThree = "event:/Scoreboard/Emergence_of_a_star_three";
        starLoose = "event:/Gameplay/Appearance_of_a_star_lose_time";
        aplauseMG = "";


        InitializeManager();
        
    }

    // Use this for initialization
    void Start () {
        
        proverkaSingleton = FindObjectOfType<SoundAndMusik>();

        if (proverkaSingleton == null)
        {

            GameObject go = new GameObject();

            go.name = "SingletonController";

            proverkaSingleton = go.AddComponent<SoundAndMusik>();

            DontDestroyOnLoad(go);

        }
        

    }
	
	// Update is called once per frame
	void Update () {
        

    }

    private void InitializeManager()
    {

        //-------------Menu---------------------
        if (SceneManager.GetActiveScene().name == "MainMenuScene")
        {

            SoundAndMusik.Instance.SetPauseEventON();

            if (SoundAndMusik.Instance.GetMusicPath() != menuMusic) playMusik = playMusikKitten/*menuMusic*/;

            SoundAndMusik.Instance.ChangeMusik(playMusik);

        }
        else if (SoundAndMusik.Instance.GetMusicPath() == playMusikKitten)
        {


        }
        else
        {

            SoundAndMusik.Instance.SetPauseEventON();

            if (SoundAndMusik.Instance.GetMusicPath() != playMusikKitten) playMusik = playMusikKitten;

            SoundAndMusik.Instance.ChangeMusik(playMusik);

        }

        SoundAndMusik.Instance.SetMenuEvents(onUpMovePanel, allMovePanel, clickOnButton, clickOnButtonContinue, clickOnButtonReboot, clickOnButtonEscMenu, starLoose, starOnOne, starOnTwo, starOnThree);
        
        SoundAndMusik.Instance.SetEvents(menuMusic, playMusik, winnerScene, looserScene);
        
        SoundAndMusik.Instance.SetActionEvents(soundShot, breakGlass, breakMetall, slowMotion, streakShots01, streakShots02, streakShots03, streakShots04, streakShots05, breakGlass01, breakGlass02, breakGlass03);
        
        SoundAndMusik.Instance.SetFallEvents(knockMetallToWood, knockMetallToWoodMore, knockVeggie, knockGlass, knockWood, knockWoodToWood);
        
        SoundAndMusik.Instance.SetTakeEvents(takeBall, takeVeggie, takeWood, takeMetallTable);

    }
    
}
