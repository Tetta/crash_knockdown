using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDisabledInState : MonoBehaviour {

    [SerializeField]
    private int[] states;

    [SerializeField]
    private Collider[] colliders;

    private bool Enabled
    {
        set
        {
            foreach (var collider in colliders)
                collider.enabled = value;
        }
    }

    private void Awake()
    {
        MainMenu.EventChangeState += OnChangeState;
    }

    private void OnDestroy()
    {
        MainMenu.EventChangeState -= OnChangeState;

    }

    private void OnChangeState()
    {
        if (Array.IndexOf(states, GameProcess.State) != -1)
        {
            Enabled = false;
        }
        else
        {
            Enabled = true;
        }
    }
}
