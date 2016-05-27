using UnityEngine;
using System.Collections;

public class BL_DeadTime : MonoBehaviour {
	public float deadTime = 0;

	void Awake () {
		Destroy (gameObject, deadTime);
	}
}