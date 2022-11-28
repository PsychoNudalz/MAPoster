using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    Vector2 startDir;

    private float speedCurrent;

    [SerializeField]
    private float speedStart;
    
    
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (startDir.normalized*speedStart);
    }

    // Update is called once per frame
    void Update()
    {
        speedCurrent = rb.velocity.magnitude;

    }
}