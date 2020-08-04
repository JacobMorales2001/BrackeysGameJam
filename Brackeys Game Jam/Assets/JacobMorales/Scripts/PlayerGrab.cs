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
    //bool InHand = false;
    Grabbable HeldObject = default;

    [SerializeField] private Transform HandLocation = default;
    [SerializeField] private float HandRadius = 0.2f;
    [SerializeField] private float ThrowForce = 10.0f;

    // For easy particles/animation swapping

    [Header("Trajectory")]

    public GameObject TrajectoryDot;
    public int TrajectoryNumberOfDots;
    public float SpaceBetweenDots;
    private GameObject[] TrajectoryDots;

    public Vector2 ThrowDirection;

    [Header("Events")]

    public UnityEvent OnGrabEvent;
    [Tooltip("Currently Unused")] public UnityEvent OnThrowEvent;
    [Tooltip("Currently Unused")] public UnityEvent OnDropEvent;

    private void Start()
    {
        TrajectoryDots = new GameObject[TrajectoryNumberOfDots];

        //for (int i = 0; i < TrajectoryNumberOfDots; i++)
        //{
        //    TrajectoryDots[i] = Instantiate(TrajectoryDot, transform.position, Quaternion.identity);
        //}


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
        if (!GrabButton && !Grabbing)
            GrabButton = input.GrabButtonDown;

        if (Grabbing && Vector2.Distance(HeldObject.transform.localPosition, Vector2.zero) > HandRadius)
        {
            HeldObject.transform.localPosition = Vector2.Lerp(HeldObject.transform.localPosition, Vector2.zero, Time.deltaTime);
        }

        if (Grabbing)
        {
            ThrowDirection = ((Vector2)(transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition))).normalized;
            //Debug.Log(ThrowDirection.magnitude);
            float Modifier = (PlayerIsFacingRight() && ThrowDirection.x >= 0) || (!PlayerIsFacingRight() && ThrowDirection.x <= 0) ? 1.0f : 0.0f;

            if (input.ThrowButtonDown)
            { //click
                for (int i = 0; i < TrajectoryNumberOfDots; i++)
                {
                    TrajectoryDots[i] = Instantiate(TrajectoryDot, gameObject.transform);
                }
            }
            if (input.ThrowButton && Modifier != 0)
            { //drag

                for (int i = 0; i < TrajectoryNumberOfDots; i++)
                {
                    TrajectoryDots[i].SetActive(true);
                    TrajectoryDots[i].transform.position = calculatePosition(i * SpaceBetweenDots);
                }
            }
            else if (input.ThrowButton && Modifier == 0)
            {
                for (int i = 0; i < TrajectoryNumberOfDots; i++)
                {
                    TrajectoryDots[i].SetActive(false);
                    TrajectoryDots[i].transform.position = calculatePosition(i * SpaceBetweenDots);
                }
            }
            if (input.ThrowButtonUp)
            { //leave

                for (int i = 0; i < TrajectoryNumberOfDots; i++)
                {
                    Destroy(TrajectoryDots[i]);
                }

                HeldObject.gameObject.AddComponent<Rigidbody2D>();
                HandLocation.DetachChildren();
                foreach (var c in HeldObject.colliders)
                {
                    c.enabled = true;
                }

                // Add force to the Held object for throwing
                //Debug.Log(ThrowDirection);
                // If the mouse is relatively in front of the player, don't apply force to that angle.
                HeldObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                HeldObject.GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
                HeldObject.GetComponent<Rigidbody2D>().AddForce(ThrowDirection * Modifier * ThrowForce, ForceMode2D.Impulse);

                if (Modifier == 0)
                    OnDropEvent.Invoke();
                else
                    OnThrowEvent.Invoke();

                Grabbing = false;
                HeldObject = null;
                //InHand = false;
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

    private bool PlayerIsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private Vector2 calculatePosition(float elapsedTime)
    {
        return (Vector2)HeldObject.transform.position + //X0
                ((ThrowDirection * ThrowForce) + (0.5f * Physics2D.gravity * elapsedTime * elapsedTime)) * elapsedTime;
    }
}
