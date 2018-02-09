using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/CreepConfigRepresentation")]
    public static void CreateCreepConfigAssetFile()
    {
        CreepConfigRepresentation asset = QuickSheet.CustomAssetUtility.CreateAsset<CreepConfigRepresentation>();
        asset.SheetName = "HOX";
        asset.WorksheetName = "CreepConfig";
        EditorUtility.SetDirty(asset);        
    }
    
}