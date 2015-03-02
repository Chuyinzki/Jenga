using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public ZSCore core;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int sensitivity = 25;
		if (Input.GetKey ("z")) {
			transform.RotateAround (Vector3.zero, Vector3.up, sensitivity*Time.deltaTime);
		}
		else if (Input.GetKey ("x")) {
			transform.RotateAround (Vector3.zero, Vector3.up, -sensitivity*Time.deltaTime);
		}
		if(core.IsTrackerTargetButtonPressed(ZSCore.TrackerTargetType.Primary,1))
		{
			transform.RotateAround (Vector3.zero, Vector3.up, -sensitivity*Time.deltaTime);
		}
		else if(core.IsTrackerTargetButtonPressed(ZSCore.TrackerTargetType.Primary,2))
		{
			transform.RotateAround (Vector3.zero, Vector3.up, sensitivity*Time.deltaTime);
		}
	}
}
