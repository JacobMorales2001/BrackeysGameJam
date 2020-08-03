using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// We need 2 colliders and a rigidbody to detect collisions
[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class PlayerGrab : MonoBehaviour
{
    PlayerInput input;
    bool GrabButton = false;
    bool Grabbing = false;
    bool InHand = false;
    Grabbable HeldObject = default;

    [SerializeField] private Transform HandLocation = default;
    [SerializeField] private float HandRadius = 0.2f;

    // For easy particles/animation swapping
    [Header("Events")]

    public UnityEvent OnGrabEvent;
    [Tooltip("Currently Unused")] public UnityEvent OnThrowEvent;
    [Tooltip("Currently Unused")] public UnityEvent OnDropEvent;

    private void Start()
    {
        input = GetComponent<PlayerInput>();

        if (OnGrabEvent == null)
            OnGrabEvent = new UnityEvent();
        if (OnThrowEvent == null)
            OnThrowEvent = new UnityEvent();
        if (OnDropEvent == null)
            OnDropEvent = new UnityEvent();
    }
    private void Update()
    {
        if (!GrabButton)
            GrabButton = input.GrabButtonDown;

        if (!InHand && Grabbing && Vector2.Distance(HeldObject.transform.localPosition, Vector2.zero) > HandRadius)
        {
            HeldObject.transform.localPosition = Vector2.Lerp(HeldObject.transform.localPosition, Vector2.zero, Time.deltaTime);
        }

        if (Grabbing)
        {
            if (!input.GrabButton) // Player released grab button
            {
                HeldObject.gameObject.AddComponent<Rigidbody2D>();
                HandLocation.DetachChildren();
                Grabbing = false;
                foreach (var c in HeldObject.colliders)
                {
                    c.enabled = true;
                }


                HeldObject = null;
                InHand = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Grabbable col = collision.GetComponent<Grabbable>();
        if (col && GrabButton && !Grabbing && HeldObject == null)
        {
            HeldObject = col;
            Grabbing = true;

            // Set the parent of the grabbed object to us.
            col.transform.SetParent(HandLocation);

            // remove its rigidbody if it has one.
            if (col.GetComponent<Rigidbody2D>())
            {
                Destroy(col.GetComponent<Rigidbody2D>());
            }

            foreach (var c in col.colliders)
            {
                c.enabled = false;
            }


            //col.transform.localPosition = Vector3.zero;

            OnGrabEvent.Invoke();
            GrabButton = false;
        }
    }
}
