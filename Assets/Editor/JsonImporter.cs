using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class JsonImporter : AssetPostprocessor
{
    private const string SCRIPTABLE_OBJECT_DESTIONATION_PATH = "Assets/PathTransforms/"; 
    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
        string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            if(!str.EndsWith( ".json", StringComparison.OrdinalIgnoreCase ) )
                continue;
            
            CreateScriptableObject(str);
            
        }
    }

    private static void CreateScriptableObject(string path)
    {
        EnsureDirectoryExists(SCRIPTABLE_OBJECT_DESTIONATION_PATH);

        string jsonFileName = Path.GetFileNameWithoutExtension(path);
        
        string destinationPath = SCRIPTABLE_OBJECT_DESTIONATION_PATH + jsonFileName + ".asset";
    
            var gm = AssetDatabase.LoadAssetAtPath<TransformData>(destinationPath);
            
            if (gm == null)
            {
                gm = ScriptableObject.CreateInstance<TransformData>();
                AssetDatabase.CreateAsset(gm, destinationPath);
            }
            
            string dataAsJson = File.ReadAllText (path,System.Text.Encoding.UTF8);     

            EditorJsonUtility.FromJsonOverwrite(dataAsJson, gm.item);

            EditorUtility.SetDirty(gm);

            AssetDatabase.Refresh();

            AssetDatabase.SaveAssets();

    }
    
    private static void EnsureDirectoryExists( string directory )
    {
        if( !Directory.Exists( directory ) )
            Directory.CreateDirectory( directory );
    }
}