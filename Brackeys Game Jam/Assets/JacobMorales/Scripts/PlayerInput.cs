using System;
using UnityEngine;

/// <summary>
/// Variables describing the user's input configuration.
/// </summary>
public class PlayerInput : MonoBehaviour
{
    // This is the input side of the player script. All inputs will be detected in fixed update

    // the state of the 'jump' button
    public bool JumpButton { get; internal set; }

    // true the first frame the 'jump' button is pressed.
    public bool JumpButtonDown { get; internal set; }

    // the state of 'grapple'
    public bool GrappleButton { get; internal set; }

    // true the first frame the 'grapple' button is pressed.
    public bool GrappleButtonDown { get; internal set; }


    /// <summary>
    /// <para> (Read-Only) Describes the mouse's movements this frame. </para>
    /// See Also: 
    /// <see cref="MouseMovementOptions"/>
    /// </summary>
    public Vector2 MouseMovement { get { return mouseMovement; } protected set { mouseMovement = value; } }
    private Vector2 mouseMovement;


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

        JumpButtonDown = Input.GetButtonDown("Jump");
        GrappleButtonDown = Input.GetButtonDown("Fire3");
    }
    void FixedUpdate()
    {
        JumpButton = Input.GetButton("Jump");

        //DashButton      = Input.GetButton(ControllerInput.P1XButton);
        //DashButtonDown  = Input.GetButtonDown(ControllerInput.P1XButton);

        GrappleButton = Input.GetButton("Fire3");

        mouseMovement.x = Input.GetAxis("Mouse X") * MouseMovementOptions.InvertX * MouseMovementOptions.SensitivityX;
        mouseMovement.y = Input.GetAxis("Mouse Y") * -MouseMovementOptions.InvertY * MouseMovementOptions.SensitivityY;

        wasd.x = Input.GetAxisRaw("Horizontal") * WASDMovementOptions.InvertX * WASDMovementOptions.SensitivityX;
        wasd.y = Input.GetAxisRaw("Vertical")   * WASDMovementOptions.InvertY * WASDMovementOptions.SensitivityY;

        //rightStick.x    = Input.GetAxis("");
        //rightStick.y    = Input.GetAxis("");
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