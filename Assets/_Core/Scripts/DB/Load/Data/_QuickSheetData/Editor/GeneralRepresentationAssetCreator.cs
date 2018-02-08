using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/GeneralRepresentation")]
    public static void CreateGeneralAssetFile()
    {
        GeneralRepresentation asset = QuickSheet.CustomAssetUtility.CreateAsset<GeneralRepresentation>();
        asset.SheetName = "HOX";
        asset.WorksheetName = "General";
        EditorUtility.SetDirty(asset);        
    }
    
}