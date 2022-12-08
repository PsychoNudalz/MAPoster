using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField]
    private string[] tags;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!tags.Contains(col.tag))
        {
            return;
        }

        onTriggerEnter.Invoke();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!tags.Contains(col.tag))
        {
            return;
        }

        onTriggerExit.Invoke();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (!tags.Contains(col.tag))
        {
            return;
        }

        onTriggerStay.Invoke();
    }
}