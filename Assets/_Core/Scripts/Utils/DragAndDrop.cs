using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace UIDragAndDrop
{
	public abstract class DragAndDropDelegate: MonoBehaviour
	{
		public abstract GameObject getDragObjectForTarget(GameObject obj);
		public virtual void updateDragPosition(Vector2 dragPosition, Vector2 offset){}
		public abstract void endDraging(GameObject obj, Vector2 dragPosition);
		public virtual void endDragingPos(Vector2 dragPosition){}
	}


	public class DragAndDrop : MonoBehaviour
	{

		[SerializeField]
		DragAndDropDelegate m_drgaDelegate;
		public DragAndDropDelegate drgaDelegate
		{
			set 
			{
				drgaDelegate = value;
			}
			get 
			{
				return m_drgaDelegate;
			}
		}

		[SerializeField]
		int m_dragLayerID = 0;//k.Layers.UIDRAG_ELEMENT;

		GameObject m_activeElement;

		bool m_isHaveActiveElement = false;
		int m_activeTapIndex = 0;
		Vector2 m_offset = Vector2.zero;

		// Use this for initialization
		void Start ()
		{
		
		}
	
		// Update is called once per frame
		void Update ()
		{
		
		}

		void OnEnable ()
		{
			IT_Gesture.onDraggingStartE -= OnDraggingStart;
			IT_Gesture.onDraggingE -= OnDragging;
			IT_Gesture.onDraggingEndE -= OnDraggingEnd;

			IT_Gesture.onDraggingStartE += OnDraggingStart;
			IT_Gesture.onDraggingE += OnDragging;
			IT_Gesture.onDraggingEndE += OnDraggingEnd;
		}

		void OnDisable ()
		{
			IT_Gesture.onDraggingStartE -= OnDraggingStart;
			IT_Gesture.onDraggingE -= OnDragging;
			IT_Gesture.onDraggingEndE -= OnDraggingEnd;
		}


		void OnDraggingStart (DragInfo dragInfo)
		{
			if (m_isHaveActiveElement || !isTouchCanObserve(dragInfo.index)) {
				return;
			}

			Vector2 direction = Vector2.zero;
			LayerMask layerMask = 1 << m_dragLayerID;
			Vector2 worlTouchdPos = Camera.main.ScreenToWorldPoint (dragInfo.pos);
			RaycastHit2D hit = Support.getHitForNearestObjectByZ (worlTouchdPos, layerMask, direction, m_activeElement);
			if (hit.collider != null) 
			{
				m_isHaveActiveElement = true;
				m_activeTapIndex = dragInfo.index;
				m_activeElement = drgaDelegate.getDragObjectForTarget (hit.collider.gameObject);
				m_offset = (Vector2)hit.collider.gameObject.transform.position - hit.point;
			}
		}

		void OnDragging (DragInfo dragInfo)
		{
			if (m_isHaveActiveElement && dragInfo.index == m_activeTapIndex) {
				Vector2 worlTouchdPos = Camera.main.ScreenToWorldPoint (dragInfo.pos);
				drgaDelegate.updateDragPosition (worlTouchdPos, m_offset);
				if (m_isHaveActiveElement) {
					ObjToDragPosition (dragInfo);
				}
			}
		}
			
		void ObjToDragPosition (DragInfo dragInfo)
		{
			if (m_activeElement != null) {
				Vector2 worlTouchdPos = getDragPositionWithOffset(dragInfo);
				float zPosition = m_activeElement.transform.position.z;
				m_activeElement.transform.position = new Vector3 (worlTouchdPos.x, worlTouchdPos.y, zPosition);
			}
		}

		Vector2 getDragPositionWithOffset(DragInfo dragInfo)
		{
			Vector2 worlTouchdPos = (Vector2)Camera.main.ScreenToWorldPoint (dragInfo.pos) + m_offset;
			return worlTouchdPos;
		}


		void OnDraggingEnd (DragInfo dragInfo)
		{
			if (m_isHaveActiveElement && dragInfo.index == m_activeTapIndex) {
				if (m_isHaveActiveElement) {
					ObjToDragPosition (dragInfo);
				}

				Vector2 direction = Vector2.zero;
				LayerMask layerMask = 1 << m_dragLayerID;
				Vector2 worlTouchdPos = Camera.main.ScreenToWorldPoint (dragInfo.pos);
				drgaDelegate.endDragingPos (worlTouchdPos);
				RaycastHit2D hit = Support.getHitForNearestObjectByZ (worlTouchdPos, layerMask, direction, m_activeElement);
				if (hit.collider != null) {
					drgaDelegate.endDraging (hit.collider.gameObject, worlTouchdPos);
				} else {
					drgaDelegate.endDraging (null, worlTouchdPos);
				}
				m_activeElement = null;
				m_isHaveActiveElement = false;
			}
		}

		public static bool isTouchCanObserve(int touchId){
			if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject () || UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject (touchId)) {
				if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject != null) {
					Button buttonObj = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button> ();
					if (buttonObj != null) {
						return false;
					}
				}
				return true;
			} else {
				return true;
			}
		}
	}
}
