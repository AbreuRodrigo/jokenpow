using UnityEngine;
using System.Collections;

public class GameCompanyController : MonoBehaviour {

	public CutScene cutScene;

	void Start () {
		StartCoroutine("WaitLoadMenu");
	}

	void LoadMenuScene(){
		Application.LoadLevel("Menu");
	}

	IEnumerator WaitLoadMenu(){
		yield return new WaitForSeconds(1);

		cutScene.FadeOut(LoadMenuScene);
	}
}
