using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MovementPredictionScript : MonoBehaviour
{
    [SerializeField]
    private LayerMask wallLayerMask;

    [SerializeField]
    private float castRange = 100f;

    private bool isStart = true;    
    
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (!lineRenderer)
        {
            Debug.LogError("No lIne Renderer");
            enabled = false;
        }
    }

    private void Update()
    {
        if (!isStart)
        {
            UpdateStartPoint();
        }
    }

    public void SetPointsRaycast(Vector3 start, Vector3 dir)
    {
        if (isStart)
        {
            isStart = false;
        }
        Vector3 end;
        RaycastHit2D hit = Physics2D.Raycast(start, dir, castRange, wallLayerMask);
        if (hit)
        {
            end = hit.point;
        }
        else
        {
            SetPoints(dir*castRange);

            return;
        }

        SetPoints(end);
    }

    public void SetPoints( Vector3 end)
    {
        lineRenderer.SetPosition(1,end);
    }

    public void UpdateStartPoint()
    {
        lineRenderer.SetPosition(0,transform.position);

    }
}
