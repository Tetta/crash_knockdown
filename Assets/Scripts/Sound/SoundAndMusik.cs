using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SoundAndMusik : MonoBehaviour
{

    public static SoundAndMusik _instance = null;

    public EventDescription myEvDes;
    public string tempPath;

    private Camera _camera;
    private bool winner;
    private int sceneNumber;
    private float winParam;
    private bool isWeiter;
    public bool isLooserOne;
    private Rigidbody myRig;
    public bool isFly;
    public bool isSlowMo = false;


    //Меняющиеся звуки для всей игры
    public string menuMusic;
    public string playMusik;
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
    


    public EventInstance musikEv;
    public ParameterInstance musikPar;
    
    public EventInstance clickEv;
    public ParameterInstance clickPar;

    public EventInstance winnerEv;
    public ParameterInstance winnerPar;

    public EventInstance glassFallEv;
    public ParameterInstance glassFallPar;
    private float myTimer = 0.5f;

    public string stopString;
    public EventInstance stopEv;
    public ParameterInstance stopPar;

    public string loadingScoreBoardWindowString = "event:/LoadingScoreBoard_window";
    public EventInstance loadingScoreBoardWindow;
    public ParameterInstance loadingScoreBoardWindowParameter;
    
    public string pauseWindowString = "event:/Pause_window";
    public EventInstance pauseWindow;
    
    private List<string> metallMass = new List<string>();
    private List<string> vegieMass = new List<string>();
    private List<string> glassMass = new List<string>();
    private List<string> woodMass = new List<string>();
    private List<string> massMeshGlass = new List<string>();
    private PhysicMaterial tempMaterial;

    private GameObject tempGO;
    private Rigidbody tempRB;




    public static SoundAndMusik Instance
    {

        get
        {
            if (_instance == null)
            {

                _instance = FindObjectOfType<SoundAndMusik>();

                if (_instance == null)
                {

                    GameObject go = new GameObject();

                    go.name = "SingletonController";

                    _instance = go.AddComponent<SoundAndMusik>();

                    DontDestroyOnLoad(go);

                }

            }

            return _instance;

        }

    }

    void Awake()
    {

        if (_instance == null)

        {
            _instance = this;

            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(gameObject);

        }

        _camera = Camera.main;

        menuMusic = "event:/Music/Kitchen/Music_gameplay";                
        
    }

    // Use this for initialization
    void Start () {

        InitializeManager();

    }

    private void InitializeManager()
    {

        //winner = false;

        if (musikEv.handle == null)
        {

            playMusik = menuMusic;

            musikEv = RuntimeManager.CreateInstance(playMusik);

            musikEv.start();

        }
		if (GameProcess.State == GameProcess.ModePlay)
		{
			try
			{
				tempGO = FindObjectOfType<FracturedObject>().gameObject;
			}
			catch { }
		}
        if (tempGO != null)
        {

            tempRB = tempGO.GetComponentInChildren<Rigidbody>();
                        

        }

        Table();

    }
	
	// Update is called once per frame
	void Update () {

        if (glassFallEv.handle.ToInt32() != 0 & myTimer > 0)
        {

            myTimer -= Time.deltaTime;

        }
        else if (glassFallEv.handle.ToInt32() != 0 & myTimer <= 0) 
        {

            glassFallEv.clearHandle();
            myTimer = 0.5f;

        }

    }
    //---------------------------------------------------------------------------------------------------------------------------------------------------------
    //Системные
    //---------------------------------------------------------------------------------------------------------------------------------------------------------
    public string GetMusicPath()
    {

        musikEv.getDescription(out myEvDes);
        myEvDes.getPath(out tempPath);
        return tempPath;

    }
    
    public void StopMisik()
    {

        musikEv.stop(STOP_MODE.ALLOWFADEOUT);

    }

    public void ChangeMusik(string pathEvent)
    {

        musikEv.stop(STOP_MODE.ALLOWFADEOUT);

        playMusik = pathEvent;
        musikEv = RuntimeManager.CreateInstance(playMusik);
        musikEv.start();

    }

    public void LoadingScoreBoardWindow()
    {

        loadingScoreBoardWindow = RuntimeManager.CreateInstance(loadingScoreBoardWindowString);
        loadingScoreBoardWindow.start();
        loadingScoreBoardWindow.triggerCue();

    }

    public void PauseWindow()
    {

        pauseWindow = RuntimeManager.CreateInstance(pauseWindowString);
        pauseWindow.start();

    }

    public void PauseWindowOff()
    {

        pauseWindow.triggerCue();

    }
    //---------------------------------------------------------------------------------------------------------------------------------------------------------
    //Задание звуков
    //---------------------------------------------------------------------------------------------------------------------------------------------------------
    public void SetEvents(string menuMusicMy, string playMusikMy, string ourWinner, string ourLoose)
    {

        menuMusic = menuMusicMy;
        playMusik = playMusikMy;
        winnerScene = ourWinner;
        looserScene = ourLoose;
        
    }

    public void SetMenuEvents(string onUpPanel,string allMovePanelMy, string clickOnButtonMy, string clickOnButtonContinueMy, string clickOnButtonRebootMy, string clickOnButtonEscMenuMy, string starLooseMy,
        string starOnMy1, string starOnMy2, string starOnMy3)
    {
        onUpMovePanel = onUpPanel;
        allMovePanel = allMovePanelMy;
        clickOnButton = clickOnButtonMy;
        clickOnButtonContinue = clickOnButtonContinueMy;
        clickOnButtonReboot = clickOnButtonRebootMy;
        clickOnButtonEscMenu = clickOnButtonEscMenuMy;
        starLoose = starLooseMy;
        starOnOne = starOnMy1;
        starOnTwo = starOnMy2;
        starOnThree = starOnMy3;

    }

    public void SetActionEvents(string soundShotMy, string breakGlassMy, string breakMetallMy, string mySlowMotion, string myStreakShots01, string myStreakShots02, string myStreakShots03, string myStreakShots04,
        string myStreakShots05, string myBreakGlass01, string myBreakGlass02, string myBreakGlass03)
    {
        soundShot = soundShotMy;
        breakGlass = breakGlassMy;
        breakMetall = breakMetallMy;
        slowMotion = mySlowMotion;
        streakShots01 = myStreakShots01;
        streakShots02 = myStreakShots02;
        streakShots03 = myStreakShots03;
        streakShots04 = myStreakShots04;
        streakShots05 = myStreakShots05;

        breakGlass01 = myBreakGlass01;
        breakGlass02 = myBreakGlass02;
        breakGlass03 = myBreakGlass03;

    }

public void SetFallEvents(string knockMetallToWoodMy, string knockMetallToWoodMoreMy, string knockVeggieMy, string knockGlassMy, string knockWoodMy, string knockWoodToWoodMy)
    {

        knockMetallToWood = knockMetallToWoodMy;
        knockMetallToWoodMore = knockMetallToWoodMoreMy;
        knockVeggie = knockVeggieMy;
        knockGlass = knockGlassMy;
        knockWood = knockWoodMy;
        knockWoodToWood = knockWoodToWoodMy;

}

    public void SetTakeEvents(string takeBallMy, string takeVeggieMy, string takeWoodMy, string takeMetallTableMy)
    {

        takeBall = takeBallMy;
        takeVeggie = takeVeggieMy;
        takeWood = takeWoodMy;
        takeMetallTable = takeMetallTableMy;

    }
    //---------------------------------------------------------------------------------------------------------------------------------------------------------
    //Сами вызовы звука
    //---------------------------------------------------------------------------------------------------------------------------------------------------------
    //Меню
    //---------------------------------------------------------------------------------------------------------------------------------------------------------       
    public void GetWinner()
    {

        if (_camera == null) _camera = Camera.main;
        RuntimeManager.PlayOneShotAttached(winnerScene, _camera.gameObject);

    }

    public void GetLooser()
    {

        if (_camera == null) _camera = Camera.main;
        RuntimeManager.PlayOneShotAttached(looserScene, _camera.gameObject);

    }

    public void GetStarOn(int index)
    {

        if (_camera == null) _camera = Camera.main;
        switch (index)
        {

            case 1:
                RuntimeManager.PlayOneShotAttached(starOnOne, _camera.gameObject);
                break;

            case 2:
                RuntimeManager.PlayOneShotAttached(starOnTwo, _camera.gameObject);
                break;

            case 3:
                RuntimeManager.PlayOneShotAttached(starOnThree, _camera.gameObject);
                break;

        }
        
    }

    public void GetStarLose()
    {

        if (_camera == null) _camera = Camera.main;
        RuntimeManager.PlayOneShotAttached(starLoose, _camera.gameObject);

    }

    public void GetStarLose(int index)
    {

        if (_camera == null) _camera = Camera.main;
        
        switch (index)
        {

            case 0:
                clickEv = RuntimeManager.CreateInstance(starLoose);
                clickEv.setParameterValue("LoseStar", 0.0f);
                clickEv.start();
                if (tempGO != null) RuntimeManager.AttachInstanceToGameObject(clickEv, tempGO.transform, tempRB);
                break;

            case 1:
                clickEv = RuntimeManager.CreateInstance(starLoose);
                clickEv.setParameterValue("LoseStar", 1.0f);
                clickEv.start();
                if (tempGO != null) RuntimeManager.AttachInstanceToGameObject(clickEv, tempGO.transform, tempRB);
                break;

            case 2:
                clickEv = RuntimeManager.CreateInstance(starLoose);
                clickEv.setParameterValue("LoseStar", 2.0f);
                clickEv.start();
                if (tempGO != null) RuntimeManager.AttachInstanceToGameObject(clickEv, tempGO.transform, tempRB);
                break;

        }

    }

    public void GetWinnerMenuDown()
    {
        /*
        stopEv = RuntimeManager.CreateInstance(stopString);
        stopEv.start();*/
        if (_camera == null) _camera = Camera.main;
        RuntimeManager.PlayOneShotAttached(onUpMovePanel, _camera.gameObject);

    }

    public void GetAllMenuDown()
    {
        /*
        stopEv = RuntimeManager.CreateInstance(stopString);
        stopEv.start();*/
        if (_camera == null) _camera = Camera.main;
        RuntimeManager.PlayOneShotAttached(allMovePanel, _camera.gameObject);

    }

    public void GetAnyButtonClick()
    {

        if (_camera == null) _camera = Camera.main;
        RuntimeManager.PlayOneShotAttached(clickOnButton, _camera.gameObject);

    }

    public void GetClickContinue()
    {

        if (_camera == null)_camera = Camera.main;
        RuntimeManager.PlayOneShotAttached(clickOnButtonContinue, _camera.gameObject);

    }

    public void GetClickReboot()
    {

        if (_camera == null) _camera = Camera.main;
        RuntimeManager.PlayOneShotAttached(clickOnButtonReboot, _camera.gameObject);

    }

    public void GetClickMenu()
    {

        if (_camera == null) _camera = Camera.main;
        RuntimeManager.PlayOneShotAttached(clickOnButtonEscMenu, _camera.gameObject);

    }
    //---------------------------------------------------------------------------------------------------------------------------------------------------------
    //Action
    //---------------------------------------------------------------------------------------------------------------------------------------------------------
    public void GetShotSound(GameObject forFMOD)
    {
        RuntimeManager.PlayOneShotAttached(soundShot, forFMOD);
    }

    public void GetShotSound(GameObject forFMOD, float strengthMy)
    {

        clickEv = RuntimeManager.CreateInstance(soundShot);
        clickEv.setParameterValue("Strength", Mathf.Clamp(strengthMy, 0.0f, 3.0f));
        clickEv.start();
        RuntimeManager.AttachInstanceToGameObject(clickEv, forFMOD.transform, forFMOD.GetComponent<Rigidbody>());

    }

    public void GetBreakGlass(GameObject forFMOD)
    {

        RuntimeManager.PlayOneShotAttached(breakGlass, forFMOD);

    }

    public void GetBreakGlass(GameObject forFMOD, float strengthMy)
    {
        
        clickEv = RuntimeManager.CreateInstance(breakGlass);
        clickEv.setParameterValue("Strength", Mathf.Clamp(strengthMy, 0.0f, 3.0f));
        clickEv.start();
        RuntimeManager.AttachInstanceToGameObject(clickEv, forFMOD.transform, forFMOD.GetComponent<Rigidbody>());
        

        /*
        if (_camera == null) _camera = Camera.main;

        if (strengthMy >= 0 & strengthMy < 1)
        {

            RuntimeManager.PlayOneShotAttached(breakGlass01, forFMOD.gameObject);

        }
        else if (strengthMy >= 1 & strengthMy < 2)
        {

            RuntimeManager.PlayOneShotAttached(breakGlass02, forFMOD.gameObject);

        }
        else if (strengthMy >= 2 & strengthMy <= 3)
        {

            RuntimeManager.PlayOneShotAttached(breakGlass03, forFMOD.gameObject);

        }
        */
    }
    public void GetBreakGlass(float strengthMy)
    {

        if (_camera == null) _camera = Camera.main;
        clickEv = RuntimeManager.CreateInstance(breakGlass);
        clickEv.setParameterValue("Strength", Mathf.Clamp(strengthMy, 0.0f, 3.0f));
        clickEv.start();
        if (tempGO != null) RuntimeManager.AttachInstanceToGameObject(clickEv, tempGO.transform, tempRB);

    }

    public void GetSlowMotion(GameObject forFMOD)
    {

        RuntimeManager.PlayOneShotAttached(slowMotion, forFMOD);

    }

    public void GetStreakShots(int index)
    {

        if (_camera == null) _camera = Camera.main;

        switch (index)
        {

            case 1:
                RuntimeManager.PlayOneShotAttached(streakShots01, _camera.gameObject);
                break;

            case 2:
                RuntimeManager.PlayOneShotAttached(streakShots02, _camera.gameObject);
                break;

            case 3:
                RuntimeManager.PlayOneShotAttached(streakShots03, _camera.gameObject);
                break;

            case 4:
                RuntimeManager.PlayOneShotAttached(streakShots04, _camera.gameObject);
                break;

            case 5:
                RuntimeManager.PlayOneShotAttached(streakShots05, _camera.gameObject);
                break;

        }

    }
    //---------------------------------------------------------------------------------------------------------------------------------------------------------
    //Falling
    //---------------------------------------------------------------------------------------------------------------------------------------------------------
    public void GetFallBallToTable(GameObject forFMOD)
    {

        RuntimeManager.PlayOneShotAttached(knockMetallToWood, forFMOD);
        
    }
    public void GetFallBallToTable(GameObject forFMOD, int index)
    {
        
        switch (index)
        {

            case 0:
                clickEv = RuntimeManager.CreateInstance(knockMetallToWood);
                clickEv.setParameterValue("Strength", 0.0f);
                clickEv.start();
                RuntimeManager.AttachInstanceToGameObject(clickEv, forFMOD.transform, forFMOD.GetComponent<Rigidbody>());
                break;

            case 1:
                clickEv = RuntimeManager.CreateInstance(knockMetallToWood);
                clickEv.setParameterValue("Strength", 1.0f);
                clickEv.start();
                RuntimeManager.AttachInstanceToGameObject(clickEv, forFMOD.transform, forFMOD.GetComponent<Rigidbody>());
                break;

            case 2:
                clickEv = RuntimeManager.CreateInstance(knockMetallToWood);
                clickEv.setParameterValue("Strength", 2.0f);
                clickEv.start();
                RuntimeManager.AttachInstanceToGameObject(clickEv, forFMOD.transform, forFMOD.GetComponent<Rigidbody>());
                break;

        }

    }
    public void GetFallMetallToTableMore(GameObject forFMOD)
    {

        RuntimeManager.PlayOneShotAttached(knockMetallToWoodMore, forFMOD);

    }

    public void GetFallVeggieToTable(GameObject forFMOD)
    {

        RuntimeManager.PlayOneShotAttached(knockVeggie, forFMOD);

    }

    public void GetFallGlass (GameObject forFMOD)
    {

        RuntimeManager.PlayOneShotAttached(knockGlass, forFMOD);

    }
    public void GetFallGlass(GameObject forFMOD, float streng)
    {
        
        if (glassFallEv.handle.ToInt32() == 0)
        {

            glassFallEv = RuntimeManager.CreateInstance(knockGlass);
            glassFallEv.setParameterValue("Strength", streng);
            glassFallEv.start();
            RuntimeManager.AttachInstanceToGameObject(glassFallEv, forFMOD.transform, forFMOD.GetComponent<Rigidbody>());

        }
        

    }

    public void GetFallWood(GameObject forFMOD)
    {

        RuntimeManager.PlayOneShotAttached(knockWood, forFMOD);

    }

    public void GetFallWoodToWood(GameObject forFMOD)
    {

        RuntimeManager.PlayOneShotAttached(knockWoodToWood, forFMOD);

    }
    //---------------------------------------------------------------------------------------------------------------------------------------------------------
    //Pick
    //---------------------------------------------------------------------------------------------------------------------------------------------------------
    public void GetPickMetallBall(GameObject forFMOD)
    {

        RuntimeManager.PlayOneShotAttached(takeBall, forFMOD);

    }
    public void GetPickMetallBall(GameObject forFMOD, float index, Rigidbody myR)
    {

        clickEv = RuntimeManager.CreateInstance(takeBall);
        clickEv.setParameterValue("Where it", index);
        clickEv.start();
        RuntimeManager.AttachInstanceToGameObject(clickEv, forFMOD.transform, myR);

    }

    public void GetPickVeggie(GameObject forFMOD)
    {

        RuntimeManager.PlayOneShotAttached(takeVeggie, forFMOD);

    }

    public void GetPickWood(GameObject forFMOD)
    {

        RuntimeManager.PlayOneShotAttached(takeWood, forFMOD);

    }

    public void GetPickMetallOnTable(GameObject forFMOD)
    {

        RuntimeManager.PlayOneShotAttached(takeMetallTable, forFMOD);

    }











    public void SetPauseEventON()
    {

        stopEv.triggerCue();

    }
    
    public void AddMassMetallOnScene(string metallMassMy)
    {

        metallMass.Add(metallMassMy);

    }
    public void AddMassWoodOnScene(string woodMassMy)
    {

        woodMass.Add(woodMassMy);

    }
    public void AddMassVegieOnScene(string vegieMassMy)
    {

        vegieMass.Add(vegieMassMy);

    }
    public void AddMassGlassOnScene(string glassMassMy)
    {

        glassMass.Add(glassMassMy);

    }
    public void AddMassMeshGlassOnScene(string massMeshGlassMy)
    {

        massMeshGlass.Add(massMeshGlassMy);

    }
    public void SetDellItemOnMass()
    {

        metallMass.Clear();
        woodMass.Clear();
        vegieMass.Clear();
        glassMass.Clear();
        massMeshGlass.Clear();
        glassFallEv.clearHandle();

    }
    public void GetTakeItem(string name, GameObject GO)
    {

        isWeiter = true;

        if (name.Length >= 12)
        {

            if (name.Substring(0, 12) == "CustomBullet")
            {

                myRig = GO.GetComponent<Rigidbody>();

                if (isFly)
                {

                    GetPickMetallBall(GO, 1.0f, myRig);

                }
                else
                {

                    GetPickMetallBall(GO, 0.0f, myRig);

                }

                isWeiter = false;
                
            }

        }
        
        if (metallMass != null & isWeiter)
        {

            for (int i = 0; i < metallMass.Count; i++)
            {
                
                if (metallMass[i] == name)
                {

                    GetPickMetallOnTable(GO);
                    isWeiter = false;
                    break;

                }

            }

        }

        if (woodMass != null & isWeiter)
        {

            for (int i = 0; i < woodMass.Count; i++)
            {

                if (woodMass[i] == name)
                {

                    GetPickWood(GO);
                    isWeiter = false;
                    break;

                }

            }

        }

        if (vegieMass != null & isWeiter)
        {

            for (int i = 0; i < vegieMass.Count; i++)
            {

                if (vegieMass[i] == name)
                {

                    GetPickVeggie(GO);
                    isWeiter = false;
                    break;

                }

            }

        }

    }
   
    private void Table()
    {

        GameObject table = GameObject.Find("table_planks(1)");
        if (table != null) table.AddComponent<CollisionWithTable>();
        table = null;
        table = GameObject.Find("table_planks");
        if (table != null) table.AddComponent<CollisionWithTable>();
        table = null;
        table = GameObject.Find("table_planks(2)");
        if (table != null) table.AddComponent<CollisionWithTable>();
        table = null;
        table = GameObject.Find("table_planks(3)");
        if (table != null) table.AddComponent<CollisionWithTable>();
        table = null;
        table = GameObject.Find("table_planks(4)");
        if (table != null) table.AddComponent<CollisionWithTable>();
        table = null;

        table = GameObject.Find("table_planks (1)");
        if (table != null) table.AddComponent<CollisionWithTable>();
        table = null;
        table = GameObject.Find("table_planks (2)");
        if (table != null) table.AddComponent<CollisionWithTable>();
        table = null;
        table = GameObject.Find("table_planks (3)");
        if (table != null) table.AddComponent<CollisionWithTable>();
        table = null;
        table = GameObject.Find("table_planks (4)");
        if (table != null) table.AddComponent<CollisionWithTable>();
        table = null;

    }


}
