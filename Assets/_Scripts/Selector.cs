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
		private static Selector instance = null;

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
			return selectorObject.transform.position;
		}

		public void InstantiateAt (Vector3 position)
		{				
			if (selectorObject != null) {
				Destroy (selectorObject.gameObject);
			}
			selectorObject = Instantiate (selector, position, Quaternion.identity) as GameObject;
		}

		public void Destroy ()
		{
			selectorObject = GameObject.FindGameObjectWithTag ("Selector");
			Destroy (selectorObject.gameObject);
		}
	}
}

