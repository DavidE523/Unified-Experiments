//
//  PhysicsBasedMovement.cs
//  Unified Experiments - Movement Physics
//
//  Created by David Edwards on 31/01/2015.
//

using UnityEngine;
using System.Collections;

public class PhysicsBasedMovement : MonoBehaviour {

	public float movePower;
	public float rotateSpeed;
	public float jumpPower;

	public Vector3 currentVelocity;
	
	// Init.
	void Start () {
	
	}
	
	// Per-frame logic.
	void Update () 
	{
		// Store the current velocity.
		currentVelocity = this.rigidbody.velocity;

		// To do: Cast ray down and detect ground instead of this hack.
		// Accept movement input while on/near the ground.
		if(currentVelocity.y > -0.1f && currentVelocity.y < 0.1f)
		{
			MovementInput();
		}

		RotationInput();

		// Cache the current height of this object.
		float currentHeight = this.transform.position.y;

		// Reset position and movement when falling off the world.
		if (currentHeight < -5f)
		{
			this.transform.position = new Vector3(0,1,0);
			this.rigidbody.velocity = Vector3.zero;
		}
	}

	// WASD-based movement controls.
	void MovementInput()
	{
		if(Input.GetKey(KeyCode.W))
		{
			this.rigidbody.AddForce(this.transform.forward * movePower);
		}
		else if(Input.GetKey(KeyCode.S))
		{
			this.rigidbody.AddForce(this.transform.forward * -movePower);
		}
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			this.rigidbody.AddForce(new Vector3(0,jumpPower,0));
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
