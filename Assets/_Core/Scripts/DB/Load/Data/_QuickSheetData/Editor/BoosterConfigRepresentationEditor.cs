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
[CustomEditor(typeof(BoosterConfigRepresentation))]
public class BoosterConfigRepresentationEditor : BaseGoogleEditor<BoosterConfigRepresentation>
{	    
    public override bool Load()
    {        
        BoosterConfigRepresentation targetData = target as BoosterConfigRepresentation;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<BoosterConfigRepresentationData>(targetData.WorksheetName) ?? db.CreateTable<BoosterConfigRepresentationData>(targetData.WorksheetName);
        
        List<BoosterConfigRepresentationData> myDataList = new List<BoosterConfigRepresentationData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            BoosterConfigRepresentationData data = new BoosterConfigRepresentationData();
            
            data = Cloner.DeepCopy<BoosterConfigRepresentationData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
