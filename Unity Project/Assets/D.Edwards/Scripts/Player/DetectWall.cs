//
//  DetectWall.cs
//  Unified Experiments - Movement Physics
//	Player component.
//
//  Created by David Edwards on 15/07/2016.
//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DetectWall : MonoBehaviour {

	public bool isOnWall = false;

	public float wallDistance;

	// Per-frame.
	void Update () 
	{
		CheckWallContact();
	}

	// Check whether the player is positioned on/near geometry marked as a wall.
	void CheckWallContact()
	{
		List<Ray> raylist = new List<Ray>();

		raylist.Add(new Ray( transform.position + (transform.right.normalized * (transform.localScale.x/2)), transform.right)); // Rays on the right and left of player object, aiming right and left.
		raylist.Add(new Ray( transform.position - (transform.right.normalized * (transform.localScale.x/2)), -transform.right));

		raylist.Add(new Ray( transform.position + (transform.forward.normalized * (transform.localScale.z/2)), transform.forward)); // And on the front and back.
		raylist.Add(new Ray( transform.position - (transform.forward.normalized * (transform.localScale.z/2)), -transform.forward));

		isOnWall = false;

		// Fire each ray and see if it collides with a wall.
		foreach(Ray ray in raylist)
		{
			RaycastHit raycastHit = new RaycastHit();

			if(Physics.Raycast(ray, out raycastHit))
			{
				Debug.DrawLine(ray.origin, raycastHit.point, Color.blue);

				if(raycastHit.collider.tag == "WalkableGround")
				{
					if(raycastHit.distance <= wallDistance)
						isOnWall = true;
				}
			}
		}
	}
}
