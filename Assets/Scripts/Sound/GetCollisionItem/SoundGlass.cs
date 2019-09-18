using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGlass : MonoBehaviour {

    int count;
    private int maxCount = 0;
    private int tempCount;
    float t;
    PhysicMaterial mat;
    bool _timer;
    private GameObject tempGO;
    private float myTimer = 0;


    void Awake()
    {

        _timer = false;
        count = 0;
        t = 0;

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (myTimer < 0.1f) myTimer += Time.deltaTime;

        if (_timer) t += Time.deltaTime;
        if (t > 0.05f)
        {

            if (maxCount != 0)
            {

                switch (mat.name)
                {

                    case "ceramic (Instance)":
                        if (myTimer > 0.06f) SoundAndMusik.Instance.GetBreakGlass(gameObject, Mathf.Clamp(((2.49f * count) / maxCount), 0.0f, 3.0f));
                        break;

                    case "ceramic":
                        if (myTimer > 0.06f) SoundAndMusik.Instance.GetBreakGlass(gameObject, Mathf.Clamp(((2.49f * count) / maxCount), 0.0f, 3.0f));
                        break;

                }

            }

            Destroy(this);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        
        _timer = true;
        mat = collision.collider.material;
        tempGO = collision.gameObject;
        //домножаю на 7, чтобы сравнять максимальное количество точек, типа по 7 точек в среднем.
        count += collision.contacts.Length / 7;
        if (collision != null && collision.gameObject != null && collision.gameObject.transform != null && collision.gameObject.transform.parent != null)
            tempCount = collision.gameObject.transform.parent.childCount * 7;
        if (tempCount > maxCount) maxCount = tempCount;

    }

    private void OnCollisionStay(Collision collision)
    {

        count += collision.contacts.Length;

    }

}
