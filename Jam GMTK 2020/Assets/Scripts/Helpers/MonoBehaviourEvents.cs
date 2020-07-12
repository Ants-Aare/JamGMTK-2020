using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoBehaviourEvents : MonoBehaviour
{
    [SerializeField]
    private UnityEvent startEvent = new UnityEvent();
    [SerializeField]
    private UnityEvent enableEvent = new UnityEvent();
    [SerializeField]
    private UnityEvent disableEvent = new UnityEvent();
    void Start()
    {
        startEvent.Invoke();
    }

    void OnEnable()
    {
        enableEvent.Invoke();
    }   
    void OnDisable()
    {
        disableEvent.Invoke();
    }     
}
