using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DynamicScrollableArea : MonoBehaviour {

	public abstract class DynamicScrollableAreaDelegate: MonoBehaviour
	{
		public abstract void updateScrollableAreaItem (ref GameObject obj, int index);
		public abstract GameObject getScrollableAreaItemTemplate ();
		public abstract Vector3 getScrollableAreaItemBoundsMin ();
		public abstract Vector3 getScrollableAreaItemBoundsMax ();
		public abstract int getItemsCount();
	}

	[SerializeField]
	private DynamicScrollableAreaDelegate m_scrollableAreaDelegate;
	DynamicScrollableAreaDelegate scrollableAreaDelegate
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

	float itemStride = 0;
	public tk2dUIScrollableArea scrollableArea;
	List<GameObject> cachedContentItems = new List<GameObject>();
	List<GameObject> unusedContentItems = new List<GameObject>();
	int firstCachedItem = -1;
	int maxVisibleItems = 0;


	void OnEnable() {
		scrollableArea.OnScroll -= OnScroll;
		scrollableArea.OnScroll += OnScroll;
	}

	void OnDisable() {
		scrollableArea.OnScroll -= OnScroll;
	}


	// Use this for initialization
	public virtual void Start () {
		init ();
	}

	public void init()
	{
		if (m_isInited || scrollableAreaDelegate == null) {
			return;
		}
		// How many items do we need to buffer?
		if (scrollableArea.scrollAxes == tk2dUIScrollableArea.Axes.XAxis) {
			itemStride = (scrollableAreaDelegate.getScrollableAreaItemBoundsMax () - scrollableAreaDelegate.getScrollableAreaItemBoundsMin ()).x;
		} else {
			itemStride = (scrollableAreaDelegate.getScrollableAreaItemBoundsMax () - scrollableAreaDelegate.getScrollableAreaItemBoundsMin ()).y;
		}
		maxVisibleItems = Mathf.CeilToInt(scrollableArea.VisibleAreaLength / itemStride) + 1;

		// Buffer the prefabs that we will need
		float value = 0;
		for (int i = 0; i < maxVisibleItems; ++i) {
			GameObject item = scrollableAreaDelegate.getScrollableAreaItemTemplate();
			item.transform.parent = scrollableArea.contentContainer.transform;
			if (scrollableArea.scrollAxes == tk2dUIScrollableArea.Axes.XAxis) {
				item.transform.localPosition = new Vector3 (value, 0, 0);
			} else {
				item.transform.localPosition = new Vector3 (0, -value, 0);
			}
			item.SetActive (false);
			unusedContentItems.Add( item );
			value += itemStride;
		}
		UpdateListGraphics();
		m_isInited = true;
	}

	void OnScroll(tk2dUIScrollableArea scrollableArea) {
		if (scrollableAreaDelegate != null) {
			UpdateListGraphics ();
		}
	}

	// Synchronizes the graphics with the scroll amount
	// Figures out the first and last visible list items, and if that doesn't correspond
	// to what is cached, it rectifies the situation
	// Only the items that actually need to be changed are changed, so as you scroll the one that goes out 
	// of view is removed, recycled and reused for the one coming into view.
	void UpdateListGraphics() {
		// Previous offset - we will need to reset the value to match the new content length
		float previousOffset = scrollableArea.Value * (scrollableArea.ContentLength - scrollableArea.VisibleAreaLength);
		int firstVisibleItem = Mathf.FloorToInt( previousOffset / itemStride );

		// If the number of elements has changed - we do some processing
		float newContentLength = scrollableAreaDelegate.getItemsCount() * itemStride;
		if (!Mathf.Approximately(newContentLength, scrollableArea.ContentLength)) {
			// If all items are visible, we simply populate as needed
			if (newContentLength < scrollableArea.VisibleAreaLength) {
				scrollableArea.Value = 0; // no more scrolling
				for (int i = 0; i < cachedContentItems.Count; ++i) {
					cachedContentItems [i].SetActive (false);
					unusedContentItems.Add(cachedContentItems[i]); // clear whole list
				}
				cachedContentItems.Clear();
				firstCachedItem = -1;
				firstVisibleItem = 0;
			}

			// The total size required to display all elements
			scrollableArea.ContentLength = newContentLength;

			// Rescale the previousOffset so it remains constant
			if (scrollableArea.ContentLength > 0) {
				scrollableArea.Value = previousOffset / (scrollableArea.ContentLength - scrollableArea.VisibleAreaLength);
			}
		}
		int lastVisibleItem = Mathf.Min(firstVisibleItem + maxVisibleItems, scrollableAreaDelegate.getItemsCount());

		// If any items are visible that shouldn't need to be visible, get rid of them
		while (firstCachedItem >= 0 && firstCachedItem < firstVisibleItem) {
			firstCachedItem++;
			cachedContentItems [0].SetActive (false);
			unusedContentItems.Add(cachedContentItems[0]);
			cachedContentItems.RemoveAt(0);
			if (cachedContentItems.Count == 0) {
				firstCachedItem = -1;
			}
		}

		// Ditto for end of list
		while (firstCachedItem >= 0 && (firstCachedItem + cachedContentItems.Count) > lastVisibleItem ) {
			cachedContentItems [cachedContentItems.Count - 1].SetActive (false);
			unusedContentItems.Add(cachedContentItems[cachedContentItems.Count - 1]);
			cachedContentItems.RemoveAt(cachedContentItems.Count - 1);
			if (cachedContentItems.Count == 0) {
				firstCachedItem = -1;
			}
		}

		// Nothing visible, simply fill as needed
		if (firstCachedItem < 0) {
			firstCachedItem = firstVisibleItem;
			int maxToAdd = Mathf.Min( firstCachedItem + maxVisibleItems, scrollableAreaDelegate.getItemsCount() );
			for (int i = firstCachedItem; i < maxToAdd; ++i) {
				GameObject t = unusedContentItems[0];
				cachedContentItems.Add(t);
				unusedContentItems.RemoveAt(0);
				scrollableAreaDelegate.updateScrollableAreaItem (ref t, i);
				updateItemPosition (ref t, i);
				t.SetActive (true);
			}
		}
		else {
			// Fill in items that should be visible but aren't
			while (firstCachedItem > firstVisibleItem) {
				--firstCachedItem;
				GameObject t = unusedContentItems[0];
				unusedContentItems.RemoveAt(0);
				cachedContentItems.Insert(0, t);

				scrollableAreaDelegate.updateScrollableAreaItem (ref t, firstCachedItem);
				updateItemPosition (ref t, firstCachedItem);
				t.SetActive (true);
			}
			while (firstCachedItem + cachedContentItems.Count < lastVisibleItem) {
				GameObject t = unusedContentItems[0];
				unusedContentItems.RemoveAt(0);

				int index = firstCachedItem + cachedContentItems.Count;
				scrollableAreaDelegate.updateScrollableAreaItem (ref t, index);
				updateItemPosition (ref t, index);
				cachedContentItems.Add(t);
				t.SetActive (true);
			}
		}
	}

	void updateItemPosition (ref GameObject obj, int index)
	{
		if (scrollableArea.scrollAxes == tk2dUIScrollableArea.Axes.XAxis) {
			obj.transform.localPosition = new Vector3 (index * itemStride, 0, 0);
		} else {
			obj.transform.localPosition = new Vector3 (0, -index * itemStride, 0);
		}
	}
}
