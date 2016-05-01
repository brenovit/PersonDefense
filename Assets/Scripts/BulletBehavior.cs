using UnityEngine;
using System.Collections;

public class BulletBehavior : MonoBehaviour {

	public float speed = 10;			//velocidade que a bala vai se moviemntar
	public int damage;					//dano que a bala vai causar
	public GameObject target;			//alvo da bala
	public Vector3 startPosition;		//posição inicial da bala
	public Vector3 targetPosition;		//posição do alva da bala

	private float  distance;			//vai medir a distancia entre o inimigo e a torre
	private float startTime;			//tempo que ela vai iniciar

	private GameManagerBehaviour gameManager;

	void Start () {
		startTime = Time.time;
		distance = Vector3.Distance (startPosition, targetPosition);
		GameObject gm = GameObject.Find ("GameManager");
		gameManager = gm.GetComponent<GameManagerBehaviour> ();
	}
	
	// Update is called once per frame
	void Update () {
		float timeInterval = Time.time - startTime;
		gameObject.transform.position = Vector3.Lerp (startPosition, targetPosition, timeInterval * speed / distance);
		if(gameObject.transform.position.Equals (targetPosition)){
			if(target != null){
				Transform healthBarTransform = target.transform.FindChild ("HealthBar");
				HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar> ();
				healthBar.currentHealth -= Mathf.Max (damage, 0);
				if(healthBar.currentHealth <= 0){
					Destroy (target);
					AudioSource audioSource = target.GetComponent<AudioSource> ();
					AudioSource.PlayClipAtPoint (audioSource.clip, transform.position);

					gameManager.Gold += 50;
				}
			}
		}
		Destroy (gameObject);	
	}
}
