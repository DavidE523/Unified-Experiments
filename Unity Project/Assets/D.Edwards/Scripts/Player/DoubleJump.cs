//
//  DoubleJump.cs
//  Unified Experiments - Movement Physics
//	Player component, inherits from SingleJump.
//
//  Created by David Edwards on 14/07/2016.
//

using UnityEngine;
using System.Collections;

public class DoubleJump : SingleJump {

	public float doubleJumpPower;

	bool doubleJumpUsed = false;

	// Per-frame.
	protected override void Update () 
	{
		base.Update();

		// Allow double jumping when in the air.
		if(groundDetectionComponent.isOnGround == true)
			doubleJumpUsed = false;
		else
			DoubleJumpInput();
	}

	// Double jump when space pressed if the player hasn't already since last touching the ground.
	void DoubleJumpInput()
	{
		if(doubleJumpUsed == false && Input.GetKeyDown(KeyCode.Space))
		{
			rigidBody.AddForce(new Vector3(0,doubleJumpPower,0));
			doubleJumpUsed = true;
		}
	}
}
