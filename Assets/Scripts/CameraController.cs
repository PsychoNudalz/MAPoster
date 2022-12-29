using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float lerpSpeed = 10f;

    [SerializeField]
    private Vector2 maxMousePos = new Vector2(4, 4);

    [SerializeField]
    private LayerMask mouseLayer;

    [Header("Layers")]
    [Header("Rotation")]
    [SerializeField]
    private Transform rotationLayer;

    [SerializeField]
    private Vector2 maxEulerRotation = new Vector2(10f, 10f);

    [SerializeField]
    private AnimationCurve rotationToPosMap_x;

    [SerializeField]
    private AnimationCurve rotationToPosMap_y;

    private Vector3 rotationLerpTarget;

    [Space(5)]
    [Header("Move")]
    [SerializeField]
    private Transform moveLayer;

    [SerializeField]
    private Vector2 maxMove = new Vector2(2f, 2f);

    [SerializeField]
    private AnimationCurve moveToPosMap_x;

    [SerializeField]
    private AnimationCurve moveToPosMap_y;

    private Vector3 moveLerpTarget;


    [Space(5)]
    [Header("Shake")]
    [SerializeField]
    private Transform screenShakeLayer;

    private Coroutine shakeCoroutine;

    [Space(10f)]
    [SerializeField]
    private Transform mouseWorld;

    [SerializeField]
    private Transform inverseMouseWorld;


    private Mouse mouse;
    [SerializeField]
    private Camera rayCamera;

    [SerializeField]
    private Transform targetCameraTransform;

    [SerializeField]
    Transform mainCameraTransform;

    
    
    public static CameraController current;


    private void Start()
    {
        UpdateMousePos();
        UpdateMoveLayer();
        UpdateRotationLayer();

    }

    private void Awake()
    {
        current = this;
        mouse = Mouse.current;
        rayCamera = Camera.main;
    }

    private void LateUpdate()
    {
        UpdateMousePos();
        UpdateMoveLayer();
        UpdateRotationLayer();

    }

    private void Update()
    {
        
        UpdateLerp();
        UpdateCamera();
    }

    void UpdateMousePos()
    {
        //     Vector3 mousePos = mouse.position.ReadValue();
        //     mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //     mousePos.z = 0f;
        //     mouseWorld.position = mousePos;
        //     inverseMouseWorld.position = new Vector3(-mousePos.x, -mousePos.y, mousePos.z);
        Ray ray = rayCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        float mouseCastDistance = 1000f;
        Debug.DrawRay(ray.origin, ray.direction * mouseCastDistance, Color.cyan, 1f);
        Physics.Raycast(ray, out var raycastHit, mouseCastDistance, mouseLayer);
        Vector3 pos = raycastHit.point;
        pos.z = 0;
        mouseWorld.position = pos;
        inverseMouseWorld.position = new Vector3(-pos.x, -pos.y, pos.z);
    }


    void UpdateRotationLayer()
    {
        // Vector3 dir = mouseWorld.position - rotationLayer.position;
        // rotationLayer.forward = dir;
        // Vector3 rotationLayerEulerAngles = rotationLayer.eulerAngles;
        // rotationLayerEulerAngles.z = 0;
        // rotationLayer.eulerAngles = rotationLayerEulerAngles;


        float x = SampleCurveMouse(rotationToPosMap_x, mouseWorld.localPosition.x, maxMousePos.x, maxEulerRotation.x);
        float y = SampleCurveMouse(rotationToPosMap_y, mouseWorld.localPosition.y, maxMousePos.y, maxEulerRotation.y);
        rotationLerpTarget = new Vector3(y, x, 0);

        rotationLayer.localEulerAngles = rotationLerpTarget;
    }

    void UpdateMoveLayer()
    {
        float x = SampleCurveMouse(moveToPosMap_x, mouseWorld.position.x, maxMousePos.x, -maxMove.x);
        float y = SampleCurveMouse(moveToPosMap_y, mouseWorld.position.y, maxMousePos.y, -maxMove.y);

        // moveLerpTarget = new Vector3(x, y, moveLayer.position.z);

        moveLayer.position = new Vector3(x, y, moveLayer.position.z);
    }


    void UpdateLerp()
    {
    //     rotationLayer.localEulerAngles =
    //         Vector3.Lerp(rotationLayer.localEulerAngles, rotationLerpTarget, lerpSpeed * Time.deltaTime);
    //
    //     moveLayer.position = Vector3.Lerp(moveLayer.position, moveLerpTarget, lerpSpeed * Time.deltaTime);
    }

    void UpdateCamera()
    {
        mainCameraTransform.position = Vector3.Lerp(mainCameraTransform.position,targetCameraTransform.position,lerpSpeed*Time.deltaTime);
        mainCameraTransform.rotation = Quaternion.Lerp(mainCameraTransform.rotation,targetCameraTransform.rotation,lerpSpeed*Time.deltaTime);
    }

    float SampleCurveMouse(AnimationCurve curve, float pos, float maxMouse, float max)
    {
        return curve.Evaluate(pos / maxMouse) * max;
    }

    public static Vector3 GetNewMouse()
    {
        return current.mouseWorld.position;
    }


    public static void ShakeCamera_S(float duration = 0.5f, float magnitude = 0.1f)
    {
        current.ShakeCamera(duration, magnitude);
    }

    public void ShakeCamera(float duration = 0.5f, float magnitude = 0.5f)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }

        shakeCoroutine = StartCoroutine(Shake(duration, magnitude));
    }

    public IEnumerator Shake(float duration = 0.5f, float magnitude = 0.5f)
    {
        Vector3 originalPos = screenShakeLayer.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            screenShakeLayer.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        screenShakeLayer.localPosition = originalPos;
    }
}