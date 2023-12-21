using AwesomeTechnologies.VegetationSystem;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace dvwinter;

[HarmonyPatch(typeof(VegetationSystemPro), "OnEnable")]
class VegetationPatch {
	static void Postfix(ref VegetationSystemPro __instance) {
		var obj = GameObject.FindObjectOfType<VegetationSystemPro>(); // name can be simplified but it's actually longer so looks less simple so fuck off vs

		var modelList = obj.VegetationCellSpawner.VegetationPackageProModelsList;
		foreach (var item in modelList) {
			var newList = item.VegetationItemModelList;

			foreach(var thing in newList) {
			thing.VegetationModel = new GameObject();
			}
		}

		//modelList[0].VegetationModel
	}
}
