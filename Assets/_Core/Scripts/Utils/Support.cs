using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Support {
	public static tk2dUIItem getNearestTk2dUIItem (Vector3 worlPos, GameObject activeElement)
	{
		Vector2 direction = Vector2.zero;
		LayerMask layerMask = Physics2D.DefaultRaycastLayers;
		RaycastHit2D hit = Support.getHitForNearestObjectByZ (worlPos, layerMask, direction, activeElement);
		if (hit.collider != null && hit.collider.gameObject != null) {
			var uiComponent = hit.collider.gameObject.GetComponent<tk2dUIItem> ();
			return uiComponent;
		} else {
			return null;
		}
	}

	public static RaycastHit2D getHitForNearestObjectByZ(Vector2 worlPos, LayerMask layerMask, Vector2 direction, GameObject activeElement)
	{
		var hits = Physics2D.RaycastAll (worlPos, direction, Mathf.Infinity, layerMask);
		List<RaycastHit2D> notNullHits = new List<RaycastHit2D>();
		foreach (var hit in hits) {
			if ((hit.collider != null) && (hit.collider.gameObject != activeElement)) {
				notNullHits.Add (hit);
			}
		}
		if (notNullHits.Count == 0) {
			return new RaycastHit2D();
		}
		var hitsOrdered = notNullHits.OrderBy(o=>o.transform.position.z).ToList();
		return hitsOrdered.First ();
	}

	public static bool getNearestObjectByDistance(Vector2 worlPos, LayerMask layerMask, Vector2 direction, GameObject activeElement, out GameObject resultObject)
	{
		resultObject = null;
		var hits = Physics2D.RaycastAll (worlPos, direction, Mathf.Infinity, layerMask);
		List<RaycastHit2D> notNullHits = new List<RaycastHit2D>();
		foreach (var hit in hits) {
			if ((hit.collider != null) && (hit.collider.gameObject != activeElement)) {
				notNullHits.Add (hit);
			}
		}
		if (notNullHits.Count == 0) {
			return false;
		}
		float minDistance = Mathf.Infinity;

		foreach (var hit in notNullHits) {
			float distance = Vector2.Distance ((Vector2)hit.collider.gameObject.transform.position, worlPos);
			if (distance < minDistance) {
				minDistance = distance;
				resultObject = hit.collider.gameObject;
			}
		}

		return true;
	}
}
