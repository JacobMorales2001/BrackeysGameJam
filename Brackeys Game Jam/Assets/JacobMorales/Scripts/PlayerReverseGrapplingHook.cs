using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerReverseGrapplingHook : MonoBehaviour
{
    [Header("Hook Properties")]
    [SerializeField] private float MaxHookDistance = 10.0f;
    [SerializeField] private float GrappleSpeed = 1.0f;
    [SerializeField] private LayerMask WhatCanBeHooked = -1;
    [SerializeField] private bool PlayerCanMoveWhileGrappling = false;

    private PlayerInput input;
    private LineRenderer lineRenderer = default;
    private TargetJoint2D targetJoint2D = default;
    private PlayerMovement movement = default;

    [Header("Events")]
    public UnityEvent OnHookLanded;
    public UnityEvent OnHookReleased;

    [Header("Debug")]
    [SerializeField, ReadOnly] private bool GrappleButton = false;
    [SerializeField, ReadOnly] private bool Grappling = false;
    [SerializeField, ReadOnly] private Vector2 LocalPushDirection = default;
    [SerializeField, ReadOnly] private Vector2 OriginalCollisionLocation = default;
    [SerializeField, ReadOnly] private bool ConnectingColliderHasRigidbody = false;
    private Transform ConnectedTransform = default;
    private Vector2 ConnectedTransformCollisionOffset = default;




    private void Start()
    {
        // Grab components.
        input = GetComponent<PlayerInput>();
        targetJoint2D = GetComponent<TargetJoint2D>();
        movement = GetComponent<PlayerMovement>();

        // Grab the line renderer even if it is inactive.
        lineRenderer = GetComponentInChildren<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = GetComponentInChildren<LineRenderer>(true);
            if (lineRenderer == null)
            {
                Debug.LogError($"Could not find a line renderer attached to one of the player's children in the PlayerReverseGrapplingHook.cs script");
            }
        }

        if (OnHookLanded == null)
            OnHookLanded = new UnityEvent();
        if (OnHookReleased == null)
            OnHookReleased = new UnityEvent();
    }
    private void Update()
    {
        if (!GrappleButton)
            GrappleButton = input.GrappleButtonDown;
    }

    private void FixedUpdate()
    {
        if (GrappleButton && input.GrappleButton && !Grappling)
        {
            // Get the origin and direction of where the hook should be sent.
            Vector2 origin = transform.position;
            Vector2 direction = ((Vector2)(Camera.main.ScreenToWorldPoint(input.MousePosition) - transform.position)).normalized;

            // TODO: Make it so the hook is not instant. Maybe add hook sprite at the end of the hook.
            // Determine if the hook hitanything.
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, MaxHookDistance, WhatCanBeHooked);
            if (hit.collider != null)
            {
                // TODO: Find another possible way that another GO can move, maybe by a layer system
                if (hit.collider.GetComponent<Rigidbody2D>() != null)
                {
                    ConnectedTransform = hit.collider.transform;
                    ConnectingColliderHasRigidbody = true;
                    ConnectedTransformCollisionOffset = hit.point - (Vector2)ConnectedTransform.position;
                }
                // Turn on the hook and render it
                lineRenderer.enabled = true;
                targetJoint2D.enabled = true;

                // Record where the original hit location is.
                OriginalCollisionLocation = hit.point;
                
                // Determine where the player should be sent.
                // TODO: Add reversing option here.
                LocalPushDirection = -transform.InverseTransformPoint(hit.point).normalized;

                // Actually push the player, mult by speed for more control.
                targetJoint2D.target = transform.TransformPoint(LocalPushDirection * GrappleSpeed);

                // Make it so the player cannot move while grappling.
                if (!PlayerCanMoveWhileGrappling)
                    movement.PlayerCanMove = false;

                // Render the line
                Vector3[] positions = new Vector3[]{transform.position, hit.point};
                lineRenderer.SetPositions(positions);

                Grappling = true;
            }

            // Invoke event
            OnHookLanded.Invoke();
        }

        // Reset button recording.
        GrappleButton = false;

        // If the player let go of the grapple button while grappling or the player went out of the reach of the hook, stop grappling
        if ((!input.GrappleButton || Vector2.Distance(transform.position, OriginalCollisionLocation) > MaxHookDistance) && Grappling)
        {
            // Disable appropriate components.
            lineRenderer.enabled = false;
            targetJoint2D.enabled = false;
            Grappling = false;

            // Let player move again if we didn't before.
            if (!PlayerCanMoveWhileGrappling)
                movement.PlayerCanMove = true;
            
            // Reset rigidbody recording
            ConnectingColliderHasRigidbody = false;
            ConnectedTransform = null;

            // Invoke event
            OnHookReleased.Invoke();
        }

        // Update positions and rendering
        if (Grappling)
        {
            // TODO: Make line look better.
            // Update player point.
            lineRenderer.SetPosition(0, transform.position);

            // If hooked object has a rigidbody, update pushdirection and renderer
            if (ConnectingColliderHasRigidbody)
            {
                LocalPushDirection = -transform.InverseTransformPoint(ConnectedTransformCollisionOffset + (Vector2)ConnectedTransform.position).normalized;
                lineRenderer.SetPosition(1, ConnectedTransformCollisionOffset + (Vector2)ConnectedTransform.position);
            }

            // Update push direction.
            targetJoint2D.target = transform.TransformPoint(LocalPushDirection * GrappleSpeed);
        }
    }

    private void OnDisable()
    {
        // Disable appropriate components.
        lineRenderer.enabled = false;
        targetJoint2D.enabled = false;
        Grappling = false;

        // Let player move again if we didn't before.
        if (!PlayerCanMoveWhileGrappling)
            movement.PlayerCanMove = true;

        // Reset rigidbody recording
        ConnectingColliderHasRigidbody = false;
        ConnectedTransform = null;

        // Invoke event
        OnHookReleased.Invoke();
    }

    //private bool GameObjectMovesByLayer(LayerMask layer)
    //{
    //    return false; // Cannot tell right now.
    //}
}
