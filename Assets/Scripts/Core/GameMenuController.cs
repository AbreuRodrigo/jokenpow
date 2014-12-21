using UnityEngine;
using System.Collections;

public class GameMenuController : MonoBehaviour {

	void Update () {
		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
	}

	public static void LoadGamePlay(){
		Application.LoadLevel("GamePlaySingle");
	}
}
