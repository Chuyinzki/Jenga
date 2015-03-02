using UnityEngine;
using System.Collections;

//Stylus1: Demonstrates how to build a custom stylus composed of multiple GameObject primitives
//			Shows how to update position in 6 degrees of freedom with zSpace's OnCoreUpdated()
//			Obviously you could create any stylus model you want by replacing the Stem and Tip 
//			with your own GameObjects. If you look at the scale of the Stylus Stem, you can see 
//			it is .08 along the z-axis, which makes it 8cm in length. The stem is centered at the 
//			zSpace Stylus tip, so I move it -4cm along the z-axis to have it fully extend out from 
//			the stylus tip into the screen. So if you create your own stem, then just move it along 
//			half its length to get the same effect. I also added the green Stylus Tip, just to 
//			clarify how it is positioned to respect a natural 6 degree of freedom movement. 

public class Stylus3 : MonoBehaviour
{

	public Camera leftCam;
	public Camera rightCam;
	public GameObject jengaHitter;

	private Quaternion initialRotation = new Quaternion();
	private Vector3 initialPosition = new Vector3();
	private ZSCore _zsCore;
	private ZSCore.TrackerTargetType _targetType = ZSCore.TrackerTargetType.Primary;
	//private GameObject createdObj;
	int time = 0;

	private Color originalColor = Color.yellow; 
	private GameObject originalObject = null;
	public static bool createdHitter = false;
	
	public void Start(){
		_zsCore = GameObject.Find ("ZSCore").GetComponent<ZSCore> ();
		_zsCore.Updated += new ZSCore.CoreEventHandler (OnCoreUpdated);
		initialRotation = transform.rotation;
		initialPosition = transform.position;
	}
	
	/// called by ZSCore after each input update.
	private void OnCoreUpdated (ZSCore sender)
	{
		UpdateStylusPose ();
	}		
	
	private void UpdateStylusPose ()
	{
		Matrix4x4 pose = _zsCore.GetTrackerTargetWorldPose (_targetType);
		transform.position = new Vector3 (pose.m03 + initialPosition.x,
		                                  pose.m13 + initialPosition.y,
		                                  pose.m23 + initialPosition.z);
		
		transform.rotation = Quaternion.LookRotation(pose.GetColumn(2), 
		                                             pose.GetColumn(1))
			* initialRotation;
		
	}

	public void Update()
	{
		RaycastHit ray;
		//Debug.DrawRay (transform.position, transform.forward*10, Color.green);
		if (Physics.Raycast (transform.position, transform.forward, out ray)) {
			if (ray.collider.tag == "JengaBlock") {
				if (originalObject != null)
					originalObject.renderer.material.color = originalColor;	
				originalObject = ray.collider.gameObject;
				originalColor = ray.collider.gameObject.renderer.material.color;
				ray.collider.gameObject.renderer.material.color = Color.red;
				//Debug.Log ("Found Jenga Block");
				//Debug.Log (ray.normal);

				if(_zsCore.IsTrackerTargetButtonPressed(ZSCore.TrackerTargetType.Primary,0) && !createdHitter)
				{
					createdHitter = true;
					GameObject createdObj = (GameObject) Instantiate(jengaHitter);
					//Vector3 newVect = (ray.point + new Vector3(0,0,-1.3f));
					//createdObj.transform.position = newVect;
					//createdObj.transform.rotation.SetLookRotation(ray.normal);

					createdObj.transform.rotation = ray.collider.gameObject.transform.rotation;
					createdObj.transform.position = ray.collider.gameObject.transform.position;

					//createdObj.transform.rotation = ray.collider.gameObject.transform.rotation;
					Vector3 changePos = new Vector3(ray.normal.x*2.0f, ray.normal.y*2.0f, ray.normal.z*2.0f);
					createdObj.transform.localPosition += changePos;
					JengaHitter.pos = ray.collider.gameObject.transform.position;
					JengaHitter.globalBool = true;
					//else if (ray.normal == Vector3.forward)
					//	createdObj.transform.localPosition += new Vector3(0,0, 2.0f);
					//Debug.Log (ray.normal);
					//Debug.Log (Vector3.back);

				}


			}
		} 
		else 
		{
			if(originalObject != null)
			{
				originalObject.renderer.material.color = originalColor;
				originalObject = null;
			}
		}


		if (createdHitter) {
			time++;
			if(time > 100)
			{
				//Destroy (createdObj);
				//JengaHitter.globalBool = false;
				//JengaHitter.didOnce = false;
				createdHitter = false;
				time = 0;
			}
		}

		if(_zsCore.IsTrackerTargetButtonPressed(ZSCore.TrackerTargetType.Primary,1) && _zsCore.IsTrackerTargetButtonPressed(ZSCore.TrackerTargetType.Primary,2))
		{
			Application.LoadLevel (0);
		}
	}

}
