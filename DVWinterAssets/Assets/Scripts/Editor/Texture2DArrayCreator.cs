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

    [MenuItem("DVWinter/Terrain Array Creator")]
    public static void ShowWindow() {
        GetWindow<TerrainArrayCreator>("Terrain Array Creator");
    }

    void OnGUI() {
        // texture2DArray = EditorGUILayout.ObjectField(texture2DArray, typeof(Texture2DArray), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2DArray;
        scrollPos2 = EditorGUILayout.BeginScrollView(scrollPos2, GUILayout.Height(1000));

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
                distanceTextures[i] = null;
            }
        }

        if(GUILayout.Button("Create Asset")) {
            CreateAsset();
        }

        if(GUILayout.Button("Build Assetbundle")) {
            BuildAssetbundle();
        }
    }

    void CreateCloseArray() {
        GUILayout.Label("0 - Rock");
        distanceTextures[0] = EditorGUILayout.ObjectField(distanceTextures[0], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("1 - Gravel (Top Of Mountain)");
        distanceTextures[1] = EditorGUILayout.ObjectField(distanceTextures[1], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("2 - Rock (Side of mountain)");
        distanceTextures[2] = EditorGUILayout.ObjectField(distanceTextures[2], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("3 - Grass 2");
        distanceTextures[3] = EditorGUILayout.ObjectField(distanceTextures[3], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        GUILayout.Label("4 - Forest or Grass");
        distanceTextures[4] = EditorGUILayout.ObjectField(distanceTextures[4], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("5 - Grass");
        distanceTextures[5] = EditorGUILayout.ObjectField(distanceTextures[5], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("6 - Grass");
        distanceTextures[6] = EditorGUILayout.ObjectField(distanceTextures[6], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("7 - City Ground, grass, thing");
        distanceTextures[7] = EditorGUILayout.ObjectField(distanceTextures[7], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        GUILayout.Label("8 - Grass");
        distanceTextures[8] = EditorGUILayout.ObjectField(distanceTextures[8], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("9 - Grass");
        distanceTextures[9] = EditorGUILayout.ObjectField(distanceTextures[9], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("10 - Grass");
        distanceTextures[10] = EditorGUILayout.ObjectField(distanceTextures[10], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("11 - Forest again");
        distanceTextures[11] = EditorGUILayout.ObjectField(distanceTextures[11], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        GUILayout.Label("12 - Fields");
        distanceTextures[12] = EditorGUILayout.ObjectField(distanceTextures[12], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("13 - River banks");
        distanceTextures[13] = EditorGUILayout.ObjectField(distanceTextures[13], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("14 - Grass");
        distanceTextures[14] = EditorGUILayout.ObjectField(distanceTextures[14], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("15 - Grass");
        distanceTextures[15] = EditorGUILayout.ObjectField(distanceTextures[15], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
    }

    void CreateDistanceArray() {
        GUILayout.Label("0 - Rock");
        distanceTextures[0] = EditorGUILayout.ObjectField(distanceTextures[0], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("1 - Gravel (Top Of Mountain)");
        distanceTextures[1] = EditorGUILayout.ObjectField(distanceTextures[1], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("2 - Rock (Side of mountain)");
        distanceTextures[2] = EditorGUILayout.ObjectField(distanceTextures[2], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("3 - Grass 2");
        distanceTextures[3] = EditorGUILayout.ObjectField(distanceTextures[3], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        GUILayout.Label("4 - Forest or Grass");
        distanceTextures[4] = EditorGUILayout.ObjectField(distanceTextures[4], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("5 - Grass");
        distanceTextures[5] = EditorGUILayout.ObjectField(distanceTextures[5], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("6 - Grass");
        distanceTextures[6] = EditorGUILayout.ObjectField(distanceTextures[6], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("7 - City Ground, grass, thing");
        distanceTextures[7] = EditorGUILayout.ObjectField(distanceTextures[7], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        GUILayout.Label("8 - Grass");
        distanceTextures[8] = EditorGUILayout.ObjectField(distanceTextures[8], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("9 - Grass");
        distanceTextures[9] = EditorGUILayout.ObjectField(distanceTextures[9], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("10 - Grass");
        distanceTextures[10] = EditorGUILayout.ObjectField(distanceTextures[10], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("11 - Forest again");
        distanceTextures[11] = EditorGUILayout.ObjectField(distanceTextures[11], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        GUILayout.Label("12 - Fields");
        distanceTextures[12] = EditorGUILayout.ObjectField(distanceTextures[12], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("13 - River banks");
        distanceTextures[13] = EditorGUILayout.ObjectField(distanceTextures[13], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("14 - Grass");
        distanceTextures[14] = EditorGUILayout.ObjectField(distanceTextures[14], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("15 - Grass");
        distanceTextures[15] = EditorGUILayout.ObjectField(distanceTextures[15], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
    }

    void CreateFarArray() {
        GUILayout.Label("BLACK AND WHITE, 0 AND 2, ARE SWITCHED FROM THE DISTANCE ARRAY");

        GUILayout.Label("0 - Rock");
        distanceTextures[0] = EditorGUILayout.ObjectField(distanceTextures[0], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("1 - Gravel (Top Of Mountain)");
        distanceTextures[1] = EditorGUILayout.ObjectField(distanceTextures[1], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("2 - Rock (Side of mountain)");
        distanceTextures[2] = EditorGUILayout.ObjectField(distanceTextures[2], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("3 - Grass 2");
        distanceTextures[3] = EditorGUILayout.ObjectField(distanceTextures[3], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        GUILayout.Label("4 - Forest or Grass");
        distanceTextures[4] = EditorGUILayout.ObjectField(distanceTextures[4], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("5 - Grass");
        distanceTextures[5] = EditorGUILayout.ObjectField(distanceTextures[5], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("6 - Grass");
        distanceTextures[6] = EditorGUILayout.ObjectField(distanceTextures[6], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("7 - City Ground, grass, thing");
        distanceTextures[7] = EditorGUILayout.ObjectField(distanceTextures[7], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        GUILayout.Label("8 - Grass");
        distanceTextures[8] = EditorGUILayout.ObjectField(distanceTextures[8], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("9 - Grass");
        distanceTextures[9] = EditorGUILayout.ObjectField(distanceTextures[9], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("10 - Grass");
        distanceTextures[10] = EditorGUILayout.ObjectField(distanceTextures[10], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("11 - Forest again");
        distanceTextures[11] = EditorGUILayout.ObjectField(distanceTextures[11], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;

        GUILayout.Label("12 - Fields");
        distanceTextures[12] = EditorGUILayout.ObjectField(distanceTextures[12], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("13 - River banks");
        distanceTextures[13] = EditorGUILayout.ObjectField(distanceTextures[13], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("14 - Grass");
        distanceTextures[14] = EditorGUILayout.ObjectField(distanceTextures[14], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
        GUILayout.Label("15 - Grass");
        distanceTextures[15] = EditorGUILayout.ObjectField(distanceTextures[15], typeof(Texture2D), false, GUILayout.Height(EditorGUIUtility.singleLineHeight)) as Texture2D;
    }

    void CreateAsset() {
        distanceTextureArray = new Texture2DArray(distanceTextures[0].width, distanceTextures[0].height, distanceTextures.Length, TextureFormat.RGBA32, false); //originalArray!.format
        for(int i = 0; i < distanceTextures.Length; i++) {
            distanceTextureArray.SetPixels(distanceTextures[i].GetPixels(0), i, 0);
        }
        distanceTextureArray.Apply();

        AssetDatabase.CreateAsset(distanceTextureArray, "Assets/arrayAsset.asset");
        AssetDatabase.SaveAssets();
        Debug.Log("Textures Exported to Texture2DArray");
    }

    void BuildAssetbundle() {
        string assetPath = Application.dataPath + "/AssetBundles";
        BuildPipeline.BuildAssetBundles(assetPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
        FileUtil.MoveFileOrDirectory("Assets/AssetBundles/", "../../Assets");

        Debug.Log(Path.Combine(Application.dataPath, "../../"));
        Debug.Log("Textures Exported to Texture2DArray");
    }
}
