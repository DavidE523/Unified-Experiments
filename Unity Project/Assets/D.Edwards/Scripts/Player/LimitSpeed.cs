//
//  LimitSpeed.cs
//  Unified Experiments - Movement Physics
//	Player component.
//
//  Created by David Edwards on 11/03/2015.
//

using UnityEngine;
using System.Collections;

public class LimitSpeed : MonoBehaviour {

	public float maxSpeed;

	Movement movementController;
	
	// Init.
	void Start () 
	{
		movementController = this.GetComponent<Movement>();
	}
	
	// Per-frame.
	void Update () 
	{
		// Calculate the current horizontal movement speed.
		float currentSpeedX = Mathf.Abs(this.GetComponent<Rigidbody>().velocity.x);
		float currentSpeedZ = Mathf.Abs(this.GetComponent<Rigidbody>().velocity.z);

		float horizontalSpeed = currentSpeedX + currentSpeedZ;

		Debug.Log(horizontalSpeed);

		// Apply a braking force in the opposite direction of the current velocity to slow the object down.
		if(horizontalSpeed > maxSpeed)
		{
			float brakeSpeed = horizontalSpeed - maxSpeed;
			Vector3 normalisedVelocity = this.GetComponent<Rigidbody>().velocity.normalized;
			Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;

			brakeVelocity.y = 0; // Only apply braking horizontally.

			this.GetComponent<Rigidbody>().AddForce(-brakeVelocity);

			movementController.ignoreNextMovementInput = true; // Ignore next player input to allow time for braking.
		}
	}
}
