using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildTree : MonoBehaviour {

	public Text upgradeValue;
	public Text destroyValue;
	private PlaceTower placeTower;

	public void SetPlaceTower(PlaceTower placeObj){
		placeTower = placeObj;
	}

	public PlaceTower GetPlaceTower(){
		return placeTower;
	}

	public void UpgradeTower(){
		this.placeTower.UpgradeTower();
		this.placeTower = null;
	}

	public void DestroyTower(){
		this.placeTower.DestroyTower();
		this.placeTower = null;
	}

	public void SetTower(GameObject tower){		
		TowerData td = tower.GetComponent<TowerData> ();
		upgradeValue.text = td.CurrentLevel.tropas.ToString("0000");
		destroyValue.text = td.CurrentLevel.tropas.ToString("0000");
	}
}
