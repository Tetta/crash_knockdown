using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSoundVolume : MonoBehaviour {


    [SerializeField] Text T;
    void Awake()
    {
        T.text = (AudioListener.volume * 100).ToString() + "%";
        gameObject.GetComponent<Button>().onClick.AddListener(delegate { TaskOnClick(); });
        
    }

    private void TaskOnClick()
    {
     
        if (gameObject.name.Substring(0, 1) == "+")
        {
            AudioListener.volume += 0.1f;
        }
        else
        {
            AudioListener.volume -= 0.1f;

        }
        if ((AudioListener.volume > 0) && (AudioListener.volume < 0.1f)) AudioListener.volume = 0.0f;
        if ((AudioListener.volume > 0.1f) && (AudioListener.volume < 0.2f)) AudioListener.volume = 0.1f;
        if ((AudioListener.volume > 0.2f) && (AudioListener.volume < 0.3f)) AudioListener.volume = 0.2f;
        if ((AudioListener.volume > 0.3f) && (AudioListener.volume < 0.4f)) AudioListener.volume = 0.3f;
        if ((AudioListener.volume > 0.4f) && (AudioListener.volume < 0.5f)) AudioListener.volume = 0.4f;
        if ((AudioListener.volume > 0.5f) && (AudioListener.volume < 0.6f)) AudioListener.volume = 0.5f;
        if ((AudioListener.volume > 0.6f) && (AudioListener.volume < 0.7f)) AudioListener.volume = 0.6f;
        if ((AudioListener.volume > 0.7f) && (AudioListener.volume < 0.8f)) AudioListener.volume = 0.7f;
        if ((AudioListener.volume > 0.8f) && (AudioListener.volume < 0.9f)) AudioListener.volume = 0.8f;
        if ((AudioListener.volume > 0.9f) && (AudioListener.volume < 1.0f)) AudioListener.volume = 0.9f;

        if (AudioListener.volume <= 0) AudioListener.volume = 0;
        if (AudioListener.volume >= 1) AudioListener.volume = 1;
        T.text = (AudioListener.volume * 100).ToString() + "%";

    }

}
