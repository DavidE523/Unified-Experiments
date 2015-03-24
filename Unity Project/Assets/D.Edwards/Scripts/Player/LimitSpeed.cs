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
	
	// Init.
	void Start () 
	{
	
	}
	
	// Per-frame.
	void Update () 
	{
		// Calculate the current horizontal movement speed.
		float currentSpeedX = Mathf.Abs(this.rigidbody.velocity.x);
		float currentSpeedZ = Mathf.Abs(this.rigidbody.velocity.z);

		float horizontalSpeed = currentSpeedX + currentSpeedZ;

		// Apply a braking force in the opposite direction of the current velocity to slow the object down.
		if(horizontalSpeed > maxSpeed)
		{
			float brakeSpeed = horizontalSpeed - maxSpeed;
			Vector3 normalisedVelocity = this.rigidbody.velocity.normalized;
			Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;

			brakeVelocity.y = 0; // Only apply braking horizontally.

			this.rigidbody.AddForce(-brakeVelocity);
		}
	}
}
