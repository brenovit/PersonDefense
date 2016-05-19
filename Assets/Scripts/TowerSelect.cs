using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerSelect : MonoBehaviour{

	public GameObject[] Slots;

	private Sprite imagem;

	/*public void SelecionarTorre(){
		GameObject spot = GameObject.Find ("SpotF2");
		PlaceMonster selectedSpot = spot.GetComponent<PlaceMonster> ();
		selectedSpot.ConstruirTorre (Tower);
		GameObject.Find ("SelectTowerPanel").SetActive (false);
	}*/


	public void EncaixarTorres (GameObject[] torres){
		for(int i = 0; i < torres.Length; i++){
			Slots [i].GetComponent<Slot> ().TrocaImagem (torres [i]);
		}
	}


}
