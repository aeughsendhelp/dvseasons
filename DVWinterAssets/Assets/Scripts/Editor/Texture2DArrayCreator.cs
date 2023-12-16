using UnityEngine;
using UnityEditor;
using System.IO;

public class TerrainArrayCreator : EditorWindow {
    private Texture2DArray closeTextureArray;
    private Texture2D[] closeTextures = new Texture2D[16];

    private Texture2DArray distanceTextureArray;
    private Texture2D[] distanceTextures = new Texture2D[16];

    private Texture2DArray farTextureArray;
    private Texture2D[] farTextures = new Texture2D[16];


    private Vector2 scrollPosition = Vector2.zero;
    private Vector2 scrollPos2 = Vector2.zero;

    private Texture2D[] texures = new Texture2D[16];

    [MenuItem("DVWinter/Terrain Array Creator")]
    public static void ShowWindow() {
        GetWindow<TerrainArrayCreator>("Terrain Array Creator");
    }

    void OnGUI() {
        // texture2DArray = EditorGUILayout.ObjectField("text", texture2DArray, typeof(Texture2DArray), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2DArray;
        scrollPos2 = EditorGUILayout.BeginScrollView(scrollPos2, GUILayout.Height(990));

        GUILayout.Label("Texture List", EditorStyles.largeLabel);
        GUILayout.Space(30);

        CreateCloseArray();
        GUILayout.Space(30);

        CreateDistanceArray();
        GUILayout.Space(30);

        CreateFarArray();
        GUILayout.Space(30);

        if(GUILayout.Button("Reset Textures")) {
            for(int i = 0; i < distanceTextures.Length; i++) {
                // distanceTextures[i] = gettext
            }
        }

        // if(GUILayout.Button("CREATE")) {
        //     AssetDatabase.CreateAsset(distanceTextureArray, "Assets/arrg.asset");
        //     AssetDatabase.SaveAssets();
        // }

        if(GUILayout.Button("Create Asset")) {
            CreateAsset();
        }

        if(GUILayout.Button("Build Assetbundle")) {
            BuildAssetbundle();
        }

        GUILayout.Space(50);

        EditorGUILayout.EndScrollView();
    }

    void CreateCloseArray() {
        GUILayout.Label("Close Texture Array");

        closeTextures[0] = EditorGUILayout.ObjectField("0 - Rock", closeTextures[0], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        closeTextures[1] = EditorGUILayout.ObjectField("1 - Gravel (Top Of Mountain)", closeTextures[1], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        closeTextures[2] = EditorGUILayout.ObjectField("2 - Rock (Side of mountain)", closeTextures[2], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        closeTextures[3] = EditorGUILayout.ObjectField("3 - Grass 2", closeTextures[3], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        closeTextures[4] = EditorGUILayout.ObjectField("4 - Forest or Grass", closeTextures[4], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        closeTextures[5] = EditorGUILayout.ObjectField("5 - Grass", closeTextures[5], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        closeTextures[6] = EditorGUILayout.ObjectField("6 - Grass", closeTextures[6], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        closeTextures[7] = EditorGUILayout.ObjectField("7 - City Ground, grass, thing", closeTextures[7], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        closeTextures[8] = EditorGUILayout.ObjectField("8 - Grass", closeTextures[8], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        closeTextures[9] = EditorGUILayout.ObjectField("9 - Grass", closeTextures[9], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        closeTextures[10] = EditorGUILayout.ObjectField("10 - Grass", closeTextures[10], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        closeTextures[11] = EditorGUILayout.ObjectField("11 - Forest again", closeTextures[11], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        closeTextures[12] = EditorGUILayout.ObjectField("12 - Fields", closeTextures[12], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        closeTextures[13] = EditorGUILayout.ObjectField("13 - River banks", closeTextures[13], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        closeTextures[14] = EditorGUILayout.ObjectField("14 - Grass", closeTextures[14], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        closeTextures[15] = EditorGUILayout.ObjectField("15 - Grass", closeTextures[15], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
    }

    void CreateDistanceArray() {
        GUILayout.Label("Distance Texture Array");

        distanceTextures[0] = EditorGUILayout.ObjectField("0 - Rock", distanceTextures[0], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        distanceTextures[1] = EditorGUILayout.ObjectField("1 - Gravel (Top Of Mountain)", distanceTextures[1], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        distanceTextures[2] = EditorGUILayout.ObjectField("2 - Rock (Side of mountain)", distanceTextures[2], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        distanceTextures[3] = EditorGUILayout.ObjectField("3 - Grass 2", distanceTextures[3], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        distanceTextures[4] = EditorGUILayout.ObjectField("4 - Forest or Grass", distanceTextures[4], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        distanceTextures[5] = EditorGUILayout.ObjectField("5 - Grass", distanceTextures[5], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        distanceTextures[6] = EditorGUILayout.ObjectField("6 - Grass", distanceTextures[6], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        distanceTextures[7] = EditorGUILayout.ObjectField("7 - City Ground, grass, thing", distanceTextures[7], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        distanceTextures[8] = EditorGUILayout.ObjectField("8 - Grass", distanceTextures[8], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        distanceTextures[9] = EditorGUILayout.ObjectField("9 - Grass", distanceTextures[9], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        distanceTextures[10] = EditorGUILayout.ObjectField("10 - Grass", distanceTextures[10], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        distanceTextures[11] = EditorGUILayout.ObjectField("11 - Forest again", distanceTextures[11], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        distanceTextures[12] = EditorGUILayout.ObjectField("12 - Fields", distanceTextures[12], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        distanceTextures[13] = EditorGUILayout.ObjectField("13 - River banks", distanceTextures[13], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        distanceTextures[14] = EditorGUILayout.ObjectField("14 - Grass", distanceTextures[14], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        distanceTextures[15] = EditorGUILayout.ObjectField("15 - Grass", distanceTextures[15], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
    }

    void CreateFarArray() {
        GUILayout.Label("Far Texture Array (BLACK AND WHITE, 0 AND 2, ARE SWITCHED FROM THE DISTANCE ARRAY)");

        farTextures[0] = EditorGUILayout.ObjectField("0 - Rock", farTextures[0], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        farTextures[1] = EditorGUILayout.ObjectField("1 - Gravel (Top Of Mountain)", farTextures[1], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        farTextures[2] = EditorGUILayout.ObjectField("2 - Rock (Side of mountain)", farTextures[2], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        farTextures[3] = EditorGUILayout.ObjectField("3 - Grass 2", farTextures[3], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        farTextures[4] = EditorGUILayout.ObjectField("4 - Forest or Grass", farTextures[4], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        farTextures[5] = EditorGUILayout.ObjectField("5 - Grass", farTextures[5], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        farTextures[6] = EditorGUILayout.ObjectField("6 - Grass", farTextures[6], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        farTextures[7] = EditorGUILayout.ObjectField("7 - City Ground, grass, thing", farTextures[7], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        farTextures[8] = EditorGUILayout.ObjectField("8 - Grass", farTextures[8], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        farTextures[9] = EditorGUILayout.ObjectField("9 - Grass", farTextures[9], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        farTextures[10] = EditorGUILayout.ObjectField("10 - Grass", farTextures[10], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        farTextures[11] = EditorGUILayout.ObjectField("11 - Forest again", farTextures[11], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        farTextures[12] = EditorGUILayout.ObjectField("12 - Fields", farTextures[12], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        farTextures[13] = EditorGUILayout.ObjectField("13 - River banks", farTextures[13], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        farTextures[14] = EditorGUILayout.ObjectField("14 - Grass", farTextures[14], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        farTextures[15] = EditorGUILayout.ObjectField("15 - Grass", farTextures[15], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
    }

    void CreateAsset() {
        // Close Textures
        closeTextureArray = new Texture2DArray(closeTextures[0].width, closeTextures[0].height, distanceTextures.Length, TextureFormat.RGBA32, false); //originalArray!.format
        for(int i = 0; i < closeTextures.Length; i++) {
            closeTextureArray.SetPixels(closeTextures[i].GetPixels(0), i, 0);
        }
        closeTextureArray.Apply();
        AssetDatabase.CreateAsset(closeTextureArray, "Assets/closeArray.asset");

        // Close Textures
        distanceTextureArray = new Texture2DArray(distanceTextures[0].width, distanceTextures[0].height, distanceTextures.Length, TextureFormat.RGBA32, false); //originalArray!.format
        for(int i = 0; i < distanceTextures.Length; i++) {
            distanceTextureArray.SetPixels(distanceTextures[i].GetPixels(0), i, 0);
        }
        distanceTextureArray.Apply();
        AssetDatabase.CreateAsset(distanceTextureArray, "Assets/distanceArray.asset");

        // Far Textures
        farTextureArray = new Texture2DArray(farTextures[0].width, farTextures[0].height, farTextures.Length, TextureFormat.RGBA32, false); //originalArray!.format
        for(int i = 0; i < farTextures.Length; i++) {
            farTextureArray.SetPixels(farTextures[i].GetPixels(0), i, 0);
        }
        farTextureArray.Apply();
        AssetDatabase.CreateAsset(farTextureArray, "Assets/farArray.asset");

        AssetDatabase.SaveAssets();
        Debug.Log("Textures Exported to Arrays");
    }

    void BuildAssetbundle() {
        string assetPath = Application.dataPath + "/AssetBundles";
        BuildPipeline.BuildAssetBundles(assetPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        // FileUtil.MoveFileOrDirectory("Assets/AssetBundles/", "../../Assets");

        Debug.Log(assetPath);
        Debug.Log("Textures Exported to Texture2DArray");
    }
}
