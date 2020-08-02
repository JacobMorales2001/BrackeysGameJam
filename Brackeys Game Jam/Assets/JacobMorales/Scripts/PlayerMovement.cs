using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
	private CharacterController2D m;
	private PlayerInput input;
	public float Speed = 40.0f;
	private float hMove;
	private bool jump = false;

	private void Start()
	{
		m = GetComponent<CharacterController2D>();
		input = GetComponent<PlayerInput>();
	}

	private void Update()
	{
		hMove = input.WASD.x * Speed;
		if (!jump)
			jump = input.JumpButtonDown;
	}


	private void FixedUpdate()
	{
		m.Move(hMove * Time.fixedDeltaTime, false, jump);
		jump = false;
	}
}
