using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StartEndPositions : MonoBehaviour
{   
    [SerializeField] int GameState=0;
    [SerializeField] Vector2 StartPositionPivot;
    [SerializeField] Vector2 EndPositionPivot;
    [SerializeField] float time;
    [SerializeField] float Delay = 0;
    [SerializeField] bool GoStart = false;
    [SerializeField] bool AwakePosEnd = false;
    [SerializeField] bool DisableAfterAwake = false;
    float transTime = 0;
    public RectTransform rect;
    float delayTime;

    private GameGUI containor; // may assign from inspector


    private void OnDestroy()
    {
        GameGUI.GoToStartPos -= OnShow;
        GameGUI.GoToEndPos -= OnHide;
    }


    void OnShow(int i)
    {
        if (i == GameState)
        {
          //  Debug.Log("Item fo Start:" + name);
            delayTime = 0;
            GoStart = true;
            

        }
    }
    void OnHide(int i)
    {
        if (i == GameState)
        {
           // Debug.Log("Item fo End:" + name);
            this.enabled = true;
            GoStart = false;
            delayTime = 0;
        }
    }

    void Awake()
    {
        delayTime = 0;
        GameGUI.GoToStartPos += OnShow;
        GameGUI.GoToEndPos += OnHide;
        rect = GetComponent<RectTransform>();
        OnAwakePosition();
    }

    public void OnAwakePosition()
    {
        if (AwakePosEnd)
        {
            rect.pivot = EndPositionPivot;
        }
        else
        {
            rect.pivot = StartPositionPivot;
        }
    }

    // Update is called once per frame
    void Update()
    { 
        delayTime += Time.deltaTime;   
        if (delayTime > Delay)
            if(GameGUI.TransitionTime<1.01f)
        {

            if (GoStart == true)
            {
                if(rect.pivot!= StartPositionPivot)
                rect.pivot = Vector2.Lerp(rect.pivot, StartPositionPivot, GameGUI.TransitionTime * time);
            }
            else
            {
                if (rect.pivot != EndPositionPivot)
                    rect.pivot = Vector2.Lerp(rect.pivot, EndPositionPivot, GameGUI.TransitionTime * time);
            }

         //   if (GameGUI.TransitionTime  > time) this.enabled = false;
        }
     
        }
    
}
