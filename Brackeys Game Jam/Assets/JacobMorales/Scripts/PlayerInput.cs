using System;
using UnityEngine;

/// <summary>
/// Variables describing the user's input configuration.
/// </summary>
public class PlayerInput : MonoBehaviour
{
    // This is the input side of the player script. All inputs will be detected in update

    // string literals
    [Header("Input Axes")]
   
    [SerializeField] private string JumpAxis = "Jump";
    [SerializeField] private string GrappleAxis = "Fire3";
    [SerializeField] private string GrabAxis = "Fire2";
    [SerializeField] private string ThrowAxis = "Fire1";
    [SerializeField] private string MovementXAxis = "Horizontal";
    [SerializeField] private string MovementYAxis = "Vertical";
    [SerializeField] private string MouseXAxis = "Mouse X";
    [SerializeField] private string MouseYAxis = "Mouse Y";


    // the state of the 'jump' button
    public bool JumpButton { get; internal set; }

    // true the first frame the 'jump' button is pressed.
    public bool JumpButtonDown { get; internal set; }

    // true the first frame the 'jump' button is released.
    public bool JumpButtonUp { get; internal set; }

    // the state of 'grapple'
    public bool GrappleButton { get; internal set; }

    // true the first frame the 'grapple' button is pressed.
    public bool GrappleButtonDown { get; internal set; }

    // true the first frame the 'grapple' button is released.
    public bool GrappleButtonUp { get; internal set; }

    // the state of 'grab'
    public bool GrabButton { get; internal set; }

    // true the first frame the 'grab' button is pressed.
    public bool GrabButtonDown { get; internal set; }

    // true the first frame the 'grab' button is released.
    public bool GrabButtonUp { get; internal set; }

    // the state of 'grab'
    public bool ThrowButton { get; internal set; }

    // true the first frame the 'grab' button is pressed.
    public bool ThrowButtonDown { get; internal set; }

    // true the first frame the 'grab' button is released.
    public bool ThrowButtonUp { get; internal set; }


    /// <summary>
    /// <para> (Read-Only) Describes the mouse's movements this frame. </para>
    /// See Also: 
    /// <see cref="MouseMovementOptions"/>
    /// </summary>
    public Vector2 MouseMovement { get { return mouseMovement; } protected set { mouseMovement = value; } }
    private Vector2 mouseMovement;

    [Header("Input Modifiers")]
    /// <summary>
    /// <para> Describes modifiers for mouse movement </para>
    /// See Also: 
    /// <see cref="MouseMovement"/>
    /// </summary>
    public MouseOptions MouseMovementOptions = new MouseOptions();

    /// <summary>
    /// <para> Describes modifiers for mouse movement </para>
    /// See Also: 
    /// <see cref="WASDOptions"/>
    /// </summary>
    public WASDOptions WASDMovementOptions = new WASDOptions();

    /// <summary>
    /// <para> (Read-Only) Describes the wasd keys this frame. </para>
    /// </summary>
    public Vector3 WASD { get { return wasd; } protected set { wasd = value; } }
    private Vector3 wasd;



    // State of the Right Stick
    //private Vector2 rightStick;
    //public Vector2 RightStick { get { return rightStick; } internal set { } }

    private void Update()
    {

        JumpButtonDown = Input.GetButtonDown(JumpAxis);
        GrappleButtonDown = Input.GetButtonDown(GrappleAxis);
        GrabButtonDown = Input.GetButtonDown(GrabAxis);
        ThrowButtonDown = Input.GetButtonDown(ThrowAxis);

        JumpButton = Input.GetButton(JumpAxis);
        GrappleButton = Input.GetButton(GrappleAxis);
        GrabButton = Input.GetButton(GrabAxis);
        ThrowButton = Input.GetButton(ThrowAxis);

        JumpButtonUp = Input.GetButtonUp(JumpAxis);
        GrappleButtonUp = Input.GetButtonUp(GrappleAxis);
        GrabButtonUp = Input.GetButtonUp(GrabAxis);
        ThrowButtonUp = Input.GetButtonUp(ThrowAxis);

        mouseMovement.x = Input.GetAxis(MouseXAxis) * MouseMovementOptions.InvertX * MouseMovementOptions.SensitivityX;
        mouseMovement.y = Input.GetAxis(MouseYAxis) * -MouseMovementOptions.InvertY * MouseMovementOptions.SensitivityY;

        wasd.x = Input.GetAxisRaw(MovementXAxis) * WASDMovementOptions.InvertX * WASDMovementOptions.SensitivityX;
        wasd.y = Input.GetAxisRaw(MovementYAxis) * WASDMovementOptions.InvertY * WASDMovementOptions.SensitivityY;
    }

    void Start()
    {
        mouseMovement = new Vector2(0.0f, 0.0f);
    }
}

[Serializable]
public class MouseOptions
{
    [Range(0.1f, 100.0f)]
    public float SensitivityX = 10.0f;
    [Range(0.1f, 100.0f)]
    public float SensitivityY = 10.0f;

    [Range(-1.0f, 1.0f)]
    public float InvertX = 1.0f;
    [Range(-1.0f, 1.0f)]
    public float InvertY = 1.0f;
}

[Serializable]
public class WASDOptions
{
    [Range(0.1f, 100.0f)]
    public float SensitivityX = 1.0f;
    [Range(0.1f, 100.0f)]
    public float SensitivityY = 1.0f;

    [Range(-1.0f, 1.0f)]
    public float InvertX = 1.0f;
    [Range(-1.0f, 1.0f)]
    public float InvertY = 1.0f;
}