using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum ReceiverState
{
    None,
    Wrong,
    Correct
}
public class WireReceiverController : MonoBehaviour
{


    [Header("Receiver")]
    [SerializeField]
    private WireChannel wireChannel;

    [SerializeField]
    private ReceiverState receiverState;

    [Header("Events")]
    [SerializeField]
    private UnityEvent noneEvent;

    [SerializeField]
    private UnityEvent wrongEvent;

    [SerializeField]
    private UnityEvent correctEvent;

    public ReceiverState ReceiverState => receiverState;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.parent.TryGetComponent(out WireController wireController))
        {
            if (wireController.WireChannel.Equals(wireChannel))
            {
                SwitchState(ReceiverState.Correct);
            }
            else
            {
                SwitchState(ReceiverState.Wrong);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.parent.TryGetComponent(out WireController wireController))
        {
            if (!receiverState.Equals(ReceiverState.None))
            {
                SwitchState(ReceiverState.None);
            }
        }
    }

    void SwitchState(ReceiverState rs)
    {
        receiverState = rs;
        switch (rs)
        {
            case ReceiverState.None:
                noneEvent.Invoke();
                break;
            case ReceiverState.Wrong:
                wrongEvent.Invoke();
                break;
            case ReceiverState.Correct:
                correctEvent.Invoke();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(rs), rs, null);
        }
        print(gameObject+" switch to: "+receiverState.ToString());
        FuzeBoxController.UpdateReceivers();
    }
}