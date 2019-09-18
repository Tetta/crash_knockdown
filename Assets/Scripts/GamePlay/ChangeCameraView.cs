using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraView : MonoBehaviour
{
    float t;
    // Use this for initialization
    void Awake()
    {
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameProcess.Cameras.Count != 1)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Slerp(transform.position, GameProcess.Cameras[GameProcess.CurrentCamera].transform.position, t - 0.5f);
            transform.rotation = Quaternion.Slerp(transform.rotation, GameProcess.Cameras[GameProcess.CurrentCamera].transform.rotation, t - 0.5f);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, GameProcess.CamerasFOV[GameProcess.CurrentCamera], t - 0.5f);
            if (t > 1.5f)
            {
                float maxpos = 2;
                for (int i = 0; i < GameProcess.DestroyObjects.Length; i++)
                {
                    float dist = Vector3.Distance(GameProcess.DestroyObjects[i].transform.position, Camera.main.transform.position);
                    if (maxpos <dist) maxpos = dist;
                }


                GameProcess.ColliderForShooting.transform.localRotation = Quaternion.Euler(Vector3.zero);
                GameProcess.ColliderForShooting.transform.localPosition = new Vector3(0,0,maxpos+0.1f);
                Destroy(this);
            }
        }
        else Destroy(this);
    }
}
