using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUISTARTMENUBAR : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        SetStars();
        GameProcess.EventWin += SetStars;
        PurchaseTrigger.OnPurchaseChapter += SetStars;
        UnlockLevels.OnUnlockLevel += SetStars;
        DifficultyPanel.OnRemoveAllLevelData += SetStars;
    }

    private void OnDestroy()
    {
        GameProcess.EventWin -= SetStars;
        PurchaseTrigger.OnPurchaseChapter -= SetStars;
        UnlockLevels.OnUnlockLevel -= SetStars;
        DifficultyPanel.OnRemoveAllLevelData -= SetStars;

    }
    void SetStars()
    {
        string str = gameObject.transform.parent.gameObject.name;
        str = str.Substring(str.Length - 4, 4);
        string me = gameObject.name.Substring(gameObject.name.Length - 1, 1);
        int stars = 0;
        if(PlayerPrefs.HasKey(str + "Stars"))
            stars = PlayerPrefs.GetInt(str + "Stars");
    //    Debug.Log(str + "   STars=  " + stars);

        gameObject.SetActive(false);
      
                    if (stars == 1) if (me == "1") gameObject.SetActive(true);
        
                    if (stars == 2) if ((me == "2")||(me=="1") )gameObject.SetActive(true);

                   
                    if (stars == 3)  gameObject.SetActive(true);

    }

}
