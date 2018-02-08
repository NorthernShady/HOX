using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/LevelListRepresentation")]
    public static void CreateLevelListAssetFile()
    {
        LevelListRepresentation asset = QuickSheet.CustomAssetUtility.CreateAsset<LevelListRepresentation>();
        asset.SheetName = "MP2";
        asset.WorksheetName = "LevelList";
        EditorUtility.SetDirty(asset);        
    }
    
}