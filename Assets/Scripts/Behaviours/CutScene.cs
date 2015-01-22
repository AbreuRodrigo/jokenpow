using UnityEngine;
using System.Collections;

public class CutScene : MonoBehaviour {

	public CutSceneState state;

	public delegate void TriggerEvents();
	public static event TriggerEvents events;

	void Awake(){
		events = null;

		switch(state){
			case CutSceneState.STARTS_FADING_IN:
				FadeIn(null);
			break;
			case CutSceneState.STARTS_FADING_OUT:
				FadeOut(null);
			break;
		}
	}

	public void FadeOut(TriggerEvents e){
		state = CutSceneState.STARTS_FADING_OUT;

		gameObject.GetComponent<Animator> ().enabled = true;
		gameObject.GetComponent<Animator>().SetBool("StartFadingOut", true);

		if(e != null){
			events += e;
		}
	}

	public void FadeIn(TriggerEvents e){
		state = CutSceneState.STARTS_FADING_IN;

		gameObject.GetComponent<Animator> ().enabled = true;
		gameObject.GetComponent<Animator>().SetBool("StartFadingIn", true);

		if(e != null){
			events += e;
		}
	}

	public void FinishCutScene(){
		Animator anm = gameObject.GetComponent<Animator>();
		
		anm.SetBool("StartFadingOut", false);
		anm.SetBool("StartFadingIn", false);
		anm.enabled = false;

		Color c = gameObject.GetComponent<SpriteRenderer>().color;

		switch(state){
			case CutSceneState.STARTS_FADING_IN:
				c.a = 0;
			break;
			case CutSceneState.STARTS_FADING_OUT:
				c.a = 1;
			break;
		}

		gameObject.GetComponent<SpriteRenderer>().color = c;

		if(events != null){
			events.Invoke();
		}
	}
}
