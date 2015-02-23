//
//  FollowPlayer.cs
//  Unified Experiments - Movement Physics
//	Camera component.
//	
//  Created by David Edwards on 23/02/2015.
//

using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public GameObject playerObject;
	
	public Vector3 positionOffset;
	
	// Init.
	void Start () 
	{
		this.transform.position = playerObject.transform.position + positionOffset;
		this.transform.forward = playerObject.transform.position - this.transform.position;
	}
	
	// Per-frame.
	void Update () 
	{
		
	}
}
