using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
///
[System.Serializable]
public class XPLevelRepresentationData
{
  [SerializeField]
  int xplevel;
  public int Xplevel { get {return xplevel; } set { xplevel = value;} }
  
  [SerializeField]
  int requiredxp;
  public int Requiredxp { get {return requiredxp; } set { requiredxp = value;} }
  
  [SerializeField]
  int intervalxp;
  public int Intervalxp { get {return intervalxp; } set { intervalxp = value;} }
  
  [SerializeField]
  string growthfactor;
  public string Growthfactor { get {return growthfactor; } set { growthfactor = value;} }
  
  [SerializeField]
  string floorfactor;
  public string Floorfactor { get {return floorfactor; } set { floorfactor = value;} }
  
}