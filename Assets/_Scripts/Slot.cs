using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Slot : MonoBehaviour {

	public GameObject torre;
	private TowerSelect towerSelect;

	void Start(){
		//towerSelect = GameObject.Find ("SelectTowerPanel").GetComponent<TowerSelect> ();
	}

	public void TrocaImagem(GameObject torreImage){
		torre = torreImage;
		GameObject aux = torre.gameObject.transform.Find ("View 1").gameObject;
		Sprite imagem = aux.GetComponent<SpriteRenderer> ().sprite;
		this.gameObject.GetComponent<Image> ().sprite = imagem;
	}

	public void SelecionouTorre(){
		towerSelect.EscolheuTorre (torre);
	}
}
