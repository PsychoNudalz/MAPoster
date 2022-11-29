using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class EnemyEntity : EntityObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBehaviour();
    }
    

    void UpdateBehaviour()
    {
        switch (entityState)
        {
            case EntityState.Placed:
                break;
            case EntityState.Grabbed:
                UpdateBehaviour_Grabbed();
                break;
            case EntityState.Placing:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void UpdateBehaviour_Grabbed()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 position = Camera.main.ScreenToWorldPoint(mousePos);
        position.z = transform.position.z;
        transform.position = position;
    }
    
    public virtual void OnGrabDrag(Vector3 pos)
    {
        
    }
}