using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CreateMusicAudioSO
{
    [MenuItem("Assets/Create/Music Object")]
    public static void CreateMyAsset()
    {
        MusicSO asset = ScriptableObject.CreateInstance<MusicSO>();

        AssetDatabase.CreateAsset(asset, "Assets/ScriptableObjects/Music/MusicSO.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
