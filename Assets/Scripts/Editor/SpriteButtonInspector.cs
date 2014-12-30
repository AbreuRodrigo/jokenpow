#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpriteButton))]
public class SpriteButtonInspector : Editor {

	public override void OnInspectorGUI(){
		if(!EditorApplication.isPlaying){
			SpriteButtonUtils.UpdateSpriteButtonsOnEditor(false);

			SpriteButton button = (SpriteButton)target;

			if(!button.anchor.Equals(SpriteButtonAnchor.NONE)){
				button.screenID = EditorGUILayout.IntField("Screen ID", button.screenID);
			}

			button.anchor = (SpriteButtonAnchor) EditorGUILayout.EnumPopup("Anchor", button.anchor);

			if(!button.anchor.Equals(SpriteButtonAnchor.NONE)){
				button.marginTop = EditorGUILayout.FloatField("Margin Top", button.marginTop);
				button.marginBottom = EditorGUILayout.FloatField("Margin Bottom", button.marginBottom);
				button.marginLeft = EditorGUILayout.FloatField("Margin Left", button.marginLeft);
				button.marginRight = EditorGUILayout.FloatField("Margin Right", button.marginRight);
			}

			if(GUI.changed){
				EditorUtility.SetDirty(target);
			}
		}
	}
}
#endif