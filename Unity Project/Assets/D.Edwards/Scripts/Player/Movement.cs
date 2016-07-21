//
//  Movement.cs
//  Unified Experiments - Movement Physics
//	Player component.
//
//  Created by David Edwards on 23/02/2015.
//

using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float normalMoveForce;
	public float sprintMoveForce;
	public float midairMoveForce;

	public float rotateSpeed;

	public float normalMaxMoveSpeed;
	public float sprintMaxMoveSpeed;

	public bool ignoreNextMovementInput;

	Rigidbody rigidBody;

	public enum MovementState {OnGround, InAir};
	public static MovementState movementState;

	// Init.
	void Start () 
	{
		rigidBody = this.GetComponent<Rigidbody>();
	}
	
	// Per-frame.
	void Update () 
	{
		switch(movementState)
		{
		case MovementState.OnGround:
			
			if(ignoreNextMovementInput == false) // Player input ignored to allow speed limiting.
			{
				if(Input.GetKey(KeyCode.LeftShift))
				{
					MovementInput(sprintMoveForce);
					LimitHorizontalSpeed(sprintMaxMoveSpeed);
				}
				else
				{
					MovementInput(normalMoveForce);
					LimitHorizontalSpeed(normalMaxMoveSpeed);
				}
			}
			else
				ignoreNextMovementInput = false;
			
			break;
		
		case MovementState.InAir:

			MovementInput(midairMoveForce);
			LimitHorizontalSpeed(sprintMaxMoveSpeed);

			break;
		}

		// Accept turning input regardless of state.
		RotationInput();
	}

	// WASD-based movement controls.
	void MovementInput(float accelerationForce)
	{
		if(Input.GetKey(KeyCode.W)) // Foward.
		{
			rigidBody.AddForce(this.transform.forward * (accelerationForce*Time.deltaTime));
		}
		else if(Input.GetKey(KeyCode.S)) // Backward.
		{
			rigidBody.AddForce(this.transform.forward * -(accelerationForce*Time.deltaTime));
		}
		else if(Input.GetKey(KeyCode.Q)) // Strafe left.
		{
			rigidBody.AddForce(this.transform.right * -(accelerationForce*Time.deltaTime));
		}
		else if(Input.GetKey(KeyCode.E)) // Strafe right.
		{
			rigidBody.AddForce(this.transform.right * (accelerationForce*Time.deltaTime));
		}
	}

	// WASD-based turning controls.
	void RotationInput()
	{
		if(Input.GetKey(KeyCode.A))
		{
			this.transform.Rotate(transform.up, -(rotateSpeed*Time.deltaTime));
		}
		else if(Input.GetKey(KeyCode.D))
		{
			this.transform.Rotate(transform.up, (rotateSpeed*Time.deltaTime));
		}
	}

	void LimitHorizontalSpeed(float maxSpeed)
	{
		// Calculate the current horizontal movement speed.
		float currentSpeedX = Mathf.Abs(rigidBody.velocity.x);
		float currentSpeedZ = Mathf.Abs(rigidBody.velocity.z);

		float horizontalSpeed = currentSpeedX + currentSpeedZ;

		// Apply a braking force in the opposite direction of the current velocity to slow the object down.
		if(horizontalSpeed > maxSpeed)
		{
			float brakeSpeed = horizontalSpeed - maxSpeed;
			Vector3 normalisedVelocity = rigidBody.velocity.normalized;
			Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;

			brakeVelocity.y = 0; // Only apply braking horizontally.

			rigidBody.AddForce(-brakeVelocity);

			ignoreNextMovementInput = true; // Ignore next player input to allow time for braking.
		}
	}
}
