using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace InGame
{
	/// <summary>
	/// This class manages all the tower construction, destruction and upgrade panel system.
	/// </summary>
	public class TowerSelect : MonoBehaviour
	{
		//esta classe vai centralizar o evento de contrui, melhorar e destruir de uma torre

		public Text lblNome;
		public Text lblDano;
		public Text lblCadencia;
		public Text lblTropas;

		public GameObject towerSelectPanel;
		public GameObject towersPanel;
		public GameObject actionsPanel;

		public ManipulaBotoes buttons;
		private GameMode mode;

		private static GameObject torre;

		private static TowerSelect instance = null;

		private void Awake ()
		{	//Singleton
			if (instance == null) {
				instance = this;
			} else if (instance != this) {
				Destroy (gameObject);
				return;
			}
			mode = new GameMode ();
		}

		private void OnEnable ()
		{
			print ("Ativei");
		}

		private void OnDisable ()
		{
			print ("Desativei");
		}

		public void Action (GameMode mode)
		{
			print ("Executei a ação: " + mode);
			this.mode = mode;
			bool state = true;

			switch (this.mode) {
			case GameMode.Building:
				buttons.ActiveBuild (true);
				towersPanel.SetActive (state);
				actionsPanel.SetActive (!state);
				towerSelectPanel.SetActive (state);
				break;
			case GameMode.Choosing:
				buttons.ActiveBuild (true);
				towersPanel.SetActive (!state);
				actionsPanel.SetActive (state);
				//Executar Evento de Construir ou Cancelar
				break;
			case GameMode.Choosed:
				buttons.ActiveBuild (false);
				towersPanel.SetActive (!state);
				actionsPanel.SetActive (state);
				//Executar Evento de Destrui ou Melhorar ou Cancelar
				break;
			}
		}

		public void ChooseTower (GameObject selectedTorre)
		{
			towerSelectPanel.SetActive (true);
			torre = selectedTorre;

			TowerData td = torre.GetComponent<TowerData> ();

			string nome = td.nome;
			int tropas = td.levels [td.getCurrentLevel () + 1].tropas;
			float cadencia = td.levels [td.getCurrentLevel () + 1].cadencia;
			int dano = td.levels [td.getCurrentLevel () + 1].dano;

			TowerInfo (nome, dano, cadencia, tropas);
			Action (GameMode.Choosing);
		}

		public void _BuildTower ()
		{
			print ("A torre sera contruida");
		}

		public void _DestroyTower ()
		{
			print ("A torre sera destruida");
			towerSelectPanel.SetActive (false);
		}

		public void _UpgradeTower ()
		{
			print ("A torre sera melhorada");
			/*this.gameObject.SetActive (true);
			painelTorres.SetActive (false);
			botoes.ModoConstruir (false);
			EscolheuTorre (selectedTorre);*/
		}

		public void _ShowInfo ()
		{
			print ("As info dtorre sera mostrada");
		}

		public void _Cancel ()
		{
			print ("Cancelarei no modo: " + mode);
			bool state = true;
			switch (this.mode) {
			case GameMode.Choosing:
				buttons.ActiveBuild (true);
				towersPanel.SetActive (state);
				actionsPanel.SetActive (!state);
				//executar ação de cancelar enquanto estiver selecionado a torre
				break;
			case GameMode.Choosed:
				buttons.ActiveBuild (true);
				towersPanel.SetActive (state);
				actionsPanel.SetActive (!state);
				towerSelectPanel.SetActive (false);
				//executar ação de cancelar	ao clicar em uma torre
				break;
			case GameMode.Building:
				actionsPanel.SetActive (!state);
				towersPanel.SetActive (state);
				//executar ação de cancelar enquanto estiver contruindo a torre
				break;
			}
		}

		public void Cancel ()
		{
			_Cancel ();
			towerSelectPanel.SetActive (false);
		}


		private void TowerInfo (string nome, int dano, float cadencia, int tropas)
		{
			lblNome.text = "Name: " + nome;
			lblDano.text = "Damag: " + dano;
			lblCadencia.text = "Fire Rate: " + cadencia + "tps";
			lblTropas.text = "Troops: " + tropas;
		}
	}
}