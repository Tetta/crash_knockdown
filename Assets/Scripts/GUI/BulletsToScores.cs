using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsToScores : MonoBehaviour {
  //  float t = 0;
    int cur;
	// Use this for initialization
	void Awake () {
   //     t = 0;
 
    }
	
	// Update is called once per frame
	void Update () {


        TimeManager.DoSlowmotion();
         if(GameProcess.GrabbedBullets.Count!=0)    GameProcess.GrabbedBullets[GameProcess.GrabbedBullets.Count - 1].AddComponent<ScaleToZero>();
            //    GameGUI.CrackSplash(new Vector2(0.5f, 0), Game.GrabbedBullets.Count* 50);

        Destroy(this);
        
	}
}
