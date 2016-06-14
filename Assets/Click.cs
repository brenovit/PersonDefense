using UnityEngine;
using System.Collections;

public class Click : MonoBehaviour {
	public string texto;

	public GameObject torre;
	private Vector3 mousePosition;
	public GameObject	selector;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown (0)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Vector2 origin = new Vector2 (ray.origin.x, ray.origin.y);
			RaycastHit2D hit = Physics2D.Linecast (origin, -Vector2.up);//, 1 << LayerMask.NameToLayer ("Towers"));


			if(hit.collider.gameObject.tag.Equals ("Tower")){
				print ("Toquei em " + hit.collider.gameObject.name);
				return;
			}else if(hit.collider.gameObject.tag.Equals ("Way")){
				print ("Toquei no Caminho");
				return;
			} else if(hit.collider.gameObject.tag.Equals ("Floor")){
				print ("Toquei no Chão");
				mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mousePosition.z = -1;

				Instantiate (torre,mousePosition, Quaternion.identity);

				GameObject selectorAux = GameObject.FindGameObjectWithTag ("Selector");
				Destroy (selectorAux);	
			}
		}	
	}
}
