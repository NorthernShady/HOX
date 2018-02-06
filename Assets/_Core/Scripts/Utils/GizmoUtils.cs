using UnityEngine;
using System.Collections;

public static class GizmoUtils
{
    public static void DrawArrowEnd(bool gizmos, Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 40.0f)
    {
        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(arrowHeadAngle, 0, 0) * Vector3.back;
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(-arrowHeadAngle, 0, 0) * Vector3.back;
        Vector3 up = Quaternion.LookRotation(direction) * Quaternion.Euler(0, arrowHeadAngle, 0) * Vector3.back;
        Vector3 down = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -arrowHeadAngle, 0) * Vector3.back;
        if (gizmos)
        {
            Gizmos.color = color;
            Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, up * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, down * arrowHeadLength);
        }
        else
        {
            Debug.DrawRay(pos + direction, right * arrowHeadLength, color);
            Debug.DrawRay(pos + direction, left * arrowHeadLength, color);
            Debug.DrawRay(pos + direction, up * arrowHeadLength, color);
            Debug.DrawRay(pos + direction, down * arrowHeadLength, color);
        }
    }

    public static void DrawCone(float circleDrawDistance, float innerCircleDrawDistanceP, float angle,
        Transform targetTransform, int stepCount)
    {
        float hyp = circleDrawDistance/Mathf.Cos(angle*Mathf.Deg2Rad*0.5f);
        float k = hyp/ circleDrawDistance;

        Gizmos.DrawRay(targetTransform.position, targetTransform.forward.normalized* circleDrawDistance);

        Vector3 dirUn = Quaternion.AngleAxis(-angle*0.5f, targetTransform.up)*targetTransform.forward;
        Vector3 dirUp = Quaternion.AngleAxis(angle*0.5f, targetTransform.up)*targetTransform.forward;
        Vector3 dirRp = Quaternion.AngleAxis(angle*0.5f, targetTransform.right)*targetTransform.forward;
        Vector3 dirRn = Quaternion.AngleAxis(-angle*0.5f, targetTransform.right)*targetTransform.forward;

        Gizmos.DrawRay(targetTransform.position, dirUn*circleDrawDistance*k);
        Gizmos.DrawRay(targetTransform.position, dirUp*circleDrawDistance*k);
        Gizmos.DrawRay(targetTransform.position, dirRp*circleDrawDistance*k);
        Gizmos.DrawRay(targetTransform.position, dirRn*circleDrawDistance*k);

        DrawCircle(stepCount, angle, targetTransform, circleDrawDistance);
        DrawCircle(stepCount, angle, targetTransform, circleDrawDistance*innerCircleDrawDistanceP);
    }

    public static void DrawCircle(int steps, float angle, Transform relativeTransform, float distance)
    {
        float hyp = distance / Mathf.Cos(angle * Mathf.Deg2Rad * 0.5f);
        float rad = Mathf.Sqrt((hyp * hyp) - (distance * distance));
        float anglestep = (Mathf.Deg2Rad * 360f) / steps;
        for (int i = 0; i < steps; i++)
        {
            float xPosStart = rad * Mathf.Cos((i * anglestep) + anglestep * 0.5f);
            float zPosStart = rad * Mathf.Sin((i * anglestep) + anglestep * 0.5f);
            float xPosEnd = rad * Mathf.Cos(((i + 1 < steps - 0 ? i + 1 : 0) * anglestep) + anglestep * 0.5f);
            float zPosEnd = rad * Mathf.Sin(((i + 1 < steps - 0 ? i + 1 : 0) * anglestep) + anglestep * 0.5f);

            Vector3 startPos = new Vector3(xPosStart, zPosStart, 0f) + relativeTransform.forward.normalized * distance +
                               relativeTransform.position;
            Vector3 endPos = new Vector3(xPosEnd, zPosEnd, 0f) + relativeTransform.forward.normalized * distance +
                             relativeTransform.position;

            Vector3 startPosR = RotatePointAroundPivot(startPos,
                relativeTransform.forward.normalized * distance + relativeTransform.position,
                relativeTransform.eulerAngles);
            Vector3 endPosR = RotatePointAroundPivot(endPos,
                relativeTransform.forward.normalized * distance + relativeTransform.position,
                relativeTransform.eulerAngles);

            Gizmos.DrawLine(startPosR, endPosR);
        }
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }

	public static Bounds GetMaxBounds(GameObject g) {
		var b = new Bounds(g.transform.position, Vector3.zero);
		foreach (Renderer r in g.GetComponentsInChildren<Renderer>()) {
			b.Encapsulate(r.bounds);
		}
		return b;
	}
	public static Bounds GetMaxUILayoutBounds(GameObject g) {
		var b = new Bounds(g.transform.position, Vector3.zero);
		foreach (tk2dUILayout r in g.GetComponentsInChildren<tk2dUILayout>()) {
			Bounds myB = new Bounds();
			float x = Mathf.Abs(r.GetMaxBounds ().x - r.GetMinBounds ().x / 2.0f);
			float y = Mathf.Abs(r.GetMaxBounds ().y - r.GetMinBounds ().y / 2.0f);
			myB.center = new Vector3 (r.GetMinBounds ().x + x, r.GetMinBounds ().y - y, 0);
			myB.extents = new Vector3 (x, y, 0);
			b.Encapsulate(myB);
		}
		return b;
	}

	public static Color getColorFromHEX (string hex) {
		var color = new Color ();
		ColorUtility.TryParseHtmlString (hex, out color);
		return color;
	}
}
