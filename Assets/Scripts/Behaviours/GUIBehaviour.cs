using UnityEngine;
using System.Collections;

public class GUIBehaviour : MonoBehaviour {

	public void SlideOutLeft(){
		gameObject.GetComponent<Animator> ().SetBool("SlideOutRight", false);
		gameObject.GetComponent<Animator> ().SetBool("SlideOutLeft", true);
	}

	public void SlideOutRight(){
		gameObject.GetComponent<Animator> ().SetBool("SlideOutLeft", false);
		gameObject.GetComponent<Animator> ().SetBool("SlideOutRight", true);
	}
}
