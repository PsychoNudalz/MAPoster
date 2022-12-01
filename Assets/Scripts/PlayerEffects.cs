using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEffects : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onCollisionEvent;

    [SerializeField]
    private UnityEvent onTakeDamageEvent;

    [SerializeField]
    private UnityEvent onDeathEvent;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        onCollisionEvent.Invoke();
    }

    public void OnTakeDamage()
    {
        onTakeDamageEvent.Invoke();
    }

    public void OnDeathEvent()
    {
        onDeathEvent.Invoke();
    }
}