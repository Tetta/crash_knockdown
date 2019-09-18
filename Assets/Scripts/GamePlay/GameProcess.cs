using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
using cakeslice;
using UnityEngine.Analytics;
using GameAnalyticsSDK;

public class GameProcess : MonoBehaviour
{

    public static GameProcess instance;

    bool AlwaysSlowMo = false;
    private static float minTimeToShowAdsSec = 110;
    private static float minTimeToShowRewardedAdsSec = 300;

    private static float lastAdsShowedTime = -1;
    private static float lastRewardedAdsShowedTime = -1;
    public static bool isLastGameSuccessful;

    private static float lastRateUsShowedTime = -1;
    private static bool isRateUsShowed = false;

    private static float startLevelTime;

      [Header("сложность игры")]
    [SerializeField] int DifficultyGame = 0;
    [Header("Хрупкости сложностей")]
    [SerializeField] Vector3 DifficultyParams = new Vector3(0.55f,0.35f,0.2f);

    [Header("Звезды: 3/2/1: по убыванию")]
    [SerializeField] int [] Starz = new int [3]  ;
    [Header("Множитель силы броска для уровня, по Горизонтали/По вертикали ")]

    [SerializeField]  Vector2 PowerMultHand = Vector2.one;
    public static Vector2 PowerMult;
    [Header("Тайминг смены Камеры")]
  
    public static float CameraTimer;
    [Header("Разбитость/ больше - проще выиграть")]
    [Range(0.1f, 0.9f)]
    [SerializeField] float PercentToFullDestroyCount = 0.46f;
    [Header("Объект Основной Пули:")]
    [SerializeField] GameObject MainBullet;
    
 
    [Header("Стартовое количество снарядов в руке")]
    [SerializeField]   int CountOfBulletsOnStart = 5;
     [Header("Физические характеристики снарядов")]

    [SerializeField] float Mass = 0.1f;
    [SerializeField] float Drag = 0.1f;
    [SerializeField] float Size = 0.1f;

    [SerializeField] PhysicMaterial PhysMaterial;

    [Header("Последовательность Камер(основную добавлять не надо)")]
    [SerializeField] GameObject [] HandCameras;

    public static event Action<Vector2, int> EvectCrack;
    public static event Action<GameObject> EventShoot;
    public static event Action<Vector2, float> EventCatch;
    public static event Action<string, float> PlayAnimationEvent;
    public static event Action EventWin;
    public static event Action EventTake;
    public static event Action EventFadeIn;
    public static event Action EventEmptyHands;
    public static event Action EventHandsNotEmpty;
    public static int [] Stars;
    public static string FadeLevelName;
    public static GameObject[] DestroyObjects, _bullets;
    public static Vector3 HandShift;
    public static float MyTimer, delayTime, VelocityTiming, CurrentVelosity, ShotTime,AnnigilateMultiplier, PowerShotMultyplier;

    public static GameObject[] Spawns;
    public static List<GameObject>  GrabbedBullets;
    public static List<float> GrabbedBulletsSizes;
    public static GameObject CameraBulletPosition, GetBulletPositionBig;
    public static float[][] StartPositions;
    public static int CurrentDestroyObject, State;
    public static float[] DestroyObjectsChilds, DestroyobjectsFragility;
    public static int[] CrackedObjects;
    Rigidbody[] MaxVelocity;
    public static GameObject CameraPivot, Particle, BulletInFly,LastBulletInFly;
    public static ParticleSystem ParticleSys;
    public static bool Rotate, Shifting, Shooting, AlwaysSlowMotion, _debug, AntiGUI, PowerShot;
    float ShiftX, ShiftY;
    public static int   LastCrackedShot, Percentage, TotalShots, StreakShots,CurrentCamera;
    Vector2 TouchPos;
    public static float perspectiveZoomSpeed = 0.5f, HelpTime;
    public static float[] CamerasFOV;
    public static OutlineEffect OE;
    public static float ScalerUICoords,Delay, DifficultyMult;
    public static GameObject PowerShotEffect,EffectDestroy;
    public static GameObject TakeShotEffect,ColliderForShooting;
    public static Vector3 CurrentAim;
    public static  List<GameObject>  Cameras;
    public static List<List<GameObject>> CameraCheckObjects;
    public static int LastState;
    public static int lastStateToMenu = 0;
    public const int ModePlay = 1, ModePause = 0, ModeWin = 2, ModeResetLevel = -1, ModeLevelLocked = 4,ModeMainMenu=5, ModeMainMenuKitchen = 6, ModeMainMenuBar = 7, ModeMainMenuLab = 8,ModeMainMenuFactory = 9;
    public const int ModeSelectLevelKitchen = 10, ModeSelectLevelBar = 11, ModeSelectLevelLab = 12, ModeSelectLevelFactory = 13, ModeUnlockBar = 14, ModeUnlockBarYesNo = 15, ModeShopBar = 20, ModeShopLab = 21, ModeShopFactory = 22, ModeDevInfo = 23, ModeMainMenuDonates = 24, ModeSelectDonatesMenu = 25;
    public const int ModeUnlockLabYesNo = 16, ModeUnlockFactoryYesNo = 17,ModeChangeDifficulty=31;
    public static ParticleSystem.ShapeModule shape;
    public static List <Mesh>  DestroyMeshes;
    public static ParticleSystem DestroyAnim;
    public static List<FracturedObject> FracturedObjects;
    public static int Difficulty
    {
        get { return PlayerPrefs.GetInt("difficulty", 1); }
        set { PlayerPrefs.SetInt("difficulty", value); }
    }
     void EffectDestroyInit()
    {
        DestroyMeshes = new List<Mesh>();
        FracturedObjects = new List<FracturedObject>();
        for (int i = 0; i < DestroyObjects.Length; i++)
        {
            DestroyMeshes.Add(DestroyObjects[i].transform.GetChild(DestroyObjects[i].transform.childCount - 1).GetComponent<MeshFilter>().mesh);
            FracturedObjects.Add(DestroyObjects[i].GetComponent<FracturedObject>());
        }

        shape = EffectDestroy.GetComponent<ParticleSystem>().shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Mesh;
        DestroyAnim = EffectDestroy.GetComponent<ParticleSystem>();
    }
    float LoseTimer;
    // Use this for initialization
    void Awake()
    {
        startLevelTime = Time.time;
        EffectDestroy = Instantiate(Resources.Load("PowerShotEXP"))as GameObject;
        EffectDestroy.SetActive(false);
        LoseTimer = 0;
        PowerMult = PowerMultHand;
          AnnigilateMultiplier =0;
        PowerShotMultyplier = 0;
            CameraTimer = 0;
        CurrentCamera = 0;
        ScalerUICoords = 1   ;
        DifficultyChanger();
        LastCrackedShot = 0;
        HelpTime = 0;
        StreakShots = 0;
          TotalShots = 0;
        Shifting = false;
        Rotate = false;
        
        _debug = false;
        CurrentVelosity = 0;
        AlwaysSlowMotion = AlwaysSlowMo;
        Particle = GameObject.Instantiate(Resources.Load("ParticleFarfor")) as GameObject;
        Particle.transform.position = Vector3.one * 100;
        ParticleSys = Particle.GetComponent<ParticleSystem>();
        PowerShotEffect = GameObject.Instantiate(Resources.Load("PowerShotEffect")) as GameObject;
        TakeShotEffect = GameObject.Instantiate(Resources.Load("TakeShotEffect")) as GameObject; ;
        TakeShotEffect.name="TakeShotEffect" ;
        TakeShotEffect.GetComponent<ParticleSystem>().Stop();
        TakeShotEffect.transform.position = Vector3.up * -10;
        PowerShotEffect.transform.position = Vector3.up * -10;
        PowerShotEffect.name = "PowerShot";
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.01f;
        Shooting = false;
        Delay = 0;
        Rotate = false; Shifting = false;
        HandShift = -Vector3.up * 0.3f;
        CameraBulletPosition = Camera.main.transform.GetChild(0).gameObject;// get position for Taking bullets animation;
        if (CameraBulletPosition.transform.childCount == 1)
            GetBulletPositionBig = CameraBulletPosition.transform.GetChild(0).gameObject;
        else
        {
            GetBulletPositionBig = GameObject.Instantiate(CameraBulletPosition);
            GetBulletPositionBig.transform.parent = CameraBulletPosition.transform;
            GetBulletPositionBig.transform.Translate((Vector3.forward - Vector3.up) * 0.13f);
        }
      
        GrabbedBullets = new List<GameObject>();
        GrabbedBulletsSizes = new List<float>();
        delayTime = 0;
        TestMainBulletCondotions();
        Bullet.EventBulletCollided += OnFly;
        State = ModePlay;
        GameProcess.Percentage = 0;
        MyTimer = 0;
        CurrentDestroyObject = 0;
        SettingCamera();
        GetSpawns();
  
        GetBulletsOnStart();
        GetDestroyObjectsHashes();
        AddGameComponents();
        PlaceWallColliderForShooting();
  
        SettingMultipleCamerasAndVFX();
        CheckFragilityOfDestroyObjects();
        SetBulletsComponents();// настройка компонента Буллет, который Ну отвечает за индивидуальную стрельбу каждой пулей.
        //   ShowCurrentDestroyObject();
        EffectDestroyInit();
        if (PlayerPrefs.HasKey("_debug"))
        {
            if (PlayerPrefs.GetInt("_debug") == 1)
                _debug = true;
            else _debug = false;
        }
        GetAllBulletsOnScene();

        if (lastAdsShowedTime == -1)
        {
            lastAdsShowedTime = Time.time;
        }
        if (lastRewardedAdsShowedTime == -1)
        {
            lastRewardedAdsShowedTime = Time.time;
        }
        if (lastRateUsShowedTime == -1)
        {
            lastRateUsShowedTime = Time.time;
        }

        if (isLastGameSuccessful)
        {
            if (Time.time - lastRewardedAdsShowedTime >= minTimeToShowRewardedAdsSec && Time.time - lastAdsShowedTime >= minTimeToShowAdsSec)
            {
                AdsManager.ShowRewarded();
                lastAdsShowedTime = Time.time;
                lastRewardedAdsShowedTime = Time.time;
            }
            else if (Time.time - lastAdsShowedTime >= minTimeToShowAdsSec)
            {
                AdsManager.Show();
                lastAdsShowedTime = Time.time;
            }
        }

        string sceneName = SceneManager.GetActiveScene().name;
        if(sceneName.Substring(0,1)!="t")
        lastStateToMenu = 6 + int.Parse(sceneName.Substring(sceneName.Length - 2, 1));

        instance = this;
    }

    private void Start()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game");
        Debug.Log("Start Round");
    }

    private void OnDestroy()
    {
        Bullet.EventBulletCollided -= OnFly;
    }
    void OnFly() { }
    void DifficultyChanger()
    {
            DifficultyMult =0.2f;
        if ((Starz.Length == 0))
        {
            //Stars =new [] { 10, 20, 30, 40, 50};
            Stars = new[] { 10, 20, 30 };
        }
        else
            Stars = Starz;
        if (Difficulty == 0)
        {
            DifficultyMult = 0.60f;

            Stars[2] = Stars[2] + 60;
            Stars[1] = Stars[1] + 25;
            Stars[0] = Stars[0] + 12;

            CountOfBulletsOnStart += 5;
        }
        else 
             if (Difficulty == 1)
        {
            DifficultyMult = 0.38f;
            CountOfBulletsOnStart += 1;
            Stars[2] = Stars[2] + 20;
            Stars[1] = Stars[1] + 10;
            Stars[0] = Stars[0] + 6;
        }
        


    }
    void Update()
    {
        //  CheckLose();
        if (Application.isEditor) ControlGame();

        delayTime += Time.deltaTime;
        ControlBulletsPositionInHand();
        if (State == ModePlay)
        {
         
            if (Shifting) ShotTime += Time.deltaTime;
            else ShotTime = 0;

            if (TotalShots > 0)
            {
                MyTimer += Time.deltaTime;
                HelpTime += Time.deltaTime;

                LoseTimer += Time.deltaTime;
            }
           
            if (ShotTime > 0.77f) Shoot();
            ControlPercentage();
     
         
       
            ControlCameraChangeByTime();
            ChangeAllMaterials();
        
        }
        if ((State != ModePause)&&(State!=ModeWin) )
          //  чтобы была задержка между тыканьями
            TouchController();// чтобы не брались объекты после победы и во время паузы
    }

    void SetBulletsComponents()
    {
        GameObject[] GO = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i=0;i<GO.Length;i++)
        if (GO[i].GetComponent<Bullet>()==null)
        {
            GO[i].AddComponent<Bullet>();
                GO[i].layer = 11;
            }
        GO = GameObject.FindGameObjectsWithTag("BulletInHand");
        for (int i = 0; i < GO.Length; i++)
            if (GO[i].GetComponent<Bullet>() == null)
            {
                GO[i].AddComponent<Bullet>();
                GO[i].layer = 11;
            }
    }
    void ChangeAllMaterials()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {



            Renderer[] rend = FindObjectsOfType(typeof(Renderer)) as Renderer[];

            Material mat = new Material(Shader.Find("Diffuse"));
            Debug.Log(mat);
            for (int i = 0; i < rend.Length; i++)
            {
                if (rend[i].materials.Length > 1) Debug.Log("Multimat!!! : " + rend[i].gameObject.name);
            }
                //   rend[i].sharedMaterial = mat;







                /*
                            Renderer[] rend = FindObjectsOfType(typeof(Renderer)) as Renderer[];

                            Material mat = new Material(Shader.Find("Diffuse"));
                            Debug.Log(mat);
                            for (int i = 0; i < rend.Length; i++)
                            {
                                var tempMaterial = new Material(rend[i].sharedMaterial);
                                tempMaterial.shader = Shader.Find("Mobile/VertexLit");
                                rend[i].sharedMaterial = tempMaterial;
                             //   rend[i].sharedMaterial = mat;

            }
             */
            }
        }
   public static void FadeLoadLevel(string str)
    {
       
        FadeLevelName = str;
       
     
        EventFadeIn();
    }
      public static  void     CheckLose()
    {

        
   
        {
              
               
         
            if ((  GameProcess.State != GameProcess.ModeWin))
            {
                       
                MyTimer = Stars[2] + 1;
                if (SoundAndMusik.Instance.isLooserOne)
                {

                    SoundAndMusik.Instance.isLooserOne = false;
                    //SoundAndMusik.Instance.GetLooser();                    

                }
                EndGameSession();
                
            }
        }
        
    }
    public static void CheckCameraAutoChange()
    {
        int z = 0;
        for (int i = 0; i < DestroyObjects.Length; i++)
        {
            if (CrackedObjects[i] == 1)
            {
                for (int k = 0; k < CameraCheckObjects[CurrentCamera].Count; k++)
                {
                    if (CameraCheckObjects[CurrentCamera][k] == DestroyObjects[i]) z++;
                }

                //        Debug.Log(DestroyObjects[i].name);

            }

        }
        if (z == CameraCheckObjects[CurrentCamera].Count)
            if(GameProcess.State==ModePlay)
        {
            CurrentCamera++;
            if (CurrentCamera == Cameras.Count) CurrentCamera = 0;
            Camera.main.gameObject.AddComponent<ChangeCameraView>();
        }


    }
    void ControlCameraChangeByTime()
    {/*
        if (CameraChanger >2)
        {
            CameraTimer += Time.deltaTime;
            if (CameraTimer > CameraChanger)
            {
                Debug.Log("ChangeCameraByTimer");
              
                CameraTimer = 0;
            }
        }
        */
    }
    void CheckFragilityOfDestroyObjects()
    {
        DestroyobjectsFragility = new float[DestroyObjects.Length];
        for (int i = 0; i < DestroyObjects.Length; i++)
            DestroyobjectsFragility[i] = PercentToFullDestroyCount;
    }

    void SettingMultipleCamerasAndVFX()
    {
        Cameras = new List<GameObject>();
        GameObject GO = Instantiate(Camera.main.gameObject);
        GO.name = "CopyOfMainCameraForChanging";
        GO.tag = "AdditionalCamera";
        Cameras.Add(GO);
        GO.transform.position = Camera.main.transform.position;
        GO.transform.rotation = Camera.main.transform.rotation;
        GO.SetActive(false);
        while(GO.transform.childCount!=0) DestroyImmediate( GO.transform.GetChild(0).transform.gameObject);
        
        foreach (GameObject c in HandCameras)
        {if(c!=null)
            if ((c != Camera.main)&&(c.name!= "TransitionCamera")&&(c.name != "GUICamera"))
            {
                Cameras.Add(c.gameObject);
                c.gameObject.SetActive(false);
              
            }
        }

        CamerasFOV = new float[Cameras.Count];
        for (int k = 0; k < Cameras.Count; k++)
            CamerasFOV[k] = Cameras[k].GetComponent<Camera>().fieldOfView;

        CameraCheckObjects = new List<List<GameObject>>();
        for(int i = 0;i< Cameras.Count; i++)
        {
            CameraCheckObjects.Add(new List<GameObject>());
            
        }
 

      

    }
 
  


 

   
    void PlaceWallColliderForShooting()
    {
        GameObject GO = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ColliderForShooting = GO;
        float maxpos = 0;
        for (int i = 0; i < DestroyObjects.Length; i++)

            if (maxpos < DestroyObjects[i].transform.position.z) maxpos = DestroyObjects[i].transform.position.z;
        for (int i = 0; i < Spawns.Length; i++)

            if (maxpos < Spawns[i].transform.position.z) maxpos = Spawns[i].transform.position.z;



        GO.transform.position = new Vector3(0, 0, maxpos + 0.33f);
        GO.transform.localScale = new Vector3(10, 10, 0.15f);
      
        GO.GetComponent<BoxCollider>().isTrigger = true;
        GO.layer = 9;
        GO.GetComponent<MeshRenderer>().enabled = false;
        GO.transform.parent = Camera.main.transform;
        GO.name = "ColliderForShootingInInfinity";
        GO.GetComponent<BoxCollider>().isTrigger = true;
    }
   

 
    void TestMainBulletCondotions()
    {
        if (SceneManager.GetActiveScene().name == "t4") MainBullet.GetComponent<Rigidbody>().mass = Mass;
        if (MainBullet)
        {
          
            if (!MainBullet.GetComponent<Rigidbody>())
            {
                Rigidbody rb = MainBullet.AddComponent<Rigidbody>();
                rb.mass = Mass;
                rb.drag = Drag;
                if (MainBullet.GetComponent<MeshCollider>())
                    MainBullet.GetComponent<MeshCollider>().material = PhysMaterial;


            }
        }
    }
    void SettingCamera()
    {
        GameObject trans = GameObject.Find("TransitionCamera");
        if (trans != null) Destroy(trans);
        int grnd = 1 << LayerMask.NameToLayer("Default");
         int fly = 1 << LayerMask.NameToLayer("GO");
         int qfly = 1 << LayerMask.NameToLayer("Optimize_a");
         int wfly = 1 << LayerMask.NameToLayer("Optimize_b");
         int efly = 1 << LayerMask.NameToLayer("Optimize_c");
         int rfly = 1 << LayerMask.NameToLayer("Optimize_f");
         int rflB = 1 << LayerMask.NameToLayer("Bullets");
         int rflB2 = 1 << LayerMask.NameToLayer("Water");
        int mask = grnd | fly| qfly|wfly| efly| rfly | rflB | rflB2;
       Camera.main.cullingMask = mask;
     //   TouchPos = Vector2.zero;
     //   CameraPivot = Camera.main.transform.parent.gameObject;
     //   ShiftX = CameraPivot.transform.rotation.eulerAngles.y;
    }
    void AddGameComponents()
    {

        if (!GameObject.FindObjectOfType<GameGUI>())
        {
            string str = SceneManager.GetActiveScene().name;

            if ((str.Substring(0, 1) == "t")) GameObject.Instantiate(Resources.Load("GUI"));
            else
       if ((str.Substring(2, 1) == "0")||(str=="0010") )   GameObject.Instantiate(Resources.Load("GUI"));
        else
             if ((str.Substring(2, 1) == "1") || (str == "0020")) GameObject.Instantiate(Resources.Load("GUIBAR"));
            else
             if ((str.Substring(2, 1) == "2") || (str == "0030")) GameObject.Instantiate(Resources.Load("GUILab"));
            else
               
            GameObject.Instantiate(Resources.Load("GUIFACTORY"));

        }
        gameObject.AddComponent<simpleFPS>();
        gameObject.AddComponent<TimeManager>();

        gameObject.AddComponent<ScoresCounter>();
    }

    public static void NextLevel()
    {
        string str = "";

        int temp;
        bool parsed = int.TryParse(SceneManager.GetActiveScene().name, out temp);
        if (parsed && ((temp + 1).ToString().Length) == 1)
            str = "000" + ((temp + 1).ToString());
        else
        {
            int number = int.Parse( SceneManager.GetActiveScene().name.Substring(1,1));
            if (number == 4)
                str = "0001";
            else
                str = "t" + (number + 1);
        }

        Debug.Log("STR=" + str);
        Debug.Log("temp=" + temp);
        if (Application.CanStreamedLevelBeLoaded(str))

          FadeLoadLevel(str);
    }


    public static void ControlStates()
    {
        if (State == ModePause)
            Time.timeScale = 0;
        else
        if (State == ModePlay)
            Time.timeScale = 1;
        if (State == 2)
            NextLevel();
        else
             if (State == ModeResetLevel)
        {
            ReloadLevel();
        }
    }
    public static void ReloadLevel()
    {
        Time.timeScale = 0;
    //    Transition.MakeSquarePngFromOurVirtualThingy();
        Debug.Log("Restart Level");
        GameProcess.FadeLoadLevel(SceneManager.GetActiveScene().name);
    }
    void GetDestroyObjectsHashes()
    {
        DestroyObjects = new GameObject[GameObject.FindGameObjectsWithTag("ObjectToDestroy").Length];
       
            DestroyObjects = GameObject.FindGameObjectsWithTag("ObjectToDestroy");
        
        CrackedObjects = new int[DestroyObjects.Length];

        for (int i = 0; i < DestroyObjects.Length; i++)
        {
            CrackedObjects[i] = 0;
        //    if (DestroyObjects[i].transform.childCount < 4)
                      //                                          DestroyObjects[i].AddComponent<RotateIfWrongHierarhi>();
        }
        DestroyObjectsChilds = new float[DestroyObjects.Length];
        //   Debug.Log("Игровых объектов обнаружено: "+DestroyObjects.Length);
        int tempCounts = 0;
        List<Rigidbody> tempVelocity = new List<Rigidbody>();
        for (int i = 0; i < DestroyObjects.Length; i++)
        {
            tempCounts += DestroyObjects[i].transform.childCount;
            DestroyObjectsChilds[i] = DestroyObjects[i].transform.childCount;
            for (int j = 0; j < DestroyObjects[i].transform.childCount; j++)
            {
                DestroyObjects[i].transform.GetChild(j).gameObject.tag = "Oskolok";
                DestroyObjects[i].transform.GetChild(j).gameObject.layer = 10;

                if (DestroyObjects[i].transform.GetChild(j).gameObject.GetComponent<Rigidbody>())
                    tempVelocity.Add(DestroyObjects[i].transform.GetChild(j).gameObject.GetComponent<Rigidbody>());
                //  DestroyObjects[i].transform.GetChild(j).gameObject.AddComponent<VelocityInterceptor>();
            }
        }
        MaxVelocity = new Rigidbody[tempVelocity.Count];
        MaxVelocity = tempVelocity.ToArray();
    }

    void ShowCurrentDestroyObject()
    {
        for (int i = 0; i < DestroyObjects.Length; i++)
        {
            if (i == CurrentDestroyObject)
            {
                DestroyObjects[i].SetActive(true);
            }
            else
                DestroyObjects[i].SetActive(false);
        }
    }
    public static void ShowChangeDifficulty()
    {
        string str = SceneManager.GetActiveScene().name;
        if((str=="0001")||( str == "0002") || (str == "0003") || (str == "0004") || (str == "0005") || (str == "0006") || (str == "0007") || (str == "0008") || (str == "0009") || (str == "0010"))
        if (isLastGameSuccessful == false)
                if(GameProcess.Difficulty>0)
        {
          


            if (PlayerPrefs.GetInt("DontShowQualityChange") == 1)
            {
                Debug.Log("Dont Show Me This SHit!!!");

            }
            else
            {


                Debug.Log("Game Failed!");
                if (PlayerPrefs.HasKey("LoseCounter" + SceneManager.GetActiveScene().name))
                {
                    int counter = 0;
                    counter = PlayerPrefs.GetInt("LoseCounter" + SceneManager.GetActiveScene().name);
                    if (counter == 3)
                    {
                        Debug.Log("SetToZero!");
                        PlayerPrefs.SetInt("LoseCounter" + SceneManager.GetActiveScene().name, -1);
                        GameProcess.State = GameProcess.ModeChangeDifficulty;
                        return;
                    }
                    else if (counter < 3)
                    {
                        PlayerPrefs.SetInt("LoseCounter" + SceneManager.GetActiveScene().name, counter + 1);
                        Debug.Log(counter + 1);

                    }


                }
                else
                {
                    Debug.Log("FirstLoseHere");
                    PlayerPrefs.SetInt("LoseCounter" + SceneManager.GetActiveScene().name, 1);
                }
                PlayerPrefs.Save();
            }
        }
        else PlayerPrefs.SetInt("isLastGameSuccessful", 0);

    }
    public static void EndGameSession()
    {
        if(GameProcess.State!=GameProcess.ModeWin)
            if (GameProcess.State != GameProcess.ModeChangeDifficulty)
            {


            TimeManager.DoSlowmotion();

            LoseStar.SetStars();

            //--<
            //->Check next Level and Open it if Locked now
            string str = "";

            int lvl = 0;

            int.TryParse(SceneManager.GetActiveScene().name, out lvl);
            if (lvl < 10)
                str = "000" + ((lvl + 1).ToString());
            else
            if (lvl < 100)
                str = "00" + ((lvl + 1).ToString());
            else
            if (lvl < 1000)
                str = "0" + ((lvl + 1).ToString());
            if (PlayerPrefs.HasKey(str + "Stars"))
            {

            }
            else PlayerPrefs.SetInt(str + "Stars", 0);
            //   Debug.Log("Previouss" + str);


            isLastGameSuccessful = Stars[2] > MyTimer;
                ShowChangeDifficulty();


                    if (isLastGameSuccessful && !RateUsPanel.IsRateUsNever && !isRateUsShowed && (lvl != 2 && lvl % 10 == 2))
                    {
                        isRateUsShowed = true;
                        MainMenu.RateUsCanvas.SetActive(true);
                    }

                    AnalyticsEvent.Custom("EndLevel", new Dictionary<string, object>() {
                { "scene_name", SceneManager.GetActiveScene().name},
                { "isWin", isLastGameSuccessful},
                { "play_time", Time.time - startLevelTime}
            });
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "game", ScoresCounter.Scores);
                Debug.Log("End Round : " + ScoresCounter.Scores);
                //<--
                PlayerPrefs.Save();
          
        }
        if (GameProcess.ModeChangeDifficulty != GameProcess.State)
        {
            GameProcess.State = GameProcess.ModeWin;
            GameProcess.EventWin();
        }
    }
 
    void GetAllBulletsOnScene()
    {
        _bullets = GameObject.FindGameObjectsWithTag("Bullet");
        for(int i=0;i<_bullets.Length;i++)
            _bullets[i].layer = 11;
    }
    void ControlVelocity()
    {
        if (GameProcess.State == ModePlay)
            if (!TimeManager.SlowMo)
            {/*
                float max = -1,temp;int maxgad=0;
            CurrentVelosity = 0;
                for (int i = 0; i < MaxVelocity.Length; i++)
                {
                   temp= MaxVelocity[i].velocity.sqrMagnitude;
                    if (max < temp) { max = temp;maxgad = i; }
                    CurrentVelosity += temp;
                }
            if (Game.CurrentVelosity > 100)
            {
                Debug.Log("Max Velocity: " + Game.CurrentVelosity);
                TimeManager.DoSlowmotion();
                Game.CurrentVelosity = 0;
                    Debug.Log("gadina = " + max+"  suchka:"+MaxVelocity[maxgad]);
            }
        }
        */
                CurrentVelosity = 0;
                for (int i = 0; i < MaxVelocity.Length; i++)
                    if (MaxVelocity[i] != null)
                    {

                        float temp = MaxVelocity[i].velocity.sqrMagnitude;
                        if (temp > 0.1f)
                        {
                            CurrentVelosity++;
                            MaxVelocity[i] = null;
                        }
                    }
                if (CurrentVelosity > 9) TimeManager.DoSlowmotion();
            }
    }
   
    void ControlPercentage()
    {
        if (State == ModePlay)
        {
          
            float OnePercentage = 0; float maxpercentbyone = 100.0f / (float)DestroyObjects.Length; float curpercent = 0;
            for (int i = 0; i < DestroyObjects.Length; i++)
            {
                if (CrackedObjects[i] == 1) curpercent += maxpercentbyone;
                else
                {
                   
                    OnePercentage = (float)(DestroyObjects[i].transform.childCount + 1.01f) / (DestroyObjectsChilds[i] + 1.01f);
                    if (OnePercentage < (DifficultyMult)+AnnigilateMultiplier+PowerShotMultyplier)
                    {
                        curpercent += maxpercentbyone;
                        CrackedObjects[i] = 1;
                        
                            EffectDestroy.transform.position = DestroyObjects[i].transform.position;
                            EffectDestroy.transform.parent = null;
                            EffectDestroy.transform.rotation = DestroyObjects[i].transform.rotation;
                            EffectDestroy.transform.localScale = DestroyObjects[i].transform.localScale;

                        shape.mesh = DestroyMeshes[i];
                            EffectDestroy.SetActive(true);
                            DestroyAnim.Stop();
                            DestroyAnim.Play();



                        


                        Vector2 temp = Vector2.zero;
                if (BulletInFly!=null) temp = Camera.main.WorldToScreenPoint(BulletInFly.transform.position);
                        temp *= ScalerUICoords;
                        //float t =  1920/Screen.width ;
                    //     Debug.Log("Bullet In Fly : " + temp);
                     //   Debug.Log("Screen Reso : " + Screen.width+"x"+Screen.height);
                     //   Debug.Log(Camera.main.WorldToScreenPoint(DestroyObjects[i].transform.position));
              //       Destroy(DestroyObjects[i].transform.GetChild(DestroyObjects[i].transform.childCount - 1).gameObject);  Удаление объекта для хелпера которого сейчас щас нет
                        for (int id = 0; id < DestroyObjects[i].transform.childCount; id++)
                            //     DestroyObjects[i].transform.GetChild(id).gameObject.AddComponent<RandomForceOnAwake>();
                            FracturedObjects[i].Explode(DestroyObjects[i].transform.position,0.1f);
                        TimeManager.DoSlowmotion();

                        if ((LastCrackedShot == TotalShots - 1))
                        {
                            StreakShots++;

                            SoundAndMusik.Instance.GetStreakShots(StreakShots);

                        }
                        else
                        {
                            StreakShots = 0;
                        }
                        EvectCrack(Vector2.zero, (1+StreakShots)*100);
                        
                        GameGUI.ChangeTime();
                        //   DestroyExplodedObject();
                        LastCrackedShot = TotalShots;
                        MyTimer -= 0 + StreakShots;
                        if (MyTimer < 0) MyTimer = 0;
                        int summ = 0;
                        for (int f = 0; f < CrackedObjects.Length; f++)
                            if (CrackedObjects[f] == 1) summ++;
                        Percentage = CrackedObjects.Length - summ;



                        if (Percentage == 0)
                        {

                            EndGameSession();

                        }
                        else
                       CheckCameraAutoChange();

                    }
                    else curpercent += (1 - OnePercentage) * maxpercentbyone;
                }
            }
         
        }
    }
   void ControlGame()
    {
        ChangeAllMaterials();
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (_debug)
            {
                PlayerPrefs.SetInt("_debug", 0);
                _debug = false;
            }
            else { _debug = true;
                PlayerPrefs.SetInt("_debug", 1);
            }
        }
        if (Input.GetKeyDown("r"))
        {
            ReloadLevel();
        }
        if (Input.GetKeyDown("y"))
        {
            GameProcess.State = GameProcess.ModeChangeDifficulty;
            Debug.Log(".ModeChangeDifficulty");

        }
        if (Input.GetKeyDown("r"))
        {
            ReloadLevel();
        }


        if (Input.GetKeyUp("a"))
        {
            if (delayTime > 0.3f)
                if (GrabbedBullets.Count > 0)
                {
                    Rotate = false;
                    Shifting = true;
                    ShotTime = 0;
                    ShotTime = 0.0f;
                Debug.Log("Hand PowerShot");
                Shoot();
            }

        }
        if (Input.GetKeyUp("s"))
        {
            if (delayTime > 0.3f)
                if (GrabbedBullets.Count > 0)
                {
                    Rotate = false;
                    Shifting = true;
                   
                    ShotTime = 0.78f;
                Debug.Log("Hand LongShot");
                Shoot();
            }
        }
        if (Input.GetKeyDown("1"))
        {
            MyTimer = Stars[2] - 1;
            EndGameSession();
            
        }
        if (Input.GetKeyDown("2"))
        {
            MyTimer = Stars[1] - 1;
            EndGameSession();
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameProcess.State = 0;
        }
        if (Input.GetKeyDown("3"))
        {
            MyTimer = Stars[0] - 1;
            EndGameSession();
            
        }
        if (Input.GetKeyDown("z"))
        {
            Debug.Log("Deleted all PlayerPrefs");
            PlayerPrefs.DeleteAll();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("ChangeCamera");
            CurrentCamera++;
            if (CurrentCamera == Cameras.Count) CurrentCamera = 0;
            Camera.main.gameObject.AddComponent<ChangeCameraView>();
        }
    }// Update is called once per frame

    void DestroyExplodedObject()
    {
        GameObject [] objs = GameObject.FindGameObjectsWithTag("Oskolok") ;
        int z = 0;
        for (int i = 0; i < objs.Length; i++)

        {
            if (z < GameGUI.VFXHidePool.Length)
            {
                if (objs[i].transform.parent == null)
                {

                    GameGUI.VFXHidePool[z].transform.position = objs[i].transform.position;
                    GameGUI.VFXHidePool[z].transform.parent = null;
                    GameGUI.VFXHidePool[z].transform.rotation = objs[i].transform.rotation;

 
                     var shape = GameGUI.VFXHidePool[z].GetComponent<ParticleSystem>().shape;
                     shape.enabled = true;
                     shape.shapeType = ParticleSystemShapeType.Mesh;
                     shape.mesh = objs[z].GetComponent<MeshFilter>().mesh;
                    GameGUI.VFXHidePool[z].SetActive(true);
                       GameGUI.VFXHidePool[z].GetComponent<ParticleSystem>().Stop();
                        GameGUI.VFXHidePool[z].GetComponent<ParticleSystem>().Play();
                         
                    Rigidbody rb = objs[i].GetComponent<Rigidbody>().GetComponent<Rigidbody>();
                    rb.detectCollisions = false;
                    rb.isKinematic = true;
                    
                  //  objs[i].GetComponent<MeshCollider>().enabled= true ;


                   
                 //   GameGUI.VFXHidePool[z].GetComponent<Rigidbody>().velocity = objs[i].GetComponent<Rigidbody>().velocity;

                    z++;
                }
            }
         
        }
            }
    void GetSpawns()
    {

        Spawns = GameObject.FindGameObjectsWithTag("Bullet");
        for (int i = 0; i < Spawns.Length; i++)
        {
            if (!Spawns[i].GetComponent<Rigidbody>())
            {
                Rigidbody rb = Spawns[i].AddComponent<Rigidbody>();
                rb.mass = Mass;
                rb.drag = Drag;
                Debug.Log("Added RigidBody to Spawn");
                //    Spawns[i].GetComponent<SphereCollider>().material = PhysMaterial;
            }
        }
        //  Debug.Log("Всего Спавнов обнаружено: " + Spawns.Length);
    }
    
    void ClearBullets()
    {
        
    }
    void GetBulletsOnStart()
    {
        if (MainBullet)
        {
            for (int i = 0; i < CountOfBulletsOnStart; i++)
            {
                GameObject _bullet = GameObject.Instantiate(MainBullet);
                Rigidbody rb;
                if (!_bullet.GetComponent<Rigidbody>()) rb = _bullet.AddComponent<Rigidbody>();
                else rb = _bullet.GetComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.useGravity = false;
                _bullet.transform.position = CameraBulletPosition.transform.position + HandShift;
                _bullet.tag = "BulletInHand";
                _bullet.name = "CustomBullet_" + i.ToString();
                GrabbedBullets.Add(_bullet);
                _bullet.layer = 11;
                GrabbedBulletsSizes.Add(_bullet.GetComponent<MeshRenderer>().bounds.max.magnitude);
             //   Debug.Log("Size Of object = " + GrabbedBulletsSizes[GrabbedBulletsSizes.Count - 1]);
            }
        }
        else
            for (int i = 0; i < CountOfBulletsOnStart; i++)
            {
                GameObject _bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Rigidbody rb = _bullet.AddComponent<Rigidbody>();
                rb.mass = Mass;
                rb.drag = Drag;
                rb.isKinematic = true;
                rb.useGravity = false;
                _bullet.transform.localScale = Vector3.one * Size;
                _bullet.transform.position = CameraBulletPosition.transform.position + HandShift;
                _bullet.GetComponent<SphereCollider>().material = PhysMaterial;
                _bullet.tag = "BulletInHand";
                _bullet.name = "AutoCreatedBullet_" + i.ToString();
                _bullet.layer = 11;

                GrabbedBullets.Add(_bullet);
                GrabbedBulletsSizes.Add(_bullet.GetComponent<MeshRenderer>().bounds.max.magnitude);
            }

    }
  public static  void ChangeBulletParamsHanded(GameObject _bullet)
    {

        Rigidbody rb = _bullet.GetComponent<Rigidbody>();

        rb.isKinematic = true;
        rb.useGravity = false;
        _bullet.tag = "BulletInHand";
    }
 public static   void ChangeBulletParamsShooted(GameObject _bullet)
    {

        Rigidbody rb = _bullet.GetComponent<Rigidbody>();
        _bullet.tag = "Untagged";
        rb.isKinematic = false;
        rb.useGravity = true;

    }
/*
    void ZoomCamera()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // Otherwise change the field of view based on the change in distance between the touches.
            Camera.main.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

            // Clamp the field of view to make sure it's between 0 and 180.
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 0.1f, 179.9f);

        }
        else

        { Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, Camera.main.fieldOfView - Input.GetAxis("Mouse ScrollWheel") * perspectiveZoomSpeed * 50, Time.deltaTime);
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 24, 100);
        }
    }
*/

    public static void TakeBulletInFly( )
    {
        if (GrabbedBullets.Count == 0) EventHandsNotEmpty();
        TakeShotEffect.transform.position = BulletInFly.transform.gameObject.transform.position;
        TakeShotEffect.transform.parent = null;
        TakeShotEffect.transform.rotation = BulletInFly.transform.gameObject.transform.rotation;
        TakeShotEffect.GetComponent<ParticleSystem>().Stop();
        var shape = TakeShotEffect.GetComponent<ParticleSystem>().shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Mesh;
        shape.mesh = BulletInFly.transform.gameObject.GetComponent<MeshFilter>().mesh;
        TakeShotEffect.GetComponent<ParticleSystem>().Play();
        EventTake();
        //   TakeShotEffect.AddComponent<ComeToCamera>();

        Debug.Log("Take Bullet in Fly!");
       
        ChangeBulletParamsHanded(BulletInFly.transform.gameObject);
        BulletInFly.transform.gameObject.transform.position = CameraBulletPosition.transform.position + HandShift;
        GrabbedBullets.Add(BulletInFly.transform.gameObject); ;
        //   AudioManager.PlaySound("Take", 0);
        SoundAndMusik.Instance.GetTakeItem(BulletInFly.transform.gameObject.name, BulletInFly.transform.gameObject);
    
        GrabbedBulletsSizes.Add(BulletInFly.transform.gameObject.GetComponent<MeshRenderer>().bounds.max.magnitude);
        GameGUI.ChangeBulletsCounter();
        if (TimeManager.SlowMo)
        {
            EventCatch(Input.mousePosition * ScalerUICoords, 50);

        }
        
        delayTime = 0;
    }
    public static  void TakeBullet(RaycastHit hit) {
        if (GrabbedBullets.Count == 0) EventHandsNotEmpty();

        TakeShotEffect.transform.position = hit.transform.gameObject.transform.position;
        TakeShotEffect.transform.parent = null;
        TakeShotEffect.transform.rotation = hit.transform.gameObject.transform.rotation;
        TakeShotEffect.GetComponent<ParticleSystem>().Stop();
        var shape = TakeShotEffect.GetComponent<ParticleSystem>().shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Mesh;
        shape.mesh = hit.transform.gameObject.GetComponent<MeshFilter>().mesh;
        TakeShotEffect.GetComponent<ParticleSystem>().Play();

        //   TakeShotEffect.AddComponent<ComeToCamera>();

        Debug.Log("Take Bullet!");
        EventTake();
        ChangeBulletParamsHanded(hit.transform.gameObject);
        hit.transform.gameObject.transform.position = CameraBulletPosition.transform.position + HandShift;
        GrabbedBullets.Add(hit.transform.gameObject); ;
        //   AudioManager.PlaySound("Take", 0);
        SoundAndMusik.Instance.GetTakeItem(hit.transform.gameObject.name, hit.transform.gameObject);

        GrabbedBulletsSizes.Add(hit.transform.gameObject.GetComponent<MeshRenderer>().bounds.max.magnitude);
        hit.transform.gameObject.transform.parent = null;
        GameGUI.ChangeBulletsCounter();
        if (TimeManager.SlowMo)
        {
            EventCatch(Input.mousePosition * ScalerUICoords, 50);

        }
        delayTime = 0;
    }
    void TouchController()
    {

        //   if (Shifting) GrabbedBullets[GrabbedBullets.Count].transform.Translate(transform.forward * 100*Time.deltaTime);
        
      


            //   ZoomCamera();
          //CameraPivot.transform.position = Vector3.Lerp(CameraPivot.transform.position, new Vector3(CameraPivot.transform.position.x - ShiftX, CameraPivot.transform.position.y - ShiftY, 0), 10 * Time.deltaTime);
         //   CameraPivot.transform.rotation = Quaternion.Slerp(CameraPivot.transform.rotation, Quaternion.Euler(0, (CameraPivot.transform.position.x) * -10 * CameraControl.z, 0), 101 * Time.deltaTime);
         //   if (CameraPivot.transform.position.x < (-CameraControl.x)) CameraPivot.transform.position = new Vector3(-CameraControl.x, CameraPivot.transform.position.y, CameraPivot.transform.position.z);
        //    if (CameraPivot.transform.position.x > (CameraControl.x)) CameraPivot.transform.position = new Vector3(CameraControl.x, CameraPivot.transform.position.y, CameraPivot.transform.position.z);
        //    if (CameraPivot.transform.position.y < (-CameraControl.y)) CameraPivot.transform.position = new Vector3(CameraPivot.transform.position.x, -CameraControl.y, CameraPivot.transform.position.z);
        //    if (CameraPivot.transform.position.y > (CameraControl.y)) CameraPivot.transform.position = new Vector3(CameraPivot.transform.position.x, CameraControl.y, CameraPivot.transform.position.z);
            //    CameraPivot.transform.position = Vector3.Lerp(CameraPivot.transform.position, new Vector3(CameraPivot.transform.position.x, CameraPivot.transform.position.y, 0), 0.33f * Time.deltaTime);
            if ((Input.touchCount == 2) || (Input.GetMouseButton(1)))
            {
                float temp1 = ((Input.mousePosition.x / Screen.width) - TouchPos.x / Screen.width);
                float temp2 = ((Input.mousePosition.y / Screen.height) - TouchPos.y / Screen.height);
                CameraBulletPosition.transform.localPosition = new Vector3(0, CameraBulletPosition.transform.localPosition.y, CameraBulletPosition.transform.localPosition.z);
                GrabbedBullets[GrabbedBullets.Count - 1].transform.rotation = Quaternion.Slerp(GrabbedBullets[GrabbedBullets.Count - 1].transform.rotation, Quaternion.Euler(temp2 * 100, -temp1 * 100, 0), Time.deltaTime * 10);
            }

            if (Rotate)

            {
                float tempX = TouchPos.x / Screen.width - Input.mousePosition.x / Screen.width;
                float tempY = ShiftY = TouchPos.y / Screen.height - Input.mousePosition.y / Screen.height;

                //   if ((tempX * tempX * 1000 > 5)||(tempY * tempY * 1000 > 5))
                {
                //    ShiftY = tempY * CameraShift.x * InvertCamera.y;
                 //   ShiftX = tempX * CameraShift.x * InvertCamera.x;
                    //     ShiftX = Mathf.Clamp(ShiftX, -CameraControl.x, CameraControl.x);
                    //     ShiftY = Mathf.Clamp(ShiftY, -CameraControl.y, CameraControl.y);
                }
            }

            if (Input.GetMouseButtonDown(0))
        {

            int rflB = 1 << LayerMask.NameToLayer("Bullets");



        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        if (Physics.Raycast(Camera.main.transform.position, ray.direction, out hit, Mathf.Infinity, rflB))
        
            {
          //      Debug.Log(hit.collider.name);

                float d = 0;
                if ((BulletInFly != null) )// &&(ShotTime>0.05f))
                {
                    Vector2 v = Camera.main.WorldToScreenPoint(BulletInFly.transform.position);
                    Vector2 h = Camera.main.WorldToScreenPoint(hit.point);
                     d = Vector2.Distance(v, h)/Screen.height;
               
                if (d < 0.19f)
                        if(Input.mousePosition.y/Screen.height>0.28f)
                    
                {
                    Debug.Log("Vector: " + d);
                        TakeBulletInFly();
                            BulletInFly = null;
                            Rotate = false;
                            Shifting = false;
                            ShotTime = 0;
                        }
                }
                
                 
                if (hit.transform != null)
                    if (hit.transform.tag == "Bullet")
                    {
                        TakeBullet(hit);
                    }
                    else
                         if (hit.transform.tag == "BulletInHand")
                    {
                        TouchPos = Input.mousePosition;
                        Rotate = false;
                        Shifting = true;
                        ShotTime = 0;
                     
                    }
                    else
                    {

                        Rotate = true;
                        Shifting = false;

                        TouchPos = Input.mousePosition;
                    }

            }
        }
        else
            if (Input.GetMouseButtonUp(0))
            {



            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            
            if (Physics.Raycast(Camera.main.transform.position, ray.direction, out hit, Mathf.Infinity ))
            
                {


                    if (Rotate)
                        Rotate = false;
                    else
                    
                    if (Shifting)
                    {
                        if (hit.collider.tag == "BulletInHand")
                        {
                            Shifting = false;
                            delayTime = 0;
                        }
                        else
                               if (GrabbedBullets.Count > 0) if (delayTime > 0.15f)
                            {
                                Shoot();

                            }
                            else Shifting = false;

                    }
                }
            }
            //  if (GrabbedBullets.Count > 0) if (delayTime < 0) // shoot
            //         Shoot(hit);






            //  TouchPos = Input.mousePosition;

       
    }
    void Shoot()
    {
        int grnd = 1 << LayerMask.NameToLayer("Default");
        int fly = 1 << LayerMask.NameToLayer("GO");
        int qfly = 1 << LayerMask.NameToLayer("Optimize_a");
        int wfly = 1 << LayerMask.NameToLayer("Optimize_b");
        int efly = 1 << LayerMask.NameToLayer("Optimize_c");
        int rfly = 1 << LayerMask.NameToLayer("Optimize_f");
        int rflB = 1 << LayerMask.NameToLayer("Bullets");
        int rflBz = 1 << LayerMask.NameToLayer("Invisible");
        int mask = grnd | fly | qfly | wfly | efly | rfly | rflB| rflBz;
        Shifting = false;
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        if (Physics.Raycast(Camera.main.transform.position, ray.direction, out hit,mask,mask))
        {
            CurrentAim = hit.point;
            TotalShots += 1;
            
            GameObject _bullet = GrabbedBullets[GrabbedBullets.Count - 1];
            BulletInFly = _bullet;
            
         //   if (GrabbedBullets[GrabbedBullets.Count - 1].transform.parent != null)
           //     _bullet.transform.localScale = GrabbedBullets[GrabbedBullets.Count - 1].transform.parent.transform.localScale;
            Rigidbody rb = _bullet.GetComponent<Rigidbody>();
            GrabbedBullets.Remove(GrabbedBullets[GrabbedBullets.Count - 1]);
            GrabbedBulletsSizes.Remove(GrabbedBulletsSizes[GrabbedBulletsSizes.Count - 1]);

            // _bullet.transform.position = Camera.main.transform.position;
            rb.isKinematic = false;
            rb.useGravity = true;
            _bullet.tag = "Bullet";
       
            float tempDist = (1- (Screen.height - Input.mousePosition.y) / Screen.height);
            ShotTime -= (tempDist * 0.01f);
            if (ShotTime < 0.12f)
            {
                PowerShotEffect.transform.position = _bullet.transform.position;
                PowerShotEffect.transform.parent = _bullet.transform;
                PowerShotEffect.transform.localScale = _bullet.transform.localScale;

                var shap  =     PowerShotEffect.GetComponent<ParticleSystem>();
                shap.Stop();
                shap.Play();
             _bullet.GetComponent<MeshCollider>().skinWidth= 0.02f;
                SoundAndMusik.Instance.GetShotSound(rb.gameObject, ShotTime);
                
                PowerShot = true;

            }
            else
            {
          
                SoundAndMusik.Instance.GetShotSound(rb.gameObject, Time.timeScale);
            }

            SoundAndMusik.Instance.isFly = true;
             _bullet.AddComponent<AutoAim>();
            EventShoot(_bullet);
 

        //    if (_bullet.GetComponent<ComeToCamera>()) Destroy(_bullet.GetComponent<ComeToCamera>());
            CurrentVelosity = 0;
            // Debug.Log("Shoot! Grabbed object!");
            delayTime = 0;
            TimeManager.delay = 2;
          //  GameGUI.ChangeBulletsCounter();
            HelpTime = 0;
            if (GrabbedBullets.Count == 0) EventEmptyHands();
        }
    }

    void ControlBulletsPositionInHand()
    {

        
        for (int i = 0; i < GrabbedBullets.Count; i++)
        {
            if (i == GrabbedBullets.Count - 1)
            {
                Vector3 Noize=Vector3.zero;
                if (ShotTime > 0.2f)
                {
                    if(ShotTime<1)
                    Noize = UnityEngine.Random.insideUnitSphere * 0.002f* ShotTime;
                    else
                        Noize = UnityEngine.Random.insideUnitSphere * 0.002f;

                }

                Vector3 ShiftPos = Vector3.zero;
                if (GrabbedBullets[i].transform.childCount>0) ShiftPos = GrabbedBullets[i].transform.GetChild(0).transform.localPosition;
                GrabbedBullets[i].transform.position = Noize+ Vector3.Lerp(GrabbedBullets[i].transform.position, CameraBulletPosition.transform.position+ CameraBulletPosition.transform.forward*ShiftPos.z+ CameraBulletPosition.transform.up*ShiftPos.y, delayTime);
            }else 
              //  GrabbedBullets[i].transform.position = CameraBulletPosition.transform.position + HandShift;
                 GrabbedBullets[i].transform.position = Vector3.Lerp(GrabbedBullets[i].transform.position, CameraBulletPosition.transform.position+HandShift, delayTime);
        }
    }
    private void OnGUI()
    {
       //  GUI.Box(new Rect(0, 470, 400, 25), "RotateChange   " + RotateChange);
     //    GUI.Box(new Rect(0, 470, 400, 25), "MaxVelocity   " + CurrentVelosity);

    }


}

