using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Screenshot))]
public class ScreenshotEditor : Editor {

	override public void OnInspectorGUI() {

		Screenshot screenshot = target as Screenshot;

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("This script was developed to easily create screenshots for AppStore (according to apple's guidelines), android and desktop.", EditorStyles.helpBox);
		EditorGUILayout.Space();

		screenshot.currentTab = GUILayout.Toolbar (screenshot.currentTab, new string [] {"iOS", "Android", "Desktop"});

		EditorGUILayout.Space();

		if (screenshot.currentTab == 0){
			EditorGUILayout.LabelField("Create iOS Screenshots", EditorStyles.boldLabel);
			screenshot.screenshot_iOS = EditorGUILayout.Toggle("iOS Screenshots", screenshot.screenshot_iOS);

			if (screenshot.screenshot_iOS) {
				screenshot.iOS_isPortrait = EditorGUILayout.Toggle ("Portrait mode", screenshot.iOS_isPortrait);
				EditorGUILayout.LabelField("Checking this box will create 6 different portrait screenshots according to apple's screenshot size guidelines. Leaving this box unchecked will create 6 different landscape screenshot according to apple's screenshot size guidelines.", EditorStyles.helpBox);
			}
		}

		if (screenshot.currentTab == 1) {
			EditorGUILayout.LabelField("Create Android Screenshots", EditorStyles.boldLabel);
			screenshot.screenshot_Android = EditorGUILayout.Toggle("Android Screenshots", screenshot.screenshot_Android);

			if (screenshot.screenshot_Android) {
				EditorGUILayout.Space();
				screenshot.android_width = EditorGUILayout.IntField("Width: ", screenshot.android_width);
				screenshot.android_height = EditorGUILayout.IntField("Height: ", screenshot.android_height);
				EditorGUILayout.LabelField("Android does not have specific size. (Min. Size: 320px; Max. Size: 3840px.)", EditorStyles.helpBox);
				EditorGUILayout.LabelField("Recommended: 480x800 (Portrait) or 800x480 (Landscape). You can use the slider below to enlarge your screenshot if needed.", EditorStyles.helpBox);

				EditorGUILayout.Space();

				EditorGUILayout.LabelField("Enlarge", EditorStyles.boldLabel);
				screenshot.enlarge = Mathf.RoundToInt(GUILayout.HorizontalSlider(screenshot.enlarge, 1f, 6f));
				EditorGUILayout.LabelField("This will enlarge your desktop and android screenshots (this will no be applied to iOS screenshots due to size guidelines in AppStore).", EditorStyles.helpBox);
			}
		}

		if (screenshot.currentTab == 2){
			EditorGUILayout.LabelField("Create Desktop Screenshots", EditorStyles.boldLabel);
			screenshot.screenshot_PC = EditorGUILayout.Toggle("Desktop Screenshots", screenshot.screenshot_PC);

			if (screenshot.screenshot_PC) {
				EditorGUILayout.Space();
				screenshot.pc_width = EditorGUILayout.IntField("Width: ", screenshot.pc_width);
				screenshot.pc_height = EditorGUILayout.IntField("Height: ", screenshot.pc_height);
				EditorGUILayout.LabelField("Recommended: 1600x900 (Portrait) or 900x1600 (Landscape). You can use the slider below to enlarge your screenshot if needed.", EditorStyles.helpBox);

				EditorGUILayout.Space();

				EditorGUILayout.LabelField("Enlarge", EditorStyles.boldLabel);
				screenshot.enlarge = Mathf.RoundToInt(GUILayout.HorizontalSlider(screenshot.enlarge, 1f, 6f));
				EditorGUILayout.LabelField("This will enlarge your desktop and android screenshots (it will no be applied to iOS screenshots due to size guidelines in AppStore).", EditorStyles.helpBox);
			}
		}

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Screenshot Shortcut", EditorStyles.boldLabel);
		screenshot.screenshotKey = (KeyCode)EditorGUILayout.EnumPopup("Shortcut: ", screenshot.screenshotKey);
		EditorGUILayout.Space();
	}
}
