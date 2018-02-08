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
[CustomEditor(typeof(LevelListRepresentation))]
public class LevelListRepresentationEditor : BaseGoogleEditor<LevelListRepresentation>
{	    
    public override bool Load()
    {        
        LevelListRepresentation targetData = target as LevelListRepresentation;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<LevelListRepresentationData>(targetData.WorksheetName) ?? db.CreateTable<LevelListRepresentationData>(targetData.WorksheetName);
        
        List<LevelListRepresentationData> myDataList = new List<LevelListRepresentationData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            LevelListRepresentationData data = new LevelListRepresentationData();
            
            data = Cloner.DeepCopy<LevelListRepresentationData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
