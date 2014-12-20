#if UNITY_EDITOR
using UnityEditor;

public class Sprite2GUI {

	[MenuItem("Sprite2GUI/Update SpriteButtons %F12")]
	static void Init() {

		if(EditorUtility.DisplayDialog("Update SpriteButtons", "Do you really want to update all spriteButtons?", "Update", "Cancel")){
			SpriteButtonUtils.UpdateSpriteButtonsOnEditor(true);
		}
	}
}
#endif