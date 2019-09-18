using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragility : MonoBehaviour {
    [Header("Разбитость/ больше - проще выиграть")]
    [Range(0, 1f)]
    [SerializeField] float FragilityPercent = 0.5f;
	// Use this for initialization
	void Awake () {
        for (int i = 0; i < GameProcess.DestroyObjects.Length; i++)
            if (GameProcess.DestroyObjects[i] == gameObject) GameProcess.DestroyobjectsFragility[i] = FragilityPercent;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
