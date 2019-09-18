using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIMenuButtonBF : MonoBehaviour {

    [SerializeField]
    private Transform pointCentr;
    [SerializeField]
    private Transform pointRight;
    [SerializeField]
    private Transform pointLeft;

    private bool isCentr = false;

    [SerializeField]
    private GameObject[] mass01;
    [SerializeField]
    private GameObject[] mass02;
 
 
    private float startXone;
    private float startXtwo;
    private float rightX;
    private float leftX;

    private bool tempBool;
    private Vector2 tempVector2;


    void Awake()
    {

        Initialized();

    }

    void OnEnable()
    {

        Initialized();

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (isCentr)
        {
            
            tempBool = true;

            for(int i = 0; i < mass02.Length; i++)
            {

                if (tempBool)
                {

                    tempVector2 = Vector2.Lerp(new Vector2(mass02[i].transform.position.x, mass02[i].transform.position.y), new Vector2(startXone, mass02[i].transform.position.y), Time.deltaTime);
                    mass02[i].transform.position.Set(tempVector2.x, tempVector2.y, mass02[i].transform.position.z);
                    tempBool = !tempBool;

                }
                else
                {

                    tempVector2 = Vector2.Lerp(new Vector2(mass02[i].transform.position.x, mass02[i].transform.position.y), new Vector2(startXtwo, mass02[i].transform.position.y), Time.deltaTime);
                    mass02[i].transform.position.Set(tempVector2.x, tempVector2.y, mass02[i].transform.position.z);
                    tempBool = !tempBool;

                }

                tempVector2 = Vector2.Lerp(new Vector2(mass01[i].transform.position.x, mass01[i].transform.position.y), new Vector2(leftX, mass01[i].transform.position.y), Time.deltaTime);
                mass01[i].transform.position.Set(tempVector2.x, tempVector2.y, mass01[i].transform.position.z);

            }
                      


        }
        else
        {
            
            tempBool = true;

            for (int i = 0; i < mass02.Length; i++)
            {

                if (tempBool)
                {

                    tempVector2 = Vector2.Lerp(new Vector2(mass01[i].transform.position.x, mass01[i].transform.position.y), new Vector2(startXone, mass01[i].transform.position.y), Time.deltaTime);
                    mass01[i].transform.position.Set(tempVector2.x, tempVector2.y, mass01[i].transform.position.z);
                    tempBool = !tempBool;

                }
                else
                {

                    tempVector2 = Vector2.Lerp(new Vector2(mass01[i].transform.position.x, mass01[i].transform.position.y), new Vector2(startXtwo, mass01[i].transform.position.y), Time.deltaTime);
                    mass01[i].transform.position.Set(tempVector2.x, tempVector2.y, mass01[i].transform.position.z);
                    tempBool = !tempBool;

                }

                tempVector2 = Vector2.Lerp(new Vector2(mass02[i].transform.position.x, mass02[i].transform.position.y), new Vector2(rightX, mass02[i].transform.position.y), Time.deltaTime);
                mass02[i].transform.position.Set(tempVector2.x, tempVector2.y, mass02[i].transform.position.z);

            }
            
        }
		*/
	}

    public void ForwardClick()
    {

          //isCentr = true;

        for (int i = 0; i < mass01.Length; i++)
        {

            mass01[i].SetActive(false);
            mass02[i].SetActive(true);

        }

    }

    public void BackwardClick()
    {

         //isCentr = false;

        for (int i = 0; i < mass01.Length; i++)
        {

            mass01[i].SetActive(true);
            mass02[i].SetActive(false);

        }

    }

    private void Initialized()
    {

        startXone = mass01[0].transform.localPosition.x;
        startXtwo = mass01[1].transform.localPosition.x;
        rightX = pointRight.transform.localPosition.x;
        leftX = pointLeft.transform.localPosition.x;

        for (int i = 0; i < mass02.Length; i++)
        {

            mass02[i].transform.position.Set(rightX, mass02[i].transform.position.x, mass02[i].transform.position.z);

        }
 
        for (int i = 0; i < mass01.Length; i++)
        {

            mass01[i].SetActive(true);
            mass02[i].SetActive(false);

        }

    }

}
