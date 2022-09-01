using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;

public class GeneralTools : Editor
{
    [MenuItem("Tools/General Tools/Change Asset Address to Asset Name")]
    public static void ChangeAddressToAssetName()
    {
        if (Selection.objects.Length<=0)
        {
            Debug.Log("You must Select some prefabs");
            return;
        }
        for (int j = 0; j < Selection.objects.Length; j++)
        {
            EditorUtility.DisplayProgressBar("Converting asset address to asset name: "+((float)j)/((float)Selection.objects.Length),"Converting",((float)j)/((float)Selection.objects.Length));
            AddressableAssetSettingsDefaultObject.Settings.FindAssetEntry(AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(Selection.objects[j]))).address = Selection.objects[j].name;
        }

        EditorUtility.ClearProgressBar();
    }

    [MenuItem("Tools/General Tools/Add LOD")]
    public static void AddLod()
    {

    }

    [MenuItem("Tools/General Tools/ Vertex Count")]
    public static void PrintVertex()
    {

    }
}
