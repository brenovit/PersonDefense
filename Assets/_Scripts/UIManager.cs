using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
	// This scene will loaded after whis level exit
	public string nomeCenaSair;
	// Pause menu canvas
	public GameObject panelPause;
	// Defeat menu canvas
	public GameObject panelDerrota;
	// Victory menu canvas
	public GameObject panelVitoria;
	// Level interface
	public GameObject panelBarraSuperior;
	// Confirmation restart level canvas
	public GameObject panelConfirmarReiniciar;
	// Confirmation Exit level canvas
	public GameObject panelConfirmarSair;
	// Avaliable troops, lives, orde and points amount
	public Text lblTropas;
	public Text lblOrdar;
	public Text lblVidas;
	public Text lblPontos;

	public GameObject buttonStartWave;
	public GameObject buttonFastWave;

	private bool gameFast = false;

	void OnEnable(){
		EventManager.CriarEvento ("BotaoPressionado",BotaoPressionado); 
	}

	void OnDisable(){
		EventManager.RemoverEvento ("BotaoPressionado",BotaoPressionado);
	}

	public void SetTropas (int valor)
	{
		lblTropas.text = valor.ToString ("0000");
	}

	public void AddTropas (int valor)
	{
		SetTropas (GetTropas () + valor);
	}

	public void RemoveTropas (int valor)
	{
		SetTropas (GetTropas () - valor);
	}

	public int GetTropas ()
	{
		int valor;
		int.TryParse (lblTropas.text, out valor);
		return valor;
	}

	public void SetVida (int valor)
	{
		lblVidas.text = valor.ToString ("0000");
	}

	public void AddVida (int valor)
	{
		SetTropas (GetVida() + valor);
	}

	public void RemoveVida (int valor)
	{
		SetTropas (GetVida() - valor);
	}

	public int GetVida ()
	{
		int valor;
		int.TryParse (lblVidas.text, out valor);
		return valor;
	}

	public void SetPontos (int valor)
	{
		lblPontos.text = valor.ToString ("0000");
	}

	public void AddPontos (int valor)
	{
		SetPontos (GetPontos () + valor);
	}

	public int GetPontos ()
	{
		int valor;
		int.TryParse (lblPontos.text, out valor);
		return valor;
	}

	public void SetOrda (int valor)
	{
		lblOrdar.text = valor.ToString ("0000");
	}

	public int GetOrda ()
	{
		int valor;
		int.TryParse (lblOrdar.text, out valor);
		return valor;
	}

	public void BotaoPressionado (GameObject obj, string param)
	{
		switch (param) {
			case "Pausar":
				Pausar ();
				break;
			case "Resumir":
				Resumir ();
				break;
			case "Sair":
				Sair ();
				break;
			case "Reiniciar":
				Reiniciar ();
				break;
			case "ConfirmarReiniciar":
				ConfirmarReiniciar();
				break;
			case "ConfirmarSair":
				ConfirmarSair();
				break;
			case "VoltarConfirmarSair":
				VoltarConfirmarSair();
				break;
			case "VoltarConfirmarReiniciar":
				VoltarConfirmarReiniciar();
				break;
			case "Efeito":
				break;
			case "Musica":
				break;
			case "StartWave":
				StartWave ();
				break;
			case "AccelerateWave":
				AccelerateWave ();
				break;
		}
	}

	public void Pausar()
	{
		panelPause.SetActive (true);
		Time.timeScale = 0;
	}

	public void Sair ()
	{
		panelConfirmarSair.SetActive (true);
	}

	public void Reiniciar ()
	{
		panelConfirmarReiniciar.SetActive (true);
	}

	public void Resumir ()
	{
		panelPause.SetActive (false);
		Time.timeScale = 1;
	}

	public void ConfirmarReiniciar ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public void ConfirmarSair ()
	{
		SceneManager.LoadScene (nomeCenaSair);
	}

	public void VoltarConfirmarSair ()
	{
		panelConfirmarSair.SetActive (false);
	}

	public void VoltarConfirmarReiniciar ()
	{
		panelConfirmarReiniciar.SetActive (false);
	}

	public void StartWave(){
		ChangeWaveButtonsVisibility ();
		EventManager.ExecutarEvento ("StartWave", null, "");
	}

	public void AccelerateWave(){
		ChangeWaveButtonsVisibility ();
		gameFast = !gameFast;
		Time.timeScale = gameFast ? 2f : 1f; 
	}

	private void ChangeWaveButtonsVisibility(){
		buttonFastWave.SetActive (buttonStartWave.activeSelf);
		buttonStartWave.SetActive (buttonFastWave.activeSelf);
		gameFast = false;
		Time.timeScale = 1f;
	}
}