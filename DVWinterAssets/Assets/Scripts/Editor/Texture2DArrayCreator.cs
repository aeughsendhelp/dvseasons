using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class TerrainArrayCreator : EditorWindow {
    private Texture2DArray arrayToReplace;

    private Vector2 scrollPosition = Vector2.zero;
    private Vector2 scrollPos2 = Vector2.zero;

    private Texture2D[] textures = new Texture2D[16];

    [MenuItem("DVWinter/Terrain Array Creator")]
    public static void ShowWindow() {
        GetWindow<TerrainArrayCreator>("Terrain Array Creator");
    }

    void OnGUI() {
        scrollPos2 = EditorGUILayout.BeginScrollView(scrollPos2, GUILayout.Height(990));
        GUILayout.Space(30);

        arrayToReplace = EditorGUILayout.ObjectField(" Array To Replace", arrayToReplace, typeof(Texture2DArray), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2DArray;
        GUILayout.Space(10);

        ShowReplacableArray();
        GUILayout.Space(30);

        if(GUILayout.Button("Replace Original Textures")) {
            ReplaceOriginal();
        }

        if(GUILayout.Button("Build Assetbundle")) {
            BuildAssetbundle();
        }

        // if(GUILayout.Button("SetArrayReadable")) {
        //     Debug.Log(AssetDatabase.GetAssetPath(originalTextureArray));
        //     SetArrayReadable(AssetDatabase.GetAssetPath(originalTextureArray));
        // }
        GUILayout.Space(50);

        EditorGUILayout.EndScrollView();
    }

    void ShowReplacableArray() {
        
        GUILayout.Label("Close Texture Array");

        if(GUILayout.Button("Reset Textures")) {
            for(int i = 0; i < textures.Length; i++) {
                textures[i] = null;
            }
        }

        textures[0] = EditorGUILayout.ObjectField("   0", textures[0], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        textures[1] = EditorGUILayout.ObjectField("   1"   , textures[1], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        textures[2] = EditorGUILayout.ObjectField("   2", textures[2], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        textures[3] = EditorGUILayout.ObjectField("   3", textures[3], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        textures[4] = EditorGUILayout.ObjectField("   4", textures[4], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        textures[5] = EditorGUILayout.ObjectField("   5", textures[5], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        textures[6] = EditorGUILayout.ObjectField("   6", textures[6], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        textures[7] = EditorGUILayout.ObjectField("   7", textures[7], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        textures[8] = EditorGUILayout.ObjectField("   8", textures[8], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        textures[9] = EditorGUILayout.ObjectField("   9", textures[9], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        textures[10] = EditorGUILayout.ObjectField("   10", textures[10], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        textures[11] = EditorGUILayout.ObjectField("   11", textures[11], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        textures[12] = EditorGUILayout.ObjectField("   12", textures[12], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        textures[13] = EditorGUILayout.ObjectField("   13", textures[13], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        textures[14] = EditorGUILayout.ObjectField("   14", textures[14], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        textures[15] = EditorGUILayout.ObjectField("   15", textures[15], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
    }

    void ReplaceOriginal() {
        Texture2DArray newArr = new Texture2DArray(1024, 1024, textures.Length, TextureFormat.RGBA32, false);

        for(int i = 0; i < textures.Length; i++) {
            if(textures[i] != null) {
                newArr.SetPixels(textures[i].GetPixels(), i, 0);
            } else {
                newArr.SetPixels(arrayToReplace.GetPixels(i), i, 0);
            }
        }
 
        newArr.Apply();
        AssetDatabase.CreateAsset(newArr, "Assets/newArray.asset");
    }

    void BuildAssetbundle() {
        string assetPath = Application.dataPath + "/AssetBundles";
        BuildPipeline.BuildAssetBundles(assetPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        // FileUtil.MoveFileOrDirectory("Assets/AssetBundles/", "../../Assets");

        Debug.Log(assetPath);
        Debug.Log("Textures Exported to Texture2DArray");
    }


    // void SetArrayReadable(string absoluteFilePath) {
    //     TextureImporter ti = (TextureImporter)AssetImporter.GetAtPath(absoluteFilePath);
    //     if(!ti.isReadable) {
    //       ti.isReadable = true;
    //       ti.SaveAndReimport();
    //       Debug.Log("Made readable");
    //     }
    // }

}