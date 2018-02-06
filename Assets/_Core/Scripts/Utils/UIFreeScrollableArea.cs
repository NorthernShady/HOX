using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFreeScrollableArea : MonoBehaviour {

	private const float SWIPE_SCROLLING_FIRST_SCROLL_THRESHOLD = 0.02f;

	bool m_allowScrolling = true;
	bool allowScrolling 
	{
		set
		{
			m_allowScrolling = value;
		}
		get {
			return m_allowScrolling;
		}
	}	

	[SerializeField]
	GameObject m_content = null;

	public tk2dUIItem m_backgroundUIItem;
	private bool m_isBackgroundButtonDown = false;
	private bool m_isScrollingInProgress = false;
	private Vector3 m_startLocalPos = Vector3.zero;
	private Vector3 m_startContentOffset = Vector3.zero;

	[SerializeField]
	bool m_lockVertical = false;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable()
	{
		if (m_backgroundUIItem != null)
		{
			m_backgroundUIItem.OnDown += BackgroundButtonDown;
			m_backgroundUIItem.OnRelease += BackgroundButtonRelease;
		}
	}

	void OnDisable()
	{
		if (m_backgroundUIItem != null)
		{
			m_backgroundUIItem.OnDown -= BackgroundButtonDown;
			m_backgroundUIItem.OnRelease -= BackgroundButtonRelease;
		}
			

		if (m_isBackgroundButtonDown)
		{
			if (tk2dUIManager.Instance__NoCreate != null)
			{
				tk2dUIManager.Instance.OnInputUpdate -= BackgroundOverUpdate;
			}
			m_isBackgroundButtonDown = false;
		}
	}


	private void BackgroundButtonDown()
	{
		if (m_allowScrolling )
		{
			if (!m_isBackgroundButtonDown)
			{
				tk2dUIManager.Instance.OnInputUpdate += BackgroundOverUpdate;
			}
			m_startLocalPos = transform.InverseTransformPoint(CalculateClickWorldPos(m_backgroundUIItem));
			m_startContentOffset = m_content.transform.localPosition;
			m_isBackgroundButtonDown = true;
			//StartCoroutine(coDeferredClearChildrenPresses());

		}
	}

	IEnumerator coDeferredClearChildrenPresses()
	{
		yield return new WaitForEndOfFrame();
		tk2dUIManager.Instance.OverrideClearAllChildrenPresses(m_backgroundUIItem);
	}

	private void BackgroundOverUpdate()
	{
		if (m_isBackgroundButtonDown) {
			UpdateScrollDestintationPosition ();
		}
	}

	private void UpdateScrollDestintationPosition()
	{
		Vector3 currTouchPosLocal = transform.InverseTransformPoint(CalculateClickWorldPos(m_backgroundUIItem));
		var distance = Vector3.Distance (currTouchPosLocal, m_startLocalPos);
		if (!m_isScrollingInProgress)
		{
			if (distance > SWIPE_SCROLLING_FIRST_SCROLL_THRESHOLD)
			{
				m_isScrollingInProgress = true;

				//unpress anything currently pressed in list
				tk2dUIManager.Instance.OverrideClearAllChildrenPresses(m_backgroundUIItem);
			}
		}
		if (m_isScrollingInProgress)
		{
			var direction = currTouchPosLocal - m_startLocalPos;
			if (m_lockVertical)
				direction.x = 0;
			direction = direction.normalized;
			direction *= distance;
			m_content.transform.localPosition = direction + m_startContentOffset;
		}
	}

	private void BackgroundButtonRelease()
	{
		m_isScrollingInProgress = false;
		if (m_allowScrolling)
		{
			if (m_isBackgroundButtonDown)
			{
				if (!m_isScrollingInProgress)
				{
					tk2dUIManager.Instance.OnInputUpdate -= BackgroundOverUpdate;
				}
			}
			m_isBackgroundButtonDown = false;
		}
	}

	private Vector3 CalculateClickWorldPos(tk2dUIItem btn)
	{
		Vector2 pos = btn.Touch.position;
		Camera viewingCamera = tk2dUIManager.Instance.GetUICameraForControl( gameObject );
		Vector3 worldPos = viewingCamera.ScreenToWorldPoint(new Vector3(pos.x, pos.y, btn.transform.position.z - viewingCamera.transform.position.z));
		worldPos.z = btn.transform.position.z;
		return worldPos;
	}

}
