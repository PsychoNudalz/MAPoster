using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public enum EntityState{
    Placed,
    Grabbed,
    Placing,
    Death
}
public class EntityObject : MonoBehaviour,IPointerEnterHandler,IPointerDownHandler, IPointerUpHandler
{

    [SerializeField]
    protected EntityState entityState= EntityState.Placed;
    
    [Space(5f)]
    [Header("Grabbing")]
    [SerializeField]
    private bool canGrab = true;
    [SerializeField]
    private UnityEvent onGrabEvent;
    [SerializeField]
    private UnityEvent onReleaseEvent;

    [SerializeField]
    private float placeTimer = 2f;

    [Header("Death")]
    [SerializeField]
    private float delayDestroyTimer;

    [SerializeField]
    private UnityEvent onDeathEvents;

    [SerializeField]
    private bool disableOnDeath = false;
    

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
        SwitchState(EntityState.Placed);
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
        print("Entity: "+this+" "+previousState+" --> "+entityState);
    }


    public virtual void OnDeath()
    {
        SwitchState(EntityState.Death);
        onDeathEvents.Invoke();
        if (disableOnDeath)
        {
            StartCoroutine(DelayDeath());
        }
        else
        {
            Destroy(gameObject,delayDestroyTimer);
        }
    }

    IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(delayDestroyTimer);
        gameObject.SetActive(false);
    }
}
