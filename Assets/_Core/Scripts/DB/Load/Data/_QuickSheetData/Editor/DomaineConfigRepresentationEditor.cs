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
[CustomEditor(typeof(DomaineConfigRepresentation))]
public class DomaineConfigRepresentationEditor : BaseGoogleEditor<DomaineConfigRepresentation>
{	    
    public override bool Load()
    {        
        DomaineConfigRepresentation targetData = target as DomaineConfigRepresentation;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<DomaineConfigRepresentationData>(targetData.WorksheetName) ?? db.CreateTable<DomaineConfigRepresentationData>(targetData.WorksheetName);
        
        List<DomaineConfigRepresentationData> myDataList = new List<DomaineConfigRepresentationData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            DomaineConfigRepresentationData data = new DomaineConfigRepresentationData();
            
            data = Cloner.DeepCopy<DomaineConfigRepresentationData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
