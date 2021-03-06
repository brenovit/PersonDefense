﻿using System.Collections;
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
	public GameObject panelGameOver;
	// Victory menu canvas
	public GameObject panelGameWon;
	// Level interface
	public GameObject panelBarraSuperior;
	// Confirmation restart level canvas
	public GameObject panelConfirmarReiniciar;
	// Confirmation Exit level canvas
	public GameObject panelConfirmarSair;

	public Text lblTropas;
	public Text lblOrdar;
	public Text lblVidas;
	public Text lblPontos;

	public GameObject buttonStartWave;
	public GameObject buttonFastWave;

	private bool gameFast = false;

	public bool GameFast{
		get {
			return gameFast;
		}
	}

	void OnEnable(){
		EventManager.CriarEvento ("BotaoPressionado",BotaoPressionado); 
		EventManager.CriarEvento ("GameOver",GameOver); 
		EventManager.CriarEvento ("GameWon",GameWon); 
	}

	void OnDisable(){
		EventManager.RemoverEvento ("BotaoPressionado",BotaoPressionado);
		EventManager.RemoverEvento ("GameOver",GameOver);
		EventManager.RemoverEvento ("GameWon",GameWon);
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
		lblVidas.text = valor.ToString ("00");
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
		lblOrdar.text = valor.ToString ("00");
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
				Efeito ();
			break;
			case "Musica":
				Musica ();
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

	public void Efeito(){
		EventManager.ExecutarEvento ("MuteEffect", null, "");
	}

	public void Musica(){
		EventManager.ExecutarEvento ("MuteMusic", null, "");
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

	public void ChangeWaveButtonsVisibility(){
		buttonFastWave.SetActive (!buttonFastWave.activeSelf);
		buttonStartWave.SetActive (!buttonStartWave.activeSelf);
		gameFast = false;
		Time.timeScale = 1f;
	}

	public void GameOver(GameObject obj, string param){
		Time.timeScale = 0;
		panelGameOver.SetActive (true);
	}

	public void GameWon(GameObject obj, string param){
		Time.timeScale = 0;
		panelGameWon.SetActive (true);
	}
}