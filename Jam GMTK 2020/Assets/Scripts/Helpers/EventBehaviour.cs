using UnityEngine;
using UnityEngine.Events;

public class EventBehaviour : MonoBehaviour
{
    [Header("Events")]
    [SerializeField]
    protected UnityEvent unityEvent = null;
    [SerializeField]
    protected BoolUnityEvent boolEvent = null;

    public virtual void InvokeEvent()
    {
        unityEvent?.Invoke();
    }
    public virtual void InvokeBoolEvent(int value)
    {
        boolEvent?.Invoke(value != 0);
    }
}   