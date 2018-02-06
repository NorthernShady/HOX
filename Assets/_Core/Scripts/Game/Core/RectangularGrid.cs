using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class RectangularGrid : MonoBehaviour {

	[SerializeField]
	Vector2Int m_gridSize;

	[SerializeField]
	Vector2 m_cellSize;

	Mesh m_mesh = null;

	void Awake()
	{
		generate();
		transform.position = -0.5f * new Vector3(m_gridSize.x * m_cellSize.x, 0.0f, m_gridSize.y * m_cellSize.y);
	}

	void generate()
	{
		GetComponent<MeshFilter>().mesh = m_mesh = new Mesh();
		m_mesh.name = "RectangularMesh";

		var vertices = new Vector3[(m_gridSize.x + 1) * (m_gridSize.y + 1)];
		var uv = new Vector2[vertices.Length];
		var tangents = new Vector4[vertices.Length];
		var constTangent = new Vector4(1.0f, 0.0f, 0.0f, -1.0f);
		for (int i = 0, y = 0; y <= m_gridSize.y; ++y)
			for (var x = 0; x <= m_gridSize.x; ++x, ++i) {
				vertices[i] = new Vector3(x * m_cellSize.x, 0.0f, y * m_cellSize.y);
				uv[i] = new Vector2((float)x / m_gridSize.x, (float)y / m_gridSize.y);
				tangents[i] = constTangent;
			}

		m_mesh.vertices = vertices;
		m_mesh.uv = uv;
		m_mesh.tangents = tangents;

		var triangles = new int[m_gridSize.x * m_gridSize.y * 6];
		for (int ti = 0, vi = 0, y = 0; y < m_gridSize.y; ++y, ++vi)
			for (int x = 0; x < m_gridSize.x; ++x, ti += 6, ++vi) {
				triangles[ti] = vi;
				triangles[ti + 3] = triangles[ti + 2] = vi + 1;
				triangles[ti + 4] = triangles[ti + 1] = vi + m_gridSize.x + 1;
				triangles[ti + 5] = vi + m_gridSize.x + 2;
			}

		m_mesh.triangles = triangles;
		m_mesh.RecalculateNormals();
	}

	void OnDrawGizmos()
	{
//		if (m_vertices == null)
//			return;
//
//		Gizmos.color = Color.black;
//		foreach (var vertex in m_vertices)
//			Gizmos.DrawSphere(transform.TransformPoint(vertex), 0.1f);
	}
}
