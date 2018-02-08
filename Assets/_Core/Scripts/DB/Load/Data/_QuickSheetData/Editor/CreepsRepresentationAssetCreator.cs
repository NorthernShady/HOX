using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/CreepsRepresentation")]
    public static void CreateCreepsAssetFile()
    {
        CreepsRepresentation asset = QuickSheet.CustomAssetUtility.CreateAsset<CreepsRepresentation>();
        asset.SheetName = "HOX";
        asset.WorksheetName = "Creeps";
        EditorUtility.SetDirty(asset);        
    }
    
}