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
[CustomEditor(typeof(CreepsRepresentation))]
public class CreepsRepresentationEditor : BaseGoogleEditor<CreepsRepresentation>
{	    
    public override bool Load()
    {        
        CreepsRepresentation targetData = target as CreepsRepresentation;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<CreepsRepresentationData>(targetData.WorksheetName) ?? db.CreateTable<CreepsRepresentationData>(targetData.WorksheetName);
        
        List<CreepsRepresentationData> myDataList = new List<CreepsRepresentationData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            CreepsRepresentationData data = new CreepsRepresentationData();
            
            data = Cloner.DeepCopy<CreepsRepresentationData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
