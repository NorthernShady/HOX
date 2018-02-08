using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/InAppItemRepresentation")]
    public static void CreateInAppItemAssetFile()
    {
        InAppItemRepresentation asset = QuickSheet.CustomAssetUtility.CreateAsset<InAppItemRepresentation>();
        asset.SheetName = "MP2";
        asset.WorksheetName = "InAppItem";
        EditorUtility.SetDirty(asset);        
    }
    
}