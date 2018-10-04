using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSkill : MonoBehaviour
{
}

[System.Serializable]
public class Skills
{
    public List<BasicSkill> skills = new List<BasicSkill>();
}
