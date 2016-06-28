using UnityEngine;
using System.Collections;

namespace InGame
{
	// <summary>
	/// This class represent the game floor. The area that the player can put or remove the towers.
	/// </summary>/
	public class Floor : MonoBehaviour
	{
		private Vector3 mousePosition;
		private TowerSelect towerSelect;
		private Selector selector;

		void OnMouseDown ()
		{
			/*Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Vector2 origin = new Vector2 (ray.origin.x, ray.origin.y);
			RaycastHit2D hit = Physics2D.Linecast (origin, -Vector2.up);//, 1 << LayerMask.NameToLayer ("Towers"));*/
			GameObject selectorAux = GameObject.FindGameObjectWithTag ("Selector");	

			print ("Toquei no Chão");
			towerSelect.Action (GameMode.Building);
			mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousePosition.z = -1;

			selector.InstantiateAt (mousePosition);	//instantiate the selector, using a static metod from the selector class
		}

		// Use this for initialization
		void Awake ()
		{
			towerSelect = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<TowerSelect> ();
			selector = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Selector> ();

		}
	}
}