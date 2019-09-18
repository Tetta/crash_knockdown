using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLevelButton : MonoBehaviour {
 GameObject stars1;
 GameObject stars2;
  GameObject stars3;
  GameObject stars4;
  GameObject stars5;
  GameObject Locked;
    // Use this for initialization

    private void Awake()
    {
     //   int z = 0;
        //Загружаю свой новый префаб звёзд для меню
        //GameObject GO = Instantiate(Resources.Load("StarsMenu")) as GameObject;
        GameObject GO = Instantiate(Resources.Load("StarsMenu2")) as GameObject;
        GO.transform.parent = gameObject.transform;
        GO.transform.localScale = new Vector3(1, 1, 1);
        GO.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        GO.GetComponent<RectTransform>().pivot = Vector2.one*0.5f;
        stars3 = GO.transform.Find("3stars").gameObject;
        stars2 = GO.transform.Find("2stars").gameObject;
        stars1 = GO.transform.Find("1stars").gameObject;
       
        stars4 = GO.transform.Find("4stars").gameObject;
        stars5 = GO.transform.Find("5stars").gameObject;
        Locked = transform.Find("Topping").gameObject;

        /*
        stars1.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        stars2.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        stars3.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        stars4.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        stars5.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        */
        ShowStarsInMenu();
        GameProcess.EventWin += ShowStarsInMenu;
        PurchaseTrigger.OnPurchaseChapter += ShowStarsInMenu;
        UnlockLevels.OnUnlockLevel += ShowStarsInMenu;
        DifficultyPanel.OnRemoveAllLevelData += ShowStarsInMenu;
        MainMenu.EventChangeState += OnChangeMenu;

    }
    void OnChangeMenu()
    {
        ShowStarsInMenu();
    }
    private void OnDestroy()
    {
        GameProcess.EventWin -= ShowStarsInMenu;
        MainMenu.EventChangeState -= OnChangeMenu;
        UnlockLevels.OnUnlockLevel -= ShowStarsInMenu;
        DifficultyPanel.OnRemoveAllLevelData -= ShowStarsInMenu;
        PurchaseTrigger.OnPurchaseChapter -= ShowStarsInMenu;
    }

    void ShowStarsInMenu() {
        int stars = 0;

        /*
        stars1.SetActive(false);
        stars2.SetActive(false);
        stars3.SetActive(false);
        */
        stars4.SetActive(false);
        stars5.SetActive(false);
        if (PlayerPrefs.HasKey(name.Substring(name.Length - 4) + "Stars"))
        {
            stars = PlayerPrefs.GetInt(name.Substring(name.Length - 4) + "Stars");
            if (transform.parent.parent.name == "Bar")
            {
                print("huint");
            }

            //новые звёзды
            if (stars == 1)
            {

                stars1.SetActive(true);
                stars2.SetActive(false);
                stars3.SetActive(false);

            }
            else if (stars == 2)
            {

                stars1.SetActive(false);
                stars2.SetActive(true);
                stars3.SetActive(true);

            }
            else if (stars >= 3)
            {

                stars1.SetActive(true);
                stars2.SetActive(true);
                stars3.SetActive(true);

            }
            else if(stars <= 0)
            {

                stars1.SetActive(false);
                stars2.SetActive(false);
                stars3.SetActive(false);

            }


            //Страрый способ показа звёзд в меню. И я удалил лишние звёзды в префабе
            /*
            if (stars == 1) stars1.SetActive(true);
            if (stars == 2) stars2.SetActive(true);
            if (stars == 3) stars3.SetActive(true);  
            if (stars == 4) stars4.SetActive(true);
            if (stars == 5) stars5.SetActive(true);
            */
            Locked.SetActive(false);
        } 
        else
        {

            stars1.SetActive(false);
            stars2.SetActive(false);
            stars3.SetActive(false);
            Locked.SetActive(true);
        }
     

       
    }
      

	
	// Update is called once per frame
	void Update () {
		
	}
}
