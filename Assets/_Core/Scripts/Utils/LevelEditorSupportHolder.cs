using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LevelEditorSupportHolder : MonoBehaviour {

/*	public static GameObject m_nodeA = null;
	public static GameObject m_nodeB = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		LevelSupportSettings settings = Resources.Load<LevelSupportSettings> (k.Resources.LEVEL_SUPPORT_SETTINGS);
		if (m_nodeA != null && m_nodeB != null) {
			var direction = m_nodeB.transform.position - m_nodeA.transform.position;
			Vector3 newVector = direction.normalized * settings.distanceBetweenElements;
			m_nodeB.transform.position = m_nodeA.transform.position + newVector;
		} 
	}

	public static void autoSetupLinks(List <GameNode> nodes)
	{
		LevelSupportSettings settings = Resources.Load<LevelSupportSettings> (k.Resources.LEVEL_SUPPORT_SETTINGS);
		foreach(var node in nodes)
		{
			foreach(var node2 in nodes)
			{
				if (node != node2) {
					float distance = Vector2.Distance (node.transform.position, node2.transform.position);
					if (distance <= settings.distanceBetweenElements) {
						var chainElementsNode1 = node.gameNodeData.chainElements;
						var chainElementsNode2 = node2.gameNodeData.chainElements;
						int index1 = chainElementsNode1.FindIndex (x => x == node2.gameNodeData.id);
						if (index1 == -1) {
							chainElementsNode1.Add (node2.gameNodeData.id);
						}
						int index2 = chainElementsNode2.FindIndex (x => x == node.gameNodeData.id);
						if (index2 == -1) {
							chainElementsNode2.Add (node.gameNodeData.id);
						}
						node.gameNodeData.chainElements = chainElementsNode1;
						node2.gameNodeData.chainElements = chainElementsNode2;
					}
				}
			}
		}
	}

	public static void autoPositionByX(List <GameNode> nodes)
	{
		LevelSupportSettings settings = Resources.Load<LevelSupportSettings> (k.Resources.LEVEL_SUPPORT_SETTINGS);
		var pos = m_nodeA.transform.position;
		int index = 0;
		foreach(var node in nodes)
		{
			node.transform.position = new Vector3 (pos.x + index * settings.distanceBetweenElements, pos.y, pos.z);
			node.transform.SetSiblingIndex (m_nodeA.transform.GetSiblingIndex() + index);
			index++;
		}
	}

	public static void autoPositionByY(List <GameNode> nodes)
	{
		LevelSupportSettings settings = Resources.Load<LevelSupportSettings> (k.Resources.LEVEL_SUPPORT_SETTINGS);
		var pos = m_nodeA.transform.position;
		int index = 0;
		foreach(var node in nodes)
		{
			node.transform.position = new Vector3 (pos.x, pos.y + index * settings.distanceBetweenElements, pos.z);
			node.transform.SetSiblingIndex (m_nodeA.transform.GetSiblingIndex() + index);
			index++;
		}
	}*/
}
