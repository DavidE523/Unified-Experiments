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

	public bool isOnGround = false;
	
	// Init.
	void Start () {
	
	}
	
	// Per-frame logic.
	void Update () 
	{
		// Store the current velocity.
		currentVelocity = this.rigidbody.velocity;

		CheckGroundContact();

		if(isOnGround == true)
		{
			// Accept movement input while on/near the ground.
			if(currentVelocity.y > -0.1f && currentVelocity.y < 0.1f)
			{
				MovementInput();
			}
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

	void CheckGroundContact()
	{
		RaycastHit raycastHit = new RaycastHit();
		Ray ray = new Ray(transform.position, Vector3.down);

		if(Physics.Raycast(ray, out raycastHit))
		{
			Debug.DrawLine(ray.origin, raycastHit.point, Color.magenta);

			if(raycastHit.collider.tag == "WalkableGround")
			{
				if(raycastHit.distance < 1f)
					isOnGround = true;
				else
					isOnGround = false;
			}
		}
	}

	// WASD-based movement controls.
	void MovementInput()
	{
		if(Input.GetKey(KeyCode.W))
		{
			this.rigidbody.AddForce(this.transform.forward * (movePower*Time.deltaTime));
		}
		else if(Input.GetKey(KeyCode.S))
		{
			this.rigidbody.AddForce(this.transform.forward * -(movePower*Time.deltaTime));
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
