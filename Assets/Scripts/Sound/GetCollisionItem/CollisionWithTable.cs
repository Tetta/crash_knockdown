using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithTable : MonoBehaviour {

    /* 1 - металл Ball;
     * 2 - металл;
     * 3 - овощи;
     * 4 - стекло;
     * 5 - дерево;
     */


    private GameObject[] allBullets; 

    private List<string> metallMass = new List<string>();
    private List<string> vegieMass = new List<string>();
    private List<string> glassMass = new List<string>();
    private List<string> woodMass = new List<string>();

    private PhysicMaterial tempMaterial;

    private MeshCollider[] massMesh;
    private List<string> massMeshGlass = new List<string>();

   
    private float timer = 0.5f;
    private bool isSound = false;

    private float minSila;
    private float maxSila;
    private Vector3 tempSila;
      

    void Awake()
    {

        Initialized();

    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (timer > 0) timer -= Time.deltaTime;
        else if (!isSound) isSound = true;
		
	}

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision != null && isSound)
        {
            
            name = collision.gameObject.name;

            SortName(collision.gameObject.name, collision.gameObject);

        }

    }

    private void Initialized()
    {

        allBullets = GameObject.FindGameObjectsWithTag("Bullet");

        massMesh = FindObjectsOfType<MeshCollider>();

        for (int i=0; i < massMesh.Length; i++)
        {

            if (massMesh[i].material.name == "ceramic" | massMesh[i].material.name == "ceramic (Instance)")
            {

                massMeshGlass.Add(massMesh[i].name);
                SoundAndMusik.Instance.AddMassMeshGlassOnScene(massMesh[i].name);

            }

        }
        
        if (allBullets != null)
        {

            SoundAndMusik.Instance.SetDellItemOnMass();

            for (int i = 0; i < allBullets.Length; i++)
            {

                tempMaterial = allBullets[i].GetComponent<MeshCollider>().material;

                if (tempMaterial.name == "metal (Instance)")
                {

                    metallMass.Add(allBullets[i].name);
                    SoundAndMusik.Instance.AddMassMetallOnScene(allBullets[i].name);

                }
                else if (tempMaterial.name == "wood (Instance)")
                {

                    woodMass.Add(allBullets[i].name);
                    SoundAndMusik.Instance.AddMassWoodOnScene(allBullets[i].name);

                }
                else if (tempMaterial.name == "flowers (Instance)")
                {

                    vegieMass.Add(allBullets[i].name);
                    SoundAndMusik.Instance.AddMassVegieOnScene(allBullets[i].name);

                }
                else if (tempMaterial.name == "ceramic (Instance)")
                {

                    glassMass.Add(allBullets[i].name);
                    SoundAndMusik.Instance.AddMassGlassOnScene(allBullets[i].name);

                }

            }

        }

    }
    
    private void SortName(string name, GameObject GO)
    {

        if (name.Length >= 13)
        {

            if (name.Substring(0, 13) == "CustomBullet_")
            {

                tempSila = GO.GetComponent<Rigidbody>().velocity;
                minSila = -6;
                
                if (tempSila.x > minSila) minSila = tempSila.x;
                if (tempSila.y > minSila) minSila = tempSila.y;
                if (tempSila.z > minSila) minSila = tempSila.z;

                if (minSila < 6.0f)
                {

                    SoundAndMusik.Instance.GetFallBallToTable(GO, 2);

                }
                else if (minSila >= 6.0f & minSila < 13)
                {

                    SoundAndMusik.Instance.GetFallBallToTable(GO, 1);

                }
                else if (minSila >= 13.0f)
                {

                    SoundAndMusik.Instance.GetFallBallToTable(GO, 0);

                }

            }

        }

        if (metallMass != null)
        {

            for (int i = 0; i < metallMass.Count; i++)
            {

                if (metallMass[i] == name)
                {

                    SoundAndMusik.Instance.GetFallMetallToTableMore(GO);

                }

            }

        }

        if (woodMass != null)
        {

            for (int i = 0; i < woodMass.Count; i++)
            {

                if (woodMass[i] == name) SoundAndMusik.Instance.GetFallWood(GO);

            }

        }

        if (vegieMass != null)
        {

            for (int i = 0; i < vegieMass.Count; i++)
            {

                if (vegieMass[i] == name) SoundAndMusik.Instance.GetFallVeggieToTable(GO);

            }

        }

        if (glassMass != null)
        {

            for (int i = 0; i < glassMass.Count; i++)
            {

                if (glassMass[i] == name) SoundAndMusik.Instance.GetFallGlass(GO);

            }

        }

        if (massMeshGlass != null)
        {

            for (int i = 0; i < massMeshGlass.Count; i++)
            {

                if (massMeshGlass[i] == name & GO != null)
                {

                    if (GO.GetComponent<Rigidbody>() == null) GO.AddComponent<Rigidbody>();

                    SoundAndMusik.Instance.GetFallGlass(GO, 0.5f);

                }

            }

        }

    }

}
