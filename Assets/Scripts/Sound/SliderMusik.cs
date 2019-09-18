using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMusik : MonoBehaviour {

    private Slider mySlider;
    private AudioOptionsMenuController myAOMC;

    void Awake()
    {

        mySlider = gameObject.GetComponent<Slider>();

        myAOMC = FindObjectOfType<AudioOptionsMenuController>();

        if (PlayerPrefs.HasKey("FXVolume"))
        {

            mySlider.value = PlayerPrefs.GetFloat("FXVolume");

        }

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (myAOMC != null) myAOMC.SetMusicVolume(mySlider.value);
        else myAOMC = FindObjectOfType<AudioOptionsMenuController>();

    }

    void OnDestroy()
    {

        PlayerPrefs.SetFloat("MusicVolume", mySlider.value);

    }

}
