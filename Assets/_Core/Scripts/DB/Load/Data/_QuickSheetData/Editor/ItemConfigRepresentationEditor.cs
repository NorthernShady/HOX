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
[CustomEditor(typeof(ItemConfigRepresentation))]
public class ItemConfigRepresentationEditor : BaseGoogleEditor<ItemConfigRepresentation>
{	    
    public override bool Load()
    {        
        ItemConfigRepresentation targetData = target as ItemConfigRepresentation;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<ItemConfigRepresentationData>(targetData.WorksheetName) ?? db.CreateTable<ItemConfigRepresentationData>(targetData.WorksheetName);
        
        List<ItemConfigRepresentationData> myDataList = new List<ItemConfigRepresentationData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            ItemConfigRepresentationData data = new ItemConfigRepresentationData();
            
            data = Cloner.DeepCopy<ItemConfigRepresentationData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
