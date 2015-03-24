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

	float currentSpeed;
	
	// Init.
	void Start () 
	{
	
	}
	
	// Per-frame.
	void Update () 
	{
		// Store the current resultant movement speed.
		currentSpeed = this.rigidbody.velocity.sqrMagnitude;

		// Apply a braking force in the opposite direction of the current velocity to slow the object down.
		if(currentSpeed > maxSpeed)
		{
			float brakeSpeed = currentSpeed - maxSpeed;
			Vector3 normalisedVelocity = this.rigidbody.velocity.normalized;
			Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;

			brakeVelocity.y = 0; // Only apply braking horizontally.

			this.rigidbody.AddForce(-brakeVelocity);
		}
	}
}
