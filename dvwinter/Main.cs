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
using DV.WorldTools;

namespace dvwinter;

public static class Main {
	[AllowNull] public static UnityModManager.ModEntry Instance { get; private set; }
		
	[AllowNull] private static Texture2DArray closeArray;
	[AllowNull] private static Texture2DArray closeArrayNorm;
	[AllowNull] private static Texture2DArray distanceArray;
	//[AllowNull] private static Texture2DArray farTextureArray;

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
		AssetBundle arraysBundle = LoadAssetBundle("terraintexturearray"); // needs to be changed to plural

		closeArray = LoadAssetFromBundle<Texture2DArray>(arraysBundle, "closeArray.asset");
		closeArrayNorm = LoadAssetFromBundle<Texture2DArray>(arraysBundle, "closeArrayNorm.asset");

		//cluster1Array = LoadAssetFromBundle<Texture2DArray>(arraysBundle, "cluster1Array.asset");
		//cluster1ArrayNorm = LoadAssetFromBundle<Texture2DArray>(arraysBundle, "cluster1ArrayNorm.asset");

		//cluster2Array = LoadAssetFromBundle<Texture2DArray>(arraysBundle, "cluster2Array.asset");
		//cluster2ArrayNorm = LoadAssetFromBundle<Texture2DArray>(arraysBundle, "cluster2ArrayNorm.asset");

		distanceArray = LoadAssetFromBundle<Texture2DArray>(arraysBundle, "distanceArray.asset");
		closeArrayNorm = LoadAssetFromBundle<Texture2DArray>(arraysBundle, "closeArrayNorm.asset");

	}

	public static void OnUpdate(UnityModManager.ModEntry modEntry, float dt) {
		if(Input.GetKeyDown(KeyCode.U)) {
			LoadAll();
		}	
	}

	public static void LoadAll() {
		var objs = GameObject.FindObjectsOfType<MicroSplatTerrain>(); // name can be simplified but it's actually longer so looks less simple so fuck off vs
		Material mat = objs[0].templateMaterial;

		// Near Textures
		mat.SetTexture("_Diffuse", closeArray);
		//mat.SetTexture("_NormalSAO", closeArrayNorm);
		mat.SetTexture("_ClusterDiffuse2", closeArray);
		//mat.SetTexture("_ClusterNormal2", texture2DArray);
		mat.SetTexture("_ClusterDiffuse3", closeArray);
		//mat.SetTexture("_ClusterNormal3", texture2DArray);

		// Distance Textures
		mat.SetTexture("_DistanceResampleHackDiff", distanceArray);
		//mat.SetTexture("_DistanceResampleHackNorm", texture2DArray);

		// Far textures
		GameObject distanceTerrrain = GameObject.Find("DistantTerrain_w3");

		MeshRenderer[] componentsInChildren = distanceTerrrain.GetComponentsInChildren<MeshRenderer>(true); // idk why includeinactive is true here
		componentsInChildren[0].sharedMaterial.SetTexture("_Splats", distanceArray);
		//meshRenderer.sharedMaterial.SetTexture("_SplatNormals", distanceArrayNorm);
	}

	private static bool IsTerrainMesh(MeshRenderer mr) {
		if((bool) mr.sharedMaterial) {
			return mr.sharedMaterial.HasProperty("_WorldNormalmap");
		}

		return false;
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
