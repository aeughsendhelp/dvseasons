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
		
	[AllowNull] private static Texture2DArray texture2DArray;
	[AllowNull] public static Texture2D[] textures = new Texture2D[16];

	[AllowNull] static Texture2D loadedTexture;

	private static bool Load(UnityModManager.ModEntry modEntry) {
		Harmony ? harmony = null;

		try {
			Instance = modEntry;

			harmony = new Harmony(modEntry.Info.Id);
			harmony.PatchAll(Assembly.GetExecutingAssembly());

			Instance.OnUpdate = OnUpdate;

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
		AssetBundle testBundle = LoadAssetBundle("test");

		loadedTexture = LoadAssetFromBundle<Texture2D>(testBundle, "Textures/test/test.png");

		for(int i = 0; i < textures.Length; i++) {
			textures[i] = loadedTexture;
		}
	}
	
	public static void OnUpdate(UnityModManager.ModEntry modEntry, float dt) {
		if(Input.GetKeyDown(KeyCode.U)) {
			LoadAll();
		}	
	}

	public static void LoadAll() {
		var objs = GameObject.FindObjectsOfType<MicroSplatTerrain>(); // name can be simplified but it's actually longer so looks less simple so fuck off vs
		Material mat = objs[0].templateMaterial;

		Texture2DArray? originalArray = mat.GetTexture("_Diffuse") as Texture2DArray;

		texture2DArray = new Texture2DArray(textures[0].width, textures[0].height, textures.Length, TextureFormat.RGBA32, false); //originalArray!.format
		//texture2DArray.filterMode = originalArray.filterMode;
		//texture2DArray.wrapMode = originalArray.wrapMode;

		for(int i = 0; i < textures.Length; i++) {
			texture2DArray.SetPixels(textures[i].GetPixels(0), i, 0);
		}
		texture2DArray.Apply();

		// Near Textures
		mat.SetTexture("_Diffuse", texture2DArray);
		//mat.SetTexture("_NormalSAO", texture2DArray);
		//mat.SetTexture("_ClusterDiffuse2", texture2DArray);
		//mat.SetTexture("_ClusterNormal2", texture2DArray);
		mat.SetTexture("_ClusterDiffuse3", texture2DArray);
		//mat.SetTexture("_ClusterNormal3", texture2DArray);

		// Distance Textures
		mat.SetTexture("_DistanceResampleHackDiff", texture2DArray);
		//mat.SetTexture("_DistanceResampleHackNorm", texture2DArray);
	}




	public static AssetBundle LoadAssetBundle(string assetBundle) {
		string assPth = Path.Combine(Instance.Path.ToString(), "assets");
		string assetPath = Path.Combine(assPth, assetBundle);

		var bundle = AssetBundle.LoadFromFile(assetPath);
		if(bundle != null) {
			return bundle;
		} else {
			Error("Failed to load assetbundle \"" + assetBundle + "\"");
			return default!;
		}
	}

	public static T LoadAssetFromBundle<T>(AssetBundle assetBundle, string assetName) where T : UnityEngine.Object {
		var asset = assetBundle.LoadAsset<T>("Assets\\" + assetName);
		if(asset != null) {
			return asset;
		} else {
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
