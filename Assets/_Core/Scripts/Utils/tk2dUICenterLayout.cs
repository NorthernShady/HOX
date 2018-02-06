using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class tk2dUICenterLayout : MonoBehaviour {

	public enum CenteringType
	{
		X_AND_Y = 0,
		X = 1,
		Y = 2
	}
	[SerializeField]
	tk2dUILayoutContainerSizer m_elementLayout;
	[SerializeField]
	CenteringType m_centeringType = CenteringType.X_AND_Y;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!Application.isPlaying) {
			setupCoordCenter ();
		}
	}

	public void setupCoordCenter()
	{
		if (m_elementLayout == null || m_elementLayout.GetInnerSize().y <= 0) {
			return;
		}
		switch (m_centeringType) {
		case CenteringType.X_AND_Y:
			{
				//x
				setupXCoordCenter();
				//y
				setupYCoordCenter();
			}
			break;
		case CenteringType.X:
			{
				//x
				setupXCoordCenter();
			}
			break;
		case CenteringType.Y:
			{
				//y
				setupYCoordCenter();
			}
			break;
		default:
			{

			}
			break;
		}
	}

	void setupXCoordCenter()
	{
		float localPositionX = 0;
		localPositionX = -m_elementLayout.GetInnerSize().x/ 2.0f;
		var currentPosition = transform.localPosition;
		currentPosition.x = localPositionX;
		transform.localPosition = currentPosition;
	}

	void setupYCoordCenter()
	{
		float localPositionY = 0;
		localPositionY = m_elementLayout.GetInnerSize().y / 2.0f;
		var currentPosition = transform.localPosition;
		currentPosition.y = localPositionY;
		transform.localPosition = currentPosition;
	}
}
