//
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

	DetectGround groundDetection;

	DetectWall wallDetection;

	Rigidbody rigidBody;

	// Init.
	void Start () 
	{
		groundDetection = this.GetComponent<DetectGround>();

		wallDetection = this.GetComponent<DetectWall>();

		rigidBody = this.GetComponent<Rigidbody>();
	}
	
	// Per-frame.
	void Update () 
	{
		// Reset wall jump flag when on the ground.
		if(groundDetection.isOnGround == true)
			wallJumpUsed = false;
		else
		{
			if(wallDetection.isOnWall == true && wallJumpUsed == false) // Allow wall jumping when in the air and close to a wall.
				WallJumpInput(); 
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
