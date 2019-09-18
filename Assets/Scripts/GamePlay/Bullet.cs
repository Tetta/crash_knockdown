using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
 Кастомные настройки для выстрела объектом, данный скрипт должен висеть на любом объекте "ПУЛЯ"
 Срабатывает по Событию из Game
*/
public class Bullet : MonoBehaviour {

    public static event Action EventBulletCollided;
    [Header("Повершот:")]
    [SerializeField] public float MaxForce = 500;
    [SerializeField] public float maxCompensation = 22;
    [Header("ЛонгШот:")]

    [SerializeField] public float MinForce = 200;
    [SerializeField] public float minCompensation = 50;

    [Header("Кламп Дальности броска: Умножает силу выстрела от расстояния!")]
    float Distance;
    [SerializeField] public Vector2 Clamp = new Vector2(1, 12);
    bool _shoot,_isPowershot,collided;
    float _shootTime;
    private Rigidbody rb;
    Vector3 LastPos;
    Vector3 rand;
    Vector3 Direction;

    private GameObject tempGO;
    private PhysicMaterial mat;
    private int count;
    private int maxCount;
    private int tempCount;

    private void Awake()
    {
      
        collided = false;
        _isPowershot = false;
    
        _shootTime = 0;
        _shoot = false;

        if (GameProcess.PowerMult == Vector2.zero) GameProcess.PowerMult = Vector2.one;

        rb = GetComponent<Rigidbody>();

        GameProcess.EventShoot += OnGameShootEvent;

        Tutorial.EventTutorialShoot += OnGameShootEvent;

        MaxForce = 1500;
        maxCompensation = 35;
        MinForce = 433;
        minCompensation = 100;

        count = 0;
        StartCoroutine(TestHeight());
    }
   

    

 IEnumerator TestHeight()
{

    yield return new WaitForSeconds(UnityEngine.Random.Range(2, 3));

        if (transform.position.y < -20)
        {
            Destroy(gameObject);
            Debug.Log("Bullet destroyed By Height -20");
        }
}
    private void OnDestroy()
    {

        Tutorial.EventTutorialShoot -= OnGameShootEvent;
        GameProcess.EventShoot -= OnGameShootEvent;

    }

    void OnGameShootEvent(GameObject go)
    {

        if (this.gameObject == go)
        {

            rand = UnityEngine.Random.onUnitSphere * 100;
            _shootTime = 0;
            _shoot = true;
            Direction = (GameProcess.CurrentAim - gameObject.transform.position);
            Vector3 Normalized = Direction.normalized;
            float HelpHeight = Mathf.Clamp(Direction.y, 1f, 4);

            Distance = Direction.magnitude;            
            Distance = Mathf.Clamp(Distance, 1,25.5f);

            Vector3 MinForceTemp = rb.mass * Normalized * MinForce + rb.mass * Vector3.up * (Distance  ) * minCompensation;

            Vector3 MaxForceTemp =  rb.mass * Normalized *MaxForce+ rb.mass * Vector3.up*(Distance )*maxCompensation;

            GameProcess.ShotTime = Mathf.Clamp(GameProcess.ShotTime, 0, 45f);

            if (GameProcess.ShotTime > 0.44f) GameProcess.ShotTime = 1;

            if (GameProcess.ShotTime < 0.15f) GameProcess.ShotTime = 0;

            Vector3 MixForce = Vector3.Lerp(MaxForceTemp, MinForceTemp,   GameProcess.ShotTime);

            if (GameProcess.ShotTime<0.15f)
            {

                //rb.isKinematic = true;
                _isPowershot = true;

            }

         //   Debug.Log("Force: " + MixForce.magnitude);
         //   Debug.Log("Distance: " + Distance);
            //Debug.Log("HelpHeight: " + HelpHeight);
            
            rb.AddForce(MixForce / Time.timeScale);
            //gameObject.AddComponent<ShootCollision>();
            //gameObject.AddComponent<AutoAim>();
            //gameObject.AddComponent<BulletSFX>();
            //rb.isKinematic = true;

        }

    }

    void TurnOnPhys()
    {

        if (_shoot)
        {

            if (!_isPowershot)
              
            {
                //   rb.isKinematic = false;

                _shoot = false;

                _shootTime = 0;

                Vector3 Direction = (GameProcess.CurrentAim - gameObject.transform.position);


                rb.AddForce(Direction * 600);

             //   Debug.Log("Force =" + Direction * 100000);


            }

        }
          
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((GameProcess.BulletInFly == gameObject)||(GameProcess.LastBulletInFly == gameObject))
            {
            if (_isPowershot)
                rb.AddForce(Direction * 100);  // TurnOnPhys();
            if (GameProcess.BulletInFly == gameObject)
                GameProcess.LastBulletInFly = GameProcess.BulletInFly;
            GameProcess.BulletInFly = null;
            if (EventBulletCollided != null)
                EventBulletCollided();
            //
            if (collision.gameObject.name.Length >= 5)
            {

                if (collision.gameObject.name.Length >= 12 && collision.gameObject.name.Substring(0, 12) == "table_planks") SoundAndMusik.Instance.isFly = false;
                else if (collision.gameObject.name.Length == 5 && collision.gameObject.name == "polka") SoundAndMusik.Instance.isFly = false;
                else if (collision.gameObject.name.Length == 6 && collision.gameObject.name == "polka2") SoundAndMusik.Instance.isFly = false;
                else if (collision.gameObject.name.Length == 9 && collision.gameObject.name == "podstavka" || collision.gameObject.name == "microwave") SoundAndMusik.Instance.isFly = false;

            }

            mat = collision.collider.material;
            tempGO = collision.gameObject;

            //домножаю на 7, чтобы сравнять максимальное количество точек, типа по 7 точек в среднем.
            count += collision.contacts.Length;
            if (collision != null && collision.gameObject != null && collision.gameObject.transform != null && collision.gameObject.transform.parent != null)
                tempCount = collision.gameObject.transform.parent.childCount;
            if (tempCount > maxCount) maxCount = tempCount;

            if (maxCount != 0)
            {

                switch (mat.name)
                {

                    case "ceramic (Instance)":
                        SoundAndMusik.Instance.GetBreakGlass(tempGO, ((2.49f * count) / maxCount));
                        //(tempGO, Mathf.Clamp(((count / maxCount) * 3.0f), 0.0f, 3.0f))
                        break;

                    case "ceramic":
                        SoundAndMusik.Instance.GetBreakGlass(tempGO, ((2.49f * count) / maxCount));
                        break;

                }

            }

        }
    }
    private void OnCollisionStay(Collision collision)
    {
        count += collision.contacts.Length;
    }

    // Update is called once per frame
    void FixedUpdate () {

        if (_shoot)
        {

            if (!_isPowershot)
            {

                rb.AddTorque(rand);
                if (!collided)
                {
                    Vector3 TempVec = (GameProcess.CurrentAim - gameObject.transform.position);

                    float dis = new Vector3(TempVec.x, 0, TempVec.z).magnitude;

                    if (dis < 0.5f * Distance)
                    {

                        _shootTime += Time.fixedDeltaTime;

                        transform.position = Vector3.Lerp(transform.position, GameProcess.CurrentAim, _shootTime);

                        //if (dis > 0.5f * Distance)                
                        //transform.position += 0.33f*Distance* Vector3.up*Time.fixedDeltaTime;                
                        //else                
                        //transform.position -= 0.33f * Distance * Vector3.up*Time.fixedDeltaTime;

                        if (dis < 0.15f * Distance) TurnOnPhys();

                    }
                }
            }
            else
            {

                _shootTime += Time.fixedDeltaTime;

                rb.AddForce(Direction);

                if (_shootTime > 1) transform.position = Vector3.one * -100;

                _shoot = false;

            }

            //Звук от Коляна
            if (TimeManager.SlowMo && !SoundAndMusik.Instance.isSlowMo)
            {

                SoundAndMusik.Instance.GetSlowMotion(gameObject);
                SoundAndMusik.Instance.isSlowMo = true;

            }

        }

    }

}
