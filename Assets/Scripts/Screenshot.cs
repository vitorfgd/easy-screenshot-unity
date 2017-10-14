using UnityEngine;
using System.Collections;
using System.IO;
using System;

[Serializable]
public class Screenshot : MonoBehaviour {
	
	//default settings is: CREATE SCREENSHOTS FOR IOS IN LANDSCAPE MODE.
	public bool screenshot_iOS = true, iOS_isPortrait = false;

	//default setting is: CREATE SCREENSHOTS FOR ANDROID WITH THE GIVEN SIZE
	public bool screenshot_Android = true;

	//default setting is: CREATE SCREENSHOTS FOR PC WITH THE GIVEN SIZE
	public bool screenshot_PC = true;

	//default setting is: SCREENSHOT'S TEXTUREFORMAT IS NOT TRANSPARENT
	//if true: TextureFormat.ARGB32
	//if false: TextureFormat.RGB24
	public bool transparent = false;
		
	//default setting is: ANDROID SCREENSHOT IS LANDSCAPE (800X480)
	public int android_width = 800, android_height = 480;

	//default setting is: PC SCREENSHOT IS LANDSCAPE (1600X900)
	public int pc_width = 1600, pc_height = 900;

	//default setting is: FILE SIZE IS EXACTLY WHAT IS GIVEN;
	public int enlarge = 1;

	//default shortcut is: F8 KEY
	public KeyCode screenshotKey = KeyCode.F8;

	private int [] iOSRes = new int[] {960, 640, 1136, 640, 1334, 750, 2208, 1242, 2048, 1536, 2732, 2048};
	private int picture;
	private bool takeHiResShot = false;
	private TextureFormat transp = TextureFormat.ARGB32;
	private TextureFormat nonTransp = TextureFormat.RGB24;
	private string size;

	[HideInInspector]
	public int currentTab;

	public static string ScreenShotName(int photoNumber, string plataform, int width, int height){
		return string.Format("{0}/../screenshots/" + photoNumber + "_" + plataform + "screen_{1}x{2}_{3}.png", Application.dataPath, width, height, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
	}
	
	public void TakeHiResShot(){;
		takeHiResShot = true;
	}

	void LateUpdate(){

		foreach(KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))){
			if(Input.GetKey(vKey)){
				if (vKey == screenshotKey){
					takeHiResShot = true;
				}
			}
		}

		if (takeHiResShot){

			//get the picture number so no matter how many times you close your unity editor screenshots will always be on a timeline.
			picture = PlayerPrefs.GetInt ("PhotoNumber");
			picture ++;
			PlayerPrefs.SetInt ("PhotoNumber", picture);

			if (screenshot_iOS) {
				if (!iOS_isPortrait) {
					LandScapeiOS ();
				} else {
					PortraitiOS ();
				}
			}

			if (screenshot_Android){
				ScreenShotAndroid ();
			}

			if (screenshot_PC){
				ScreenshotPC ();
			}

			takeHiResShot = false;
		}
	}

	public void LandScapeiOS (){
		for (int i = 0; i < iOSRes.Length; i += 2){
			TextureFormat textForm = nonTransp;

			if (transparent)
				textForm = transp;

			if (i == 0)
				size = "3.5";
			else if (i == 2)
				size = "4";
			else if (i == 4)
				size = "4.7";
			else if (i == 6)
				size = "5.5";
			else if (i == 8)
				size = "iPad";
			else if (i == 10)
				size = "iPadPro";

			RenderTexture rt = new RenderTexture(iOSRes[i], iOSRes[i+1], 24);
			Camera.main.targetTexture = rt;
			Texture2D screenShot = new Texture2D(iOSRes[i], iOSRes[i+1], textForm, false);
			Camera.main.Render();
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect(0, 0, iOSRes[i], iOSRes[i+1]), 0, 0);
			Camera.main.targetTexture = null;
			RenderTexture.active = null;
			Destroy(rt);
			byte[] bytes = screenShot.EncodeToPNG();
			string filename = ScreenShotName(picture, "IOS_" + size + "_LANDSCAPE+", iOSRes[i] * enlarge, iOSRes[i+1] * enlarge);
			
			if (Directory.Exists(Application.dataPath + "/../screenshots/") == false)
				Directory.CreateDirectory(Application.dataPath + "/../screenshots/");
			
			System.IO.File.WriteAllBytes(filename, bytes);
			Debug.Log(string.Format("Took screenshot to: {0}", filename));
		}
	}

	public void PortraitiOS (){
		for (int i = 0; i < iOSRes.Length; i += 2){
			TextureFormat textForm = nonTransp;

			if (transparent)
				textForm = transp;

			if (i == 0)
				size = "3.5";
			else if (i == 2)
				size = "4";
			else if (i == 4)
				size = "4.7";
			else if (i == 6)
				size = "5.5";
			else if (i == 8)
				size = "iPad";
			else if (i == 10)
				size = "iPadPro";

			RenderTexture rt = new RenderTexture(iOSRes[i+1], iOSRes[i], 24);
			Camera.main.targetTexture = rt;
			Texture2D screenShot = new Texture2D(iOSRes[i+1], iOSRes[i], textForm, false);
			Camera.main.Render();
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect(0, 0, iOSRes[i+1], iOSRes[i]), 0, 0);
			Camera.main.targetTexture = null;
			RenderTexture.active = null;
			Destroy(rt);
			byte[] bytes = screenShot.EncodeToPNG();
			string filename = ScreenShotName(picture, "IOS_" + size + "_PORTRAIT+", iOSRes[i+1] * enlarge, iOSRes[i] * enlarge);
			
			if (Directory.Exists(Application.dataPath + "/../screenshots/") == false)
				Directory.CreateDirectory(Application.dataPath + "/../screenshots/");
			
			System.IO.File.WriteAllBytes(filename, bytes);
			Debug.Log(string.Format("Took screenshot to: {0}", filename));
		}
	}

	public void ScreenShotAndroid (){

		if (android_width == 0) {
			android_width = 800;
		}

		if (android_height == 0) {
			android_height = 480;
		}

		TextureFormat textForm = nonTransp;
		if (transparent)
			textForm = transp;
		RenderTexture rt = new RenderTexture(android_width * enlarge, android_height * enlarge, 24);
		Camera.main.targetTexture = rt;
		Texture2D screenShot = new Texture2D(android_width * enlarge, android_height * enlarge, textForm, false);
		Camera.main.Render();
		RenderTexture.active = rt;
		screenShot.ReadPixels(new Rect(0, 0, android_width * enlarge, android_height * enlarge), 0, 0);
		Camera.main.targetTexture = null;
		RenderTexture.active = null;
		Destroy(rt);
		byte[] bytes = screenShot.EncodeToPNG();
		string filename = ScreenShotName(picture, "ANDROID+", android_width * enlarge, android_height * enlarge);

		if (Directory.Exists(Application.dataPath + "/../screenshots/") == false)
			Directory.CreateDirectory(Application.dataPath + "/../screenshots/");

		System.IO.File.WriteAllBytes(filename, bytes);
		Debug.Log(string.Format("Took screenshot to: {0}", filename));
	}

	public void ScreenshotPC (){

		if (pc_width == 0) {
			pc_width = 1600;
		}

		if (pc_height == 0) {
			pc_height = 900;
		}

		TextureFormat textForm = nonTransp;
		if (transparent)
			textForm = transp;
		RenderTexture rt = new RenderTexture(pc_width * enlarge, pc_height * enlarge, 24);
		Camera.main.targetTexture = rt;
		Texture2D screenShot = new Texture2D(pc_width * enlarge, pc_height * enlarge, textForm, false);
		Camera.main.Render();
		RenderTexture.active = rt;
		screenShot.ReadPixels(new Rect(0, 0, pc_width * enlarge, pc_height * enlarge), 0, 0);
		Camera.main.targetTexture = null;
		RenderTexture.active = null;
		Destroy(rt);
		byte[] bytes = screenShot.EncodeToPNG();
		string filename = ScreenShotName(picture, "PC+", pc_width * enlarge, pc_height * enlarge);

		if (Directory.Exists(Application.dataPath + "/../screenshots/") == false)
			Directory.CreateDirectory(Application.dataPath + "/../screenshots/");

		System.IO.File.WriteAllBytes(filename, bytes);
		Debug.Log(string.Format("Took screenshot to: {0}", filename));
	}
}