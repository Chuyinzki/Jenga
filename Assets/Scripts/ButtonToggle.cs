using UnityEngine;
using System.Collections;

public class ButtonToggle : MonoBehaviour {

	public ZSCore core;

	// Use this for initialization
	void Start () {
		core.SetTrackerTargetVibrationEnabled(ZSCore.TrackerTargetType.Primary, true);
	}
	
	// Update is called once per frame
	public void Update()
	{
		if(core.IsTrackerTargetButtonPressed(ZSCore.TrackerTargetType.Primary,0))
		{
			collider.isTrigger = false;
			core.StartTrackerTargetVibration(ZSCore.TrackerTargetType.Primary,1.0f,1.0f,1);
		}
		else{
			collider.isTrigger = true;
			core.StopTrackerTargetVibration(ZSCore.TrackerTargetType.Primary);
		}
	}
}
