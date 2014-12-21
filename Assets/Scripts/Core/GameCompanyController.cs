using UnityEngine;
using System.Collections;

public class GameCompanyController : MonoBehaviour {

	public CutScene cutScene;

	void Awake(){
		Screen.fullScreen = true;
	}

	void Start () {
		StartCoroutine("WaitLoadMenu");
	}

	void LoadMenuScene(){
		Application.LoadLevel("Menu");
	}

	IEnumerator WaitLoadMenu(){
		yield return new WaitForSeconds(3);

		cutScene.FadeOut(LoadMenuScene);
	}
}
