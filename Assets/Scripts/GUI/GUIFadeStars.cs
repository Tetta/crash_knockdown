using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class GUIFadeStars : MonoBehaviour
{
    [SerializeField] Image[] Stars;
    bool fade;
    Color c;
    float t;
    int starscount;
    Text T;
    // Use this for initialization
    void Awake()
    {
        T = transform.parent.gameObject.GetComponentInParent<Text>();
        T.text = SceneManager.GetActiveScene().name;
        GameProcess.EventFadeIn += OnFadeIn;
        c = Color.white;
        c.a = 0;
        Stars[0].color = c;
        Stars[1].color = c;
        Stars[2].color = c;
        fade = false;
        t = 0;
        c.a = 1;
        starscount = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "Stars");
        if (starscount > 0) Stars[0].color = c;
        if (starscount > 1) Stars[1].color = c;
        if (starscount > 2) Stars[2].color = c;
    }
    void OnDestroy()
    {
        GameProcess.EventFadeIn -= OnFadeIn;
    }
    void OnFadeIn()
    {
        fade = true;
        starscount = PlayerPrefs.GetInt(GameProcess.FadeLevelName + "Stars");
        T.text = GameProcess.FadeLevelName;
        c.a = 0;
        Debug.Log("Stars: " + starscount);
        t = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (t < 1)
        {
            if (fade)
            {
                t += Time.deltaTime;
                c.a = Mathf.Lerp(0, 1, t);
                if (starscount == 1) Stars[0].color = c;
                if (starscount == 2)
                {
                    Stars[0].color = c;
                    Stars[1].color = c;
                }
                if (starscount == 3)
                {
                    Stars[0].color = c;
                    Stars[1].color = c;
                    Stars[2].color = c;
                }
            
            }
            else
            {
                t += Time.deltaTime;
                c.a = Mathf.Lerp(1, 0, t);

                if (starscount > 0) Stars[0].color = c;
                if (starscount > 1) Stars[1].color = c;
                if (starscount > 2) Stars[2].color = c;

            }

        }
    }
}
