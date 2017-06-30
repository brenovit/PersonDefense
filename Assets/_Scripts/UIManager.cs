using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
	//public static UIManager instancia = null;
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
	public GameObject panelConfirmarReiniciar;
	public GameObject panelConfirmarSair;
	// Avaliable points amount
	public Text lblTropas;
	public Text lblOrdar;
	public Text lblVidas;
	public Text lblPontos;

	void Start ()
	{
	}

	void OnEnable(){
		EventManager.CriarEvento(""
	}

	void OnDisable(){

	}

	public void AddTropas (int valor)
	{
		SetTropas (GetTropas () + valor);
	}

	public void RemoveTropas (int valor)
	{
		SetTropas (GetTropas () - valor);
	}

	public void SetTropas (int valor)
	{
		lblTropas.text = valor.ToString ("0000");
	}

	public int GetTropas ()
	{
		int valor;
		int.TryParse (lblTropas.text, out valor);
		return valor;
	}

	public void AddVida (int valor)
	{
		SetTropas (GetTropas () + valor);
	}

	public void RemoveVida (int valor)
	{
		SetTropas (GetTropas () - valor);
	}

	public void SetVida (int valor)
	{
		lblVidas.text = valor.ToString ("0000");
	}

	public int GetVida ()
	{
		int valor;
		int.TryParse (lblVidas.text, out valor);
		return valor;
	}

	public void AddPontos (int valor)
	{
		SetPontos (GetPontos () + valor);
	}

	public void SetPontos (int valor)
	{
		lblPontos.text = valor.ToString ("0000");
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

	public void FimJogo ()
	{
		
	}

	public void PauseGame ()
	{

	}

	public void BotaoPressionado (GameObject obj, string param)
	{
		switch (param) {
		case "Pausar":
			PauseGame ();
			break;
		case "Sair":		
			break;
		case "Voltar":
			break;
		case "Reiniciar":
			break;		
		}
	}


}