using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using UnityEngine.Analytics;

public class GameGUI : MonoBehaviour
{   
    [SerializeField] private GameObject Cracked;
    [Header ("0-Pause; 1-Play; 2-Win;3-KitchenMenu")]
    [SerializeField] GameObject[] GuiStates;
 
    public static GameObject[] GameStates;
 
    public static Text RecordText;
    public static Text WinTimeText;
    public static Text percentage;
     //public static Text BulletsCounterObject;
  
    public static float TransitionTime = 0;
   public static int CurrentCrackSplash;
    public static GameObject CrackedObj;
    public static GameObject[] VFXHidePool;
    public static Text [] CrackedPoolTexts;
    public static Text  StarTimerText, CrackedPoolText,CatchedText;
    public static int LastState;
    public static RectTransform Aim;
    public static Animation CrackedAnim, CatchedSplasAnim;
   // GameObject _Transition;
   //  [SerializeField] Vector3 _TransitionVector = Vector3.one;
    public static int timer, currenttime,currenpercent;

    // Use this for initialization
    public static RectTransform CatchedSplashRect, CrackedSplashRect;
    public static event Action<int> GoToStartPos;
    public static event Action<int> GoToEndPos;
   // public static event Action<Vector2> EventStarsShifting;
 
    public static event Action EventOnceASec;

    private static float startMenuTime = -1;
   // public static event Action <int,int> EventUnlockTheme;
 

  //  private bool isLooserMy = true;

    private void OnDestroy()
    {
        GameProcess.EvectCrack -= CrackSplash;
        GameProcess.EventCatch -= CatchedSplash;
        GameProcess.EventTake -= OnTake;
    }
    void OnTake()
    {
    }
    
    void Awake()
    {
        GameProcess.EvectCrack += CrackSplash;
        GameProcess.EventCatch += CatchedSplash;
        GameProcess.EventTake += OnTake ;

        if (startMenuTime == -1)
            startMenuTime = Time.time;


        if (GameObject.Find("Aim"))
        {
            Aim = GameObject.Find("Aim").GetComponent<RectTransform>();
            GameGUI.Aim.anchoredPosition = Vector2.one * -200;
        }
        GameProcess.ScalerUICoords =1/ GetComponent<RectTransform>().localScale.x;
    
        RecordText = GameObject.Find("RecordText").GetComponent<Text>();
        WinTimeText = GameObject.Find("TimeText").GetComponent<Text>();
        if(GameObject.Find("Percentage"))
        percentage= GameObject.Find("Percentage").GetComponent<Text>();
            //BulletsCounterObject= GameObject.Find("CounterText").GetComponent<Text>();
        StarTimerText = GameObject.Find("StarTimerText").GetComponent<Text>();
        if(GameObject.Find("Catched!"))
        CatchedSplashRect = GameObject.Find("Catched!").GetComponent<RectTransform>();
        CurrentCrackSplash = 0;

        TransitionTime = 0;
      //  _Transition = GameObject.Find("TransitionWall");
        LastState = -1;
     
        GameStates = GuiStates;

        
        SetTransitionPosition();
        ChangeBulletsCounter();
        currenpercent = GameProcess.Percentage;
        ChangePercentage();
        timer = 0;
        ChangeBulletsCounter();
        LastState = GameProcess.State;
     //   CreateHideVFXPool();
        CreateCrackedPool();
        ChangeTime();

        Table();

    }

    private void OnApplicationQuit()
    {
        AnalyticsEvent.Custom("GameSessionTime", new Dictionary<string, object>() { { "time", Time.time - startMenuTime} });
    }

    void CreateCrackedPool()
    {
        CrackedAnim = Cracked.GetComponent<Animation>();
        CrackedAnim.Stop();
        CrackedSplashRect = Cracked.GetComponent<RectTransform>();

        CrackedSplashRect.anchoredPosition =new Vector2(-2000,-3000);
        CrackedPoolText = Cracked.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>();

        if (CatchedSplashRect != null)
        {
            CatchedText = CatchedSplashRect.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
            CatchedSplasAnim = CatchedText.gameObject.transform.parent.gameObject.GetComponent<Animation>();
            CatchedSplashRect.anchoredPosition = new Vector2(-2000, -3000);
        }
        /*
        CrackedPool = new GameObject[5];
        CrackedPoolTexts = new Text[5];
        for (int i = 0; i < 5; i++)
        {
            if (i == 0) CrackedPool[i] = Cracked;
            else
                CrackedPool[i] = GameObject.Instantiate(Cracked);
            CrackedPool[i].name = "CrackedPool_" + i.ToString();
            CrackedPool[i].SetActive(false);
          //  CrackedPool[i].transform.parent = gameObject.transform;
            CrackedPool[i].GetComponent<RectTransform>().localPosition = Vector3.zero;
            CrackedPoolTexts[i] = CrackedPool[i].transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>();


        }
        */
    }
    void CreateHideVFXPool()
   {
        VFXHidePool = new GameObject[25];
        
        for (int i = 0; i < 5; i++)
        {
            if (i == 0)
                VFXHidePool[0] = Instantiate(Resources.Load("DestroyEffect")) as GameObject; 

            else
            VFXHidePool[i] = Instantiate(VFXHidePool[0]) ;
            VFXHidePool[i].name = "VFXHidePool_" + i.ToString();
            VFXHidePool[i].SetActive(false);
            VFXHidePool[i].transform.parent = null;
            

        }
    }


    public static void CatchedSplash(Vector2 v, float Streak)
    {
        Debug.Log("Catched!");
        CatchedSplashRect.anchoredPosition = v;
       CatchedText.text="+"+ Streak.ToString();
        CatchedSplasAnim.Stop("Cracked");
        CatchedSplasAnim.Play("Cracked");
    }
 public static  void CrackSplash(Vector2 v,int Streak)
    {
        //   if (CurrentCrackSplash > 4) CurrentCrackSplash = 0;


        /*   if (Game.ModeWin == Game.State)
           {

               CrackedPool[CurrentCrackSplash].GetComponent<RectTransform>().pivot = new Vector2(0.5f, 6.5f);
               CrackedPool[CurrentCrackSplash].GetComponent<Animation>().Stop("CrackedSlow");
               CrackedPool[CurrentCrackSplash].GetComponent<Animation>().Play("CrackedSlow");
               //   CrackedPool[CurrentCrackSplash].GetComponent<Animation>().Stop("CrackedSlow");
               //     CrackedPool[CurrentCrackSplash].GetComponent<Animation>().Play("CrackedSlow");
           }
           else*/

        CrackedSplashRect.anchoredPosition =new Vector2(1080,3360);
     
        CrackedAnim.Stop("Cracked");
            CrackedAnim.Play("Cracked");

         
        //   CrackedPool[CurrentCrackSplash].GetComponent<RectTransform>().anchoredPosition=new Vector2(Screen.width,Screen.height)-v;
        //   CrackedPool[CurrentCrackSplash].AddComponent<MoveToScreenCenterAfterAwake>();
        // CrackedPool[CurrentCrackSplash].GetComponent<RectTransform>().localPosition= Vector3.zero;
       
     

        CrackedPoolText.text ="+"+ ((Streak)).ToString()+"!";
       

        SoundAndMusik.Instance.GetBreakGlass(3.0f);

  
    }

    [ContextMenu("All GUI on own Places!")]
    private void AllGUIOnOwnPlaces()
    {
        StartEndPositions[] startEndPositions = GameObject.FindObjectsOfType<StartEndPositions>();
        foreach (var startEndPosition in startEndPositions)
        {
            startEndPosition.rect = startEndPosition.GetComponent<RectTransform>();
            startEndPosition.OnAwakePosition();
        }
    }
    
    public static void ControlPanel(int id, bool GoStart)
    {
        if (GoStart)
        {
            GoToStartPos(id);
        }
        else
        {
            GoToEndPos(id);
        }
        if (id == -1)
        {
            for (int i=0;i<14;i++)
                if (GoStart)
                {
                    GoToStartPos(i);
                }
                else
                {
                    GoToEndPos(i);
                }
        }
    }
    public static void UpdateStates()
    {
        if (LastState != GameProcess.State)
        {
            if (GameProcess.State == GameProcess.ModePause)
            {
                ControlPanel(-1, false);
              

                ControlPanel(GameProcess.ModePause, true);

                SoundAndMusik.Instance.GetAllMenuDown();
            }

            if (GameProcess.State == GameProcess.ModePlay)
            {
                ControlPanel(-1, false);
                ControlPanel(GameProcess.ModePlay, true);

          
                SoundAndMusik.Instance.GetAllMenuDown();

            }
            if (GameProcess.State == GameProcess.ModeChangeDifficulty)
            {
                ControlPanel(-1, false);
                ControlPanel(GameProcess.ModeChangeDifficulty, true);


                SoundAndMusik.Instance.GetAllMenuDown();

            }
            // --> if Win Achieved
            if (GameProcess.State == GameProcess.ModeWin)
            {
                ControlPanel(-1, false);
               
                ControlPanel(2, true);
                WinTimeText.text = Mathf.RoundToInt(GameProcess.MyTimer).ToString();
                WinTimeText.gameObject.AddComponent<GrowNumbers>();
                RecordText.text = Mathf.RoundToInt(PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name)).ToString();
       
                SoundAndMusik.Instance.GetWinnerMenuDown();

            }
      
            if (GameProcess.State == GameProcess.ModeMainMenu)
            {

                ControlPanel(-1, false);
                ControlPanel(GameProcess.ModeMainMenu, true);

            }
            if (GameProcess.State == GameProcess.ModeMainMenuKitchen)
            {
                ControlPanel(-1, false);
                ControlPanel(GameProcess.ModeMainMenuKitchen, true);
            }
            if (GameProcess.State == GameProcess.ModeMainMenuBar)
            {
                ControlPanel(-1, false);
                ControlPanel(GameProcess.ModeMainMenuBar, true);
            }
            if (GameProcess.State == GameProcess.ModeMainMenuLab)
            {
                ControlPanel(-1, false);
                ControlPanel(GameProcess.ModeMainMenuLab, true);
            }
            if (GameProcess.State == GameProcess.ModeMainMenuFactory)
            {
                ControlPanel(-1, false);
                ControlPanel(GameProcess.ModeMainMenuFactory, true);
            }
            if (GameProcess.State == GameProcess.ModeSelectLevelKitchen)
            {
          
                ControlPanel(-1, false);
                ControlPanel(GameProcess.ModeSelectLevelKitchen, true);
                SoundAndMusik.Instance.GetAllMenuDown();

            }
            if (GameProcess.State == GameProcess.ModeSelectLevelBar)
            {
                ControlPanel(-1, false);
                ControlPanel(GameProcess.ModeSelectLevelBar, true);
           
                SoundAndMusik.Instance.GetAllMenuDown();

            }
            if (GameProcess.State == GameProcess.ModeSelectLevelLab)
            {
                ControlPanel(-1, false);
                ControlPanel(GameProcess.ModeSelectLevelLab, true);
             
                SoundAndMusik.Instance.GetAllMenuDown();

            }
            if (GameProcess.State == GameProcess.ModeSelectLevelFactory)
            {
                ControlPanel(-1, false);
                ControlPanel(GameProcess.ModeSelectLevelFactory, true);
             
                SoundAndMusik.Instance.GetAllMenuDown();

            }
        //    Debug.Log("States Changed: " + Game.State);
        }
        TransitionTime = 0;
        LastState = GameProcess.State;
    }
    public static void ChangeBulletsCounter()
    {
        //if(Game.State==Game.ModePlay)
           //BulletsCounterObject.text  = Game.GrabbedBullets.Count.ToString();
    
    }
     
    void SetTransitionPosition()
    {
 
        // _TransitionVector = new Vector3(Camera.main.aspect,1,1);
    //    Matrix4x4 Proj = Camera.main.projectionMatrix;
    //    float X = 0.18f / Proj[0, 0] * Camera.main.nearClipPlane;
    //    float Y = 0.18f / Proj[1, 1] * Camera.main.nearClipPlane;
    //    _Transition.transform.localScale = new Vector3(X, 0, Y);
      //  _Transition.transform.position = new Vector3(0, 0, Camera.main.transform.position.z + Camera.main.nearClipPlane + 0.00001f);
        //(Screen.width / 2) * (1.0f/Mathf.Tan(Camera.main.fieldOfView / 2.0f))

    } 
    public static void ChangeTime()
    {
        timer = Mathf.RoundToInt(GameProcess.MyTimer);
        if (timer != currenttime & GameProcess.State != GameProcess.ModeWin)
        {
            if (GameProcess.Stars[2] < GameProcess.MyTimer)
            {

                if (SoundAndMusik.Instance.isLooserOne)
                {

                    SoundAndMusik.Instance.isLooserOne = false;
                    //SoundAndMusik.Instance.GetLooser();

                }

                GameProcess.EndGameSession();                

            }

            int t=0;
            int temp = Mathf.RoundToInt( GameProcess.Stars[0] - timer);
            int temp2 = Mathf.RoundToInt( GameProcess.Stars[1] - timer);
            int temp3 = Mathf.RoundToInt( GameProcess.Stars[2] - timer);
            //int temp4 = Mathf.RoundToInt( Game.Stars[2] - timer);
            //int temp5 = Mathf.RoundToInt( Game.Stars[2] - timer);
            if (temp > 0)
                t = temp;
            else
                if(temp2 > 0)
                t = temp2 ;
            else    if(temp3 > 0)
                t = temp3;
            /*else    if(temp4 > 0)
                t = temp4;
            else
                if(temp5 > 0)
                t = temp5;*/
         
            EventOnceASec();
            currenttime = timer;

            StarTimerText.text = t.ToString();            
     //     if (t == 3) AudioManager.PlaySound("StarCounterHide", -3);  
         
        }

    }
    void ChangePercentage()
    {
       
        if (   GameProcess.Percentage!= currenpercent)
        {
            percentage.text = GameProcess.Percentage.ToString();
            currenpercent = GameProcess.Percentage;
        }
    }
    void ControlPanels(int panel_iD)
    {

    }
    // Update is called once per frame
    void Update()
    {
       //   Matrix4x4 Proj = Camera.main.projectionMatrix;
        //  float X = _TransitionVector.x / Proj[0, 0] * Camera.main.nearClipPlane;
        //  float Y = _TransitionVector.x / Proj[1, 1] * Camera.main.nearClipPlane;
       //   _Transition.transform.localScale = new Vector3(X, 0.01f, Y);
       //   _Transition.transform.position = new Vector3(0, 0, Camera.main.transform.position.z + Camera.main.nearClipPlane + 0.01f);
    
        UpdateStates();
        TransitionTime += Time.unscaledDeltaTime;
        ChangeTime();
        ChangePercentage();
    }

    //Метод, который вешает на стол скрипт для воспроизведения звуков падения. Нужен был скрипт, который в авейке работает в каждой комнате. ))
    private void Table()
    {

        SoundAndMusik.Instance.isLooserOne = true;

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
