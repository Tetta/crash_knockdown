using UnityEngine;

public class TimeManager : MonoBehaviour {

	public static float slowdownFactor = 0.05f;
	public static float slowdownLength = 2f;
    public static bool SlowMo = false;
    public static float temp,startSlou,delay=0;
   // private GUIStyle guiStyle = new GUIStyle();
    private void Awake()
    {
        SlowMo = false;
        startSlou = 0.05f;
        Time.timeScale = startSlou;
    }
    void Update ()
	{

        delay -= Time.deltaTime;
        temp += 2f * Time.unscaledDeltaTime;
        temp = Mathf.Clamp(temp, 0.02f, 1f);
        Time.timeScale = temp;
        
        if (SlowMo == true)
        {
                         
            slowdownLength += Time.unscaledDeltaTime;
            temp -=  24* Time.unscaledDeltaTime;
            temp = Mathf.Clamp(temp, 0.02f, 1f);
            Time.timeScale = temp;
            //  if (Time.timeScale < 0.0051f) SlowMo = false;
            if (slowdownLength > 1)
            {

                GameProcess.CurrentVelosity = 0;
                SlowMo = false;
                SoundAndMusik.Instance.isSlowMo = false;

            }
           
        }
        else
        {
            temp +=1f* Time.deltaTime;
            temp = Mathf.Clamp(temp, 0.02f, 1f);
            Time.timeScale = temp;
          
        }
        Time.fixedDeltaTime = Time.timeScale * .01f;
    }

	public static void DoSlowmotion ()
    {
        if (delay > 0)
            if (SlowMo != true)
        {
                delay = -1;
            SlowMo = true;
          //  Debug.Log("DoSlowMotion");
            slowdownLength = 0;
            
            //  Time.timeScale =  ;
        }

    }
 
 
 
}
