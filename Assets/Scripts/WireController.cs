using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public enum WireChannel
{
    Red,
    Yellow,
    Blue,
    Green,
    White,
    Orange
}

public class WireController : MonoBehaviour
{
    [Header("Wire")]
    [SerializeField]
    private WireChannel wireChannel;

    [SerializeField]
    private Transform wireHead;

    [SerializeField]
    private bool useLerp = false;

    [SerializeField]
    private float lerpSpeed = 5;

    [SerializeField]
    private float deadZone = .2f;

    private Vector3 targetPosition;

    [Header("Line Renderer")]
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private Vector3 lineStartOffset = new Vector3(4, 0, 0);

    public WireChannel WireChannel => wireChannel;

    public void OnMouseDrag()
    {
        SetPosition(GetMousePos());
    }

    [ContextMenu("Update Colour")]
    public void UpdateColour()
    {
        switch (wireChannel)
        {
            case WireChannel.Red:
                lineRenderer.material.color = Color.red;
                lineRenderer.material.SetColor("_Color", Color.red);
                break;
            case WireChannel.Yellow:
                lineRenderer.material.color = Color.yellow;
                lineRenderer.material.SetColor("_Color", Color.yellow);
                break;
            case WireChannel.Blue:
                lineRenderer.material.color = Color.blue;
                lineRenderer.material.SetColor("_Color", Color.blue);
                break;
            case WireChannel.Green:
                lineRenderer.material.color = Color.green;
                lineRenderer.material.SetColor("_Color", Color.green);
                break;
            case WireChannel.White:
                lineRenderer.material.color = Color.white;
                lineRenderer.material.SetColor("_Color", Color.white);
                break;
            case WireChannel.Orange:
                Color orange = new Color(1f, .4f, 0f);
                lineRenderer.material.color = orange;
                lineRenderer.material.SetColor("_Color", orange);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Awake()
    {
        targetPosition = transform.position;
        InitialiseLine();
    }

    private void Start()
    {
        UpdateColour();
    }

    private void Update()
    {
        if (useLerp)
        {
            if (Vector3.Distance(transform.position, targetPosition) > deadZone)
            {
                MoveWire_Lerp();
            }
        }
        else
        {
            MoveWire();
        }
    }

    private void MoveWire()
    {
        Vector3 wireTarget = transform.InverseTransformPoint(targetPosition);

        try
        {
            lineRenderer.SetPosition(2, wireTarget);
        }
        catch (Exception e)
        {
            Debug.LogError("Line renderer error");
            Debug.LogWarning(e.StackTrace);
            throw;
        }

        if (wireHead)
        {
            wireHead.position = targetPosition;
        }
    }

    private void MoveWire_Lerp()
    {
        Vector3 wireTarget = transform.InverseTransformPoint(targetPosition);

        try
        {
            lineRenderer.SetPosition(2,
                Vector3.Lerp(lineRenderer.GetPosition(2), wireTarget, lerpSpeed * Time.deltaTime));
        }
        catch (Exception e)
        {
            Debug.LogError("Line renderer error");
            Debug.LogWarning(e.StackTrace);
            throw;
        }

        if (wireHead)
        {
            wireHead.position = targetPosition;
        }
    }

    public void SetPosition(Vector3 targetPos)
    {
        targetPosition = targetPos;
        MoveWire_Lerp();
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0;
        return worldPos;
    }

    void InitialiseLine()
    {
        lineRenderer.positionCount = 0;
        lineRenderer.positionCount = 3;
        lineRenderer.SetPositions(new Vector3[] { lineStartOffset, new Vector3(), new Vector3(0, 0, 0) });
    }
}