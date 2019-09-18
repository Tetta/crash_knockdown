using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSFX : MonoBehaviour {
    int count;
    private int maxCount;
    private int tempCount;
    float t;
    PhysicMaterial mat;
    bool _timer;
    private GameObject tempGO;
	// Use this for initialization
	void Awake () {
        _timer = false;
        count = 0;
        t = 0;

	}
    private void Update()
    {
        if (_timer) t += Time.deltaTime;
        if (t > 0.05f)
        {
        //    Debug.Log("ContactsCount: " + count + " - Material:" + mat);
            
            //Тута ивент по типу материала в зависимости от count и mat
            if (maxCount != 0)
            {

                switch (mat.name)
                {

                    case "ceramic (Instance)":
                        SoundAndMusik.Instance.GetBreakGlass(tempGO, ((2.49f * count) / maxCount));
                        break;

                    case "ceramic":
                        SoundAndMusik.Instance.GetBreakGlass(tempGO, ((2.49f * count) / maxCount));
                        break;

                }

            }

            Destroy(this);
        }

        if (TimeManager.SlowMo && !SoundAndMusik.Instance.isSlowMo)
        {

            SoundAndMusik.Instance.GetSlowMotion(gameObject);
            SoundAndMusik.Instance.isSlowMo = true;

        }

    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name.Length >= 5)
        {

            if (collision.gameObject.name.Length >= 12 && collision.gameObject.name.Substring(0, 12) == "table_planks") SoundAndMusik.Instance.isFly = false;
            else if (collision.gameObject.name.Length == 5 && collision.gameObject.name == "polka") SoundAndMusik.Instance.isFly = false;
            else if (collision.gameObject.name.Length == 6 && collision.gameObject.name == "polka2") SoundAndMusik.Instance.isFly = false;
            else if (collision.gameObject.name.Length == 9 && collision.gameObject.name == "podstavka" || collision.gameObject.name == "microwave") SoundAndMusik.Instance.isFly = false;

        }

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
