using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteButton : MonoBehaviour {
	public SpriteButtonAnchor anchor = SpriteButtonAnchor.NONE;
	public int marginLeft = 0;
	public int marginRight = 0;
	public int marginTop = 0;
	public int marginBottom = 0;

	void Awake(){
		Settings();

		#if !UNITY_EDITOR
		if(!anchor.Equals(SpriteButtonAnchor.NONE)){
			gameObject.transform.position = SpriteButtonUtils.CalculateAnchors(this);
		}
		#endif
	}

	private void Settings(){
		if(!collider2D) {
			Debug.LogWarning("Sprite2GUI - The object " + gameObject.name + " doesn't have a collider2D, a boxCollider2D will be attached to it.");
			gameObject.AddComponent<BoxCollider2D>();
		}
	}

	void OnMouseDown(){
		Animator anm = gameObject.GetComponent<Animator>();
		anm.SetBool("hasPressed", true);
		SpriteButtonEvents.Instance.OnPress(this);
	}

	void OnMouseUp(){
		Animator anm = gameObject.GetComponent<Animator>();
		anm.SetBool("hasReleased", true);
		SpriteButtonEvents.Instance.OnRelease(this);
	}

	void StopAnimations(){
		Animator anm = gameObject.GetComponent<Animator>();
		anm.SetBool("hasReleased", false);
		anm.SetBool("hasPressed", false);
	}

	public void Enable(){
		this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
		this.gameObject.GetComponent<Animator>().enabled = true;
		this.gameObject.collider2D.enabled = true;
	}

	public void Disable(){
		this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
		this.gameObject.GetComponent<Animator>().enabled = false;
		this.gameObject.collider2D.enabled = false;
	}
}