using UnityEngine;
using System.Collections;

public class NewPlaceTower : MonoBehaviour {
	public GameObject torre;
	private Vector3 mousePosition;
	public GameObject	selector;

	private GameManagerBehaviour gameManager;

	void Awake() {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManagerBehaviour> ();			//o gamemanager vai procurar o gameobject que tiver o componente gamemanagerbehaiour
	}

	void OnMouseUp(){
		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
