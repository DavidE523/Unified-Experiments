﻿//
// 	WallJump.cs
//  Unified Experiments - Movement Physics
//	Player component.
//
//  Created by David Edwards on 18/07/2016.
//

using UnityEngine;
using System.Collections;

public class WallJump : MonoBehaviour {

	public float wallJumpPower;

	bool wallJumpUsed = false;

	DetectWall wallDetection;

	Rigidbody rigidBody;

	// Init.
	void Start () 
	{
		wallDetection = this.GetComponent<DetectWall>();

		rigidBody = this.GetComponent<Rigidbody>();
	}
	
	// Per-frame.
	void Update () 
	{
		// Reset wall jump flag when on the ground.
		if(Movement.movementState == Movement.MovementState.OnGround)
			wallJumpUsed = false;
		else
		{
			if(wallDetection.isOnWall == true) // Allow wall jumping when in the air and close to a wall.
			{
				if(wallJumpUsed == false) 
					WallJumpInput();

				rigidBody.AddForce(-(Physics.gravity/2), ForceMode.Acceleration); // Half gravity, simulating sliding down wall.
			}
		}
	}

	void WallJumpInput()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			rigidBody.AddForce(new Vector3(0,wallJumpPower,0));
			wallJumpUsed = true;
		}
	}
}
