using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public enum EntityState
{
    Placed,
    Grabbed,
    Placing,
    Death
}

public class EntityObject : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    protected EntityState entityState = EntityState.Placed;

    [Space(5f)]
    [Header("Grabbing")]
    [SerializeField]
    private bool canGrab = true;

    [SerializeField]
    private UnityEvent onGrabEvent;

    [SerializeField]
    private UnityEvent onReleaseEvent;


    [Space(5f)]
    [Header("Placing")]
    [SerializeField]
    private float placeTimer = 1f;

    private float placeTimeNow = 0;

    [SerializeField]
    private SpriteRenderer placeTimerRing;

    [Header("Death")]
    [SerializeField]
    private float delayDestroyTimer;

    [SerializeField]
    private UnityEvent onDeathEvents;

    [SerializeField]
    private bool disableOnDeath = false;

    [Header("Components")]
    [SerializeField]
    private Collider2D[] mainBodyCollider;

    private void Start()
    {
        StartBehaviour();
    }

    protected virtual void StartBehaviour()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        UpdateBehaviour();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnRelease();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnGrab();
    }

    public virtual void OnGrab()
    {
        SwitchState(EntityState.Grabbed);
        onGrabEvent.Invoke();
    }


    public virtual void OnRelease()
    {
        SwitchState(EntityState.Placing);
        onReleaseEvent.Invoke();
    }

    public virtual void SwitchState(EntityState es)
    {
        if (es == EntityState.Death)
        {
            return;
        }

        EntityState previousState = entityState;
        entityState = es;
        print("Entity: " + this + " " + previousState + " --> " + entityState);

        switch (es)
        {
            case EntityState.Placed:
                SwitchState_Placed();
                break;
            case EntityState.Grabbed:
                SwitchState_Grabbed();
                break;
            case EntityState.Placing:
                SwitchState_Placing();
                break;
            case EntityState.Death:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(es), es, null);
        }
    }

    protected virtual void SwitchState_Placing()
    {
        placeTimeNow = placeTimer;
        
    }
    
    protected virtual void SwitchState_Placed()
    {
        SetAllMainBodyCollider(true);

    }


    protected virtual void SwitchState_Grabbed()
    {
        SetAllMainBodyCollider(false);
    }

    
    protected virtual void UpdateBehaviour()
    {
        switch (entityState)
        {
            case EntityState.Placed:
                UpdateBehaviour_Placed();
                break;
            case EntityState.Grabbed:
                UpdateBehaviour_Grabbed();
                break;
            case EntityState.Placing:
                UpdateBehaviour_Placing();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected virtual void UpdateBehaviour_Placed()
    {
    }

    protected virtual void UpdateBehaviour_Grabbed()
    {
        // Vector3 mousePos = Mouse.current.position.ReadValue();
        // Vector3 position = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 position = CameraController.GetNewMouse();
        position.z = transform.position.z;
        transform.position = position;
    }
    
    protected virtual void UpdateBehaviour_Placing()
    {
        placeTimeNow -= Time.deltaTime;
        if (placeTimeNow <= 0f)
        {
            placeTimeNow = 0f;
            SwitchState(EntityState.Placed);
        }

        
        if (placeTimerRing)
        {
            placeTimerRing.material.SetFloat("_CircleStrength",placeTimeNow/placeTimer);
        }
    }


    public virtual void OnDeath()
    {
        if (entityState == EntityState.Placing)
        {
            return;
        }
        SwitchState(EntityState.Death);
        onDeathEvents.Invoke();
        if (disableOnDeath)
        {
            StartCoroutine(DelayDeath());
        }
        else
        {
            Destroy(gameObject, delayDestroyTimer);
        }
    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(delayDestroyTimer);
        gameObject.SetActive(false);
    }

    protected void SetAllMainBodyCollider(bool b)
    {
        foreach (Collider2D c in mainBodyCollider)
        {
            c.enabled = b;
        }
    }
}