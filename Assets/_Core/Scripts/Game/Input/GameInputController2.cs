using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputController2 : MonoBehaviour {

	bool m_isActive = true;
	public bool isActive 
	{
		set
		{
			if (value == false) {
				onRelease ();
			}
			m_isActive = value;
		}
		get {
			return m_isActive;
		}
	}	

	bool m_isActiveGameInput = true;
	public bool isActiveGameInput 
	{
		set
		{
			m_isActiveGameInput = value;
		}
		get {
			return m_isActiveGameInput;
		}
	}

	bool isWait = false;

	public tk2dUIItem m_inputUIItem;

	bool m_isTapDown = false;

	Vector3 m_startTapScreenPosition = Vector3.zero;
	Vector3 m_startTapWorldPosition = Vector3.zero;

	Vector3 m_endTapScreenPosition = Vector3.zero;
	Vector3 m_endTapWorldPosition = Vector3.zero;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnEnable()
	{
		if (m_inputUIItem != null)
		{
			m_inputUIItem.OnDown += onDown;
			m_inputUIItem.OnRelease += onRelease;
		}
	}

	void OnDisable()
	{
		if (m_inputUIItem != null)
		{
			m_inputUIItem.OnDown -= onDown;
			m_inputUIItem.OnRelease -= onRelease;
		}


		if (m_isTapDown)
		{
			if (tk2dUIManager.Instance__NoCreate != null)
			{
				tk2dUIManager.Instance.OnInputUpdate -= onInputUpdate;
			}
			m_isTapDown = false;
		}
	}


	private void onDown()
	{
		if (m_isActive )
		{
			if (!m_isTapDown)
			{
				tk2dUIManager.Instance.OnInputUpdate += onInputUpdate;
			}
			m_startTapWorldPosition = CalculateClickWorldPos(m_inputUIItem);
			m_startTapScreenPosition = CalculateClickScreenPos (m_inputUIItem);
			m_isTapDown = true;

			EventManager.Instance.Raise (new GameInputEvents.OnTapBegin (m_startTapWorldPosition, m_startTapScreenPosition));

			StartCoroutine(coDeferredClearChildrenPresses());
		}
	}

	IEnumerator coDeferredClearChildrenPresses()
	{
		yield return new WaitForEndOfFrame();
		tk2dUIManager.Instance.OverrideClearAllChildrenPresses(m_inputUIItem);
	}

	private void onInputUpdate()
	{
		if (m_isTapDown) {
			updateTapPosition ();
		}
	}

	private void updateTapPosition()
	{
		m_endTapWorldPosition = CalculateClickWorldPos(m_inputUIItem);
		m_endTapScreenPosition = CalculateClickScreenPos (m_inputUIItem);
		if (isActive && m_isTapDown)
		{
			//unpress anything currently pressed in list
			tk2dUIManager.Instance.OverrideClearAllChildrenPresses(m_inputUIItem);
		}
	}

	private void onRelease()
	{
		if (m_isActive && m_isTapDown)
		{
			updateTapPosition ();
			tk2dUIManager.Instance.OnInputUpdate -= onInputUpdate;

			m_isTapDown = false;
			EventManager.Instance.Raise (new GameInputEvents.OnTapEnd(m_startTapWorldPosition, m_startTapScreenPosition, m_endTapWorldPosition, m_endTapScreenPosition));
			EventManager.Instance.Raise (new GameInputEvents.OnClick(m_startTapWorldPosition, m_startTapScreenPosition, m_endTapWorldPosition, m_endTapScreenPosition));

			onReleaseGameInput ();
		}
	}

	private void onReleaseGameInput()
	{
		if (m_isActiveGameInput)
		{
			EventManager.Instance.Raise (new GameInputEvents.OnClickGameField(m_startTapWorldPosition, m_startTapScreenPosition, m_endTapWorldPosition, m_endTapScreenPosition));
		}
	}

	private Vector3 CalculateClickWorldPos(tk2dUIItem btn)
	{
		Camera viewingCamera = tk2dUIManager.Instance.GetUICameraForControl( gameObject );
		Vector3 worldPos = viewingCamera.ScreenToWorldPoint(CalculateClickScreenPos(btn));
		worldPos.z = btn.transform.position.z;
		return worldPos;
	}

	private Vector3 CalculateClickScreenPos(tk2dUIItem btn)
	{
		Vector2 screenPos = m_inputUIItem.Touch.position;
		Camera viewingCamera = tk2dUIManager.Instance.GetUICameraForControl( gameObject );
		screenPos = new Vector3 (screenPos.x, screenPos.y, m_inputUIItem.transform.position.z - viewingCamera.transform.position.z);
		return screenPos;
	}

}
