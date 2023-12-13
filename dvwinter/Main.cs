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
			Material mat = objs[i].templateMaterial;
			mat.SetTexture("_Diffuse", texture2DArray);
			mat.SetTexture("_ClusterDiffuse2", texture2DArray);
			mat.SetTexture("_ClusterDiffuse3", texture2DArray);

		        texture2DArray = mat.GetTexture("_Diffuse") as Texture2DArray;
			// Texture2D textureArray[] = new Texture2D[]
		 //        for (int i = 0; i < texture2DArray.depth; i++) {
		 //            Color32[] pixels = texture2DArray.GetPixels32(i, 0);
		 //            Texture2D texture = new Texture2D(texture2DArray.width, texture2DArray.height);
		 //            texture.SetPixels32(pixels);
		 //            texture.Apply();
		 //            textureArray.Add(texture); //List of Texture2D
		 //        }
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
