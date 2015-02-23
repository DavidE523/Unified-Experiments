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

	public float fullMovePower;
	public float midairMovePower;

	public float rotateSpeed;

	public Vector3 currentVelocity;

	DetectGround groundDetectionComponent;

	// Init.
	void Start () 
	{
		groundDetectionComponent = this.GetComponent<DetectGround>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Store the current velocity.
		currentVelocity = this.rigidbody.velocity;

		// Accept movement based on whether player is on ground or in air.
		if(groundDetectionComponent.isOnGround == true)
			GroundMovementInput();
		else
			MidairMovementInput();
		
		RotationInput();
	}

	// WASD-based movement controls.
	void GroundMovementInput()
	{
		if(Input.GetKey(KeyCode.W))
		{
			this.rigidbody.AddForce(this.transform.forward * (fullMovePower*Time.deltaTime));
		}
		else if(Input.GetKey(KeyCode.S))
		{
			this.rigidbody.AddForce(this.transform.forward * -(fullMovePower*Time.deltaTime));
		}
	}
	void MidairMovementInput()
	{
		if(Input.GetKey(KeyCode.W))
		{
			this.rigidbody.AddForce(this.transform.forward * (midairMovePower*Time.deltaTime));
		}
		else if(Input.GetKey(KeyCode.S))
		{
			this.rigidbody.AddForce(this.transform.forward * -(midairMovePower*Time.deltaTime));
		}
	}
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
