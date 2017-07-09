using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Slot : MonoBehaviour {

	public GameObject torre;

	private BuildTree buildTree;
	private Image icon;
	private Text troops;

	void Awake(){
		icon = gameObject.transform.Find("Icon").gameObject.GetComponent<Image> ();
		troops = gameObject.GetComponentInChildren<Text> ();
		buildTree = gameObject.GetComponentInParent<BuildTree> ();

		if (buildTree == null)
			Debug.Log ("No Build Tree");
		
		if (icon != null) {
			//define o icone do botão de construir para a imagem da torre
			//TrocaImagem()
			icon.sprite = torre.GetComponent<TowerData> ().icon;
			//TowerData td = torre.GetComponent<TowerData> ();
			//GameObject aux = td.levels[0].visualizacao;
			//Sprite view = aux.GetComponent<SpriteRenderer> ().sprite;
			//icon.sprite = view;
		}
		if (troops != null) {
			troops.text = torre.GetComponent<TowerData> ().levels [0].tropas.ToString ("000");
		}
	}

	public void TrocaImagem(GameObject torreImage){
		torre = torreImage;
		GameObject aux = torre.gameObject.transform.Find ("View 1").gameObject;
		Sprite imagem = aux.GetComponent<SpriteRenderer> ().sprite;
		this.gameObject.GetComponent<Image> ().sprite = imagem;
	}

	public void ChooseTower(){
		//EventManager.ExecutarEvento ("BuildTower", torre, "");
		buildTree.GetPlaceTower().BuildTower(torre);
	}
}
