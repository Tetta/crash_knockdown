using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSingletonSoundAndMusik : MonoBehaviour {

    private GameObject audioOptionsMenu;

    private SoundManager soundManager;

    void Awake()
    {

        audioOptionsMenu = GameObject.Find("AudioOptionsMenu");

        if (audioOptionsMenu == null)
        {

            audioOptionsMenu = Resources.Load("AudioOptionsMenu") as GameObject;

        }

        soundManager = FindObjectOfType<SoundManager>();

        if (soundManager == null)
        {

            gameObject.AddComponent<SoundManager>();

        }

    }

    // Use this for initialization
    void Start () {

        

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
