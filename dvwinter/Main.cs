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
		
	[AllowNull] private static Texture2DArray closeTextureArray;
	[AllowNull] private static Texture2DArray distanceTextureArray;
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
		AssetBundle testBundle = LoadAssetBundle("terraintexturearray"); // needs to be changed to plural

		closeTextureArray = LoadAssetFromBundle<Texture2DArray>(testBundle, "closeArray.asset");
		distanceTextureArray = LoadAssetFromBundle<Texture2DArray>(testBundle, "distanceArray.asset");
		//farTextureArray = LoadAssetFromBundle<Texture2DArray>(testBundle, "farArray.asset");
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
		mat.SetTexture("_Diffuse", closeTextureArray);
		//mat.SetTexture("_NormalSAO", texture2DArray);
		mat.SetTexture("_ClusterDiffuse2", closeTextureArray);
		//mat.SetTexture("_ClusterNormal2", texture2DArray);
		mat.SetTexture("_ClusterDiffuse3", closeTextureArray);
		//mat.SetTexture("_ClusterNormal3", texture2DArray);

		// Distance Textures
		mat.SetTexture("_DistanceResampleHackDiff", distanceTextureArray);
		//mat.SetTexture("_DistanceResampleHackNorm", texture2DArray);

		// Far textures
		GameObject distTer = GameObject.Find("DistantTerrain_w3");

		MeshRenderer[] componentsInChildren = distTer.GetComponentsInChildren<MeshRenderer>(includeInactive: true);
		foreach(MeshRenderer meshRenderer in componentsInChildren) {
			meshRenderer.sharedMaterial.SetTexture("_Splats", distanceTextureArray);
			//meshRenderer.sharedMaterial.SetTexture("_SplatNormals", texture2DArray);

			Log(meshRenderer.sharedMaterial.name);
		}
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
