using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FuzeBoxController : MonoBehaviour
{
    [SerializeField]
    private WireReceiverController[] wireReceiverControllers;


    [SerializeField]
    private float checkFuzeTime = 5f;

    [SerializeField]
    private UnityEvent onComplete;

    private Coroutine checkCoroutine;

    public static FuzeBoxController Current;
    // Start is called before the first frame update

    private void Awake()
    {
        wireReceiverControllers = GetComponentsInChildren<WireReceiverController>();
        if (!Current)
        {
            Current = this;
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void UpdateReceivers()
    {
        Current.UpdateReceivers_Self();
    }

    public void UpdateReceivers_Self()
    {
        if (checkCoroutine != null)
        {
            StopCoroutine(checkCoroutine);
        }

        StartCoroutine(DelayCheck());
    }

    public void CheckReceivers()
    {
        foreach (WireReceiverController currentWireReceiverController in wireReceiverControllers)
        {
            if (!currentWireReceiverController.ReceiverState.Equals(ReceiverState.Correct))
            {
                print("Fuze failed");

                return;
            }
        }

        onComplete.Invoke();
        print("Fuze clear");
    }

    IEnumerator DelayCheck()
    {
        yield return new WaitForSeconds(checkFuzeTime);
        CheckReceivers();
    }
}