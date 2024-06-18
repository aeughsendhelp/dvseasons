using JBooth.MicroSplat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace dvseasons;

internal class WorldLoad {
	public static void LoadAll() {
		var objs = GameObject.FindObjectsOfType<MicroSplatTerrain>(); // name can be simplified but it's actually longer so looks less simple so fuck off vs
		Material mat = objs[0].templateMaterial;

		// Near Textures
		mat.SetTexture("_Diffuse", Main.closeArray);
		//mat.SetTexture("_NormalSAO", closeArrayNorm);

		mat.SetTexture("_ClusterDiffuse2", Main.closeArray);
		//mat.SetTexture("_ClusterNormal2", closeArrayNorm);
		mat.SetTexture("_ClusterDiffuse3", Main.closeArray);
		//mat.SetTexture("_ClusterNormal3", closeArrayNorm);

		// Distance Textures
		mat.SetTexture("_DistanceResampleHackDiff", Main.distanceArray);
		//mat.SetTexture("_DistanceResampleHackNorm", texture2DArray);

		// Far textures
		GameObject distanceTerrrain = GameObject.Find("DistantTerrain_w3");

		MeshRenderer[] componentsInChildren = distanceTerrrain.GetComponentsInChildren<MeshRenderer>(true); // idk why includeinactive is true here
		componentsInChildren[0].sharedMaterial.SetTexture("_Splats", Main.distanceArray);

		//for(int i = 1; i < componentsInChildren.Length; i++) {
		//	componentsInChildren[i].gameObject.SetActive(false);
		//	componentsInChildren[i].gameObject.SetActive(true);
		//}
		//meshRenderer.sharedMaterial.SetTexture("_SplatNormals", distanceArrayNorm);
	}
}
