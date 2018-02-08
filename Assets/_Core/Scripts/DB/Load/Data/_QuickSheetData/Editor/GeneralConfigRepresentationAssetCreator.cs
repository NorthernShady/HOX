using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/GeneralConfigRepresentation")]
    public static void CreateGeneralConfigAssetFile()
    {
        GeneralConfigRepresentation asset = QuickSheet.CustomAssetUtility.CreateAsset<GeneralConfigRepresentation>();
        asset.SheetName = "MP2";
        asset.WorksheetName = "GeneralConfig";
        EditorUtility.SetDirty(asset);        
    }
    
}