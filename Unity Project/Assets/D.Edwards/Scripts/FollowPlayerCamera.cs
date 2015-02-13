//
//  FollowPlayerCamera.cs
//  Unified Experiments - Movement Physics
//
//  Created by David Edwards on 01/02/2015.
//

using UnityEngine;
using System.Collections;

public class FollowPlayerCamera : MonoBehaviour {

	public GameObject playerObject;

	public Vector3 positionOffset;

	// Init.
	void Start () 
	{
		this.transform.position = playerObject.transform.position + positionOffset;
		this.transform.forward = playerObject.transform.position - this.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
