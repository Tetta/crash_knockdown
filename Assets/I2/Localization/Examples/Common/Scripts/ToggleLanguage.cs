using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace I2.Loc
{
	public class ToggleLanguage : MonoBehaviour
    {
         
        void Awake()
        {
            gameObject.GetComponent<Button>().onClick.AddListener(delegate { TaskOnClick(); });
 
     
        }
    void TaskOnClick()
        {
			//--  to move into the next language ----

				List<string> languages = LocalizationManager.GetAllLanguages();
				int Index = languages.IndexOf(LocalizationManager.CurrentLanguage);
            if (Index < 0)
                Index = 0;
            else
            {
                if (gameObject.name.Substring(0, 1) == "+")
                {
                    Index = (Index + 1) % languages.Count;
                    Debug.Log("Language++");
                }
                else
                {
                    Debug.Log("Language--");
                    Index = (Index - 1) % languages.Count;
                }
            }
            LocalizationManager.CurrentLanguage = languages[Index];

        }
	}
}