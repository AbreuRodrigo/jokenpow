using UnityEngine;
using System.Collections;

public class PlayerStatsMenu : MonoBehaviour {

	void Awake(){
		Vector3 oldPos = gameObject.transform.position;

		float newX = Camera.main.ScreenToWorldPoint(new Vector3 (0, 0, 0)).x;

		oldPos.x = newX;

		gameObject.transform.position = oldPos;
	}
}
