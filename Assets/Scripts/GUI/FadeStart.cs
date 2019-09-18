using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeStart : MonoBehaviour {

    // Use this for initialization
    float t;
    Image Img;
    // Use this for initialization
    void Awake()
    {
        t = 0;
        Img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        Color clr = Color.black;
        clr.a = Mathf.LerpUnclamped(1, 0,  t);
        Img.color = clr;
        if (t > 1)
        {
              gameObject.GetComponent<Image>().enabled = false;

            // gameObject.SetActive(false);
            gameObject.AddComponent<FadeLoad>();
            Destroy(this);

        }
    }
}
