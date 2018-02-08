using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
///
[System.Serializable]
public class LevelListRepresentationData
{
  [SerializeField]
  string levelname;
  public string Levelname { get {return levelname; } set { levelname = value;} }
  
  [SerializeField]
  string[] leveldataname = new string[0];
  public string[] Leveldataname { get {return leveldataname; } set { leveldataname = value;} }
  
  [SerializeField]
  string[] leveltopologyname = new string[0];
  public string[] Leveltopologyname { get {return leveltopologyname; } set { leveltopologyname = value;} }
  
  [SerializeField]
  string[] levelbackname = new string[0];
  public string[] Levelbackname { get {return levelbackname; } set { levelbackname = value;} }
  
  [SerializeField]
  string tutorialimagename;
  public string Tutorialimagename { get {return tutorialimagename; } set { tutorialimagename = value;} }
  
  [SerializeField]
  string tutorialprefab;
  public string Tutorialprefab { get {return tutorialprefab; } set { tutorialprefab = value;} }
  
  [SerializeField]
  string newelements;
  public string Newelements { get {return newelements; } set { newelements = value;} }
  
}