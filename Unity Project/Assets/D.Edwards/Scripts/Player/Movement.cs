//
//  Movement.cs
//  Unified Experiments - Movement Physics
//	Player component.
//
//  Created by David Edwards on 23/02/2015.
//

using UnityEngine;
using System.Collections;

[RequireComponent (typeof (DetectGround))]
public class Movement : MonoBehaviour {

	public float fullMovePower;
	public float midairMovePower;

	public float rotateSpeed;

	public bool ignoreNextMovementInput;

	DetectGround groundDetectionComponent;

	Rigidbody rigidBody;

	// Init.
	void Start () 
	{
		groundDetectionComponent = this.GetComponent<DetectGround>();

		rigidBody = this.GetComponent<Rigidbody>();
	}
	
	// Per-frame.
	void Update () 
	{
		
		// Accept movement based on whether player is on the ground or in the air.
		if(groundDetectionComponent.isOnGround == true)
		{
			// Player input ignored to allow speed limiting.
			if(ignoreNextMovementInput == false)
				MovementInput(fullMovePower);
			else
				ignoreNextMovementInput = false;
		}
		else
			MovementInput(midairMovePower);

		// Accept turning input regardless of state.
		RotationInput();
	}

	// WASD-based movement controls.
	void MovementInput(float accelerationForce)
	{
		if(Input.GetKey(KeyCode.W))
		{
			rigidBody.AddForce(this.transform.forward * (accelerationForce*Time.deltaTime));
		}
		else if(Input.GetKey(KeyCode.S))
		{
			rigidBody.AddForce(this.transform.forward * -(accelerationForce*Time.deltaTime));
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

}
