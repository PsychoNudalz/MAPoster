using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // [Header("Light Controller")]
    // [SerializeField]
    // private LightController lightController;
    //
    // private bool isLight;
    //
    // [Header("Magnifying Controller")]
    // [SerializeField]
    // private MagnifyingGlassController magnifyingGlassController;

    private bool isMag;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        // if (lightController && isLight)
        // {
        //     MoveLight();
        // }
        //
        // if (magnifyingGlassController && isMag)
        // {
        //     MoveMag();
        // }
    }

    private void MoveLight()
    {
        var worldPos = GetMousePos();
        // lightController.SetPosition(worldPos);
    }

    private void MoveMag()
    {
        var worldPos = GetMousePos();
        // magnifyingGlassController.SetPosition(worldPos);
    }

    private static Vector3 GetMousePos()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;
        return worldPos;
    }


    public void OnLight(InputAction.CallbackContext callbackContext)
    {
        // isLight = callbackContext.performed;
    }

    public void OnMag(InputAction.CallbackContext callbackContext)
    {
        isMag = callbackContext.performed;
    }
}