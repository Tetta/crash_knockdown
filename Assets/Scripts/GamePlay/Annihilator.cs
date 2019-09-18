using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Annihilator : MonoBehaviour {
    [SerializeField] float AnigilatorPower=1;
    float t;
    bool started;
	void Awake() {
        t = 0;
        started = false;
	}

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag=="Bullet")
        GameProcess.AnnigilateMultiplier = AnigilatorPower;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Bullet")
            started = true; 
    }
    void Update () {
        if (started)
        {
            t += Time.deltaTime;
            if (t > 2)
            {
                GameProcess.AnnigilateMultiplier = 0;
                Destroy(this);
            }
        }
	}
}
