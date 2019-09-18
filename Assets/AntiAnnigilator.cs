using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiAnnigilator : MonoBehaviour {
    void FixedUpdate()
    {
        if (GameProcess.PowerShot)
        {
            gameObject.layer = 4;
            GameProcess.AnnigilateMultiplier = 0.4f;
        }
        else
        {
            gameObject.layer = 0;


        }
    }
} 