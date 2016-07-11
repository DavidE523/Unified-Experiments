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

	public bool isOnGround = false;

	public Vector3[] rayOffsets;

	// Init.
	void Start () 
	{

	}
	
	// Per-frame.
	void Update () 
	{
		CheckGroundContact();
	}

	// Check whether the player positioned on/near geometry marked as walkable ground.
	void CheckGroundContact()
	{
		List<Ray> raylist = new List<Ray>();
		
		foreach(Vector3 offset in rayOffsets)
		{
			raylist.Add(new Ray(transform.position + offset, Vector3.down));
		}
		
		isOnGround = false;
		
		foreach(Ray ray in raylist)
		{
			RaycastHit raycastHit = new RaycastHit();
			
			if(Physics.Raycast(ray, out raycastHit))
			{
				Debug.DrawLine(ray.origin, raycastHit.point, Color.magenta);
				
				if(raycastHit.collider.tag == "WalkableGround")
				{
					if(raycastHit.distance <= 1.1f)
						isOnGround = true;
				}
			}
		}
	}

}
