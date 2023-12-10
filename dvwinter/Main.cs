using System;
using System.IO;
using System.Reflection;
using HarmonyLib;
using UnityModManagerNet;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;
using DV.Scenarios;
using System.Linq;
using JBooth.MicroSplat;
using UnityEngine.Assertions;
using VRTK.Examples.Archery;

namespace dvwinter;

public static class Main {
	[AllowNull] public static UnityModManager.ModEntry Instance { get; private set; }

	[AllowNull] public static Texture2D[] textures = new Texture2D[1];
		
	[AllowNull] private static Texture2DArray texture2DArray;

	private static bool Load(UnityModManager.ModEntry modEntry) {
		Harmony ? harmony = null;

		try {
			Instance = modEntry;

			harmony = new Harmony(modEntry.Info.Id);
			harmony.PatchAll(Assembly.GetExecutingAssembly());

			// Other plugin startup logic
			StartLogic();

			WorldStreamingInit.LoadingFinished += LoadAll;

		} catch (Exception ex) {
			modEntry.Logger.LogException($"Failed to load {modEntry.Info.DisplayName}:", ex);
			harmony?.UnpatchAll(modEntry.Info.Id);
			return false;
		}

		return true;
	}

	public static void StartLogic() {
		AssetBundle testBundle = LoadAssetBundle("snowed");
		textures[0] = LoadAssetFromBundle<Texture2D>(testBundle, "Textures/snow_02/snow_02_diff_2k.png");
		Log(textures[0].name);
	}
	
	public static void LoadAll() {
		Log("It loaded (I think)");


		Texture2DArray newArray = new Texture2DArray(textures[0].width, textures[0].height, textures.Length, textures[0].format, false);
		newArray.filterMode = FilterMode.Bilinear;
		newArray.wrapMode = TextureWrapMode.Repeat;

		Log("#1");
		var pix = textures[0].GetPixels(0);
		Log("#2");
		newArray.SetPixels(pix, 0, 0);
		Log("#3");

		//for(int i = 0; i < textures.Length; i++) {
		//	newArray.SetPixels(textures[i].GetPixels(0), i, 0);
		//}

		//var objs = GameObject.FindObjectsOfType<MicroSplatTerrain>(); // name can be simplified but it's actually longer so looks less simple so fuck off vs
		//for(int i = 0; i < objs.Length; i++) {
		//objs[i].templateMaterial.SetTexture("_Diffuse", texture2DArray);

		//Texture2DArray originalArray = objs[i].templateMaterial.GetTexture("_Diffuse");


		//for(int j = 0; j < textures.Length; j++) {
		//	newArray.SetPixels(textures[j].GetPixels(), j);
		//}


		//newArray.Apply();
		// }
		//MicroSplatObject.SyncAll();
	}
	
	public static AssetBundle LoadAssetBundle(string assetBundle) {
		try { // I don't think this try statement actually catches things lol, woops
			string assPth = Path.Combine(Instance.Path.ToString(), "assets");
			string assetPath = Path.Combine(assPth, assetBundle);

			Log(assetPath);
			return AssetBundle.LoadFromFile(assetPath);
		} catch {
			Error("Failed to load assetbundle \"" + assetBundle + "\"");
			return default!;
		}
	}

	public static T LoadAssetFromBundle<T>(AssetBundle assetBundle, string assetName) where T : UnityEngine.Object {
		try {

			return assetBundle.LoadAsset<T>("Assets\\" + assetName);
		} catch {
			Error("Failed to load asset \"" + assetName + "\" from \"" + assetBundle.name + "\"");
			return default!;
		}
	}

	// Logger Commands
	public static void Log(string message) {
		Instance.Logger.Log(message);
	}
	public static void Warning(string message) {
		Instance.Logger.Warning(message);
	}
	public static void Error(string message) {
		Instance.Logger.Error(message);
	}
}
