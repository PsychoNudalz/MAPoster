using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public enum EntityState{
    Placed,
    Grabbed,
    Placing
}
public class EntityObject : MonoBehaviour,IPointerEnterHandler,IPointerDownHandler, IPointerUpHandler
{

    [SerializeField]
    protected EntityState entityState= EntityState.Placed;
    
    [Space(5f)]
    [SerializeField]
    private bool canGrab = true;

    [SerializeField]
    private UnityEvent onGrabEvent;
    [SerializeField]
    private UnityEvent onReleaseEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        print("On Mouse Down "+gameObject);
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
        EntityState previousState = entityState;
        entityState = es;
        print("Entity: "+this+" "+previousState+" --> "+entityState);
    }
}
