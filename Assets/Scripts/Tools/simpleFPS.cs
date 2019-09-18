using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;
using System.Collections.Generic;

public class simpleFPS : MonoBehaviour
{

    //  private HxVolumetricLight hxLight;
    //   private HxVolumetricImageEffect hxImageEffect;
    // private HxVolumetricCamera hxCamera;
    public static PostProcessVolume postProcVol;
    public static PostProcessLayer postProcLayer;
    public static DepthOfField dof;

    public static LightProbeGroup lightProbeGroup;
    public static ReflectionProbe reflectProbe;
    public static AmbientOcclusion ambientOccl;

    public float updateInterval = 1F, inter = 5;
    public static Text fpstext;
    public static Text Commenttext ;

    bool isLoad;
    public static bool Intro;
    public static int MyQuality, EqualQuality;
    public static float maxWidth, maxHeight, MyCounter, LastTime;
    public static int MyFPS, QualCounter, More, shagnazad;
    
    [SerializeField]
    private Camera screenCamera;
    [SerializeField]
    private Transform logo;

    [Header("Indication")]
    [SerializeField]
    private FPSCalibrationPanel panel;

    private bool calibrationFinished = false;
    private const string FPS_CALIBRATION_QUALITY_KEY = "fps_calibration_quality";
    private int prevQuality = 0, prevPrevQuality = 0;

    private static List<int> fpss = new List<int>();

    public static float AverageFPS
    {
        get
        {
            if (fpss.Count != 0)
            {
                float sum = 0;
                foreach (var fps in fpss)
                    sum += fps;
                return sum / fpss.Count;
            }
            else
            {
                return 30;
            }
        }
    }

    public static int FPSCalibrationQuality
    {
        get { return PlayerPrefs.GetInt(FPS_CALIBRATION_QUALITY_KEY, 0); }
        set { PlayerPrefs.SetInt(FPS_CALIBRATION_QUALITY_KEY, value); }
    }

    public static bool IsDontNeedCalibration
    {
        get { return PlayerPrefs.HasKey("isDontNeedCalibration"); }
        set
        {
            if (value)
            {
                PlayerPrefs.SetInt("isDontNeedCalibration", 1);
            }
            else
                PlayerPrefs.DeleteKey("isDontNeedCalibration");
        }
    }

    void Awake()
    {
        
        Application.targetFrameRate = 60;
        shagnazad = 0;
        More = 0;
        EqualQuality = 0;
        QualCounter = 0;
     //   fpstext = GameObject.Find("FPSTEXT").gameObject.GetComponent<Text>();
        isLoad = false;
        inter = 0;
        TestResolution();
        Intro = false;
        
        if (SceneManager.GetActiveScene().name == "calibration_scene")
        {
            FPSCalibrationQuality = 0;
            Commenttext = GameObject.Find("ComText").gameObject.GetComponent<Text>();
            
            Intro = true;
            TurnAllPostEffects(false);
            MyQuality = 5;
            ChangeMyQuality(MyQuality);
            SetResolution(0.25f);
            Commenttext.text = "IntroStarted!!!";
        }
        //else if (Application.isEditor)
        //{
        //    FPSCalibrationQuality = 10;
        //}


        //    if (PlayerPrefs.HasKey("MyQuality")) MyQuality = PlayerPrefs.GetInt("MyQuality");
        // else

        if (Intro == false)
        {
            MyQuality = FPSCalibrationQuality;
            ChangeMyQuality(MyQuality);
            calibrationFinished = true;
            //    Debug.Log("Changing Quality By tests:" + MyQuality);
        }
        else
        {
            panel.Show();
        }
        LastTime = Time.realtimeSinceStartup;
        MyCounter = 0;
        MyFPS = 0;

       // fpsCadr.text = MyQuality.ToString();
    }

    [ContextMenu("ResetQualityKey")]
    public void ResetQualityKey()
    {
        FPSCalibrationQuality = 0;
    }

    void Update()
    {
        MyFPS++;

        if (LastTime + 1 < Time.realtimeSinceStartup)
        {
            More++;
         //   fpstext.text = Mathf.RoundToInt(MyFPS).ToString();

            //    Debug.Log("Qual==" + MyQuality + "__" + QualCounter + "   FPS: " + MyFPS);
            
            if (!calibrationFinished && Intro)
            {
                prevPrevQuality = prevQuality;
                prevQuality = MyQuality;
                if (MyFPS < 29.81f)
                {
                    ChangeMyQuality(--MyQuality);
                }
                else if (MyFPS > 55f)
                {
                    MyQuality += 2;
                    ChangeMyQuality(MyQuality);
                }
                else if (MyFPS > 39.9f)
                {
                    ChangeMyQuality(++MyQuality);
                }
                else if (More > 4)
                {
                    MyQuality -= 1;
                    if (MyQuality > 5 && IsBlack())
                        MyQuality = 5;
                    FPSCalibrationQuality = MyQuality;
                    ChangeMyQuality(MyQuality);
                    calibrationFinished = true;
                    panel.Hide();
                }

                if (More > 4 && MyQuality == prevPrevQuality || More > 6)
                {
                    MyQuality = Mathf.Max(prevQuality, MyQuality) - 2;
                    if (MyQuality > 5 && IsBlack())
                        MyQuality = 5;
                    ChangeMyQuality(MyQuality);
                    FPSCalibrationQuality = MyQuality;
                    calibrationFinished = true;
                    panel.Hide();
                }
            }
            //    else Game.FadeLoadLevel("0001");

            //  if (Intro)
            {
                //        if (shagnazad < MyQuality) shagnazad = MyQuality;
                //        if (shagnazad == More - 2) Game.FadeLoadLevel("0001");
            }
            if (SceneManager.GetActiveScene().name != "calibration_scene" && SceneManager.GetActiveScene().name != "mainmenu" && More % 30 == 3)
            {
                AnalyticsEvent.Custom("FPS", new Dictionary<string, object> {
                    { "scene_name", SceneManager.GetActiveScene().name},
                    { "fps", MyFPS}
                });
            }
            LastTime = Time.realtimeSinceStartup;
            fpss.Add(MyFPS);
            if (fpss.Count > 10)
                fpss.RemoveAt(0);
            MyFPS = 0;
        }
    }
    
    private bool IsBlack()
    {
        if (screenCamera == null || logo == null)
            return false;

        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        screenCamera.targetTexture = rt;
        screenCamera.Render();
        RenderTexture.active = rt;
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        texture.Apply();

        Vector3 logoPosition = screenCamera.WorldToScreenPoint(logo.position);

        Color[] colors = new Color[3];
        float grayscaleSum = 0;
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = texture.GetPixel((int)(logoPosition.x + i * 3), (int)(logoPosition.y + i * 3));
            grayscaleSum += colors[i].grayscale;
        }

        if (grayscaleSum > .5f)
            return false;
        else
            return true;
    }

    public static void SetResolution(float scale)
    {
        //   Debug.Log("Changing Resolution to *" + Mathf.RoundToInt(PlayerPrefs.GetFloat("maxresWid") * scale) + "x" + Mathf.RoundToInt(PlayerPrefs.GetFloat("maxresHeight") * scale));
        Screen.SetResolution(Mathf.RoundToInt(PlayerPrefs.GetFloat("maxresWid") * scale), Mathf.RoundToInt(PlayerPrefs.GetFloat("maxresHeight") * scale), true);
    }
    public static void TurnPostEffects()
    {
        //    Debug.Log("PostProcessing Turner clicked");
        if (FindObjectOfType<PostProcessLayer>())
        {
            GameObject G = FindObjectOfType<PostProcessLayer>().gameObject;
            PostProcessLayer PL = G.GetComponent<PostProcessLayer>();
            if (PL.enabled) PL.enabled = false;
            else PL.enabled = true;
        }
    }
    public static void ChangeResolution(int id)
    {

        if (id > 0)
        {
            if (maxWidth / Screen.width == 2) SetResolution(1);
            if (maxWidth / Screen.width == 4) SetResolution(0.5f);
            if (maxWidth / Screen.width == 8) SetResolution(0.25f);
        }
        else
        {
            if (maxWidth / Screen.width == 1) SetResolution(0.5f);
            if (maxWidth / Screen.width == 2) SetResolution(0.25f);


        }
    }
    public static void ChangeAA(int id)
    {
        int aa = QualitySettings.antiAliasing;
        if (id > 0)
        {
            QualitySettings.antiAliasing = aa + 1;
        }
        else
        {
            QualitySettings.antiAliasing = aa - 1;
        }
    }
    public static void TestResolution()
    {
        if (SceneManager.GetActiveScene().name == "calibration_scene")
        {
            maxWidth = Screen.width;
            maxHeight = Screen.height; ;
            PlayerPrefs.SetFloat("maxresWid", maxWidth);
            PlayerPrefs.SetFloat("maxresHeight", maxHeight);
        }


        float width = 0; float height = 0;
        if (PlayerPrefs.HasKey("maxresWid"))
        {
            //    Debug.Log("max resolution W = " + PlayerPrefs.GetFloat("maxresWid"));
            width = PlayerPrefs.GetFloat("maxresWid");
            if (width < Screen.currentResolution.width)
            {
                width = Screen.currentResolution.width;
                PlayerPrefs.SetFloat("maxresWid", width);
                maxWidth = width;
            }
            if (PlayerPrefs.HasKey("maxresHeight"))
            {
                height = PlayerPrefs.GetFloat("maxresHeight");
                if (height < Screen.currentResolution.width)
                {
                    height = Screen.currentResolution.width;
                    PlayerPrefs.SetFloat("maxresHeight", width);
                    maxHeight = height;
                }
            }
        }
        else
        {
            maxWidth = Screen.width;
            maxHeight = Screen.height; ;
            PlayerPrefs.SetFloat("maxresWid", maxWidth);
            PlayerPrefs.SetFloat("maxresHeight", maxHeight);

        }
        PlayerPrefs.Save();
    }
    public static void TurnAllPostEffects(bool param)
    {
        DOFProcess(param);
        SSRProcess(param);
        AOprocess(param);
        BLoomProcess(param);
        VignetteProcess(param);
        CCprocess(param);
        MotionBlurProcess(param);

    }
    public static void ChangeMyQuality(int qual)
    {
        MyQuality = qual;
        Debug.Log("MyQuality Change To :" + qual);
        if (MyQuality < 1) MyQuality = 1;
        if (MyQuality > 10) MyQuality = 10;

      //  if (Intro) fpsCadr.text = MyQuality.ToString();
        switch (MyQuality)
        {
            case 10:
                SetResolution(1);
                QualitySettings.SetQualityLevel(5, true);

                DOFProcess(true);
                SSRProcess(false);
                AOprocess(true);
                BLoomProcess(true);
                VignetteProcess(true);
                CCprocess(true);
                MotionBlurProcess(false);

                break;
            case 9:
                QualitySettings.SetQualityLevel(5, true);
                AOprocess(true);
                BLoomProcess(true);
                VignetteProcess(true);
                CCprocess(true);
                DOFProcess(false);
                SSRProcess(false);
                MotionBlurProcess(false);
                SetResolution(1);

                break;
            case 8:
                QualitySettings.SetQualityLevel(4, true);
                AOprocess(true);
                BLoomProcess(true);
                VignetteProcess(true);
                CCprocess(true);
                DOFProcess(false);
                SSRProcess(false);
                MotionBlurProcess(false);
                SetResolution(1);

                break;
            case 7:
                QualitySettings.SetQualityLevel(4, true);
                SetResolution(0.5f);

                AOprocess(false);
                BLoomProcess(true);
                VignetteProcess(true);
                CCprocess(true);
                DOFProcess(false);
                SSRProcess(false);
                MotionBlurProcess(false);
                break;
            case 6:
                TurnAllPostEffects(false);
                SetResolution(0.5f);

                QualitySettings.SetQualityLevel(4, true);
            
                VignetteProcess(true);
                CCprocess(true);
               

                break;
            case 5:
                QualitySettings.SetQualityLevel(3, true);
                SetResolution(0.5f);
                TurnAllPostEffects(false);
               
                break;
            case 4:
                QualitySettings.SetQualityLevel(3, true);
                if (Intro) Commenttext.text = "4!Resol: 1/4 /Quality Level -> 4";
                SetResolution(0.5f);
                TurnAllPostEffects(false);

                break;
            case 3:
                QualitySettings.SetQualityLevel(2, true); ;
                if (Intro) Commenttext.text = "3!Quality Level -> 3";
                TurnAllPostEffects(false);
                SetResolution(0.5f);


                break;
            case 2:
                QualitySettings.SetQualityLevel(1, true); ;
                TurnAllPostEffects(false);
                SetResolution(0.5f);

                if (Intro) Commenttext.text = "2!Quality Level -> 2";
                break;
            case 1:
                QualitySettings.SetQualityLevel(1, true); ;

                TurnAllPostEffects(false);
                SetResolution(0.5f);

                if (Intro) Commenttext.text = "1!Quality Level -> 1";
                break;
        }
        PlayerPrefs.Save();
    }



    public static void AOprocess(bool On)
    {
        AmbientOcclusion param = null;
        PostProcessVolume volume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        //   volume.profile.TryGetSettings(out bloomLayer);
        if (volume != null)
        {

            volume.profile.TryGetSettings(out param);
            if (param)
                if (!On)
                    param.enabled.value = false;
                else
                    param.enabled.value = true;

        }

    }

    public static void CCprocess(bool On)
    {
        ColorGrading param = null;
        PostProcessVolume volume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        if (volume != null)
        {

            volume.profile.TryGetSettings(out param);
            if (param)
                if (!On)
                    param.enabled.value = false;
                else
                    param.enabled.value = true;

        }
    }

    public static void BLoomProcess(bool On)
    {
        Bloom param = null;
        PostProcessVolume volume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        if (volume != null)
        {

            volume.profile.TryGetSettings(out param);
            if (param)
                if (!On)
                    param.enabled.value = false;
                else
                    param.enabled.value = true;

        }
    }

    public static void SSRProcess(bool On)
    {
        ScreenSpaceReflections param = null;
        PostProcessVolume volume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        if (volume != null)
        {

            volume.profile.TryGetSettings(out param);
            if (param)
                if (!On)
                    param.enabled.value = false;
                else
                    param.enabled.value = true;

        }
    }

    public static void VignetteProcess(bool On)
    {
        Vignette param = null;
        PostProcessVolume volume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        if (volume != null)
        {

            volume.profile.TryGetSettings(out param);
            if (param)
                if (!On)
                    param.enabled.value = false;
                else
                    param.enabled.value = true;
        }
    }

    public static void DOFProcess(bool On)
    {
        DepthOfField param = null;
        PostProcessVolume volume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        if (volume != null)
        {

            volume.profile.TryGetSettings(out param);
            if (param)
                if (!On)
                    param.enabled.value = false;
                else
                    param.enabled.value = true;
        }
    }

    public static void MotionBlurProcess(bool On)
    {
        MotionBlur param = null;
        PostProcessVolume volume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        if (volume != null)
        {

            volume.profile.TryGetSettings(out param);
            if (param)
                if (!On)
                    param.enabled.value = false;
                else
                    param.enabled.value = true;
        }
    }

    private void CAProcess(bool On)
    {
        ChromaticAberration param = null;
        PostProcessVolume volume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
        if (volume != null)
        {

            volume.profile.TryGetSettings(out param);
            if (!On)
                param.enabled.value = false;
            else
                param.enabled.value = true;
        }
    }

}