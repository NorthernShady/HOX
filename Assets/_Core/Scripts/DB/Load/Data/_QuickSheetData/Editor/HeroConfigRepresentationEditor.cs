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
[CustomEditor(typeof(HeroConfigRepresentation))]
public class HeroConfigRepresentationEditor : BaseGoogleEditor<HeroConfigRepresentation>
{	    
    public override bool Load()
    {        
        HeroConfigRepresentation targetData = target as HeroConfigRepresentation;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<HeroConfigRepresentationData>(targetData.WorksheetName) ?? db.CreateTable<HeroConfigRepresentationData>(targetData.WorksheetName);
        
        List<HeroConfigRepresentationData> myDataList = new List<HeroConfigRepresentationData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            HeroConfigRepresentationData data = new HeroConfigRepresentationData();
            
            data = Cloner.DeepCopy<HeroConfigRepresentationData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
