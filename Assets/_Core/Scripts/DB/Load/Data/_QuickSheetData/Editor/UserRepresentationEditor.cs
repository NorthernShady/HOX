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
[CustomEditor(typeof(UserRepresentation))]
public class UserRepresentationEditor : BaseGoogleEditor<UserRepresentation>
{	    
    public override bool Load()
    {        
        UserRepresentation targetData = target as UserRepresentation;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<UserRepresentationData>(targetData.WorksheetName) ?? db.CreateTable<UserRepresentationData>(targetData.WorksheetName);
        
        List<UserRepresentationData> myDataList = new List<UserRepresentationData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            UserRepresentationData data = new UserRepresentationData();
            
            data = Cloner.DeepCopy<UserRepresentationData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
