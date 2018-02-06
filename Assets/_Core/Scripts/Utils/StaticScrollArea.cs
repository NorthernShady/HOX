using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticScrollArea : MonoBehaviour {


	public abstract class StaticScrollAreaDelegate: MonoBehaviour
	{
		public struct ItemTemplate
		{
			public GameObject item;
			public float itemStride;
		}
		
		public abstract ItemTemplate getScrollableAreaItemTemplate (int index);
		public abstract float getScrollBetweenObjectsOffset ();
		public abstract float getScrollStartOffset ();
		public abstract float getScrollEndOffset ();
		public abstract bool getIsScrollContentFull ();
		public abstract float getVisibleAreaLength (Transform scrollTopPosition);
		public abstract void reset();
	}

	struct ScrollItem
	{
		public StaticScrollAreaDelegate.ItemTemplate itemTemplate;
		public float itemScrollPosition;
	}

	[SerializeField]
	private StaticScrollAreaDelegate m_scrollableAreaDelegate;
	StaticScrollAreaDelegate scrollableAreaDelegate
	{
		set
		{ 
			m_scrollableAreaDelegate = value;
		}
		get
		{ 
			return m_scrollableAreaDelegate;
		}
	}

	private bool m_isInited = false;

	public tk2dUIScrollableArea scrollableArea;
	List<ScrollItem> m_cachedContentItems = new List<ScrollItem>();


	public virtual void OnEnable() {
		scrollableArea.OnScroll += OnScroll;
	}

	public virtual void OnDisable() {
		scrollableArea.OnScroll -= OnScroll;
	}


	// Use this for initialization
	public virtual void Start () {
		StartCoroutine (initCoroutine());
	}

	IEnumerator initCoroutine()
	{
		yield return new WaitForEndOfFrame ();
		init ();
	}

	public void init()
	{
		if (m_isInited || scrollableAreaDelegate == null) {
			return;
		}
			
		fillScroll ();
	}

	void fillScroll()
	{
		scrollableArea.VisibleAreaLength = scrollableAreaDelegate.getVisibleAreaLength (scrollableArea.transform);
		int index = 0;
		float currentOffset = scrollableAreaDelegate.getScrollStartOffset();
		while (!scrollableAreaDelegate.getIsScrollContentFull()) {
			var itemTemplate = scrollableAreaDelegate.getScrollableAreaItemTemplate(index);
			index++;
			itemTemplate.item.transform.parent = scrollableArea.contentContainer.transform;
			if (scrollableArea.scrollAxes == tk2dUIScrollableArea.Axes.XAxis) {
				itemTemplate.item.transform.localPosition = new Vector3 (currentOffset, 0, 0);
			} else {
				itemTemplate.item.transform.localPosition = new Vector3 (0, -currentOffset, 0);
			}
			itemTemplate.item.SetActive (false);
			ScrollItem scrollItem = new ScrollItem ();
			scrollItem.itemTemplate = itemTemplate;
			scrollItem.itemScrollPosition = currentOffset;
			m_cachedContentItems.Add( scrollItem );
			currentOffset += itemTemplate.itemStride + scrollableAreaDelegate.getScrollBetweenObjectsOffset ();
		}
		currentOffset -= scrollableAreaDelegate.getScrollBetweenObjectsOffset ();
		float newContentLength = currentOffset + scrollableAreaDelegate.getScrollEndOffset ();
		float topOffset = scrollableArea.Value * (scrollableArea.ContentLength - scrollableArea.VisibleAreaLength);
		if (!Mathf.Approximately (newContentLength, scrollableArea.ContentLength)) {
			// If all items are visible, we simply populate as needed
			if (newContentLength < scrollableArea.VisibleAreaLength) {
				scrollableArea.Value = 0; // no more scrolling
			}

			// The total size required to display all elements
			scrollableArea.ContentLength = newContentLength;

			// Rescale the previousOffset so it remains constant
			if (scrollableArea.ContentLength > 0) {
				scrollableArea.Value = topOffset / (scrollableArea.ContentLength - scrollableArea.VisibleAreaLength);
			}
		}

		UpdateListGraphics();
		m_isInited = true;
	}

	public void reFillScroll()
	{
		scrollableAreaDelegate.reset ();
//		float value = scrollableArea.Value;
		foreach (var elem in m_cachedContentItems) {
			Destroy(elem.itemTemplate.item.gameObject);
		}
		m_cachedContentItems.Clear ();
		fillScroll ();
		scrollableArea.Value = 0.0f;
	}

	void OnScroll(tk2dUIScrollableArea scrollableArea) {
		if (scrollableAreaDelegate != null) {
			UpdateListGraphics ();
		}
	}

	void UpdateListGraphics() {
		// Previous offset - we will need to reset the value to match the new content length
		float startOffsetPos = scrollableArea.Value * (scrollableArea.ContentLength - scrollableArea.VisibleAreaLength);
		float endOffsetPos = startOffsetPos + scrollableArea.VisibleAreaLength;
		foreach (var scrollItem in m_cachedContentItems) {
			var itemStartPosition = scrollItem.itemScrollPosition;
			var itemEndPosition = scrollItem.itemTemplate.itemStride + scrollItem.itemScrollPosition;
			if (scrollableArea.scrollAxes == tk2dUIScrollableArea.Axes.XAxis) {
				if (itemStartPosition > endOffsetPos || itemEndPosition < startOffsetPos) {
					scrollItem.itemTemplate.item.SetActive (false);
				} else {
					scrollItem.itemTemplate.item.SetActive (true);
				}
			} else {
				if (itemStartPosition > endOffsetPos || itemEndPosition < startOffsetPos) {
					scrollItem.itemTemplate.item.SetActive (false);
				} else {
					scrollItem.itemTemplate.item.SetActive (true);
				}
			}
		}
	}

	public void centerToPosition(float position)
	{
		float scrollableAreaValue  = Mathf.Abs(position / (scrollableArea.ContentLength - scrollableArea.VisibleAreaLength));
		scrollableArea.Value = scrollableAreaValue;
	}
}
