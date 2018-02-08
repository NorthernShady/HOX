using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/UserRepresentation")]
    public static void CreateUserAssetFile()
    {
        UserRepresentation asset = QuickSheet.CustomAssetUtility.CreateAsset<UserRepresentation>();
        asset.SheetName = "MP2";
        asset.WorksheetName = "User";
        EditorUtility.SetDirty(asset);        
    }
    
}