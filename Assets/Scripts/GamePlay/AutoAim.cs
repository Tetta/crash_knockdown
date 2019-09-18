using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour {
  //  Rigidbody rb;
  //  float t;
 //   float dist;
  //  Vector3 rand;
	// Use this for initialization
	void Awake() {
     //   rb = GetComponent<Rigidbody>();
     //   t = 0;
     //   rand = UnityEngine.Random.onUnitSphere*100;
     //   dist= (Game.CurrentAim - transform.position).magnitude;
      
        if (GameProcess.State == 1)
        {
            GameGUI.Aim.anchoredPosition = Camera.main.WorldToScreenPoint(GameProcess.CurrentAim);
            GameGUI.Aim.anchoredPosition *= GameProcess.ScalerUICoords;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(GameProcess.State==1)        GameGUI.Aim.anchoredPosition = Vector2.one * -3000;
        Destroy(this);
    }
 
     
}
