using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {

    public void bt_start() {
        Debug.Log("Mudou de cena");
    }

    public void bt_opcao(){
        Debug.Log("Opção ativada");
    }

    public void bt_exit() {
        //Debug.Log("Você fechou o jogo");
        Application.Quit();
    }

    public void ChamaCenaFases()  {
        Application.LoadLevel("Fases");
    }

    public void ChamaVolume(){
        Application.LoadLevel("Volumes");
    }

    public void chamaMenu(){
        Application.LoadLevel("Menu");
    }

}
