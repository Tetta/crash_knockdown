using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCollision : MonoBehaviour
{
    float delay = 1f, Counts, Powers;
    // Use this for initialization
    void Awake()
    {
        delay = 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (delay > 1)
        {

            if (collision.collider.tag == "Oskolok")
            {
                //  AudioManager.PlaySound("DestroyGlass", 0);

                if (GameProcess.AlwaysSlowMotion)
                {
                    GameProcess.Particle.transform.position = collision.contacts[0].point;
                    GameProcess.Particle.transform.rotation = Quaternion.Euler(collision.contacts[0].normal);
                    GameProcess.ParticleSys.Play();
                    TimeManager.DoSlowmotion();
                    delay = 0;
                }
                else if (collision.relativeVelocity.sqrMagnitude > 50)
                {

                    GameProcess.Particle.transform.position = collision.contacts[0].point;
                    GameProcess.Particle.transform.rotation = Quaternion.Euler(collision.contacts[0].normal);
                    GameProcess.ParticleSys.Play();
                    delay = 0;
                    //     Debug.Log("Velocity: " + collision.relativeVelocity.sqrMagnitude);

                }
                else if (collision.relativeVelocity.sqrMagnitude > 50)
                {
                    GameProcess.Particle.transform.position = collision.contacts[0].point;
                    GameProcess.Particle.transform.rotation = Quaternion.Euler(collision.contacts[0].normal);
                    GameProcess.ParticleSys.Play();
                    delay = 0;

                }

            }

        }
            
    }
   
    // Update is called once per frame
    void Update()
    {
        delay += Time.deltaTime;
        if (delay > 5) Destroy(this);
       
    }
}

