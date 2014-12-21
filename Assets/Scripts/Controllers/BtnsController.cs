using UnityEngine;
using System.Collections;

public class BtnsController : MonoBehaviour {

	public void ShowButtons(){
		gameObject.GetComponent<Animator>().SetBool("HideButtons", false);
		gameObject.GetComponent<Animator>().SetBool("ShowButtons", true);	
	}

	public void HideButtons(){
		gameObject.GetComponent<Animator>().SetBool("ShowButtons", false);	
		gameObject.GetComponent<Animator>().SetBool("HideButtons", true);	
	}
}
