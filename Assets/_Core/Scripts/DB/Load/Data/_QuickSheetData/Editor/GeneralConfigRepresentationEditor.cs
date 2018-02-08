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
[CustomEditor(typeof(GeneralConfigRepresentation))]
public class GeneralConfigRepresentationEditor : BaseGoogleEditor<GeneralConfigRepresentation>
{	    
    public override bool Load()
    {        
        GeneralConfigRepresentation targetData = target as GeneralConfigRepresentation;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<GeneralConfigRepresentationData>(targetData.WorksheetName) ?? db.CreateTable<GeneralConfigRepresentationData>(targetData.WorksheetName);
        
        List<GeneralConfigRepresentationData> myDataList = new List<GeneralConfigRepresentationData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            GeneralConfigRepresentationData data = new GeneralConfigRepresentationData();
            
            data = Cloner.DeepCopy<GeneralConfigRepresentationData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
