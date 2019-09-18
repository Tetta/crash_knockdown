using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleDontShowChangingQuality : MonoBehaviour
{
 
    Toggle m_Toggle;
    Text m_Text;

    void Awake()
    {
      
        m_Toggle = GetComponent<Toggle>();
        //Add listener for when the state of the Toggle changes, to take action
        m_Toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(m_Toggle);
        });
        if (PlayerPrefs.HasKey("DontShowQualityChange"))
        {
            if (PlayerPrefs.GetInt("DontShowQualityChange") == 1) m_Toggle.isOn = true;
            else m_Toggle.isOn = false;
        }
        else m_Toggle.isOn = false;
    }

    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change)
    {
        if (m_Toggle.isOn)
        {
            PlayerPrefs.SetInt("DontShowQualityChange", 1);
            Debug.Log("DontShowQualityChange -> 1");
        }
        else
        {
            PlayerPrefs.SetInt("DontShowQualityChange", 0);

            Debug.Log("DontShowQualityChange -> 0");
        }
    }
}
