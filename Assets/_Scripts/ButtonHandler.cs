using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour {

	public void BotaoPressionado(string nomeBotao){
		EventManager.ExecutarEvento ("BotaoPressionado", null, nomeBotao);

	}

	public void PlaySound(string audioName){
		EventManager.ExecutarEvento ("PlaySound", null, audioName);
	}
}
