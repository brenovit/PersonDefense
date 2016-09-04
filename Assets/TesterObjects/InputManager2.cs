using UnityEngine;
using System.Collections;

public class InputManager2 : MonoBehaviour
{

	public delegate void OnClickEvent (GameObject go);

	public event OnClickEvent OnClick;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonUp (0)) {
			var ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				if (OnClick != null) {
					OnClick (hit.transform.gameObject);
				}
			}
		}
	}
}
