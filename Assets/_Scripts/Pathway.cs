using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Pathway : MonoBehaviour {


	#if UNITY_EDITOR
	void Update(){
		Waypoint[] waypoints = GetComponentsInChildren<Waypoint>();
		if (waypoints.Length > 1)
		{
			int idx;
			for (idx = 1; idx < waypoints.Length; idx++)
			{
				// Draw blue lines along pathway in edit mode
				Debug.DrawLine(waypoints[idx - 1].transform.position, waypoints[idx].transform.position, new Color(0.7f, 0f, 0f));
			}
		}
	}
	#endif
}
