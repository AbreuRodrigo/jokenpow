using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpriteButtonUtils {

	public static Vector3 CalculateAnchors(SpriteButton button) {
		Vector3 newVector = button.transform.position;

		int id = button.screenID;

		float w = Screen.width;
		float h = Screen.height;

		if(Application.platform.Equals(RuntimePlatform.WindowsEditor) ||
		   Application.platform.Equals(RuntimePlatform.OSXEditor)){
			w = EditorRes().x;
			h = EditorRes().y;
		}
		
		Vector3 s = button.gameObject.collider2D.bounds.size * 0.5f;
		
		Vector3 pw = Camera.main.ScreenToWorldPoint(new Vector3(w - (w * id), 0, 10));
		Vector3 ph = Camera.main.ScreenToWorldPoint(new Vector3(0, h, 10));

		float wLimit = pw.x;
		float hLimit = ph.y;

		float ml = button.marginLeft / wLimit;
		float mr = button.marginRight / wLimit;
		float mt = button.marginTop / hLimit;
		float mb = button.marginBottom / hLimit;
		float mh = mr - ml;
		float mv = mt - mb;
		
		if(!button.anchor.Equals(SpriteButtonAnchor.NONE)){
			switch(button.anchor){
			case SpriteButtonAnchor.TOP_LEFT:
				newVector = ph - new Vector3(-s.x + mh, s.y + mv, 0);
				break;
			case SpriteButtonAnchor.TOP_CENTER:
				newVector = ph - new Vector3(-pw.x + mh, s.y + mv, 0);
				break;
			case SpriteButtonAnchor.TOP_RIGHT:
				newVector = ph - new Vector3(-pw.x * 2 + s.x + mh, s.y + mv, 0);
				break;
			case SpriteButtonAnchor.MIDDLE_LEFT:
				newVector = new Vector3(-pw.x + s.x - mh, 0 - mv, 0);
				break;
			case SpriteButtonAnchor.MIDDLE_CENTER:
				newVector = new Vector3(0 - mh, 0 - mv, 0);
				break;
			case SpriteButtonAnchor.MIDDLE_RIGHT:
				newVector = new Vector3(pw.x - s.x - mh, 0 - mv, 0);
				break;
			case SpriteButtonAnchor.BOTTOM_LEFT:
				newVector = new Vector3(-pw.x + s.x - mh, -ph.y + s.y - mv, 0);
				break;
			case SpriteButtonAnchor.BOTTOM_CENTER:
				newVector = new Vector3(0 - mh, -ph.y + s.y - mv, 0);
				break;
			case SpriteButtonAnchor.BOTTOM_RIGHT:
				newVector = new Vector3(pw.x - s.x - mh, -ph.y + s.y - mv, 0);
				break;
			}
		}

		return newVector;
	}

	public static void UpdateSpriteButtonsOnEditor(bool force){
		#if UNITY_EDITOR
		if(!EditorApplication.isPlaying || force){
			SpriteButton[] buttons = GameObject.FindObjectsOfType<SpriteButton>();
			
			if(buttons != null && buttons.Length > 0) {
				foreach(SpriteButton btn in buttons){
					btn.gameObject.transform.position = CalculateAnchors(btn);
				}
			}
		}
		#endif
	}

	private static Vector2 EditorRes() {
		System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
		System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", 
		                                                                 System.Reflection.BindingFlags.NonPublic | 
		                                                                 System.Reflection.BindingFlags.Static);
		System.Object res = GetSizeOfMainGameView.Invoke(null, null);
		
		return (Vector2)res;
	}
}