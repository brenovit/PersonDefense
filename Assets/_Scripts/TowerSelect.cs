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

		private GameObject tower = null;

		private static TowerSelect instance = null;
		private Selector selector;
		private GameObject gameManager;

		private void Awake ()
		{	//Singleton
			if (instance == null) {
				instance = this;
			} else if (instance != this) {
				Destroy (gameObject);
				return;
			}
			mode = new GameMode ();
			if (gameManager == null)
				gameManager = GameObject.FindGameObjectWithTag ("GameManager");
		}

		private void OnEnable ()
		{
			//print ("Ativei");
		}

		private void OnDisable ()
		{
			//print ("Desativei");
		}

		public void Action (GameMode mode)	//essa rotina faz a validação da exibição dos botões, modo contruir, escolher
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

		public void SelectTower (GameObject selectedTorre)
		{
			towerSelectPanel.SetActive (true);
			print ("TowerSelect pegou: " + selectedTorre.GetComponent<TowerData> ().nome);
			/*if (towerAux != tower && towerAux != null) {
				Destroy (towerAux.gameObject);
			}
			//towerAux = Instantiate (tower, selector.Position (), Quaternion.identity) as GameObject;
			//selector.Tower = towerAux;*/

			//tower = selectedTorre;
			GameObject selectore = GameObject.FindGameObjectWithTag("Selector");
			if (selectore == null)
				print ("selectore igual null");

			selector = gameManager.GetComponent<Selector> ();
			if (selector == null)
				print ("Selector igual null");
				
			Instantiate (selectedTorre, selector.Position(), Quaternion.identity);

			TowerData td = selectedTorre.GetComponent<TowerData> ();

			string nome = td.nome;
			int tropas = td.levels [td.getCurrentLevel () + 1].tropas;
			float cadencia = td.levels [td.getCurrentLevel () + 1].cadencia;
			int dano = td.levels [td.getCurrentLevel () + 1].dano;

			//TowerInfo (nome, dano, cadencia, tropas);
			Action (GameMode.Choosing);
		}

		public void _BuildTower ()
		{
			selector = GameObject.FindGameObjectWithTag ("GameManager").GetComponent <Selector> ();

			print ("A torre " + selector.Tower.name + " sera contruida em: " + selector.gameObject.transform.position.x + ", " + selector.gameObject.transform.position.y + ", " + selector.gameObject.transform.position.z);

			Instantiate (selector.Tower, selector.Position (), Quaternion.identity);
			Destroy (selector.gameObject);
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