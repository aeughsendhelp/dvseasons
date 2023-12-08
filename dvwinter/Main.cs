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

namespace dvwinter;

public static class Main {
	[AllowNull] public static UnityModManager.ModEntry Instance { get; private set; }

	private static Material mat;

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
		AssetBundle microsplat = LoadAssetBundle("microsplat");
		mat = LoadAssetFromBundle<Material>(microsplat, "Terrains/MicroSplatData/MicroSplat.mat");
	}

	public static void LoadAll() {
		Log("It loaded (I think)");

		var objs = GameObject.FindObjectsOfType<MicroSplatTerrain>();
		for(int i = 0; i < objs.Length; i++) {
			objs[i].templateMaterial = mat;
		}
	}

	public static AssetBundle LoadAssetBundle(string assetBundle) {
		string assetPath = Path.Combine(Instance.Path.ToString(), "assets");

		AssetBundle loadedBundle = AssetBundle.LoadFromFile(Path.Combine(assetPath, assetBundle));
		return loadedBundle;
	}

	public static T LoadAssetFromBundle<T>(AssetBundle assetBundle, string assetName) where T : UnityEngine.Object {
		T loadedObject = assetBundle.LoadAsset<T>("Assets/" + assetName);

		if(loadedObject == null) {
			Error("Failed to load " + assetName);
			return default!;
		}

		return loadedObject;
	}

	public static GameObject InstantiateLoadedObject(GameObject toLoad, Material mat, Transform toParent) {
		GameObject obj = UnityEngine.Object.Instantiate(toLoad);

		for(int i = 0; i < obj.transform.childCount; i++) {
			obj.transform.GetChild(i).GetComponent<MeshRenderer>().material = mat;
		}

		//obj.transform.parent = toParent;
		//obj.transform.localPosition = new Vector3(0, 0, objOffset);
		//obj.transform.localRotation = Quaternion.identity;

		return obj;
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
