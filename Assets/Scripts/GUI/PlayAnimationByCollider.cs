using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationByCollider : MonoBehaviour {
    [Header("Объект с компонентом Animator, который будет запускаться по триггеру")]
    [SerializeField] Animator[] animObject;
 [Header("Объект который будет уничтожаться по триггеру")]
    [SerializeField] GameObject[] ToDestroy;

    bool[] Once;
    bool[] OnceAnim;
    bool[] OnceDestroy;
    [Header("Объект который будет Выключаться/включаться по триггеру ")]
    [SerializeField] GameObject [] ToOnOffOnce;
    float[] T;
    
    void Awake ()
    {
        Once = new bool[ToOnOffOnce.Length];
        T = new float[ToOnOffOnce.Length];
      
        for (int i=0;i<Once.Length;i++)
        Once[i] = false;
	}
    
    private void OnTriggerEnter(Collider other)

    {
      
       
            for (int i = 0; i < Once.Length; i++)
                    if (Once[i] == false)
        {
            if (ToOnOffOnce[i].activeSelf) ToOnOffOnce[i].SetActive(false);
            else ToOnOffOnce[i].SetActive(true);
            Once[i] = true;
            T[i] = 0;
                Destroy(this);
        }
        for (int i = 0; i < animObject.Length; i++)
        {

            animObject[i].enabled = false;
            animObject[i].enabled = true;
            Destroy(this);
            animObject[i].gameObject.GetComponent<Animation>().Stop();
            animObject[i].gameObject.GetComponent<Animation>().Play();
           
        }
        for (int i = 0; i < ToDestroy.Length; i++)
            {
            Destroy(ToDestroy[i]);
        }
    }
    // Update is called once per frame
    void Update ()
    {
        
            for (int i = 0; i < Once.Length; i++)
            if (Once[i] == true)
        {
            T[i] += Time.deltaTime;
            if (T[i] > 2) Once[i] = false;
        }	
	}
}
