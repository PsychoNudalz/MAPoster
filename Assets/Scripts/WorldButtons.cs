using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldButtons : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnClick;

    private void OnMouseDown()
    {
        print("On Mouse Down "+gameObject);
        OnClick.Invoke();
    }
}