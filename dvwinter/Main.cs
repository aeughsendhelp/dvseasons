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
			Instance.OnUpdate = OnUpdate;

			WorldStreamingInit.LoadingFinished += LoadAll;

		} catch (Exception ex) {
			modEntry.Logger.LogException($"Failed to load {modEntry.Info.DisplayName}:", ex);
			harmony?.UnpatchAll(modEntry.Info.Id);
			return false;
		}

		return true;
	}

	public static void StartLogic() {
		AssetBundle testBundle = LoadAssetBundle("fabric_pattern_07_2k");

		textures[0] = LoadAssetFromBundle<Texture2D>(testBundle, "Textures/fabric_pattern_07_2k/fabric_pattern_07_col_1_2k.png");

		//Log("test1");
		//textures[0] = LoadAssetFromBundle<Texture2D>(testBundle, "Textures/test/test1.png");
		//Log("test2");
		//textures[1] = LoadAssetFromBundle<Texture2D>(testBundle, "Textures/test/test2.png");
		//Log("test3");
		//textures[2] = LoadAssetFromBundle<Texture2D>(testBundle, "Textures/test/test3.png");
		//Log("test4");
		//textures[3] = LoadAssetFromBundle<Texture2D>(testBundle, "Textures/test/test4.png");
	}

	public static void OnUpdate(UnityModManager.ModEntry modEntry, float dt) {
		if(Input.GetKeyDown(KeyCode.U)) {
			LoadAll();
		}	
	}
	
	public static void LoadAll() {
		Log("It loaded (I think)");

		Texture2DArray newArray = new Texture2DArray(textures[0].width, textures[0].height, textures.Length, textures[0].format, false);
		newArray.filterMode = FilterMode.Bilinear;
		newArray.wrapMode = TextureWrapMode.Repeat;

		for(int i = 0; i < textures.Length; i++) {
			Log(i.ToString());
			newArray.SetPixels(textures[i].GetPixels(0), i, 0);
			newArray.Apply();
			Log(i.ToString());
		}

		var objs = GameObject.FindObjectsOfType<MicroSplatTerrain>(); // name can be simplified but it's actually longer so looks less simple so fuck off vs

		for(int i = 0; i < objs.Length; i++) {
			objs[i].templateMaterial.SetTexture("_Diffuse", texture2DArray);
			objs[i].templateMaterial.SetTexture("_ClusterDiffuse2", texture2DArray);
			objs[i].templateMaterial.SetTexture("_ClusterDiffuse3", texture2DArray);

			//	Texture2DArray originalArray = objs[i].templateMaterial.GetTexture("_Diffuse");
		}

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
		try { // This try statement doesn't work here either
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
