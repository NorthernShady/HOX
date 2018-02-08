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
[CustomEditor(typeof(HeroesRepresentation))]
public class HeroesRepresentationEditor : BaseGoogleEditor<HeroesRepresentation>
{	    
    public override bool Load()
    {        
        HeroesRepresentation targetData = target as HeroesRepresentation;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<HeroesRepresentationData>(targetData.WorksheetName) ?? db.CreateTable<HeroesRepresentationData>(targetData.WorksheetName);
        
        List<HeroesRepresentationData> myDataList = new List<HeroesRepresentationData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            HeroesRepresentationData data = new HeroesRepresentationData();
            
            data = Cloner.DeepCopy<HeroesRepresentationData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
