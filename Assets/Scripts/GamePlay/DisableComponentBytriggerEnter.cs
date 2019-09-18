using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableComponentBytriggerEnter : MonoBehaviour {
    [SerializeField] ConfigurableJoint joint;
	// Use this for initialization
	void Start () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Destroy(joint);
            joint.zMotion = ConfigurableJointMotion.Free;
            joint.xMotion = ConfigurableJointMotion.Free; ;
            joint.yMotion = ConfigurableJointMotion.Free; ;
            joint.angularXMotion = ConfigurableJointMotion.Free;
            joint.angularYMotion = ConfigurableJointMotion.Free;
            joint.angularZMotion = ConfigurableJointMotion.Free;

        };

    }
    // Update is called once per frame
    void Update () {
		
	}
}
