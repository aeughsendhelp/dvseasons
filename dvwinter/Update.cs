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
			var obj = GameObject.FindObjectOfType<VegetationSystemPro>(); // name can be simplified but it's actually longer so looks less simple so fuck off vs

			var modelList = obj.VegetationCellSpawner.VegetationPackageProModelsList[0].VegetationItemModelList;

			//modelList[0].VegetationModel

			foreach(var thing in modelList) {
				//Main.Log(thing.VegetationModel.name);

				thing.VegetationModel = null;
			}
		}
	}
}
