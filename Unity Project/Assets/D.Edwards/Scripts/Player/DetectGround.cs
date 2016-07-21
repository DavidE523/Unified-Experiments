//
//  DetectGround.cs
//  Unified Experiments - Movement Physics
//	Player component.
//
//  Created by David Edwards on 23/02/2015.
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DetectGround : MonoBehaviour {

	public float groundDistance;
	
	// Per-frame.
	void Update () 
	{
		CheckGroundContact();
	}

	// Check whether the player is positioned on/near geometry marked as walkable ground.
	void CheckGroundContact()
	{
		List<Ray> raylist = new List<Ray>();

		raylist.Add(new Ray( transform.position + (transform.right.normalized * (transform.localScale.x/2)), Vector3.down)); // Set up rays on the right and left of player object, aiming down.
		raylist.Add(new Ray( transform.position - (transform.right.normalized * (transform.localScale.x/2)), Vector3.down));

		raylist.Add(new Ray( transform.position + (transform.forward.normalized * (transform.localScale.z/2)), Vector3.down)); // And on the front and back.
		raylist.Add(new Ray( transform.position - (transform.forward.normalized * (transform.localScale.z/2)), Vector3.down));

		Movement.movementState = Movement.MovementState.InAir;

		// Fire each ray and see if it collides with walkable ground.
		foreach(Ray ray in raylist)
		{
			RaycastHit raycastHit = new RaycastHit();
			
			if(Physics.Raycast(ray, out raycastHit))
			{
				Debug.DrawLine(ray.origin, raycastHit.point, Color.magenta);
				
				if(raycastHit.collider.tag == "WalkableGround")
				{
					if(raycastHit.distance <= groundDistance)
						Movement.movementState = Movement.MovementState.OnGround;
				}
			}
		}
	}

}
