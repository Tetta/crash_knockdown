//using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioOptionsMenuController : MonoBehaviour
{
    FMOD.Studio.VCA MusicVCA;
    FMOD.Studio.VCA FXVCA;

    [SerializeField]
    string MusicVCAName = "Music";

    [SerializeField]
    string FXVCAName = "SFX";

    public float volumeFX;
    public float volumeMusik;

    private void Awake()
    {

        MusicVCA = FMODUnity.RuntimeManager.GetVCA("vca:/" + MusicVCAName);

        FXVCA = FMODUnity.RuntimeManager.GetVCA("vca:/" + FXVCAName);
        /*
        if (PlayerPrefs.HasKey("FXVolume"))
        {

            volumeFX = PlayerPrefs.GetFloat("FXVolume");
            SetFXVolume(volumeFX);

            volumeMusik = PlayerPrefs.GetFloat("MusicVolume");
            SetMusicVolume(volumeMusik);

        }        
        */
    }

    public void SetMusicVolume(float volume)
    {

        volumeMusik = volume;
            
        MusicVCA.setVolume(volume);

    }

    public void SetFXVolume(float volume)
    {

        volumeFX = volume;

        FXVCA.setVolume(volume);

    }
    
}
