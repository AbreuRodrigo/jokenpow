using UnityEngine;
using System.Collections;

public class RoundCounterBehaviour : MonoBehaviour {
	public TextMesh text;
	public TextMesh textShadow;

	private Animator animator;

	private GamePlayController game;

	private TextsController texts;

	void Awake(){
		animator = gameObject.GetComponent<Animator>();
		texts = GameObject.FindObjectOfType<TextsController>();
		game = GameObject.FindObjectOfType<GamePlayController>();
	}

	public void PlayShowAnimation(){
		AdjustRoundText();
		animator.Play("Show");
	}

	public void PlayHideAnimation(){
		animator.Play("Hide");
	}

	private void AdjustRoundText(){
		string newText = !texts.IsFinalRound() ? ("Round " + texts.CurrentRound) : ("Final Round");

		text.text = textShadow.text = newText;
	}

	public void ShowCardsOnGamePlay(){
		game.ShowCards();
	}
}
