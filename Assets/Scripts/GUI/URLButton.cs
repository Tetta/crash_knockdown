using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLButton : MonoBehaviour {

	public void OnClick(string url)
    {
        Application.OpenURL(url);
    }
}
