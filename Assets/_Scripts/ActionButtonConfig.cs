using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace InGame
{
	public class ActionButtonConfig : MonoBehaviour
	{
		public string buttonName = "";
		private GameManagerBehaviour gameManager;
		private TowerSelect towerSelect;
		private Selector selector;

		private double towerPrice = 1;
		private int troops = 0;

		private TowerData td;

		void Start ()
		{
			if (buttonName == "") {
				print ("Não tem nome");
			} else {
				td = this.gameObject.GetComponent<Slot> ().tower.GetComponent<TowerData> ();
				if (td != null) {
					towerPrice = td.levels [0].tropas;
				}
			}
		}

		private void OnEnable ()	//ativa antes do start
		{
			gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManagerBehaviour> ();
			selector = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Selector> ();
			towerSelect = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<TowerSelect> ();

			troops = gameManager.Tropas;

			if (td != null) {
				td = selector.Tower.GetComponent<TowerData> ();
				towerPrice = td.levels [td.getCurrentLevel ()].tropas;
				if (buttonName.Equals ("Destroy")) {
					towerPrice *= 0.4;
				}
			}
			this.gameObject.GetComponentInChildren<Text> ().text = string.Format ("{0}\n{1}", buttonName, towerPrice);

			if (troops < towerPrice) {
				gameObject.GetComponent<Button> ().interactable = false;
			} else {
				gameObject.GetComponent<Button> ().interactable = true;
			}
		}
	}
}
