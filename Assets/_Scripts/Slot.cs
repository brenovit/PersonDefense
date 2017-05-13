using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace InGame
{
	public class Slot : MonoBehaviour
	{
		public GameObject tower;

		private static GameObject towerAux = null;
		private static TowerSelect towerSelect;
		//private Selector selector;

		void Awake ()
		{
			if (tower != null) {
				ChangeImage ();
				//selector = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Selector> ();
				towerSelect = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<TowerSelect> ();
			}						
		}

		/*private void OnEnable ()
		{
			if (troops < towerPrice) {
				gameObject.GetComponent<Button> ().interactable = false;
			} else {
				gameObject.GetComponent<Button> ().interactable = true;
			}
		}*/

		private void ChangeImage ()
		{
			GameObject aux = tower.gameObject.transform.FindChild ("View 1").gameObject;
			Sprite imagem = aux.GetComponent<SpriteRenderer> ().sprite;
			this.gameObject.GetComponent<Image> ().sprite = imagem;
		}

		//quando o jogar clicar no botão da torre, o gameobject tower será mandado para o seletor
		public void SelectTower ()	//manda a torre atual para o tower select
		{	
			if (towerAux != tower && towerAux != null) {
				Destroy (towerAux.gameObject);
			}
			//towerAux = Instantiate (tower, selector.Position (), Quaternion.identity) as GameObject;
			print ("Slot mandou: " + tower.GetComponent<TowerData> ().nome);
			//selector.Tower = tower;

			towerSelect.SelectTower (tower);
		}
	}
}