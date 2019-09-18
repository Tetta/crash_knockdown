using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuGUIStatesController : MonoBehaviour {
    [SerializeField] int State;

private CanvasGroup Can;
    [SerializeField] GameObject yes;
    [SerializeField] GameObject no;
   private float t;
   private bool changing,LastState;
	// Use this for initialization
	void Awake () {
        LastState = false;
        t = 0;
        changing = false;
        Can = GetComponent<CanvasGroup>() ;
        Can.alpha = 0;
        
        MainMenu.EventChangeState += OnChange;
        for (int i = 0; i < gameObject.transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
     
        MainMenu.EventChangeState -= OnChange;

    }
    void OnChange()
    {
        if (GameProcess.State == State)
        {
            changing = true;
            LastState = false;
          //  Debug.Log(gameObject.name + ": FadeIn!");
            t = 0;
            for (int i = 0; i < gameObject.transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(true);
        }
       else
        if(Can.alpha==1)
        {
            changing = true;
        //    Debug.Log(gameObject.name+": FadeOut!");
            t = 0;
            LastState = true;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if (changing)
            if (false==LastState)
            {
                t += Time.deltaTime;
                Can.alpha = Mathf.Lerp(0, 1, t*2);

                if (t > 1)
                {
                    LastState = false;
                    changing = false;

                }
            }
            else
            {
                t += Time.deltaTime;
                Can.alpha = Mathf.Lerp(1, 0, t * 2);
                if (t > 1)
                {
                    LastState = true;
                    changing = false;
                    for (int i = 0; i < gameObject.transform.childCount; i++)
                        transform.GetChild(i).gameObject.SetActive(false);

                }
            }
    } 
    
}
