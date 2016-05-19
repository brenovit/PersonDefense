using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slot : MonoBehaviour {

	public GameObject torre;
	public GameObject todosSpots;


	public void TrocaImagem(GameObject torreImage){
		torre = torreImage;
		GameObject aux = torre.gameObject.transform.FindChild ("View 1").gameObject;
		Sprite imagem = aux.GetComponent<SpriteRenderer> ().sprite;
		this.gameObject.GetComponent<Image> ().sprite = imagem;
	}

	public void SelecionouTorre(){
		GameObject[] spots = todosSpots.GetComponent<Spots> ().spots;
		for(int i = 0; i < spots.Length; i++){
			if(spots[i].GetComponent<PlaceMonster>().euChamei){
				spots [i].GetComponent<PlaceMonster> ().ConstruirTorre (torre);
				break;
			}
		}
	}
}
