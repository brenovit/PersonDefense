using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{	
	void Start(){
		
	}

	public void CarregarCena (string cena)
	{		
		PlayerPrefs.DeleteAll ();
		SceneManager.LoadScene (cena);
	}

	public void Sair ()
	{
		Application.Quit ();
	}

}
