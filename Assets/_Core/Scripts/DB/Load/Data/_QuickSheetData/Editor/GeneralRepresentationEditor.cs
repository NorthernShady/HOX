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
[CustomEditor(typeof(GeneralRepresentation))]
public class GeneralRepresentationEditor : BaseGoogleEditor<GeneralRepresentation>
{	    
    public override bool Load()
    {        
        GeneralRepresentation targetData = target as GeneralRepresentation;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<GeneralRepresentationData>(targetData.WorksheetName) ?? db.CreateTable<GeneralRepresentationData>(targetData.WorksheetName);
        
        List<GeneralRepresentationData> myDataList = new List<GeneralRepresentationData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            GeneralRepresentationData data = new GeneralRepresentationData();
            
            data = Cloner.DeepCopy<GeneralRepresentationData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
