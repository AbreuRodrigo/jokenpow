using UnityEngine;
using System.Collections;

public class TextsBehaviour : MonoBehaviour {
	
	public delegate void TriggerEvents();
	public static event TriggerEvents events;

	void Awake(){
		events = null;
	}

	public void Fall(TriggerEvents e = null){
		if(e != null){
			events += e;
		}

		gameObject.GetComponent<Animator>().SetBool ("Falling", true);
	}

	public void FinishEvents(){
		if(events != null){
			events.Invoke();
		}
	}
}
