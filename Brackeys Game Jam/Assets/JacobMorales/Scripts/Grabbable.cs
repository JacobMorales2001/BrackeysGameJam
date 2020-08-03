using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Grabbable : MonoBehaviour
{
    public UnityEvent OnGrabbedEvent;
    public UnityEvent OnThrownEvent;
    public UnityEvent OnDroppedEvent;
    public List<Collider2D> colliders;

    private void Start()
    {
        if (OnGrabbedEvent == null)
            OnGrabbedEvent = new UnityEvent();
        if (OnThrownEvent == null)
            OnThrownEvent = new UnityEvent();
        if (OnDroppedEvent == null)
            OnDroppedEvent = new UnityEvent();

        colliders = new List<Collider2D>(GetComponents<Collider2D>());
    }
}
