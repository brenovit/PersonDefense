using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerSelect : MonoBehaviour{

	public GameObject[] Slots;
	[SerializeField]	private GameObject todosSpots = null;

	public Text lblNome;
	public Text lblDano;
	public Text lblCadencia;
	public Text lblTropas;

	public GameObject painelTorres;
	public GameObject painelAction;

	public ManipulaBotoes botoes;

	private GameObject torre;

	public void AttTorresDisponiveis (GameObject[] torres){
		torre = null;
		this.gameObject.SetActive (true);
		painelTorres.SetActive (true);
		AttCampos ("", 0, 0.0f, 0);
		botoes.ModoConstruir (true);
		for(int i = 0; i < torres.Length; i++){
			Slots [i].GetComponent<Slot> ().TrocaImagem (torres [i]);
		}
	}

	public void EscolheuTorre(GameObject selectedTorre){
		torre = selectedTorre;

		TowerData ta = torre.GetComponent<TowerData> ();

		string nome = ta.nome;
		int tropas = ta.levels[ta.getCurrentLevel ()+1].tropas;
		float cadencia = ta.levels[ta.getCurrentLevel ()+1].cadencia;
		int dano = ta.levels[ta.getCurrentLevel ()+1].dano;

		AttCampos (nome, dano,cadencia,tropas);
	}

	public void ConstruirMelhorarTorre (){
		if (torre != null) {
			GameObject[] spots = todosSpots.GetComponent<Spots> ().spots;
			for (int i = 0; i < spots.Length; i++) {
				if (spots [i].GetComponent<PlaceTower> ().euChamei) {
					spots [i].GetComponent<PlaceTower> ().ConstruirMelhorarTorre (torre);
					break;
				}
			}
			AttCampos ("", 0, 0.0f, 0);
			this.gameObject.SetActive (false);
		} else
			return;
	}

	public void DestruirTorre(){
		if (torre != null) {
			GameObject[] spots = todosSpots.GetComponent<Spots> ().spots;
			for (int i = 0; i < spots.Length; i++) {
				if (spots [i].GetComponent<PlaceTower> ().euChamei) {
					spots [i].GetComponent<PlaceTower> ().Destruir ();
					break;
				}
			}
			this.gameObject.SetActive (false);
		} else
			return;
	}

	public void MelhorarDestruirTorre (GameObject selectedTorre){
		this.gameObject.SetActive (true);
		painelTorres.SetActive (false);
		botoes.ModoConstruir (false);
		EscolheuTorre (selectedTorre);
	}

	private void AttCampos(string nome, int dano, float cadencia, int tropas){
		lblNome.text = "Nome: "+ nome;
		lblDano.text = "Dano: "+dano;
		lblCadencia.text = "Cadencia: "+cadencia+"s";
		lblTropas.text = "Tropas: "+tropas;
	}

	public void Cancelar(){
		this.gameObject.SetActive (false);
	}
}
