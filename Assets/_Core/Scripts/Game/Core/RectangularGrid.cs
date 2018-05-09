using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class RectangularGrid : BasicGrid {

	Mesh m_mesh = null;

	public override void createGrid(GridData gridData)
	{
		m_gridData = gridData;
		generate();
	}

	void generate()
	{
		GetComponent<MeshFilter>().mesh = m_mesh = new Mesh();
		m_mesh.name = "RectangularMesh";

		var gridSize = m_gridData.gridSize;
		var cellSize = m_gridData.cellSize;

		var vertices = new Vector3[(gridSize.x + 1) * (gridSize.y + 1)];
		var uv = new Vector2[vertices.Length];
		var tangents = new Vector4[vertices.Length];
		var constTangent = new Vector4(1.0f, 0.0f, 0.0f, -1.0f);
		for (int i = 0, y = 0; y <= gridSize.y; ++y)
			for (var x = 0; x <= gridSize.x; ++x, ++i) {
				vertices[i] = new Vector3(x * cellSize.x, 0.0f, y * cellSize.y);
				uv[i] = new Vector2((float)x / gridSize.x, (float)y / gridSize.y);
				tangents[i] = constTangent;
			}

		m_mesh.vertices = vertices;
		m_mesh.uv = uv;
		m_mesh.tangents = tangents;

		var triangles = new int[gridSize.x * gridSize.y * 6];
		for (int ti = 0, vi = 0, y = 0; y < gridSize.y; ++y, ++vi)
			for (int x = 0; x < gridSize.x; ++x, ti += 6, ++vi) {
				triangles[ti] = vi;
				triangles[ti + 3] = triangles[ti + 2] = vi + 1;
				triangles[ti + 4] = triangles[ti + 1] = vi + gridSize.x + 1;
				triangles[ti + 5] = vi + gridSize.x + 2;
			}

		m_mesh.triangles = triangles;
		m_mesh.RecalculateNormals();

		GetComponent<MeshCollider>().sharedMesh = m_mesh;

		transform.position = -0.5f * new Vector3(gridSize.x * cellSize.x, 0.0f, gridSize.y * cellSize.y);
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
