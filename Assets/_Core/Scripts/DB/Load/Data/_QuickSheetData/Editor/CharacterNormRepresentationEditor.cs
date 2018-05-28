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
[CustomEditor(typeof(CharacterNormRepresentation))]
public class CharacterNormRepresentationEditor : BaseGoogleEditor<CharacterNormRepresentation>
{	    
    public override bool Load()
    {        
        CharacterNormRepresentation targetData = target as CharacterNormRepresentation;
        
        var client = new DatabaseClient("", "");
        string error = string.Empty;
        var db = client.GetDatabase(targetData.SheetName, ref error);	
        var table = db.GetTable<CharacterNormRepresentationData>(targetData.WorksheetName) ?? db.CreateTable<CharacterNormRepresentationData>(targetData.WorksheetName);
        
        List<CharacterNormRepresentationData> myDataList = new List<CharacterNormRepresentationData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            CharacterNormRepresentationData data = new CharacterNormRepresentationData();
            
            data = Cloner.DeepCopy<CharacterNormRepresentationData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
