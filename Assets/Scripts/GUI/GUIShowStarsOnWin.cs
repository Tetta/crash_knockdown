using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GUIShowStarsOnWin : MonoBehaviour {
    [SerializeField] GameObject Star1;
    [SerializeField] GameObject Star2;
    [SerializeField] GameObject Star3;
    //[SerializeField] GameObject Star4;
    //[SerializeField] GameObject Star5;
    [SerializeField] GameObject button;
    float delay;
    AudioSource GettingStar;
    Animator anim1, anim2, anim3, anim4, anim5, karpButton;
    bool awakening,del1,del2,del3, del4, del5;
    // Use this for initialization
    void Awake()
    {
        GettingStar = GetComponent<AudioSource>();
        GameProcess .EventWin += ShowStars;
        delay = 0;
        anim1 = Star1.GetComponent<Animator>();
        anim2 = Star2.GetComponent<Animator>();
        anim3 = Star3.GetComponent<Animator>();
        //anim4 = Star4.GetComponent<Animator>();
        //anim5 = Star5.GetComponent<Animator>();

     if (button!=null)   karpButton = button.GetComponent<Animator>();
     if(karpButton!=null)   karpButton.enabled = false;

        anim1.enabled = false;
        anim2.enabled = false;
        anim3.enabled = false;
        //anim4.enabled = false;
        //anim5.enabled = false;
        //  anim1.clip.legacy = true;
        //   anim2.clip.legacy = true;
        //  anim3.clip.legacy = true;

        Star1.SetActive(false);
        Star2.SetActive(false);
        Star3.SetActive(false);
        //Star4.SetActive(false);
        //Star5.SetActive(false);
        //Star5.transform.localScale = Vector3.zero;
        //Star4.transform.localScale = Vector3.zero;
        Star3.transform.localScale = Vector3.zero;
        Star2.transform.localScale = Vector3.zero;
        Star1.transform.localScale = Vector3.zero;
        awakening = false;
        del1 = false;del2 = false; del3 = false; del4 = false; del5 = false;

    }
    private void OnDestroy()
    {
        GameProcess.EventWin -= ShowStars;

    }

    void ShowStars()
    {
        awakening = true;

    }
    // Update is called once per frame
    void Update () {

        if (awakening)
        {

            delay += Time.deltaTime;

            StartCoroutine(StorProgs());
            
            
            /*
            else if (Game.MyTimer < Game.Stars[3])
            {
                Star1.SetActive(true);
                Star3.SetActive(true);
            }
            else if (Game.MyTimer < Game.Stars[4])
            {

                Star1.SetActive(true);

            }
            */
            if (del1 == false)
            {

                if (delay > 0.2f)
                {

                    anim1.enabled = false;
                    anim1.enabled = true;
                    del1 = true;


                }

            }  

            if (del2 == false)
            {

                if (delay > 0.5f)
                {

                    anim2.enabled = false;
                    anim2.enabled = true;
                    del2 = true;

                }

            }

            if (del3 == false)
            {

                if (delay > 0.8f)
                {
                    anim3.enabled = false;
                    anim3.enabled = true;
                    del3 = true;
                }

            }
            /*
            if (del4 == false)
            {

                if (delay > 1.1f)
                {
                    anim4.enabled = false;
                    anim4.enabled = true;
                    del4 = true;
                }

            }

            if (del5 == false)
            {

                if (delay > 1.4f)
                {
                    anim5.enabled = false;
                    anim5.enabled = true;

                    del5 = true;
                }

            }*/
        }

    }

    IEnumerator StorProgs()
    {

        SoundAndMusik.Instance.LoadingScoreBoardWindow();

        yield return new WaitForSeconds(2);

        if (GameProcess.MyTimer < GameProcess.Stars[0])
        {

            Star3.SetActive(true);
            yield return new WaitForSeconds(0.6f);            
            Star2.SetActive(true);
            yield return new WaitForSeconds(0.6f);
            Star1.SetActive(true);
            
        }
        else if (GameProcess.MyTimer < GameProcess.Stars[1])
        {

            Star3.SetActive(true);
            yield return new WaitForSeconds(0.6f);
            Star2.SetActive(true);

        }
        else if (GameProcess.MyTimer < GameProcess.Stars[2])
        {

            Star3.SetActive(true);

        }

      if(  karpButton!=null) karpButton.enabled = true;

    }

}
