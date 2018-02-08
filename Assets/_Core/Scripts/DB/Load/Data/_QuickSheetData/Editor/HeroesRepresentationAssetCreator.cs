using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/HeroesRepresentation")]
    public static void CreateHeroesAssetFile()
    {
        HeroesRepresentation asset = QuickSheet.CustomAssetUtility.CreateAsset<HeroesRepresentation>();
        asset.SheetName = "HOX";
        asset.WorksheetName = "Heroes";
        EditorUtility.SetDirty(asset);        
    }
    
}