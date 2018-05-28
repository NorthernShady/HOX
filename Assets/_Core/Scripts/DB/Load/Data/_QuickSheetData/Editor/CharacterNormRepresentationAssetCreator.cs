using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/CharacterNormRepresentation")]
    public static void CreateCharacterNormAssetFile()
    {
        CharacterNormRepresentation asset = QuickSheet.CustomAssetUtility.CreateAsset<CharacterNormRepresentation>();
        asset.SheetName = "HOX";
        asset.WorksheetName = "CharacterNorm";
        EditorUtility.SetDirty(asset);        
    }
    
}