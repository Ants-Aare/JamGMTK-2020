using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class TimedEventBehaviour : EventBehaviour
{
    [Header("Values")]
    [SerializeField]
    private float delay;
    [SerializeField]
    private bool invokeOnStart;

    void Start()
    {
        if(invokeOnStart)
        {
            InvokeEvent();
        }
    }

    public override void InvokeEvent()
    {
        StartCoroutine(InvokeAfter(delay));
    }
    private IEnumerator InvokeAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        unityEvent.Invoke();
    }
}   