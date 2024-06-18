using System;
using System.IO;
using System.Reflection;
using HarmonyLib;
using UnityModManagerNet;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;

namespace dvseasons;

public static class Main {
	[AllowNull] public static UnityModManager.ModEntry Instance { get; private set; }
		
	[AllowNull] public static Texture2DArray closeArray;
	[AllowNull] public static Texture2DArray closeArrayNorm;
	[AllowNull] public static Texture2DArray distanceArray;
	//[AllowNull] private static Texture2DArray farTextureArray;

	[AllowNull] static Texture2D loadedTexture;

	private static bool Load(UnityModManager.ModEntry modEntry) {
		Harmony ? harmony = null;

		try {
			Instance = modEntry;

			harmony = new Harmony(modEntry.Info.Id);
			harmony.PatchAll(Assembly.GetExecutingAssembly());

			Instance.OnUpdate = Update.OnUpdate;

			StartLogic();

			WorldStreamingInit.LoadingFinished += WorldLoad.LoadAll;

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
		//closeArrayNorm = LoadAssetFromBundle<Texture2DArray>(arraysBundle, "closeArrayNorm.asset");

		//cluster1Array = LoadAssetFromBundle<Texture2DArray>(arraysBundle, "cluster1Array.asset");
		//cluster1ArrayNorm = LoadAssetFromBundle<Texture2DArray>(arraysBundle, "cluster1ArrayNorm.asset");
		//cluster2Array = LoadAssetFromBundle<Texture2DArray>(arraysBundle, "cluster2Array.asset");
		//cluster2ArrayNorm = LoadAssetFromBundle<Texture2DArray>(arraysBundle, "cluster2ArrayNorm.asset");

		distanceArray = LoadAssetFromBundle<Texture2DArray>(arraysBundle, "distanceArray.asset");
		closeArrayNorm = LoadAssetFromBundle<Texture2DArray>(arraysBundle, "closeArrayNorm.asset");
		arraysBundle.Unload(false);
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
