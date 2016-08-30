using UnityEngine;
using System.Collections;
using System.IO;

public class HighResSS : MonoBehaviour {

	public bool TakeScreenShot_iOS = false, iOSPortrait = false, TakeScreenShotAndroid = false, androidPortrait = false;
	private int [] iOSRes = new int[] {960, 640, 1136, 640, 1334, 750, 2208, 1242, 2048, 1536, 2732, 2048};
	private int picture = 0;

	[Tooltip("Android does not have specific size (Min. Size: 320px; Max. Size: 3840px.)")]
	public int android_width, android_height;

	[Range(1, 6)]
	public int enlarge = 1;
	
	public bool transparent = false;
	
	public KeyCode screenshotKey = KeyCode.F8;
	private bool takeHiResShot = false;
	private TextureFormat transp = TextureFormat.ARGB32;
	private TextureFormat nonTransp = TextureFormat.RGB24;
	private string size;

	public static string ScreenShotName(int photoNumber, string plataform, int width, int height){
		return string.Format("{0}/../screenshots/" + photoNumber + "_" + plataform + "screen_{1}x{2}_{3}.png",
		                     Application.dataPath,
		                     width, height,
		                     System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
	}
	
	public void TakeHiResShot(){;
		takeHiResShot = true;
	}

	void LateUpdate(){
		takeHiResShot |= Input.GetKeyDown(KeyCode.F8);
		if (takeHiResShot){
			picture ++;
			if (TakeScreenShot_iOS && !TakeScreenShotAndroid){
				if (!iOSPortrait)
					LandScapeiOS ();
				else if (iOSPortrait)
					PortraitiOS ();
			}

			else if (!TakeScreenShot_iOS && TakeScreenShotAndroid){
				ScreenShotAndroid ();
			}

			else if (TakeScreenShot_iOS && TakeScreenShotAndroid){
				if (!iOSPortrait)
					LandScapeiOS ();
				else if (iOSPortrait)
					PortraitiOS ();
				ScreenShotAndroid();
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

			RenderTexture rt = new RenderTexture(iOSRes[i] * enlarge, iOSRes[i+1] * enlarge, 24);
			Camera.main.targetTexture = rt;
			Texture2D screenShot = new Texture2D(iOSRes[i] * enlarge, iOSRes[i+1] * enlarge, textForm, false);
			Camera.main.Render();
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect(0, 0, iOSRes[i] * enlarge, iOSRes[i+1] * enlarge), 0, 0);
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

			RenderTexture rt = new RenderTexture(iOSRes[i+1] * enlarge, iOSRes[i] * enlarge, 24);
			Camera.main.targetTexture = rt;
			Texture2D screenShot = new Texture2D(iOSRes[i+1] * enlarge, iOSRes[i] * enlarge, textForm, false);
			Camera.main.Render();
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect(0, 0, iOSRes[i+1] * enlarge, iOSRes[i] * enlarge), 0, 0);
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
			if (!androidPortrait)
				android_width = 1600;
			else
				android_width = 900;
		}

		if (android_height == 0) {
			if (!androidPortrait)
				android_height = 900;
			else
				android_height = 1600;
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
}