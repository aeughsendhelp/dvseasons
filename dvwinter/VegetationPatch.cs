using AwesomeTechnologies.Vegetation;
using AwesomeTechnologies;
using AwesomeTechnologies.VegetationSystem;
using HarmonyLib;
using Unity.Jobs;
using UnityEngine;
using AwesomeTechnologies.Vegetation.Masks;
using AwesomeTechnologies.Vegetation.PersistentStorage;
using Unity.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using PlaceholderSoftware.WetStuff.Debugging;
using static SteamVR_TrackedObject;

namespace dvwinter;

//[HarmonyPatch(typeof(VegetationCellSpawner), "ExecuteSpawnRules", new[] { typeof(VegetationCell), typeof(Rect), typeof(int), typeof(int) })]
//class VegetationTranspilerPatch {
//	static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
//		var codes = new List<CodeInstruction>(instructions);

//		codes[16].opcode = OpCodes.Nop;
//		codes[17].opcode = OpCodes.Nop;
//		codes[18].opcode = OpCodes.Nop;
//		codes[19].opcode = OpCodes.Nop;
//		codes[20].opcode = OpCodes.Nop;
//		codes[21].opcode = OpCodes.Nop;
//		codes[22].opcode = OpCodes.Nop;
//		codes[23].opcode = OpCodes.Nop;

//		return codes.AsEnumerable();
//	}
//}


[HarmonyPatch(typeof(VegetationCellSpawner), "ExecuteSpawnRules")]
class VegetationPrefixPatch {
	static void Prefix(ref VegetationCellSpawner __instance) {
		Main.Log("_instance is one of the top 10 gamer moments of all time");

		//VegetationSystemPro vegetationManager = GameObject.FindObjectOfType<VegetationSystemPro>(); // name can be simplified but it's actually longer so looks less simple so fuck off vs

		var modelList = __instance.VegetationPackageProModelsList;

		foreach(var item in modelList) {
			var newList = item.VegetationItemModelList;

			foreach(var thing in newList) {
				Main.Log(thing.VegetationModel.name);

				// Vegetation editing will go right here
			}
		}
	}
}
