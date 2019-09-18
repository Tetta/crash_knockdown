using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {
	bool[] Once;
    [SerializeField] GameObject [] ToOnOffOnce;
    float[] T;
    [SerializeField] GameObject ObjectToDestroy;
    [SerializeField] ParticleSystem PSys;
    int count;
    void Awake () {
        count = ObjectToDestroy.transform.childCount;
        PSys.Stop();
    {
        Once = new bool[ToOnOffOnce.Length];
        T = new float[ToOnOffOnce.Length];
      
        for (int i=0;i<Once.Length;i++)
        Once[i] = false;
	}
	}



	// Update is called once per frame
	void Update () {
   if (count > ObjectToDestroy.transform.childCount)
	   
            for (int i = 0; i < Once.Length; i++)
                    if (Once[i] == false)
        {
            if (ToOnOffOnce[i].activeSelf) ToOnOffOnce[i].SetActive(false);
            else ToOnOffOnce[i].SetActive(true);
            Once[i] = true;
            T[i] = 0;
        }

   if (count > ObjectToDestroy.transform.childCount)
        {
            
            gameObject.GetComponent<ExplosionSource>().enabled = true;
            if (PSys != null) PSys.Play();

            Destroy(this );
        }
    }
}
