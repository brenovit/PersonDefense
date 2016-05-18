using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TowerSelect : MonoBehaviour {

	public GameObject Tower;
	private Sprite imagem;

	public void OnMouseDown(){
		CliqueiNoBagulho ();
	}

	public void CliqueiNoBagulho(){
		print ("Clicou em: "+gameObject.name);
	}

	// Use this for initialization
	void Start () {
		GameObject aux = Tower.gameObject.transform.FindChild ("Monster2").gameObject;
		imagem = aux.GetComponent<SpriteRenderer> ().sprite;
		this.gameObject.GetComponent<Image> ().sprite = imagem;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown (0)){
			if(EventSystem.current.IsPointerOverGameObject()){
				CliqueiNoBagulho ();
			}
		}
	}
}
