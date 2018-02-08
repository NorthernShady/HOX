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
[CustomEditor(typeof(XPLevelRepresentation))]
public class XPLevelRepresentationEditor : BaseGoogleEditor<XPLevelRepresentation>
{	    
    public override bool Load()
    {        
        XPLevelRepresentation targetData = target as XPLevelRepresentation;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<XPLevelRepresentationData>(targetData.WorksheetName) ?? db.CreateTable<XPLevelRepresentationData>(targetData.WorksheetName);
        
        List<XPLevelRepresentationData> myDataList = new List<XPLevelRepresentationData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            XPLevelRepresentationData data = new XPLevelRepresentationData();
            
            data = Cloner.DeepCopy<XPLevelRepresentationData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
