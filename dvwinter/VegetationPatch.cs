using AwesomeTechnologies.VegetationSystem;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace dvwinter;

[HarmonyPatch(typeof(VegetationSystemPro), "SpawnVegetationCell", new Type[] {typeof(VegetationCell)} )]
class VegetationPatch {
	static void Prefix(ref VegetationSystemPro __instance) {
		Main.Log("This is one of the top 10 gamer moments of all time");

		VegetationSystemPro vegetationManager = GameObject.FindObjectOfType<VegetationSystemPro>(); // name can be simplified but it's actually longer so looks less simple so fuck off vs
		Main.Log(vegetationManager.gameObject.name);

		var modelList = vegetationManager.VegetationCellSpawner.VegetationPackageProModelsList;

		foreach(var item in modelList) {
			var newList = item.VegetationItemModelList;

			foreach(var thing in newList) {
				Main.Log(thing.VegetationModel.name);
				thing.VegetationModel = new GameObject();
				Main.Log(thing.VegetationModel.name);
			}
		}

		//modelList[0].VegetationModel
	}

	//static MethodBase TargetMethod() {
	//	return AccessTools.Method(
	//		typeof(VegetationSystemPro),
	//			"SpawnVegetationCell",
	//		new Type[] {
	//			typeof(float),
	//			typeof(int).MakeByRefType(), // note: this is not constant expression
	//		}
	//	);
	//}
}
