//
//  PhysicsBasedMovement.cs
//  Unified Experiments - Movement Physics
//
//  Created by David Edwards on 31/01/2015.
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhysicsBasedMovement : MonoBehaviour {

	public float fullMovePower;
	public float midairMovePower;
	public float rotateSpeed;
	public float jumpPower;

	public Vector3 currentVelocity;

	public bool isOnGround = false;

	public Vector3[] rayOffsets;
	
	// Init.
	void Start () 
	{
		Debug.Log(this.transform.collider.bounds.ToString());
	}
	
	// Per-frame logic.
	void Update () 
	{
		// Store the current velocity.
		currentVelocity = this.rigidbody.velocity;

		CheckGroundContact();

		// Accept movement based on whether player is on ground or in air.
		if(isOnGround == true)
			GroundMovementInput();
		else
			MidairMovementInput();

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
		List<Ray> raylist = new List<Ray>();

		foreach(Vector3 offset in rayOffsets)
		{
			raylist.Add(new Ray(transform.position + offset, Vector3.down));
		}

		isOnGround = false;

		foreach(Ray ray in raylist)
		{
			RaycastHit raycastHit = new RaycastHit();

			if(Physics.Raycast(ray, out raycastHit))
			{
				Debug.DrawLine(ray.origin, raycastHit.point, Color.magenta);

				if(raycastHit.collider.tag == "WalkableGround")
				{
					if(raycastHit.distance < 1f)
						isOnGround = true;
				}
			}
		}
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
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			this.rigidbody.AddForce(new Vector3(0,jumpPower,0));
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
