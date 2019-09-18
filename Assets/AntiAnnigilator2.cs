using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAnnigilator2 : MonoBehaviour {

  
    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameProcess.PowerShot)
        {
            gameObject.layer = 0;
            GameProcess.AnnigilateMultiplier = 0.4f;
            Destroy(this.gameObject);
        }
        else
        {
            gameObject.layer = 4;


        }
    }
}
