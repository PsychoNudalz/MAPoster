using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Trigger2DEvents : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onTriggerEnter;
    [SerializeField]
    private UnityEvent onTriggerExit;
    [SerializeField]
    private UnityEvent onTriggerStay;

    private void OnTriggerEnter2D(Collider2D col)
    {
        onTriggerEnter.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        onTriggerExit.Invoke();

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        onTriggerStay.Invoke();

    }
}
