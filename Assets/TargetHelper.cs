using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetHelper : MonoBehaviour
{
    GameObject Effect;
    float delay;
    Image img;
    bool _showHelp;
    // Use this for initialization
    void Awake()
    {
        delay = 0;
        img = GetComponent<Image>();
        _showHelp = false;
        img.enabled = false;

        Effect = Instantiate(Resources.Load("HelpDestroy")) as GameObject;
        Effect.SetActive(false);

        GameProcess.EventShoot += OnShot;

    }
    private void OnDestroy()
    {
        GameProcess.EventShoot -= OnShot;

    }
    void OnShot(GameObject go)
    {
        _showHelp = false;
        Effect.SetActive(false);
        GameProcess.HelpTime = 0;
        delay = 0;

    }
    // Update is called once per frame
    void Update()
    {
        if (_showHelp == true)
        {
            delay += Time.deltaTime;
            if (delay > 3)
            {
                GameProcess.HelpTime = 0;
                delay = 0;
                _showHelp = false;
                Effect.SetActive(true);
            }
        }
        if (GameProcess.HelpTime > 3)
        {
          //  Debug.Log("aaaaa");
            if (GameProcess.CameraCheckObjects.Count == 1)
            {
              

                for (int i = 0; i < GameProcess.DestroyObjects.Length; i++)
                    if (GameProcess.CrackedObjects[i] == 0)
                    {
                        GameProcess.HelpTime = 0;
                        Debug.Log("Help Object Activated!");
                        Effect.transform.position = GameProcess.DestroyObjects[i].transform.position;
                        Effect.transform.parent = null;
                        Effect.transform.rotation = GameProcess.DestroyObjects[i].transform.rotation;
                        Effect.transform.localScale = GameProcess.DestroyObjects[i].transform.localScale;

                        var shape = Effect.GetComponent<ParticleSystem>().shape;
                        shape.enabled = true;
                        shape.shapeType = ParticleSystemShapeType.Mesh;
                        shape.mesh = GameProcess.DestroyObjects[i].transform.GetChild(GameProcess.DestroyObjects[i].transform.childCount - 1).GetComponent<MeshFilter>().mesh;
                        Effect.SetActive(true);
                        Effect.GetComponent<ParticleSystem>().Stop();
                        Effect.GetComponent<ParticleSystem>().Play();
                        _showHelp = true;
                        return;
                    }







            }
            else
                for (int k = 0; k < GameProcess.CameraCheckObjects[GameProcess.CurrentCamera].Count; k++)
                {
                    if (GameProcess.CameraCheckObjects[GameProcess.CurrentCamera][k].transform.childCount > 2)
                    {
                        GameProcess.HelpTime = 0;
                 //        Debug.Log("Help Object Activated!");
                        Effect.transform.position = GameProcess.CameraCheckObjects[GameProcess.CurrentCamera][k].transform.position;
                        Effect.transform.parent = null;
                        Effect.transform.rotation = GameProcess.CameraCheckObjects[GameProcess.CurrentCamera][k].transform.rotation;
                        Effect.transform.localScale = GameProcess.CameraCheckObjects[GameProcess.CurrentCamera][k].transform.localScale;

                        var shape = Effect.GetComponent<ParticleSystem>().shape;
                        shape.enabled = true;
                        shape.shapeType = ParticleSystemShapeType.Mesh;
                        shape.mesh = GameProcess.CameraCheckObjects[GameProcess.CurrentCamera][k].transform.GetChild(GameProcess.CameraCheckObjects[GameProcess.CurrentCamera][k].transform.childCount - 1).GetComponent<MeshFilter>().mesh;
                        Effect.SetActive(true);
                        Effect.GetComponent<ParticleSystem>().Stop();
                        Effect.GetComponent<ParticleSystem>().Play();
                        _showHelp = true;
                        return;
                    }



                }
            }
            
           

        }
    }
 