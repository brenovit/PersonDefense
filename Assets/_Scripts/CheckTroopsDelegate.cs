using UnityEngine;
using System.Collections;

namespace InGame
{
	public class CheckTroopsDelegate : MonoBehaviour
	{
		public delegate void CheckDelegate (GameObject slot);

		public CheckDelegate slot;
		private GameManagerBehaviour gameManager;

		void OnActive ()
		{
			if (slot != null) {
				print (gameObject.name + " ativou. Troops: " + gameManager.Tropas);
			}
		}

		// Use this for initialization
		void Start ()
		{
			gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManagerBehaviour> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
	}
}