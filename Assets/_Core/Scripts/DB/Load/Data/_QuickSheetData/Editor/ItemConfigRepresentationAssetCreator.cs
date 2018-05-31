using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/ItemConfigRepresentation")]
    public static void CreateItemConfigAssetFile()
    {
        ItemConfigRepresentation asset = QuickSheet.CustomAssetUtility.CreateAsset<ItemConfigRepresentation>();
        asset.SheetName = "HOX";
        asset.WorksheetName = "ItemConfig";
        EditorUtility.SetDirty(asset);        
    }
    
}