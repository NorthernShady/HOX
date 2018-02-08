using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/NotificationsRepresentation")]
    public static void CreateNotificationsAssetFile()
    {
        NotificationsRepresentation asset = QuickSheet.CustomAssetUtility.CreateAsset<NotificationsRepresentation>();
        asset.SheetName = "MP2";
        asset.WorksheetName = "Notifications";
        EditorUtility.SetDirty(asset);        
    }
    
}