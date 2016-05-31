using UnityEngine;
using System.Collections;

public class EsconderTowerSelect : MonoBehaviour {
	//Essa classe serve para esconder o painel de torre, clicando em qualquer lugar do mapa, contanto que não seja um Spot.
	public GameObject panel;				//gameobject do painel

	void OnMouseUp(){						//quando clicar no collider
		panel.SetActive (false);			//o painel apaga
		GameObject selectorAux = GameObject.FindGameObjectWithTag ("Selector");	//procura o seletor(indicador que mostra qual lugar foi selecionado)
		Destroy (selectorAux);				//destroi o seletor
	}
}
