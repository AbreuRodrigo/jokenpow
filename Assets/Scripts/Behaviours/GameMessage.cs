using UnityEngine;
using System.Collections;

public class GameMessage : MonoBehaviour {
	public Sprite draw;
	public Sprite win;
	public Sprite lose;

	void Start () {	
	}

	public void RunDrawMessage(){
		gameObject.GetComponent<SpriteRenderer>().sprite = draw;
		ShowMessage();
	}

	public void RunWinMessage(){
		gameObject.GetComponent<SpriteRenderer>().sprite = win;
		ShowMessage();
	}

	public void RunLoseMessage(){
		gameObject.GetComponent<SpriteRenderer>().sprite = lose;
		ShowMessage();
	}

	private void ShowMessage(){
		gameObject.GetComponent<Animator>().SetBool ("MessageHide", false);
		gameObject.GetComponent<Animator>().SetBool ("MessageShow", true);
	}

	public void HideMessage(){
		gameObject.GetComponent<Animator>().SetBool ("MessageShow", false);
		gameObject.GetComponent<Animator>().SetBool ("MessageHide", true);
	}
}
