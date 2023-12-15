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

	[AllowNull] public static Texture2D[] textures = new Texture2D[16];
		
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

		Texture2D loadedTexture = LoadAssetFromBundle<Texture2D>(testBundle, "Textures/fabric_pattern_07_2k/fabric_pattern_07_col_1_2k.png");
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
		Log("It loaded (I think)");

		texture2DArray = new Texture2DArray(textures[0].width, textures[0].height, textures.Length, textures[0].format, false);
		texture2DArray.filterMode = FilterMode.Bilinear;
		texture2DArray.wrapMode = TextureWrapMode.Repeat;

		for(int i = 0; i < textures.Length; i++) {
			texture2DArray.SetPixels(textures[i].GetPixels(4), i, 4);
			texture2DArray.Apply();
		}

		var objs = GameObject.FindObjectsOfType<MicroSplatTerrain>(); // name can be simplified but it's actually longer so looks less simple so fuck off vs

		for(int i = 0; i < objs.Length; i++) {
			// Near Textures
			Material mat = objs[i].templateMaterial;
			mat.SetTexture("_Diffuse", texture2DArray);
			mat.SetTexture("_NormalSAO", texture2DArray);
			mat.SetTexture("_ClusterDiffuse2", texture2DArray);
			mat.SetTexture("_ClusterNormal2", texture2DArray);
			mat.SetTexture("_ClusterDiffuse3", texture2DArray);
			mat.SetTexture("_ClusterNormal3", texture2DArray);

			// Distance Textures
			mat.SetTexture("_DistanceResampleHackDiff", texture2DArray);
			mat.SetTexture("_DistanceResampleHackNorm", texture2DArray);


			Texture2DArray? originalArray = mat.GetTexture("_Diffuse") as Texture2DArray;
		}

		//MicroSplatObject.SyncAll();
	}

	public static AssetBundle LoadAssetBundle(string assetBundle) {
		string assPth = Path.Combine(Instance.Path.ToString(), "assets");
		string assetPath = Path.Combine(assPth, assetBundle);

		var bundle = AssetBundle.LoadFromFile(assetPath);
		if(bundle != null {
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
