using AwesomeTechnologies.VegetationSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityModManagerNet;

namespace dvwinter;

internal class Update {
	public static void OnUpdate(UnityModManager.ModEntry modEntry, float dt) {
		if(Input.GetKeyDown(KeyCode.U)) {
			var objs = GameObject.FindObjectsOfType<UnityTerrain>(); // name can be simplified but it's actually longer so looks less simple so fuck off vs

			List<GameObject> treePrefabs = new List<GameObject>();

			for(int i = 1; i < objs.Length; i++) {
				objs[i].gameObject.GetComponent<Terrain>().drawTreesAndFoliage = false;

				Main.Log(objs[i].gameObject.name);

				//for(int j = 0; j < safsd; j++) {
				//	Log(objs[i].GetComponent<Terrain>().terrainData.treePrototypes[j].prefab.name);
				//}
			}
		}
	}
}
