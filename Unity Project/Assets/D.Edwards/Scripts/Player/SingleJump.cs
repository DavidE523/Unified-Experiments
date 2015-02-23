//
//  SingleJump.cs
//  Unified Experiments - Movement Physics
//	Player component.
//
//  Created by David Edwards on 23/02/2015.
//

using UnityEngine;
using System.Collections;

public class SingleJump : MonoBehaviour {

	public float jumpPower;

	DetectGround groundDetectionComponent;

	// Init.
	void Start () 
	{
		groundDetectionComponent = this.GetComponent<DetectGround>();
	}
	
	// Per-frame.
	void Update () 
	{
		if(groundDetectionComponent.isOnGround == true)
			JumpInput();
	}

	void JumpInput()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			this.rigidbody.AddForce(new Vector3(0,jumpPower,0));
		}
	}
}
