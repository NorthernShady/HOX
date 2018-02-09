using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/HeroConfigRepresentation")]
    public static void CreateHeroConfigAssetFile()
    {
        HeroConfigRepresentation asset = QuickSheet.CustomAssetUtility.CreateAsset<HeroConfigRepresentation>();
        asset.SheetName = "HOX";
        asset.WorksheetName = "HeroConfig";
        EditorUtility.SetDirty(asset);        
    }
    
}