using UnityEngine;
using System.Collections;

namespace InGame
{
	public class HideTowerSelectPanel : MonoBehaviour
	{
		//Essa classe serve para esconder o painel de torre, clicando em qualquer lugar do mapa, contanto que não seja um Spot.
		private TowerSelect towerSelect;
		private Selector selector;


		void Start ()
		{
			selector = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Selector> ();
			towerSelect = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<TowerSelect> ();
		}


		void OnMouseUp ()
		{						//quando clicar no collider
			towerSelect.Cancel ();		//o painel é cancelado
			selector.DestroyAll ();
		}
	}
}