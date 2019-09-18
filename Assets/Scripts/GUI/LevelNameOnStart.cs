using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelNameOnStart : MonoBehaviour {
    float t;
    Color C,C2;
   [SerializeField] Text T1, T2,T3,T4;
   [SerializeField] Image I;
    // Use this for initialization
	void Awake () {

        t = 0;
        C = Color.white;
        C2 = Color.black;
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name)) T4.text = Mathf.RoundToInt(PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name)).ToString();
        else T4.text = (GameProcess.Stars[0] - 1).ToString();

    }
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime*0.5f;
        C.a = Mathf.Lerp(1, 0, t);
        C2.a = Mathf.Lerp(1, 0, t);
        T1.color = C;
        T2.color = C;
        T3.color = C2;
        T4.color = C2;

        I.color = C;
        if (t > 1) Destroy(gameObject);
	}
}
