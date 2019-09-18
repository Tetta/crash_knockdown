using System.Collections;
using System;

using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
   private float t = 0;
   private Rigidbody rb;
  private   int cur;
  private  Vector3 startpos;
    private Quaternion startrot;
    [Header ("объект, куда мы целимся")]
    [SerializeField] public GameObject  Aim;
    [Header ("Тестовый Баклажан")]
    [SerializeField] public GameObject  _Bullet;
    [Range(0.1f, 0.87f)]
    [Header("Время выстрела. 0.1 = Повершот, 0.87 Лонг шот")]
    [SerializeField] public float ShootiongTime=0.44f;
    

    public static event Action <GameObject> EventTutorialShoot;

    // Use this for initialization
    void Awake () {
        t = 0;
        startrot = _Bullet.transform.rotation;
        startpos = _Bullet.gameObject. transform.position;
        rb = _Bullet.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        cur = 0;
    
       
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        GameProcess.CurrentAim = Aim.transform.position;
        t += Time.fixedDeltaTime;
        if (t > 1)
        {
           
            rb.isKinematic = true;
            //(transform.position - Aims[cur].transform.position).magnitude)

            _Bullet.transform.position = startpos;
            _Bullet.transform.rotation = startrot;
        }
        if (t > 2)
        {
            rb.isKinematic = false;
           
                GameProcess.ShotTime = ShootiongTime;
                EventTutorialShoot(_Bullet);
           

            t = 0;
            cur++;
            if (cur == 2) cur = 0;
        }
	}
}
