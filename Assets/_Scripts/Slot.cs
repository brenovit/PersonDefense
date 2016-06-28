using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace InGame
{
	public class Slot : MonoBehaviour
	{
		public GameObject tower;

		private static GameObject towerAux = null;
		private GameManagerBehaviour gameManager;
		private TowerSelect towerSelect;
		private Selector selector;

		private int towerPrice = 1;
		private int troops = 0;

		void Awake ()
		{
			if (tower != null) {
				ChangeImage ();
				gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManagerBehaviour> ();
				selector = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Selector> ();
				towerSelect = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<TowerSelect> ();
				troops = gameManager.Tropas;
				towerPrice = tower.GetComponent<TowerData> ().levels [0].tropas;
			}						
		}

		private void OnEnable ()
		{
			if (troops < towerPrice) {
				gameObject.GetComponent<Button> ().interactable = false;
			} else {
				gameObject.GetComponent<Button> ().interactable = true;
			}

		}

		private void ChangeImage ()
		{
			GameObject aux = tower.gameObject.transform.FindChild ("View 1").gameObject;
			Sprite imagem = aux.GetComponent<SpriteRenderer> ().sprite;
			this.gameObject.GetComponent<Image> ().sprite = imagem;
		}

		public void SelectTower ()
		{	
			if (towerAux != tower && towerAux != null) {
				Destroy (towerAux.gameObject);
			}
			towerAux = Instantiate (tower, selector.Position (), Quaternion.identity) as GameObject;
			towerSelect.ChooseTower (towerAux);
		}
	}
}