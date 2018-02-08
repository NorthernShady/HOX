using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/XPLevelRepresentation")]
    public static void CreateXPLevelAssetFile()
    {
        XPLevelRepresentation asset = QuickSheet.CustomAssetUtility.CreateAsset<XPLevelRepresentation>();
        asset.SheetName = "MP2";
        asset.WorksheetName = "XPLevel";
        EditorUtility.SetDirty(asset);        
    }
    
}