using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using GDataDB;
using GDataDB.Linq;

using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
[CustomEditor(typeof(NotificationsRepresentation))]
public class NotificationsRepresentationEditor : BaseGoogleEditor<NotificationsRepresentation>
{	    
    public override bool Load()
    {        
        NotificationsRepresentation targetData = target as NotificationsRepresentation;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<NotificationsRepresentationData>(targetData.WorksheetName) ?? db.CreateTable<NotificationsRepresentationData>(targetData.WorksheetName);
        
        List<NotificationsRepresentationData> myDataList = new List<NotificationsRepresentationData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            NotificationsRepresentationData data = new NotificationsRepresentationData();
            
            data = Cloner.DeepCopy<NotificationsRepresentationData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
