using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingCamera : MonoBehaviour {
    [Header("как в физии линк, 123456 - просто текстом ту камеру в которой учитывать убийство этого объекта")]
    [SerializeField] string CameraNumber;
	// Use this for initialization
	void Awake () {
		for(int i = 0; i < CameraNumber.Length; i++)
        {
            int k = 0;
            int.TryParse(CameraNumber.Substring(i,1),out k);
            GameProcess.CameraCheckObjects[k].Add(this.gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
