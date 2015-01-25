using UnityEngine;
using System.Collections;

public class RoundCounterBehaviour : MonoBehaviour {
	public TextMesh text;
	public TextMesh textShadow;

	private Animator animator;

	public delegate void AfterEvents();
	public event AfterEvents showEvents;
	public event AfterEvents hideEvents;

	private TextsController texts;

	void Awake(){
		animator = gameObject.GetComponent<Animator>();
		texts = GameObject.FindObjectOfType<TextsController>();
	}

	public void AddShowEvents(AfterEvents events){
		AdjustRoundText();
		showEvents += events;
	}

	public void AddHideEvents(AfterEvents events){
		AdjustRoundText();
		hideEvents += events;
	}

	public void PlayShowAnimation(){
		AdjustRoundText();
		animator.Play("Show");
	}

	public void PlayHideAnimation(){
		animator.Play("Hide");
	}

	public void AfterShowEvents(){
		if(showEvents != null){
			showEvents.Invoke();
		}
	}

	public void AfterHideEvents(){
		if(hideEvents != null){
			hideEvents.Invoke();
		}
	}

	private void AdjustRoundText(){
		string newText = !texts.IsFinalRound() ? ("Round " + texts.CurrentRound) : ("Final Round");

		text.text = textShadow.text = newText;
	}
}
