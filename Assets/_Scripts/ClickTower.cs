using UnityEngine;
using System.Collections;

namespace InGame
{
	public class ClickTower : MonoBehaviour
	{
		private TowerSelect towerSelect;
		private Selector selector;

		void Start ()
		{
			selector = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Selector> ();
			towerSelect = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<TowerSelect> ();
		}

		void OnMouseDown ()
		{
			print ("Eu sou: " + this.gameObject.name);
			selector.InstantiateAt (this.gameObject.transform.position);
		}
	}
}
