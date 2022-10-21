using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class LightFlicker : MonoBehaviour
{
    [SerializeField]
    private Light2D light2D;

    [SerializeField]
    private AnimationCurve flickerPattern;

    [SerializeField]
    private Vector2 offsetRange = new Vector2(0, 1);

    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private float offset = 0;

    private float startIntensity;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        startIntensity = light2D.intensity;
        offset = Random.Range(offsetRange.x, offsetRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (light2D)
        {
            light2D.intensity = startIntensity * flickerPattern.Evaluate(Time.time*speed+offset);
        }
    }
}