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
[CustomEditor(typeof(CreepConfigRepresentation))]
public class CreepConfigRepresentationEditor : BaseGoogleEditor<CreepConfigRepresentation>
{	    
    public override bool Load()
    {        
        CreepConfigRepresentation targetData = target as CreepConfigRepresentation;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<CreepConfigRepresentationData>(targetData.WorksheetName) ?? db.CreateTable<CreepConfigRepresentationData>(targetData.WorksheetName);
        
        List<CreepConfigRepresentationData> myDataList = new List<CreepConfigRepresentationData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            CreepConfigRepresentationData data = new CreepConfigRepresentationData();
            
            data = Cloner.DeepCopy<CreepConfigRepresentationData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
