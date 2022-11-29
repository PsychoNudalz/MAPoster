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

    public void SetPointsRaycast(Vector3 start, Vector3 dir)
    {
        Vector3 end;
        RaycastHit2D hit = Physics2D.Raycast(start, dir, castRange, wallLayerMask);
        if (hit)
        {
            end = hit.point;
        }
        else
        {
            SetPoints(start,dir*castRange);

            return;
        }
        RaycastHit2D hitBack = Physics2D.Raycast(start, -dir, castRange, wallLayerMask);
        if (hitBack)
        {
            start = hitBack.point;
        }
        SetPoints(start,end);
    }

    public void SetPoints(Vector3 start, Vector3 end)
    {
        lineRenderer.SetPosition(0,start);
        lineRenderer.SetPosition(1,end);
    }
}
