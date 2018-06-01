using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/DomaineConfigRepresentation")]
    public static void CreateDomaineConfigAssetFile()
    {
        DomaineConfigRepresentation asset = QuickSheet.CustomAssetUtility.CreateAsset<DomaineConfigRepresentation>();
        asset.SheetName = "HOX";
        asset.WorksheetName = "DomaineConfig";
        EditorUtility.SetDirty(asset);        
    }
    
}