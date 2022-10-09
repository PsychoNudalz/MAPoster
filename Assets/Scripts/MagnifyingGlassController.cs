using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingGlassController : MonoBehaviour
{
    [SerializeField]
    private float lerpSpeed = 5;

    [SerializeField]
    private float deadZone = .2f;
    private Vector3 targetPosition;

    private void Awake()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, targetPosition) > deadZone)
        {
            MoveLight();
        }
    }

    private void MoveLight()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
    }

    public void SetPosition(Vector3 targetPos)
    {
        targetPosition = targetPos;
        MoveLight();
    }
}