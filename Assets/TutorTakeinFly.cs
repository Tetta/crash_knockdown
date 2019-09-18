using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorTakeinFly : MonoBehaviour {
    float t;
    bool slo;
    [SerializeField] Image img;
	// Use this for initialization
	void Awake () {
        t = 0;
        Bullet.EventBulletCollided += OnFly;
        img.rectTransform.anchoredPosition = -new Vector2(5550, 0);
        slo = false;
	}
    private void OnDestroy()
    {
        Bullet.EventBulletCollided -= OnFly;

    }
    void OnFly()
    {
        slo = true;
    }
	void Update () {
       if(slo)
            if(TimeManager.SlowMo)
        {
                img.rectTransform.localScale = Vector2.Lerp(img.rectTransform.localScale, Vector2.one, t * 4);
            t += Time.deltaTime;
            if (t > 1)
            {
                t = 0;
                slo = false;
            }
          //  Time.timeScale = 0.005f;
        
            img.rectTransform.anchoredPosition =GameProcess.ScalerUICoords* Camera.main.WorldToScreenPoint(GameProcess.LastBulletInFly.transform.position);
        }
            else
            {
                img.rectTransform.localScale = Vector2.Lerp(img.rectTransform.localScale, Vector2.zero, t * 4);
            }
	}
}
