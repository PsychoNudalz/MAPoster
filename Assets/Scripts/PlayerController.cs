using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Light Controller")]
    [SerializeField]
    private LightController lightController;

    [SerializeField]
    private bool isLight;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if (lightController && isLight)
        {
            MoveLight();
        }
    }

    private void MoveLight()
    {
        
        Vector3 mousePos = Mouse.current.position.ReadValue();   
        mousePos.z=Camera.main.nearClipPlane;
        Vector3 worldPos=Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;
        lightController.SetPosition(worldPos);
    }

    public void OnLight(InputAction.CallbackContext callbackContext)
    {
        isLight = callbackContext.performed;
    }
}