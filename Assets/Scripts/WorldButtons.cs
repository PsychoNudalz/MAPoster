using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WorldButtons : MonoBehaviour,IPointerEnterHandler,IPointerDownHandler
{
    [SerializeField]
    private UnityEvent OnClick;

    // private void OnMouseDown()
    // {
    //     print("On Mouse Down "+gameObject);
    //     // OnClick.Invoke();
    // }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("MouseEnter");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("On Mouse Down "+gameObject);
        OnClick.Invoke();
    }
}