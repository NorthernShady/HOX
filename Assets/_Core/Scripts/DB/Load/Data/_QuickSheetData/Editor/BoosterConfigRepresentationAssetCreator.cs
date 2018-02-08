using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/BoosterConfigRepresentation")]
    public static void CreateBoosterConfigAssetFile()
    {
        BoosterConfigRepresentation asset = QuickSheet.CustomAssetUtility.CreateAsset<BoosterConfigRepresentation>();
        asset.SheetName = "MP2";
        asset.WorksheetName = "BoosterConfig";
        EditorUtility.SetDirty(asset);        
    }
    
}