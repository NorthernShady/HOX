using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInputController : MonoBehaviour
{

    public System.Action<Vector3> OnTap;
    public System.Action<Vector2, int, GameObject> OnDraggingStarted;
    public System.Action<Vector2, int> OnDragging;
    public System.Action<Vector2, int> OnDraggingFinished;

    private bool m_allowGameTouches = true;
	public bool allowGameTouches {
		set {
			m_allowGameTouches = value;
			var mapLayer = 1 << k.Layers.MAP;
			m_layerMask = m_allowGameTouches ? m_layerMask | mapLayer : m_layerMask & ~mapLayer;
		}
	}

	private int m_layerMask = 1 << k.Layers.MAP;
    public void AddLayerMask(int layerMask)
    {
        m_layerMask |= layerMask;
    }

    public void RemoveLayerMask(int layerMask)
    {
        m_layerMask &= ~layerMask;
    }

    void OnEnable()
    {
        //IT_Gesture.onMultiTapE += onMultiTap;
        IT_Gesture.onTouchDownPosE += onTap;
        IT_Gesture.onMouse1DownE += onTap;
        IT_Gesture.onTouchPosE += onTap;
        IT_Gesture.onMouse1E += onTap;
        // IT_Gesture.onChargeStartE += onTap;
        //		IT_Gesture.onSwipeE += onSwipe;

        IT_Gesture.onMFDraggingStartE += onDraggingStarted;
        IT_Gesture.onMFDraggingE += onDragging;
        IT_Gesture.onMFDraggingEndE += onDraggingFinished;
    }

    void OnDisable()
    {
        //IT_Gesture.onMultiTapE -= onMultiTap;
        IT_Gesture.onTouchDownPosE -= onTap;
        IT_Gesture.onMouse1DownE -= onTap;
        IT_Gesture.onTouchPosE -= onTap;
        IT_Gesture.onMouse1E -= onTap;
        // IT_Gesture.onChargeStartE -= onTap;
        //		IT_Gesture.onSwipeE -= onSwipe;

        IT_Gesture.onMFDraggingStartE -= onDraggingStarted;
        IT_Gesture.onMFDraggingE -= onDragging;
        IT_Gesture.onMFDraggingEndE -= onDraggingFinished;
    }

    void onTap(Vector2 pos)
    {
        RaycastHit hit;
        if (raycast(pos, out hit, m_layerMask))
        {
            if (OnTap != null)
                OnTap(hit.point);
        }
    }

    void onMultiTap(Tap tap)
    {
        RaycastHit hit;
        if (raycast(tap.pos, out hit, m_layerMask))
        {
            if (OnTap != null)
                OnTap(hit.point);
        }
    }

    void onSwipe(SwipeInfo swipeInfo)
    {
    }

    void onDraggingStarted(DragInfo dragInfo)
    {
        RaycastHit hit;

        Debug.Log("Dragging started");

		if (raycast(dragInfo.pos, out hit, m_layerMask))
		{
			if (OnDraggingStarted != null)
				OnDraggingStarted(dragInfo.pos, dragInfo.index, hit.collider.gameObject);
		}

        // Should we store drag indices?
    }

    void onDragging(DragInfo dragInfo)
    {
        if (OnDragging != null)
            OnDragging(dragInfo.pos, dragInfo.index);
    }

    void onDraggingFinished(DragInfo dragInfo)
    {
        if (OnDraggingFinished != null)
            OnDraggingFinished(dragInfo.pos, dragInfo.index);
    }

    bool raycast(Vector2 position, out RaycastHit hit, int layerMask) // = Physics.DefaultRaycastLayers)
    {
        if (isUiTouch(-1) || layerMask == 0 || isUiCameraTouch(position, out hit)) {
            hit = new RaycastHit();
            return false;
        }

        Ray ray = Camera.main.ScreenPointToRay(position);
        return Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
    }

    bool isUiCameraTouch(Vector2 position, out RaycastHit hit)
    {
        // if (tk2dUIManager.Instance.PressedUIItem != null)
        // {
        //     Debug.Log("pressedUiItem!");
        //     hit = new RaycastHit();
        //     return false;
        // }

        // Try to avoid raycasting!

        var camera = FindObjectOfType<tk2dUICamera>();
        Ray ray = camera.HostCamera.ScreenPointToRay(position);
        return Physics.Raycast(ray, out hit, camera.HostCamera.farClipPlane - camera.HostCamera.nearClipPlane, camera.FilteredMask);
    }

    public static bool isUiTouch(int touchId)
    {
        var eventSystem = UnityEngine.EventSystems.EventSystem.current;
        bool isPointerOverGameObject = eventSystem.IsPointerOverGameObject() || eventSystem.IsPointerOverGameObject(touchId);
        if (isPointerOverGameObject && eventSystem.currentSelectedGameObject != null)
        {
            Button button = eventSystem.currentSelectedGameObject.GetComponent<Button>();
            if (button != null)
                return true;
        }

        return false;
    }
}
