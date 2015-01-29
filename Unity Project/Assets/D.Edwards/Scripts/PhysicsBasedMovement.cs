using UnityEngine;
using System.Collections;

public class PhysicsBasedMovement : MonoBehaviour {

	public float movePower = 50f;

	public float jumpPower = 500f;
	
	// Init.
	void Start () {
	
	}
	
	// Per-frame logic.
	void Update () 
	{
		// Cache the current height of this object.
		float currentHeight = this.transform.position.y;

		// Accept movement input while on/near the ground.
		if(currentHeight > 0f && currentHeight < 1.5f)
		{
			CharacterMovementInput();
		}
		// Reset position and movement when falling off the world.
		else if (currentHeight < -5f)
		{
			this.transform.position = new Vector3(0,1,0);
			this.rigidbody.velocity = Vector3.zero;
		}
	}

	// WASD-based movement controls.
	void CharacterMovementInput()
	{
		if(Input.GetKey(KeyCode.W))
		{
			this.rigidbody.AddForce(new Vector3(0,0,movePower));
		}
		if(Input.GetKey(KeyCode.S))
		{
			this.rigidbody.AddForce(new Vector3(0,0,-movePower));
		}
		if(Input.GetKey(KeyCode.A))
		{
			this.rigidbody.AddForce(new Vector3(-movePower,0,0));
		}
		if(Input.GetKey(KeyCode.D))
		{
			this.rigidbody.AddForce(new Vector3(movePower,0,0));
		}
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			this.rigidbody.AddForce(new Vector3(0,jumpPower,0));
		}
	}
}
