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
		MessageFallAnimation();
	}

	public void RunWinMessage(){
		gameObject.GetComponent<SpriteRenderer>().sprite = win;
		MessageFallAnimation();
	}

	public void RunLoseMessage(){
		gameObject.GetComponent<SpriteRenderer>().sprite = lose;
		MessageFallAnimation();
	}

	private void MessageFallAnimation(){
		gameObject.GetComponent<Animator>().SetBool ("MessageFall", true);
	}
}
