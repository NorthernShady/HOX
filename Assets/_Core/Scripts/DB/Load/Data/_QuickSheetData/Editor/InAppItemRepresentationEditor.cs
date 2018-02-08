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
[CustomEditor(typeof(InAppItemRepresentation))]
public class InAppItemRepresentationEditor : BaseGoogleEditor<InAppItemRepresentation>
{	    
    public override bool Load()
    {        
        InAppItemRepresentation targetData = target as InAppItemRepresentation;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<InAppItemRepresentationData>(targetData.WorksheetName) ?? db.CreateTable<InAppItemRepresentationData>(targetData.WorksheetName);
        
        List<InAppItemRepresentationData> myDataList = new List<InAppItemRepresentationData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            InAppItemRepresentationData data = new InAppItemRepresentationData();
            
            data = Cloner.DeepCopy<InAppItemRepresentationData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
