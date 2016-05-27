using UnityEngine;
using System.Collections;

public class MovingShip : MonoBehaviour {

	[SerializeField]
	GameObject ship = null;

	[SerializeField]
	Transform startTransform = null;

	[SerializeField]
	Transform endTransform = null;

	[SerializeField]
	float platformSpeed = 0f;

	Vector3 direction;
	Transform destination;
	bool shipHere = false;

	void FixedUpdate(){
		if (ship == null){
			ship = GameObject.FindGameObjectWithTag ("Ship");
			shipHere = true;
			return;
		}
		if(shipHere){
			SetDestination(startTransform);
			shipHere = false;
		}
		ship.GetComponent<Rigidbody>().MovePosition(ship.transform.position + direction * platformSpeed * Time.fixedDeltaTime);

		if(Vector3.Distance (ship.transform.position, destination.position) < platformSpeed * Time.fixedDeltaTime){
			SetDestination(destination == startTransform ? endTransform : startTransform);
		}
	}

	void SetDestination(Transform dest){
		destination = dest;
		direction = (destination.position - ship.transform.position).normalized;
	}

}
