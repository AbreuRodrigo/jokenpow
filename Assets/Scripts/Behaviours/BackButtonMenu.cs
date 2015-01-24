using UnityEngine;
using System.Collections;

public class BackButtonMenu : MonoBehaviour {

	void Start(){
		GameObject container = GameObject.Find("GUIRoundSelection");

		Vector3 oldPos = gameObject.transform.position;

		Vector3 size = gameObject.collider2D.bounds.size * 0.5f;

		Vector3 screenUnits = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

		gameObject.transform.position = 
			new Vector3(container.transform.position.x - screenUnits.x + size.x, oldPos.y, oldPos.z);
	}
}
