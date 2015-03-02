using UnityEngine;
using System.Collections;

public class JengaHitter : MonoBehaviour {
	public static bool globalBool = false;
	public static Vector3 pos;

	private int i;
	private Vector3 newPos;
	private bool didOnce = false;

	private int time;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (globalBool && !didOnce) {
			newPos = (pos - transform.position);

			float best = Mathf.Abs (newPos.x);
			i = 0;

			if (Mathf.Abs (newPos.y) > best) {
				best = Mathf.Abs (newPos.y);
				i = 1;
			}

			if (Mathf.Abs (newPos.z) > best) {
				best = Mathf.Abs (newPos.z);
				i = 2;
			}

			didOnce = true;
		} 
		else if (globalBool && didOnce) 
		{
			switch(i)
			{
			case 0:
				if(newPos.x > 0)
				{
					transform.localPosition += new Vector3(0.1f,0,0);
					//transform.Translate (transform.right, Space.Self);
				}
				else
					transform.localPosition += new Vector3(-0.1f,0,0);
					//transform.Translate (-transform.right, Space.Self);
				break;

			case 1:
				if(newPos.y > 0)
				{
					transform.localPosition += new Vector3(0,0.1f,0);
					//transform.Translate (transform.up, Space.Self);
				}
				else
					transform.localPosition += new Vector3(0,-0.1f,0);
					//transform.Translate (-transform.up, Space.Self);
				break;

			case 2:
				if(newPos.z > 0)
				{
					transform.localPosition += new Vector3(0,0,0.1f);
					//transform.Translate (transform.forward, Space.Self);
				}
				else
					transform.localPosition += new Vector3(0,0,-0.1f);
					//transform.Translate (-transform.forward, Space.Self);
				break;
			}
		}


	}
}
