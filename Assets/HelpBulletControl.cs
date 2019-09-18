using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpBulletControl : MonoBehaviour {
    Image img;
    float t;
    GameObject Effect;
    bool on;
	// Use this for initialization
	void Awake () {
        t = 0;
        on = false;
        img = GetComponent<Image>();
        img.enabled = false;
        GameProcess.EventEmptyHands += OnEmptyHands;
        GameProcess.EventHandsNotEmpty += OnNotEmptyHands;
        GameProcess.EventWin += OnWin;
        Effect = Instantiate(Resources.Load("Starglow_Object")) as GameObject;
        Effect.SetActive(false);

    }
    private void OnDestroy()
    {
        GameProcess.EventEmptyHands -= OnEmptyHands;
        GameProcess.EventHandsNotEmpty -= OnNotEmptyHands;
        GameProcess.EventWin-= OnWin;
    }
    void OnWin()
    {
        Destroy(gameObject);
    }
    void OnEmptyHands()
    {
        on = true;
        t = 0;
     

    }    
    void OnNotEmptyHands()
    {
      Effect.SetActive(false) ;
        on = false;
        t = 0;

    }
    // Update is called once per frame
    void Update () {
        if (on)
        {
            t += Time.deltaTime;
            if (t > 1)
            {
                t = 0;
                int z = 0;
                GameObject[] o = GameObject.FindGameObjectsWithTag("Bullet");

                for (int i = 0; i < o.Length; i++)
                {
                    Vector2 v = Camera.main.WorldToScreenPoint(o[i].transform.position);
                    if ((v.x < Screen.width) && (v.x > 0) && (v.y > 0) && (v.y < Screen.height))
                    {
                        RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(v);
                        if (Physics.Raycast(Camera.main.transform.position, ray.direction, out hit, Mathf.Infinity))
                            if (hit.collider.tag == "Bullet")
                            {
                                Debug.Log("Help Activated!");
                                Effect.transform.position = o[i].transform.position;
                                Effect.transform.parent = null;
                                Effect.transform.rotation = o[i].transform.rotation;
                                Effect.transform.localScale = o[i].transform.localScale;

                                var shape = Effect.GetComponent<ParticleSystem>().shape;
                                shape.enabled = true;
                                shape.shapeType = ParticleSystemShapeType.Mesh;
                                shape.mesh = o[i].GetComponent<MeshFilter>().mesh;
                                Effect.SetActive(true);
                                Effect.GetComponent<ParticleSystem>().Stop();
                                Effect.GetComponent<ParticleSystem>().Play();
                                z++;
                                break;
                            }
                    }

                }
                if(z==0)
                {
                    GameProcess.CheckLose();
                }


            }
        }
	}
}
