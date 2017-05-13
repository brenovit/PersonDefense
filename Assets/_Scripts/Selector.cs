using UnityEngine;
using System.Collections;

namespace InGame
{
	/// <summary>
	/// This class refer to red circle that indicate where the player clicked
	/// </summary>
	public class Selector : MonoBehaviour
	{
		public GameObject selector;
		private GameObject selectorObject;

		private static GameObject tower;

		private static Selector instance = null;

		public GameObject Tower {
			get { return tower; }
			set {				
				tower = value; 
				print ("Estou selecionando: " + tower.gameObject.name);
			}
		}

		private void Awake ()
		{	//Singleton
			if (instance == null) {
				instance = this;
			} else if (instance != this) {
				Destroy (gameObject);
				return;
			}
		}

		public Vector3 Position ()
		{
			print (this.selectorObject.transform.position);
			return this.selectorObject.transform.position;
		}

		/// <summary>
		/// Instantiates at position the Selector and set the tower.
		/// </summary>
		/// <param name="position">Position.</param>
		/// <param name="tower">Tower.</param>
		public void InstantiateAt (Vector3 position, GameObject tower)
		{			
			InstantiateAt (position);
			Tower = tower;
		}

		/// <summary>
		/// Instantiates at position the selector.
		/// </summary>
		/// <param name="position">Position.</param>
		public void InstantiateAt (Vector3 position)
		{				
			if (selectorObject != null) {
				Destroy (selectorObject.gameObject);
			}
			selectorObject = Instantiate (selector, position, Quaternion.identity) as GameObject;
			if (tower != null) {
				tower.gameObject.transform.position = selectorObject.gameObject.transform.position;
			}

		}

		public void Destroy ()
		{
			//selectorObject = GameObject.FindGameObjectWithTag ("Selector");
			if(this.selectorObject != null)
				Destroy (this.selectorObject.gameObject);
		}

		public void DestroyAll ()
		{
			Destroy ();
			if (tower != null) {
				Destroy (tower);
				tower = null;
			}
		}
	}
}

